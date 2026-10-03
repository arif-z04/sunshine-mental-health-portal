using System;
using System.Threading.Tasks;

namespace Sunshine.App.Services;

public record PaymentChargeRequest(
    decimal Amount,
    string Currency, // BDT
    string PaymentMethod, // BKASH, NAGAD, ROCKET, CARD
    string AccountNumberOrCard, // 017xxxxxxxx or Card Number
    string? AccountHolderName,
    string? OtpOrPin,
    string Description
);

public record PaymentGatewayResult(
    bool Success,
    string Message,
    string? TransactionId
);

public interface IPaymentGatewayService
{
    Task<PaymentGatewayResult> ChargeAsync(PaymentChargeRequest request);
    Task<PaymentGatewayResult> RefundAsync(string transactionId, decimal amount);
}

public class MockPaymentGatewayService : IPaymentGatewayService
{
    public async Task<PaymentGatewayResult> ChargeAsync(PaymentChargeRequest request)
    {
        await Task.Delay(250); // Simulate bKash/Nagad gateway latency

        var acc = request.AccountNumberOrCard.Replace(" ", "").Replace("-", "");

        if (acc.EndsWith("0002"))
        {
            return new PaymentGatewayResult(false, "Declined: Insufficient wallet balance (bKash / Nagad Sandbox).", null);
        }
        if (acc.EndsWith("0003"))
        {
            return new PaymentGatewayResult(false, "Declined: Invalid MFS PIN or session timeout.", null);
        }
        if (acc.EndsWith("0004"))
        {
            return new PaymentGatewayResult(false, "Declined: High security risk detected by Bangladesh financial gateway.", null);
        }

        var methodPrefix = request.PaymentMethod?.ToUpperInvariant() switch
        {
            "NAGAD" => "TRX-NGD",
            "ROCKET" => "TRX-RKT",
            "CARD" => "TRX-SSL",
            _ => "TRX-BKS"
        };

        var txnId = $"{methodPrefix}-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";
        return new PaymentGatewayResult(true, $"Payment of ৳{request.Amount:N0} BDT authorized successfully via {request.PaymentMethod}.", txnId);
    }

    public async Task<PaymentGatewayResult> RefundAsync(string transactionId, decimal amount)
    {
        await Task.Delay(200);
        return new PaymentGatewayResult(true, $"Refund of ৳{amount:N0} BDT processed for {transactionId}.", $"REF-{Guid.NewGuid().ToString("N")[..8].ToUpper()}");
    }
}
