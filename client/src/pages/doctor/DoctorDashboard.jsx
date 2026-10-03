import React, { useState, useEffect } from 'react';
import { useAuth } from '../../context/AuthContext';
import { doctorApi } from '../../api/apiClient';
import { formatBDT } from '../../utils/dateUtils';
import {
  Stethoscope, Calendar, Video, FileText, CheckCircle2,
  ToggleLeft, ToggleRight, RefreshCw
} from 'lucide-react';

export default function DoctorDashboard() {
  const { user } = useAuth();
  const [profile, setProfile] = useState(null);
  const [appointments, setAppointments] = useState([]);
  const [loading, setLoading] = useState(true);
  const [policyUpdating, setPolicyUpdating] = useState(false);
  const [policyMessage, setPolicyMessage] = useState('');

  // Note Modal state
  const [activeNoteModal, setActiveNoteModal] = useState(null);
  const [noteContent, setNoteContent] = useState('');
  const [updatingStatus, setUpdatingStatus] = useState(false);

  const loadData = async () => {
    setLoading(true);
    try {
      const [profData, aptData] = await Promise.all([
        doctorApi.getProfile(),
        doctorApi.getAppointments()
      ]);
      setProfile(profData);
      setAppointments(aptData || []);
    } catch (err) {
      console.error('Failed to load doctor dashboard', err);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadData();
  }, []);

  const handleTogglePolicy = async () => {
    if (!profile) return;
    const newPolicy = profile.paymentPolicy === 'POST_PAYMENT' ? 'ADVANCE' : 'POST_PAYMENT';
    setPolicyUpdating(true);
    setPolicyMessage('');
    try {
      const updated = await doctorApi.updatePaymentPolicy(newPolicy);
      setProfile(updated);
      setPolicyMessage(`Payment policy switched to ${newPolicy === 'ADVANCE' ? 'Advance (15-Min Hold)' : 'Post-Payment'}.`);
    } catch (err) {
      setPolicyMessage('Failed to update policy: ' + err.message);
    } finally {
      setPolicyUpdating(false);
    }
  };

  const handleOpenNoteModal = (apt, targetStatus = 'COMPLETED') => {
    setActiveNoteModal({ apt, targetStatus });
    setNoteContent(apt.doctorNotes || '');
  };

  const handleSaveStatusWithNote = async (e) => {
    e.preventDefault();
    if (!activeNoteModal) return;
    setUpdatingStatus(true);
    try {
      await doctorApi.updateAppointmentStatus(
        activeNoteModal.apt.id,
        activeNoteModal.targetStatus,
        noteContent.trim()
      );
      setActiveNoteModal(null);
      loadData();
    } catch (err) {
      alert('Error updating status: ' + err.message);
    } finally {
      setUpdatingStatus(false);
    }
  };

  const isAdvance = profile?.paymentPolicy !== 'POST_PAYMENT';

  return (
    <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8 space-y-8">
      
      {/* Header Bar */}
      <div className="bg-white rounded-3xl p-6 sm:p-8 border border-slate-100 shadow-sm flex flex-col md:flex-row md:items-center justify-between gap-6">
        <div className="flex items-center gap-4">
          <div className="w-14 h-14 rounded-2xl bg-gradient-to-tr from-teal-600 to-teal-800 text-white font-black text-2xl flex items-center justify-center shadow-md shadow-teal-700/20 shrink-0">
            <Stethoscope className="w-7 h-7" />
          </div>
          <div>
            <div className="flex items-center gap-2">
              <h1 className="text-xl sm:text-2xl font-black text-slate-900 tracking-tight">
                {profile?.fullName || user?.fullName || 'Clinician Portal'}
              </h1>
              <span className="text-[10px] font-extrabold uppercase px-2.5 py-0.5 rounded-full bg-teal-50 text-teal-800 border border-teal-200">
                Verified Doctor
              </span>
            </div>
            <p className="text-xs text-slate-500 mt-1">
              {profile?.specialization || 'Psychiatry & Psychotherapy'} • {profile?.qualification || 'BMDC Certified'}
            </p>
          </div>
        </div>

        {/* Fast Policy Switch */}
        <div className="bg-slate-50 p-4 rounded-2xl border border-slate-200/80 flex items-center justify-between gap-4">
          <div>
            <span className="text-[10px] font-extrabold text-slate-500 uppercase tracking-wider block">
              Consultation Payment Policy
            </span>
            <span className="text-xs font-bold text-slate-800">
              {isAdvance ? 'Advance (15-Min Hold)' : 'Post-Payment (Instant)'}
            </span>
          </div>

          <button
            onClick={handleTogglePolicy}
            disabled={policyUpdating}
            className="p-1 rounded-xl text-teal-600 hover:text-teal-700 focus:outline-hidden disabled:opacity-50"
            title="Toggle between Advance and Post-Payment"
          >
            {isAdvance ? (
              <ToggleLeft className="w-9 h-9 text-amber-500" />
            ) : (
              <ToggleRight className="w-9 h-9 text-emerald-600" />
            )}
          </button>
        </div>
      </div>

      {policyMessage && (
        <div className="p-3 bg-teal-50 border border-teal-200 text-teal-800 rounded-xl text-xs flex items-center gap-2">
          <CheckCircle2 className="w-4 h-4 text-teal-600" />
          <span>{policyMessage}</span>
        </div>
      )}

      {/* Practice Stats Overview */}
      <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-4">
        <div className="bg-white p-5 rounded-2xl border border-slate-100 shadow-xs">
          <span className="text-xs font-bold text-slate-400 block uppercase">Session Fee</span>
          <span className="text-2xl font-black text-slate-900 mt-1 block">
            {formatBDT(profile?.consultationFee || 1000)}
          </span>
          <span className="text-[11px] text-teal-600 font-semibold">Standard 45-min visit</span>
        </div>

        <div className="bg-white p-5 rounded-2xl border border-slate-100 shadow-xs">
          <span className="text-xs font-bold text-slate-400 block uppercase">Total Sessions</span>
          <span className="text-2xl font-black text-slate-900 mt-1 block">
            {appointments.length}
          </span>
          <span className="text-[11px] text-slate-500">All time consultations</span>
        </div>

        <div className="bg-white p-5 rounded-2xl border border-slate-100 shadow-xs">
          <span className="text-xs font-bold text-slate-400 block uppercase">Upcoming Confirmed</span>
          <span className="text-2xl font-black text-emerald-600 mt-1 block">
            {appointments.filter(a => a.status === 'CONFIRMED').length}
          </span>
          <span className="text-[11px] text-emerald-700 font-semibold">Ready for consultation</span>
        </div>

        <div className="bg-white p-5 rounded-2xl border border-slate-100 shadow-xs">
          <span className="text-xs font-bold text-slate-400 block uppercase">Advance Held</span>
          <span className="text-2xl font-black text-amber-600 mt-1 block">
            {appointments.filter(a => a.status === 'HELD').length}
          </span>
          <span className="text-[11px] text-amber-700 font-semibold">15-minute countdown active</span>
        </div>
      </div>

      {/* Patient Appointments Queue */}
      <div className="space-y-4">
        <div className="flex items-center justify-between">
          <div>
            <h2 className="text-xl font-black text-slate-900 tracking-tight">
              Patient Consultation Ledger
            </h2>
            <p className="text-xs text-slate-500">
              Manage appointments, join encrypted video rooms, and record confidential clinical notes.
            </p>
          </div>
          <button
            onClick={loadData}
            className="text-xs font-bold text-teal-700 hover:text-teal-800 flex items-center gap-1.5 px-3 py-1.5 rounded-lg bg-teal-50 border border-teal-200"
          >
            <RefreshCw className="w-3.5 h-3.5" /> Refresh Queue
          </button>
        </div>

        {loading ? (
          <div className="py-16 text-center text-xs text-slate-400">Loading consultations...</div>
        ) : appointments.length === 0 ? (
          <div className="bg-white rounded-3xl p-12 text-center border border-slate-100">
            <Calendar className="w-8 h-8 text-slate-300 mx-auto mb-2" />
            <p className="text-sm font-semibold text-slate-500">No appointments scheduled in your ledger.</p>
          </div>
        ) : (
          <div className="bg-white rounded-3xl border border-slate-100 shadow-xs overflow-hidden">
            <div className="overflow-x-auto">
              <table className="w-full text-left text-xs">
                <thead className="bg-slate-50 border-b border-slate-100 text-slate-600 uppercase font-black tracking-wider text-[10px]">
                  <tr>
                    <th className="py-3.5 px-4">Patient</th>
                    <th className="py-3.5 px-4">Date & Time</th>
                    <th className="py-3.5 px-4">Modality</th>
                    <th className="py-3.5 px-4">Status</th>
                    <th className="py-3.5 px-4">Fee</th>
                    <th className="py-3.5 px-4 text-right">Actions</th>
                  </tr>
                </thead>
                <tbody className="divide-y divide-slate-100">
                  {appointments.map((apt) => (
                    <tr key={apt.id} className="hover:bg-slate-50/80 transition-colors">
                      <td className="py-4 px-4 font-bold text-slate-800">
                        <div className="flex items-center gap-2">
                          <div className="w-7 h-7 rounded-lg bg-teal-50 text-teal-700 font-bold flex items-center justify-center">
                            {apt.patientName?.charAt(0) || 'P'}
                          </div>
                          <div>
                            <div>{apt.patientName || 'Patient'}</div>
                            {apt.notes && <div className="text-[10px] text-slate-400 font-normal line-clamp-1">{apt.notes}</div>}
                          </div>
                        </div>
                      </td>
                      <td className="py-4 px-4 text-slate-600 font-medium">
                        {apt.appointmentDate} at <strong>{apt.startTime}</strong>
                      </td>
                      <td className="py-4 px-4">
                        <span className="font-semibold text-slate-700">{apt.sessionType || 'VIDEO'}</span>
                      </td>
                      <td className="py-4 px-4">
                        <span className={`text-[10px] font-black uppercase px-2 py-0.5 rounded-full border ${
                          apt.status === 'CONFIRMED'
                            ? 'bg-emerald-50 text-emerald-800 border-emerald-200'
                            : apt.status === 'HELD'
                            ? 'bg-amber-50 text-amber-900 border-amber-200'
                            : apt.status === 'COMPLETED'
                            ? 'bg-teal-50 text-teal-800 border-teal-200'
                            : 'bg-slate-100 text-slate-600 border-slate-200'
                        }`}>
                          {apt.status}
                        </span>
                      </td>
                      <td className="py-4 px-4 font-black text-slate-800">
                        {formatBDT(apt.fee || apt.doctorFee)}
                      </td>
                      <td className="py-4 px-4 text-right">
                        <div className="inline-flex items-center gap-1.5">
                          {apt.status === 'CONFIRMED' && (
                            <a
                              href={`https://meet.jit.si/SunshineTelehealth-${apt.id}`}
                              target="_blank"
                              rel="noopener noreferrer"
                              className="py-1 px-2.5 rounded-lg bg-teal-600 text-white font-bold text-[11px] hover:bg-teal-700 flex items-center gap-1 shadow-xs"
                            >
                              <Video className="w-3 h-3" /> Join Room
                            </a>
                          )}

                          <button
                            onClick={() => handleOpenNoteModal(apt, 'COMPLETED')}
                            className="py-1 px-2.5 rounded-lg bg-slate-100 hover:bg-slate-200 text-slate-700 font-bold text-[11px] flex items-center gap-1"
                          >
                            <FileText className="w-3 h-3" />
                            {apt.doctorNotes ? 'Edit Notes' : 'Add Note / Complete'}
                          </button>

                          {apt.status !== 'CANCELLED' && apt.status !== 'COMPLETED' && (
                            <button
                              onClick={() => handleOpenNoteModal(apt, 'CANCELLED')}
                              className="py-1 px-2 rounded-lg text-red-600 hover:bg-red-50 text-[11px] font-bold"
                              title="Cancel Session"
                            >
                              Cancel
                            </button>
                          )}
                        </div>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </div>
        )}
      </div>

      {/* Clinical Notes & Status Modal */}
      {activeNoteModal && (
        <div className="fixed inset-0 z-50 flex items-center justify-center p-4 bg-slate-950/60 backdrop-blur-xs">
          <div className="bg-white rounded-3xl max-w-md w-full p-6 shadow-2xl border border-slate-100 space-y-4 animate-in fade-in zoom-in-95">
            <div className="flex items-center justify-between pb-3 border-b border-slate-100">
              <div>
                <h3 className="font-extrabold text-base text-slate-900">
                  {activeNoteModal.targetStatus === 'COMPLETED' ? 'Mark Completed & Add Notes' : 'Cancel Session with Reason'}
                </h3>
                <p className="text-xs text-slate-500">Patient: {activeNoteModal.apt.patientName}</p>
              </div>
              <button onClick={() => setActiveNoteModal(null)} className="text-slate-400 hover:text-slate-700 text-xs font-bold">
                ✕
              </button>
            </div>

            <form onSubmit={handleSaveStatusWithNote} className="space-y-4">
              <div>
                <label className="block text-xs font-bold text-slate-700 mb-1">
                  Confidential Clinical Notes / Diagnosis / Action Plan
                </label>
                <textarea
                  rows={4}
                  required
                  value={noteContent}
                  onChange={(e) => setNoteContent(e.target.value)}
                  placeholder="Record assessment notes, CBT homework, or cancellation rationale..."
                  className="w-full p-3 bg-slate-50 border border-slate-200 rounded-xl text-xs focus:outline-hidden focus:border-teal-500"
                />
              </div>

              <div className="flex gap-2">
                <button
                  type="button"
                  onClick={() => setActiveNoteModal(null)}
                  className="w-1/2 py-2 px-3 rounded-xl border border-slate-200 text-slate-700 font-bold text-xs"
                >
                  Close
                </button>
                <button
                  type="submit"
                  disabled={updatingStatus}
                  className="w-1/2 py-2 px-3 rounded-xl bg-teal-600 hover:bg-teal-700 text-white font-extrabold text-xs shadow-sm flex items-center justify-center gap-1"
                >
                  {updatingStatus ? 'Saving...' : 'Save & Update'}
                </button>
              </div>
            </form>
          </div>
        </div>
      )}

    </div>
  );
}
