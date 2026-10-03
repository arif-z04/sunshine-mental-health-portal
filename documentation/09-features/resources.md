# Digital Resource Vault & Subscription Paywall

Providing psychoeducational tools and CBT workbooks.

---

## 1. Open Access vs. Premium Tiers
* **Free Open Resources**: Public educational guides (e.g. "Understanding Panic Attacks") accessible to any visitor.
* **Premium Clinical Workbooks**: Multi-page interactive CBT workbooks (e.g. "Mastering Cognitive Restructuring: Bengali Edition") gated behind subscriptions.

---

## 2. Subscription Plans (৳ BDT)
1. **Monthly Mindcare (মাসিক মাইন্ডকেয়ার)**: ৳499 BDT / 30 days.
2. **Quarterly Wellness Pass (ত্রৈমাসিক ওয়েলনেস পাস)**: ৳1,299 BDT / 90 days.
3. **Annual Holistic Pass (বার্ষিক হলিস্টিক পাস)**: ৳3,999 BDT / 365 days.

When an unsubscribed user attempts to download a premium resource via `GET /api/resource/items/{id}`, the API returns `HTTP 401 Unauthorized` with an upgrade prompt.
