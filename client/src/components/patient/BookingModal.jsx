import React, { useState, useEffect } from 'react';
import { X, Calendar, Clock, Video, FileText, AlertCircle, ShieldAlert, CheckCircle2 } from 'lucide-react';
import { patientApi } from '../../api/apiClient';
import { formatBDT } from '../../utils/dateUtils';
import { useAuth } from '../../context/AuthContext';
import { useNavigate } from 'react-router-dom';

export default function BookingModal({ doctor, onClose, onBookingComplete }) {
  const { isAuthenticated } = useAuth();
  const navigate = useNavigate();

  const [selectedDate, setSelectedDate] = useState(() => {
    const d = new Date();
    d.setDate(d.getDate() + 1);
    return d.toISOString().split('T')[0];
  });
  const [slots, setSlots] = useState([]);
  const [selectedSlot, setSelectedSlot] = useState('');
  const [sessionType, setSessionType] = useState('VIDEO');
  const [notes, setNotes] = useState('');
  const [loadingSlots, setLoadingSlots] = useState(false);
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [error, setError] = useState('');

  // Fetch slots on date change
  useEffect(() => {
    if (!doctor?.id || !selectedDate) return;
    let isMounted = true;
    async function loadSlots() {
      setLoadingSlots(true);
      setError('');
      try {
        const data = await patientApi.getDoctorSlots(doctor.id, selectedDate);
        if (isMounted) {
          setSlots(data || []);
          setSelectedSlot('');
        }
      } catch (err) {
        if (isMounted) setError(err.message || 'Failed to load doctor slots.');
      } finally {
        if (isMounted) setLoadingSlots(false);
      }
    }
    loadSlots();
    return () => { isMounted = false; };
  }, [doctor?.id, selectedDate]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!isAuthenticated) {
      navigate('/login', { state: { returnTo: '/patient' } });
      return;
    }

    if (!selectedSlot) {
      setError('Please select a consultation time slot.');
      return;
    }

    setIsSubmitting(true);
    setError('');

    try {
      const payload = {
        doctorId: doctor.id,
        appointmentDate: selectedDate,
        startTime: selectedSlot,
        sessionType,
        notes: notes.trim(),
      };

      const result = await patientApi.bookAppointment(payload);
      onBookingComplete(result);
    } catch (err) {
      setError(err.message || 'Double-booking error: This slot was already booked or hold expired. Please choose another slot.');
    } finally {
      setIsSubmitting(false);
    }
  };

  const minDate = new Date().toISOString().split('T')[0];
  const isAdvance = doctor.paymentPolicy !== 'POST_PAYMENT';

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center p-4 bg-slate-950/60 backdrop-blur-xs">
      <div className="bg-white rounded-3xl max-w-lg w-full shadow-2xl overflow-hidden border border-slate-100 animate-in fade-in zoom-in-95 duration-200">
        
        {/* Modal Header */}
        <div className="bg-teal-700 p-5 text-white flex items-center justify-between">
          <div className="flex items-center gap-3">
            <div className="w-10 h-10 rounded-xl bg-white/20 backdrop-blur-xs flex items-center justify-center font-black text-lg">
              ☀️
            </div>
            <div>
              <h3 className="font-extrabold text-base leading-tight">Book Mental Health Session</h3>
              <p className="text-xs text-teal-100">{doctor.fullName} • {doctor.specialization}</p>
            </div>
          </div>
          <button onClick={onClose} className="p-1 rounded-lg hover:bg-white/20 text-white/90">
            <X className="w-5 h-5" />
          </button>
        </div>

        {/* Hold Policy Notice */}
        <div className={`px-5 py-2.5 text-xs border-b flex items-center gap-2 ${
          isAdvance ? 'bg-amber-50 text-amber-900 border-amber-200' : 'bg-emerald-50 text-emerald-900 border-emerald-200'
        }`}>
          {isAdvance ? (
            <>
              <Clock className="w-4 h-4 shrink-0 text-amber-600" />
              <span>
                <strong>15-Minute Advance Hold:</strong> Selecting a slot reserves it for 15 minutes to complete payment via bKash / Nagad.
              </span>
            </>
          ) : (
            <>
              <CheckCircle2 className="w-4 h-4 shrink-0 text-emerald-600" />
              <span>
                <strong>Post-Payment Policy:</strong> Booking is confirmed immediately. Pay after consultation.
              </span>
            </>
          )}
        </div>

        <form onSubmit={handleSubmit} className="p-6 space-y-4">
          {error && (
            <div className="p-3 bg-red-50 border border-red-200 text-red-700 rounded-xl text-xs flex items-start gap-2">
              <AlertCircle className="w-4 h-4 shrink-0 mt-0.5" />
              <span>{error}</span>
            </div>
          )}

          {/* Date Picker */}
          <div>
            <label className="block text-xs font-bold text-slate-700 mb-1">
              Consultation Date (Asia/Dhaka)
            </label>
            <input
              type="date"
              min={minDate}
              value={selectedDate}
              onChange={(e) => setSelectedDate(e.target.value)}
              className="w-full px-3 py-2.5 bg-slate-50 border border-slate-200 rounded-xl text-sm font-semibold focus:outline-hidden focus:border-teal-500"
              required
            />
          </div>

          {/* Time Slot Picker */}
          <div>
            <div className="flex items-center justify-between mb-1.5">
              <label className="text-xs font-bold text-slate-700">
                Available Slots ({slots.filter(s => s.isAvailable).length} available)
              </label>
              <span className="text-[11px] text-teal-700 font-semibold">45 mins session</span>
            </div>

            {loadingSlots ? (
              <div className="py-6 text-center text-xs text-slate-400">
                <div className="w-5 h-5 border-2 border-teal-600 border-t-transparent rounded-full animate-spin mx-auto mb-2"></div>
                Loading doctor schedule...
              </div>
            ) : slots.length === 0 ? (
              <div className="p-4 bg-slate-50 rounded-xl text-center text-xs text-slate-500 border border-dashed border-slate-200">
                No slots open on this date. Please pick another date.
              </div>
            ) : (
              <div className="grid grid-cols-3 gap-2 max-h-40 overflow-y-auto pr-1">
                {slots.map((slot) => {
                  const isAvailable = slot.isAvailable;
                  const isSelected = selectedSlot === slot.startTime;
                  return (
                    <button
                      key={slot.startTime}
                      type="button"
                      disabled={!isAvailable}
                      onClick={() => setSelectedSlot(slot.startTime)}
                      className={`p-2 rounded-xl text-xs font-bold text-center border transition-all ${
                        !isAvailable
                          ? 'bg-slate-100 text-slate-400 border-slate-200 cursor-not-allowed line-through'
                          : isSelected
                          ? 'bg-teal-600 text-white border-teal-600 shadow-sm'
                          : 'bg-white text-slate-700 border-slate-200 hover:border-teal-500'
                      }`}
                    >
                      {slot.startTime}
                    </button>
                  );
                })}
              </div>
            )}
          </div>

          {/* Session Type */}
          <div>
            <label className="block text-xs font-bold text-slate-700 mb-1">
              Telehealth Modality
            </label>
            <div className="grid grid-cols-2 gap-2">
              {['VIDEO', 'AUDIO'].map((type) => (
                <button
                  key={type}
                  type="button"
                  onClick={() => setSessionType(type)}
                  className={`py-2 px-3 rounded-xl text-xs font-bold border transition ${
                    sessionType === type
                      ? 'border-teal-600 bg-teal-50 text-teal-800'
                      : 'border-slate-200 text-slate-600 hover:border-slate-300'
                  }`}
                >
                  {type === 'VIDEO' ? '🎥 Encrypted Video Call' : '📞 Private Audio Call'}
                </button>
              ))}
            </div>
          </div>

          {/* Notes */}
          <div>
            <label className="block text-xs font-bold text-slate-700 mb-1">
              Brief Symptoms or Reason for Visit (Confidential)
            </label>
            <textarea
              rows={2}
              value={notes}
              onChange={(e) => setNotes(e.target.value)}
              placeholder="e.g. Anxiety attacks, depression, sleeping difficulty..."
              className="w-full p-2.5 bg-slate-50 border border-slate-200 rounded-xl text-xs focus:outline-hidden focus:border-teal-500"
            />
          </div>

          {/* Total & Action */}
          <div className="pt-2 border-t border-slate-100 flex items-center justify-between">
            <div>
              <span className="text-[11px] font-bold text-slate-400 uppercase tracking-wider block">
                Total Fee
              </span>
              <span className="text-xl font-black text-slate-900">
                {formatBDT(doctor.consultationFee)}
              </span>
            </div>

            <button
              type="submit"
              disabled={isSubmitting || !selectedSlot}
              className="py-3 px-6 rounded-xl bg-teal-600 hover:bg-teal-700 text-white font-black text-xs shadow-md shadow-teal-600/20 active:scale-95 disabled:opacity-50 transition-all flex items-center gap-2"
            >
              {isSubmitting ? (
                <>
                  <div className="w-4 h-4 border-2 border-white border-t-transparent rounded-full animate-spin"></div>
                  Locking Slot...
                </>
              ) : isAdvance ? (
                <>
                  <Clock className="w-4 h-4" />
                  Hold Slot & Proceed to Pay
                </>
              ) : (
                <>
                  <CheckCircle2 className="w-4 h-4" />
                  Confirm Booking
                </>
              )}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
