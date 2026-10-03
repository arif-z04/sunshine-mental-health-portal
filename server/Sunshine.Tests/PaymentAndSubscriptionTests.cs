using System;
using System.Threading.Tasks;
using Sunshine.App.DTOs;
using Sunshine.App.Models;
using Sunshine.App.Services;
using Xunit;

namespace Sunshine.Tests;

public class PaymentAndSubscriptionTests
{
    [Fact]
    public async Task ProcessPaymentAsync_SubscriptionBkash_ActivatesPlanSuccessfully()
    {
        using var db = TestHelpers.CreateInMemoryDbContext(nameof(ProcessPaymentAsync_SubscriptionBkash_ActivatesPlanSuccessfully));
        var (_, _, patUser, _) = TestHelpers.SeedDoctorAndPatient(db);

        // Add BDT Plan
        var plan = new SubscriptionPlan
        {
            Id = 1,
            Name = "Monthly Mindcare (মাসিক মাইন্ডকেয়ার)",
            DurationType = "MONTHLY",
            DurationDays = 30,
            Price = 499.00m,
            IsActive = true
        };
        db.SubscriptionPlans.Add(plan);
        await db.SaveChangesAsync();

        var gateway = new MockPaymentGatewayService();
        var notifService = new NotificationService(db);
        var subService = new SubscriptionService(db, notifService);
        var paymentService = new PaymentService(db, gateway, notifService);

        var request = new ProcessPaymentRequest(
            PaymentType: PaymentTypes.Subscription,
            AppointmentId: null,
            PlanId: plan.Id,
            PaymentMethod: PaymentMethods.Bkash,
            AccountNumberOrCard: "01711223344",
            AccountHolderName: "Test Patient",
            OtpOrPin: "12345"
        );

        var result = await paymentService.ProcessPaymentAsync(patUser.Id, request);

        Assert.True(result.Success);
        Assert.NotNull(result.TransactionId);
        Assert.StartsWith("TRX-BKS-", result.TransactionId);

        // Verify active subscription
        var activeSub = await subService.GetUserActiveSubscriptionAsync(patUser.Id);
        Assert.NotNull(activeSub);
        Assert.Equal(plan.Id, activeSub.PlanId);
        Assert.Equal(SubscriptionStatuses.Active, activeSub.Status);
    }

    [Fact]
    public async Task AccessResourceAsync_FreeResource_AlwaysAllowed()
    {
        using var db = TestHelpers.CreateInMemoryDbContext(nameof(AccessResourceAsync_FreeResource_AlwaysAllowed));
        var (_, _, patUser, _) = TestHelpers.SeedDoctorAndPatient(db);

        var cat = new ResourceCategory { Id = 1, Name = "Anxiety", Slug = "anxiety" };
        db.ResourceCategories.Add(cat);

        var freeResource = new Resource
        {
            Id = 1,
            CategoryId = cat.Id,
            Title = "Free Grounding Exercise",
            Author = "Dr. Rahman",
            ResourceType = "ARTICLE",
            ContentUrl = "https://resources.sunshine.org/free.pdf",
            IsPremium = false
        };
        db.Resources.Add(freeResource);
        await db.SaveChangesAsync();

        var notifService = new NotificationService(db);
        var subService = new SubscriptionService(db, notifService);
        var resourceService = new ResourceService(db, subService);

        var accessed = await resourceService.AccessResourceAsync(patUser.Id, freeResource.Id);
        Assert.NotNull(accessed);
        Assert.True(accessed.HasAccess);
    }

    [Fact]
    public async Task AccessResourceAsync_PremiumResource_WithoutSubscription_ThrowsUnauthorized()
    {
        using var db = TestHelpers.CreateInMemoryDbContext(nameof(AccessResourceAsync_PremiumResource_WithoutSubscription_ThrowsUnauthorized));
        var (_, _, patUser, _) = TestHelpers.SeedDoctorAndPatient(db);

        var cat = new ResourceCategory { Id = 1, Name = "Anxiety", Slug = "anxiety" };
        db.ResourceCategories.Add(cat);

        var premiumResource = new Resource
        {
            Id = 2,
            CategoryId = cat.Id,
            Title = "Premium CBT Masterclass",
            Author = "Dr. Rahman",
            ResourceType = "BOOK",
            ContentUrl = "https://resources.sunshine.org/premium.pdf",
            IsPremium = true
        };
        db.Resources.Add(premiumResource);
        await db.SaveChangesAsync();

        var notifService = new NotificationService(db);
        var subService = new SubscriptionService(db, notifService);
        var resourceService = new ResourceService(db, subService);

        var ex = await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
            resourceService.AccessResourceAsync(patUser.Id, premiumResource.Id)
        );

        Assert.Contains("active subscription", ex.Message, StringComparison.OrdinalIgnoreCase);
    }
}
