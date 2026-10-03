# The Complete API Request Flow

Tracing an API call from browser to server:

```text
1. User clicks "Confirm ৳1,500 Payment"
                  │
                  ▼
2. JavaScript gathers phone number ("01700000000") and PIN ("12345")
                  │
                  ▼
3. fetch('/api/payment/simulate', { method: 'POST', body: JSON })
                  │
                  ▼
4. Network transmits TCP packets to port 5000 on localhost
                  │
                  ▼
5. Kestrel web server accepts connection and routes to PaymentController.cs
                  │
                  ▼
6. PaymentController calls PaymentService.ProcessPaymentAsync()
                  │
                  ▼
7. Service verifies appointment, updates status to CONFIRMED, inserts payment
                  │
                  ▼
8. Controller returns HTTP 200 with transaction ID ("TRX-BKS-6D29EE42")
                  │
                  ▼
9. Browser JavaScript displays green success confirmation modal
```
