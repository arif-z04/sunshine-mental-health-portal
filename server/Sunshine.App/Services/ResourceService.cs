using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sunshine.App.Data;
using Sunshine.App.DTOs;
using Sunshine.App.Models;

namespace Sunshine.App.Services;

public interface IResourceService
{
    Task<List<ResourceCategoryDto>> GetCategoriesAsync();
    Task<List<ResourceDto>> GetResourcesAsync(int? categoryId, string? search, int? currentUserId);
    Task<ResourceDto> AccessResourceAsync(int userId, int resourceId);
    Task<ResourceDto> CreateOrUpdateResourceAsync(CreateResourceRequest request, int? resourceId);
    Task<bool> DeleteResourceAsync(int resourceId);
}

public class ResourceService : IResourceService
{
    private readonly ApplicationDbContext _db;
    private readonly ISubscriptionService _subscriptions;

    public ResourceService(ApplicationDbContext db, ISubscriptionService subscriptions)
    {
        _db = db;
        _subscriptions = subscriptions;
    }

    public async Task<List<ResourceCategoryDto>> GetCategoriesAsync()
    {
        return await _db.ResourceCategories
            .Include(c => c.Resources)
            .OrderBy(c => c.Name)
            .Select(c => new ResourceCategoryDto(
                c.Id,
                c.Name,
                c.Slug,
                c.Description,
                c.Resources.Count
            ))
            .ToListAsync();
    }

    public async Task<List<ResourceDto>> GetResourcesAsync(int? categoryId, string? search, int? currentUserId)
    {
        var hasActiveSub = false;
        if (currentUserId.HasValue)
        {
            hasActiveSub = await _subscriptions.HasActiveSubscriptionAsync(currentUserId.Value);
        }

        var query = _db.Resources
            .Include(r => r.Category)
            .AsQueryable();

        if (categoryId.HasValue)
        {
            query = query.Where(r => r.CategoryId == categoryId.Value);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            var s = search.Trim().ToLower();
            query = query.Where(r => r.Title.ToLower().Contains(s) || r.Author.ToLower().Contains(s) || (r.Description != null && r.Description.ToLower().Contains(s)));
        }

        var list = await query.OrderBy(r => r.Title).ToListAsync();

        return list.Select(r =>
        {
            var userHasAccess = !r.IsPremium || hasActiveSub;
            return new ResourceDto(
                r.Id,
                r.CategoryId,
                r.Category.Name,
                r.Category.Slug,
                r.Title,
                r.Author,
                r.Description,
                r.ResourceType,
                userHasAccess ? r.ContentUrl : null, // Masked server-side if paywalled!
                r.IsPremium,
                r.ThumbnailUrl,
                userHasAccess,
                r.CreatedAt
            );
        }).ToList();
    }

    public async Task<ResourceDto> AccessResourceAsync(int userId, int resourceId)
    {
        var resource = await _db.Resources
            .Include(r => r.Category)
            .FirstOrDefaultAsync(r => r.Id == resourceId);

        if (resource == null)
        {
            throw new KeyNotFoundException("Resource not found.");
        }

        // Server-side paywall validation
        if (resource.IsPremium)
        {
            var hasActiveSub = await _subscriptions.HasActiveSubscriptionAsync(userId);
            if (!hasActiveSub)
            {
                throw new UnauthorizedAccessException("An active subscription is required to access this premium resource.");
            }
        }

        // Record access audit log
        _db.ResourceAccesses.Add(new ResourceAccess
        {
            UserId = userId,
            ResourceId = resource.Id,
            AccessedAt = DateTime.UtcNow
        });

        await _db.SaveChangesAsync();

        return new ResourceDto(
            resource.Id,
            resource.CategoryId,
            resource.Category.Name,
            resource.Category.Slug,
            resource.Title,
            resource.Author,
            resource.Description,
            resource.ResourceType,
            resource.ContentUrl,
            resource.IsPremium,
            resource.ThumbnailUrl,
            true,
            resource.CreatedAt
        );
    }

    public async Task<ResourceDto> CreateOrUpdateResourceAsync(CreateResourceRequest request, int? resourceId)
    {
        var category = await _db.ResourceCategories.FirstOrDefaultAsync(c => c.Id == request.CategoryId)
            ?? throw new KeyNotFoundException("Resource category not found.");

        Resource resource;
        if (resourceId.HasValue && resourceId.Value > 0)
        {
            resource = await _db.Resources.FirstOrDefaultAsync(r => r.Id == resourceId.Value)
                ?? throw new KeyNotFoundException("Resource not found.");

            resource.CategoryId = request.CategoryId;
            resource.Title = request.Title.Trim();
            resource.Author = request.Author.Trim();
            resource.Description = request.Description?.Trim();
            resource.ResourceType = request.ResourceType.ToUpperInvariant();
            resource.ContentUrl = request.ContentUrl.Trim();
            resource.IsPremium = request.IsPremium;
            resource.ThumbnailUrl = request.ThumbnailUrl?.Trim();
        }
        else
        {
            resource = new Resource
            {
                CategoryId = request.CategoryId,
                Title = request.Title.Trim(),
                Author = request.Author.Trim(),
                Description = request.Description?.Trim(),
                ResourceType = request.ResourceType.ToUpperInvariant(),
                ContentUrl = request.ContentUrl.Trim(),
                IsPremium = request.IsPremium,
                ThumbnailUrl = request.ThumbnailUrl?.Trim(),
                CreatedAt = DateTime.UtcNow
            };
            _db.Resources.Add(resource);
        }

        await _db.SaveChangesAsync();

        return new ResourceDto(
            resource.Id,
            resource.CategoryId,
            category.Name,
            category.Slug,
            resource.Title,
            resource.Author,
            resource.Description,
            resource.ResourceType,
            resource.ContentUrl,
            resource.IsPremium,
            resource.ThumbnailUrl,
            true,
            resource.CreatedAt
        );
    }

    public async Task<bool> DeleteResourceAsync(int resourceId)
    {
        var resource = await _db.Resources.FirstOrDefaultAsync(r => r.Id == resourceId);
        if (resource == null) return false;

        _db.Resources.Remove(resource);
        await _db.SaveChangesAsync();
        return true;
    }
}
