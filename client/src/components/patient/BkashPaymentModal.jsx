import React, { useState, useEffect } from 'react';
import { X, ShieldCheck, Clock, CheckCircle2, AlertCircle, Smartphone } from 'lucide-react';
import { patientApi } from '../../api/apiClient';
import { formatBDT, getHoldRemainingTime } from '../../utils/dateUtils';

export default function BkashPaymentModal({ appointment, onClose, onSuccess }) {
  const [method, setMethod] = useState('bKash');
  const [phone, setPhone] = useState('01712345678');
  const [pin, setPin] = useState('12345');
  const [trxId, setTrxId] = useState('');
  const [isProcessing, setIsProcessing] = useState(false);
  const [error, setError] = useState('');
  const [remainingHold, setRemainingHold] = useState({ expired: false, formatted: '15:00' });

  // Update 15-min countdown timer every second
  useEffect(() => {
    if (!appointment?.createdAt) return;

    const interval = setInterval(() => {
      const hold = getHoldRemainingTime(appointment.createdAt, 15);
      setRemainingHold(hold);
      if (hold.expired) {
        setError('Your 15-minute advance hold has expired. Please select a new slot.');
      }
    }, 1000);

    // Initial check
    setRemainingHold(getHoldRemainingTime(appointment.createdAt, 15));

    return () => clearInterval(interval);
  }, [appointment]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (remainingHold.expired) {
      setError('Hold expired. Cannot complete payment for expired slot.');
      return;
    }

    if (!phone || phone.length < 11) {
      setError('Please provide a valid 11-digit Bangladeshi mobile number (e.g., 01712345678).');
      return;
    }

    setIsProcessing(true);
    setError('');

    try {
      const generatedTrx = trxId.trim() || `TXN${Date.now()}`;
      const payload = {
        appointmentId: appointment.id,
        paymentType: 'APPOINTMENT',
        paymentMethod: method,
        amount: appointment.fee || appointment.doctorFee || 1000,
        transactionId: generatedTrx,
        phoneNumber: phone.trim(),
      };

      const result = await patientApi.processPayment(payload);
      if (result.success) {
        onSuccess(result);
      } else {
        setError(result.message || 'Payment processing failed. Please check your credentials.');
      }
    } catch (err) {
      setError(err.message || 'Payment failed. Please verify your connection.');
    } finally {
      setIsProcessing(false);
    }
  };

  const amount = appointment.fee || appointment.doctorFee || 1000;

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center p-4 bg-slate-950/60 backdrop-blur-xs">
      <div className="bg-white rounded-3xl max-w-md w-full shadow-2xl overflow-hidden border border-slate-100 animate-in fade-in zoom-in-95 duration-200">
        
        {/* Header with MFS Brand Color */}
        <div className={`p-5 text-white flex items-center justify-between transition-colors ${
          method === 'bKash' ? 'bg-[#e2136e]' : method === 'Nagad' ? 'bg-[#f7941d]' : method === 'Rocket' ? 'bg-[#8c3494]' : 'bg-teal-700'
        }`}>
          <div className="flex items-center gap-2.5">
            <div className="w-10 h-10 rounded-xl bg-white/20 backdrop-blur-xs flex items-center justify-center text-xl font-black">
              ৳
            </div>
            <div>
              <h3 className="font-extrabold text-base leading-tight">MFS Checkout Bangladesh</h3>
              <p className="text-xs text-white/80 font-medium">Secured Payment Gateway Sandbox</p>
            </div>
          </div>
          <button
            onClick={onClose}
            className="p-1 rounded-lg hover:bg-white/20 text-white/90 transition-colors"
          >
            <X className="w-5 h-5" />
          </button>
        </div>

        {/* 15-Minute Advance Hold Countdown Notice */}
        <div className={`px-5 py-2.5 flex items-center justify-between text-xs border-b ${
          remainingHold.expired
            ? 'bg-red-50 text-red-700 border-red-200'
            : 'bg-amber-50 text-amber-900 border-amber-200'
        }`}>
          <span className="flex items-center gap-1.5 font-semibold">
            <Clock className="w-4 h-4 shrink-0 text-amber-600" />
            15-Min Advance Hold Policy:
          </span>
          <span className="font-black font-mono text-sm tracking-wider">
            {remainingHold.expired ? 'EXPIRED' : remainingHold.formatted}
          </span>
        </div>

        <form onSubmit={handleSubmit} className="p-6 space-y-5">
          {/* Method Selection Tabs */}
          <div>
            <label className="block text-xs font-bold text-slate-700 uppercase tracking-wider mb-2">
              Select Payment Method
            </label>
            <div className="grid grid-cols-4 gap-2">
              {[
                { id: 'bKash', label: 'bKash', color: 'border-[#e2136e] text-[#e2136e]' },
                { id: 'Nagad', label: 'Nagad', color: 'border-[#f7941d] text-[#f7941d]' },
                { id: 'Rocket', label: 'Rocket', color: 'border-[#8c3494] text-[#8c3494]' },
                { id: 'Card', label: 'Card', color: 'border-teal-600 text-teal-600' }
              ].map((m) => (
                <button
                  key={m.id}
                  type="button"
                  onClick={() => setMethod(m.id)}
                  className={`py-2 px-1 text-center text-xs font-bold rounded-xl border-2 transition-all ${
                    method === m.id
                      ? `${m.color} bg-slate-50 shadow-xs scale-102`
                      : 'border-slate-200 text-slate-600 hover:border-slate-300'
                  }`}
                >
                  {m.label}
                </button>
              ))}
            </div>
          </div>

          {/* Amount & Consultation Summary */}
          <div className="bg-slate-50 rounded-2xl p-4 border border-slate-100 flex items-center justify-between">
            <div>
              <span className="text-[11px] font-bold text-slate-500 uppercase tracking-wider block">
                Session Fee
              </span>
              <span className="text-xs font-semibold text-slate-700">
                Dr. {appointment.doctorName || 'Specialist'}
              </span>
            </div>
            <div className="text-right">
              <span className="text-xl font-black text-slate-900">
                {formatBDT(amount)}
              </span>
              <span className="block text-[10px] text-teal-700 font-bold uppercase">
                BDT (Bangladeshi Taka)
              </span>
            </div>
          </div>

          {/* Error Alert */}
          {error && (
            <div className="p-3 bg-red-50 border border-red-200 text-red-700 rounded-xl text-xs flex items-start gap-2">
              <AlertCircle className="w-4 h-4 shrink-0 mt-0.5" />
              <span>{error}</span>
            </div>
          )}

          {/* Account Number */}
          <div>
            <label className="block text-xs font-bold text-slate-700 mb-1">
              {method} Account Number
            </label>
            <div className="relative">
              <Smartphone className="w-4 h-4 absolute left-3.5 top-3 text-slate-400" />
              <input
                type="tel"
                value={phone}
                onChange={(e) => setPhone(e.target.value)}
                placeholder="017XXXXXXXX"
                className="w-full pl-10 pr-3 py-2.5 text-sm bg-white border border-slate-200 rounded-xl font-mono focus:outline-hidden focus:border-teal-500 focus:ring-1 focus:ring-teal-500"
                required
              />
            </div>
            <span className="text-[11px] text-slate-400 mt-1 block">
              Default test number: 01712345678
            </span>
          </div>

          {/* PIN / OTP */}
          <div>
            <label className="block text-xs font-bold text-slate-700 mb-1">
              Sandbox PIN / OTP
            </label>
            <input
              type="password"
              maxLength={5}
              value={pin}
              onChange={(e) => setPin(e.target.value)}
              placeholder="•••••"
              className="w-full px-3 py-2.5 text-sm bg-white border border-slate-200 rounded-xl font-mono tracking-widest text-center focus:outline-hidden focus:border-teal-500"
              required
            />
            <span className="text-[11px] text-slate-400 mt-1 block text-center">
              Enter any 4-5 digit simulation PIN (e.g. 12345)
            </span>
          </div>

          {/* Submit Button */}
          <button
            type="submit"
            disabled={isProcessing || remainingHold.expired}
            className={`w-full py-3 px-4 rounded-xl font-black text-sm text-white shadow-md transition-all flex items-center justify-center gap-2 ${
              remainingHold.expired
                ? 'bg-slate-400 cursor-not-allowed'
                : method === 'bKash'
                ? 'bg-[#e2136e] hover:bg-[#c20f5c]'
                : method === 'Nagad'
                ? 'bg-[#f7941d] hover:bg-[#dc8216]'
                : method === 'Rocket'
                ? 'bg-[#8c3494] hover:bg-[#77287e]'
                : 'bg-teal-600 hover:bg-teal-700'
            }`}
          >
            {isProcessing ? (
              <>
                <div className="w-4 h-4 border-2 border-white border-t-transparent rounded-full animate-spin"></div>
                Processing {formatBDT(amount)}...
              </>
            ) : (
              <>
                <ShieldCheck className="w-4 h-4" />
                Confirm & Pay {formatBDT(amount)}
              </>
            )}
          </button>
        </form>
      </div>
    </div>
  );
}
