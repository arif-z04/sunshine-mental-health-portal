import React, { useState, useEffect } from 'react';
import { useSearchParams } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';
import { patientApi } from '../../api/apiClient';
import { formatBDT, getHoldRemainingTime } from '../../utils/dateUtils';
import DoctorCard from '../../components/patient/DoctorCard';
import BookingModal from '../../components/patient/BookingModal';
import BkashPaymentModal from '../../components/patient/BkashPaymentModal';
import RescheduleModal from '../../components/patient/RescheduleModal';
import {
  Calendar, Clock, BookOpen, User, Search,
  CheckCircle2, Video, RefreshCw, XCircle, FileText, ChevronRight
} from 'lucide-react';

export default function PatientPortal() {
  const { user } = useAuth();
  const [searchParams, setSearchParams] = useSearchParams();
  const currentTab = searchParams.get('tab') || 'specialists';

  // Specialists state
  const [doctors, setDoctors] = useState([]);
  const [loadingDoctors, setLoadingDoctors] = useState(false);
  const [selectedSpecialization, setSelectedSpecialization] = useState('');
  const [selectedPolicy, setSelectedPolicy] = useState('');
  const [searchQuery, setSearchQuery] = useState('');

  // Appointments state
  const [appointments, setAppointments] = useState([]);
  const [loadingAppointments, setLoadingAppointments] = useState(false);

  // CBT Resources state
  const [resources, setResources] = useState([]);
  const [categories, setCategories] = useState([]);
  const [selectedCategory, setSelectedCategory] = useState('');
  const [loadingResources, setLoadingResources] = useState(false);

  // Profile state
  const [profileForm, setProfileForm] = useState({
    fullName: '',
    phone: '',
    dateOfBirth: '',
    gender: 'Male',
    emergencyContact: '',
    medicalHistoryNotes: ''
  });
  const [profileSaving, setProfileSaving] = useState(false);
  const [profileMsg, setProfileMsg] = useState('');

  // Modals state
  const [bookingDoctor, setBookingDoctor] = useState(null);
  const [activePaymentAppointment, setActivePaymentAppointment] = useState(null);
  const [activeRescheduleAppointment, setActiveRescheduleAppointment] = useState(null);
  const [viewingNotes, setViewingNotes] = useState(null);

  const setTab = (tabName) => {
    setSearchParams({ tab: tabName });
  };

  // 1. Fetch Doctors
  const loadDoctors = async () => {
    setLoadingDoctors(true);
    try {
      const data = await patientApi.getDoctors({
        specialization: selectedSpecialization || undefined,
        paymentPolicy: selectedPolicy || undefined,
        availableOnly: true
      });
      setDoctors(data || []);
    } catch (err) {
      console.error(err);
    } finally {
      setLoadingDoctors(false);
    }
  };

  // 2. Fetch Appointments
  const loadAppointments = async () => {
    setLoadingAppointments(true);
    try {
      const data = await patientApi.getMyAppointments();
      setAppointments(data || []);
    } catch (err) {
      console.error(err);
    } finally {
      setLoadingAppointments(false);
    }
  };

  // 3. Fetch CBT Resources
  const loadResources = async () => {
    setLoadingResources(true);
    try {
      const [resData, catData] = await Promise.all([
        patientApi.getResources(selectedCategory || null),
        patientApi.getCategories()
      ]);
      setResources(resData || []);
      setCategories(catData || []);
    } catch (err) {
      console.error(err);
    } finally {
      setLoadingResources(false);
    }
  };

  // 4. Fetch Profile
  const loadProfile = async () => {
    try {
      const p = await patientApi.getProfile();
      setProfile(p);
      setProfileForm({
        fullName: p.fullName || '',
        phone: p.phone || '',
        dateOfBirth: p.dateOfBirth ? p.dateOfBirth.split('T')[0] : '',
        gender: p.gender || 'Male',
        emergencyContact: p.emergencyContact || '',
        medicalHistoryNotes: p.medicalHistoryNotes || ''
      });
    } catch (err) {
      console.error(err);
    }
  };

  useEffect(() => {
    if (currentTab === 'specialists') loadDoctors();
    if (currentTab === 'appointments') loadAppointments();
    if (currentTab === 'resources') loadResources();
    if (currentTab === 'profile') loadProfile();
  }, [currentTab, selectedSpecialization, selectedPolicy, selectedCategory]);

  const handleBookingSuccess = (createdAppointment) => {
    setBookingDoctor(null);
    if (createdAppointment.status === 'HELD' || createdAppointment.doctorPaymentPolicy !== 'POST_PAYMENT') {
      setActivePaymentAppointment(createdAppointment);
    } else {
      setTab('appointments');
      loadAppointments();
    }
  };

  const handlePaymentSuccess = () => {
    setActivePaymentAppointment(null);
    setTab('appointments');
    loadAppointments();
  };

  const handleCancelAppointment = async (id) => {
    if (!window.confirm('Are you sure you want to cancel this appointment?')) return;
    try {
      await patientApi.cancelAppointment(id);
      loadAppointments();
    } catch (err) {
      alert(err.message || 'Failed to cancel appointment');
    }
  };

  const handleSaveProfile = async (e) => {
    e.preventDefault();
    setProfileSaving(true);
    setProfileMsg('');
    try {
      await patientApi.updateProfile(profileForm);
      setProfileMsg('Profile updated successfully!');
      loadProfile();
    } catch (err) {
      setProfileMsg('Failed to update profile: ' + err.message);
    } finally {
      setProfileSaving(false);
    }
  };

  const filteredDoctors = doctors.filter(d => {
    if (!searchQuery) return true;
    const q = searchQuery.toLowerCase();
    return (
      d.fullName?.toLowerCase().includes(q) ||
      d.specialization?.toLowerCase().includes(q) ||
      d.qualification?.toLowerCase().includes(q)
    );
  });

  return (
    <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8 space-y-8">
      
      {/* Patient Welcome Header */}
      <div className="bg-white rounded-3xl p-6 sm:p-8 border border-slate-100 shadow-sm flex flex-col md:flex-row md:items-center justify-between gap-6">
        <div className="flex items-center gap-4">
          <div className="w-14 h-14 rounded-2xl bg-gradient-to-tr from-teal-600 to-amber-500 text-white font-black text-2xl flex items-center justify-center shadow-md shadow-teal-600/20 shrink-0">
            {user?.fullName?.charAt(0) || 'P'}
          </div>
          <div>
            <div className="flex items-center gap-2">
              <h1 className="text-xl sm:text-2xl font-black text-slate-900 tracking-tight">
                Welcome, {user?.fullName || 'Patient'}
              </h1>
              <span className="text-[10px] font-extrabold uppercase px-2 py-0.5 rounded-full bg-teal-50 text-teal-800 border border-teal-200">
                Patient Portal
              </span>
            </div>
            <p className="text-xs text-slate-500 mt-1">
              Confidential telehealth portal • Asia/Dhaka BST Timezone
            </p>
          </div>
        </div>

        {/* Quick Tabs Pill Nav */}
        <div className="flex flex-wrap items-center gap-1.5 p-1.5 bg-slate-100 rounded-2xl">
          {[
            { id: 'specialists', label: 'Find Counselors', icon: Search },
            { id: 'appointments', label: 'My Consultations', icon: Calendar },
            { id: 'resources', label: 'CBT Vault', icon: BookOpen },
            { id: 'profile', label: 'Medical Profile', icon: User }
          ].map((tab) => {
            const Icon = tab.icon;
            const isActive = currentTab === tab.id;
            return (
              <button
                key={tab.id}
                onClick={() => setTab(tab.id)}
                className={`py-2 px-3.5 rounded-xl text-xs font-bold transition-all flex items-center gap-1.5 ${
                  isActive
                    ? 'bg-white text-teal-900 shadow-xs'
                    : 'text-slate-600 hover:text-slate-900'
                }`}
              >
                <Icon className={`w-3.5 h-3.5 ${isActive ? 'text-teal-600' : 'text-slate-400'}`} />
                {tab.label}
              </button>
            );
          })}
        </div>
      </div>

      {/* TAB 1: FIND COUNSELORS */}
      {currentTab === 'specialists' && (
        <div className="space-y-6">
          {/* Filters Bar */}
          <div className="bg-white p-4 rounded-2xl border border-slate-100 shadow-xs grid grid-cols-1 md:grid-cols-3 gap-3">
            <div className="relative">
              <Search className="w-4 h-4 absolute left-3 top-3 text-slate-400" />
              <input
                type="text"
                value={searchQuery}
                onChange={(e) => setSearchQuery(e.target.value)}
                placeholder="Search counselor by name or symptom..."
                className="w-full pl-9 pr-3 py-2 bg-slate-50 border border-slate-200 rounded-xl text-xs font-medium focus:outline-hidden focus:border-teal-500"
              />
            </div>

            <div>
              <select
                value={selectedSpecialization}
                onChange={(e) => setSelectedSpecialization(e.target.value)}
                className="w-full px-3 py-2 bg-slate-50 border border-slate-200 rounded-xl text-xs font-medium focus:outline-hidden focus:border-teal-500"
              >
                <option value="">All Specializations</option>
                <option value="Clinical Psychology">Clinical Psychology</option>
                <option value="CBT">CBT Specialist</option>
                <option value="Depression">Depression & Mood Disorders</option>
                <option value="Anxiety">Anxiety & Trauma</option>
                <option value="Child">Child & Adolescent</option>
              </select>
            </div>

            <div>
              <select
                value={selectedPolicy}
                onChange={(e) => setSelectedPolicy(e.target.value)}
                className="w-full px-3 py-2 bg-slate-50 border border-slate-200 rounded-xl text-xs font-medium focus:outline-hidden focus:border-teal-500"
              >
                <option value="">All Payment Policies</option>
                <option value="ADVANCE">Advance (15-Min Hold)</option>
                <option value="POST_PAYMENT">Post-Payment</option>
              </select>
            </div>
          </div>

          {/* Doctors Grid */}
          {loadingDoctors ? (
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
              {[1, 2, 3].map((i) => (
                <div key={i} className="h-64 bg-slate-100 rounded-3xl animate-pulse"></div>
              ))}
            </div>
          ) : filteredDoctors.length === 0 ? (
            <div className="text-center py-16 bg-white rounded-3xl border border-slate-100 p-8">
              <p className="text-sm font-semibold text-slate-500">No counselors found matching your criteria.</p>
            </div>
          ) : (
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
              {filteredDoctors.map((doc) => (
                <DoctorCard
                  key={doc.id}
                  doctor={doc}
                  onSelectDoctor={(d) => setBookingDoctor(d)}
                />
              ))}
            </div>
          )}
        </div>
      )}

      {/* TAB 2: MY CONSULTATIONS */}
      {currentTab === 'appointments' && (
        <div className="space-y-6">
          <div className="flex items-center justify-between">
            <h2 className="text-xl font-black text-slate-900 tracking-tight">
              My Consultations & Telehealth Sessions
            </h2>
            <button
              onClick={loadAppointments}
              className="text-xs font-bold text-teal-700 hover:text-teal-800 flex items-center gap-1.5 px-3 py-1.5 rounded-lg bg-teal-50 border border-teal-200"
            >
              <RefreshCw className="w-3.5 h-3.5" /> Refresh List
            </button>
          </div>

          {loadingAppointments ? (
            <div className="py-16 text-center text-xs text-slate-400">
              <div className="w-6 h-6 border-2 border-teal-600 border-t-transparent rounded-full animate-spin mx-auto mb-2"></div>
              Loading consultations...
            </div>
          ) : appointments.length === 0 ? (
            <div className="bg-white rounded-3xl p-12 text-center border border-slate-100 space-y-4">
              <div className="w-14 h-14 rounded-2xl bg-teal-50 text-teal-600 flex items-center justify-center mx-auto">
                <Calendar className="w-7 h-7" />
              </div>
              <h3 className="text-base font-bold text-slate-900">No consultations scheduled yet</h3>
              <p className="text-xs text-slate-500 max-w-sm mx-auto">
                You do not have any past or active appointments. Search our directory to book your first confidential session.
              </p>
              <button
                onClick={() => setTab('specialists')}
                className="px-5 py-2.5 rounded-xl bg-teal-600 text-white font-bold text-xs shadow-sm hover:bg-teal-700 transition"
              >
                Find Counselors
              </button>
            </div>
          ) : (
            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
              {appointments.map((apt) => {
                const isHeld = apt.status === 'HELD';
                const isConfirmed = apt.status === 'CONFIRMED';
                const isCompleted = apt.status === 'COMPLETED';

                // Calculate hold time
                const hold = isHeld ? getHoldRemainingTime(apt.createdAt, 15) : null;

                return (
                  <div
                    key={apt.id}
                    className={`bg-white rounded-3xl p-6 border transition-all shadow-xs flex flex-col justify-between ${
                      isHeld ? 'border-amber-300 ring-2 ring-amber-100' : 'border-slate-100'
                    }`}
                  >
                    <div>
                      {/* Status & Policy Badges */}
                      <div className="flex items-center justify-between gap-2 mb-3">
                        <span className={`text-[10px] font-black uppercase px-2.5 py-0.5 rounded-full border ${
                          isConfirmed
                            ? 'bg-emerald-50 text-emerald-800 border-emerald-200'
                            : isHeld
                            ? 'bg-amber-50 text-amber-900 border-amber-300'
                            : isCompleted
                            ? 'bg-teal-50 text-teal-800 border-teal-200'
                            : 'bg-slate-100 text-slate-600 border-slate-200'
                        }`}>
                          {apt.status}
                        </span>

                        <span className="text-xs font-bold text-slate-500">
                          {formatBDT(apt.fee || apt.doctorFee)}
                        </span>
                      </div>

                      {/* Doctor Info */}
                      <h3 className="font-extrabold text-base text-slate-900">
                        {apt.doctorName || 'Dr. Specialist'}
                      </h3>
                      <p className="text-xs text-teal-600 font-semibold mb-2">
                        {apt.doctorSpecialization || 'Clinical Psychology'}
                      </p>

                      {/* Schedule Info */}
                      <div className="p-3 bg-slate-50 rounded-2xl space-y-1.5 text-xs text-slate-600 mb-3 border border-slate-100">
                        <div className="flex items-center gap-2">
                          <Calendar className="w-3.5 h-3.5 text-slate-400" />
                          <span>{apt.appointmentDate} at <strong>{apt.startTime}</strong> (BST)</span>
                        </div>
                        <div className="flex items-center gap-2">
                          <Video className="w-3.5 h-3.5 text-slate-400" />
                          <span>Session: {apt.sessionType || 'VIDEO'} Telehealth</span>
                        </div>
                        {apt.notes && (
                          <div className="text-[11px] text-slate-500 pt-1 border-t border-slate-200/50">
                            Notes: {apt.notes}
                          </div>
                        )}
                      </div>

                      {/* 15-Minute Advance Hold Countdown Alert */}
                      {isHeld && hold && (
                        <div className={`p-3 rounded-2xl text-xs mb-3 flex items-center justify-between border ${
                          hold.expired ? 'bg-red-50 text-red-800 border-red-200' : 'bg-amber-50 text-amber-900 border-amber-200'
                        }`}>
                          <div className="flex items-center gap-1.5 font-bold">
                            <Clock className="w-4 h-4 text-amber-700" />
                            <span>Advance Hold Expires In:</span>
                          </div>
                          <span className="font-black font-mono text-sm">
                            {hold.expired ? 'EXPIRED' : hold.formatted}
                          </span>
                        </div>
                      )}
                    </div>

                    {/* Action Buttons */}
                    <div className="pt-3 border-t border-slate-100 flex flex-wrap items-center gap-2">
                      {isHeld && !hold?.expired && (
                        <button
                          onClick={() => setActivePaymentAppointment(apt)}
                          className="w-full py-2.5 px-3 rounded-xl bg-[#e2136e] hover:bg-[#c20f5c] text-white font-extrabold text-xs shadow-sm transition flex items-center justify-center gap-1.5"
                        >
                          <span>Pay with bKash / Nagad</span>
                          <ChevronRight className="w-3.5 h-3.5" />
                        </button>
                      )}

                      {isConfirmed && (
                        <a
                          href={`https://meet.jit.si/SunshineTelehealth-${apt.id}`}
                          target="_blank"
                          rel="noopener noreferrer"
                          className="flex-1 py-2 px-3 rounded-xl bg-teal-600 hover:bg-teal-700 text-white font-extrabold text-xs text-center shadow-xs transition flex items-center justify-center gap-1.5"
                        >
                          <Video className="w-3.5 h-3.5" /> Join Telehealth
                        </a>
                      )}

                      {(isConfirmed || isHeld) && (
                        <button
                          onClick={() => setActiveRescheduleAppointment(apt)}
                          className="py-2 px-3 rounded-xl bg-slate-100 hover:bg-slate-200 text-slate-700 font-bold text-xs transition"
                        >
                          Reschedule
                        </button>
                      )}

                      {(isConfirmed || isHeld) && (
                        <button
                          onClick={() => handleCancelAppointment(apt.id)}
                          className="py-2 px-2 rounded-xl text-red-600 hover:bg-red-50 text-xs font-bold transition"
                          title="Cancel Session"
                        >
                          <XCircle className="w-4 h-4" />
                        </button>
                      )}

                      {apt.doctorNotes && (
                        <button
                          onClick={() => setViewingNotes(apt.doctorNotes)}
                          className="py-2 px-3 rounded-xl bg-teal-50 text-teal-800 text-xs font-bold border border-teal-200 hover:bg-teal-100 transition flex items-center gap-1"
                        >
                          <FileText className="w-3.5 h-3.5" /> Doctor Notes
                        </button>
                      )}
                    </div>
                  </div>
                );
              })}
            </div>
          )}
        </div>
      )}

      {/* TAB 3: CBT RESOURCE VAULT */}
      {currentTab === 'resources' && (
        <div className="space-y-6">
          <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-4">
            <div>
              <h2 className="text-xl font-black text-slate-900 tracking-tight">
                CBT & Mindfulness Vault
              </h2>
              <p className="text-xs text-slate-500 mt-1">
                Evidence-based self-help modules, clinical psychoeducation, and relaxation exercises.
              </p>
            </div>

            {/* Category Filter */}
            <div className="flex items-center gap-2 overflow-x-auto">
              <button
                onClick={() => setSelectedCategory('')}
                className={`px-3 py-1.5 rounded-xl text-xs font-bold whitespace-nowrap transition ${
                  !selectedCategory ? 'bg-teal-600 text-white' : 'bg-white border text-slate-600'
                }`}
              >
                All Categories
              </button>
              {categories.map((c) => (
                <button
                  key={c.id}
                  onClick={() => setSelectedCategory(c.id.toString())}
                  className={`px-3 py-1.5 rounded-xl text-xs font-bold whitespace-nowrap transition ${
                    selectedCategory === c.id.toString() ? 'bg-teal-600 text-white' : 'bg-white border text-slate-600'
                  }`}
                >
                  {c.name}
                </button>
              ))}
            </div>
          </div>

          {loadingResources ? (
            <div className="py-16 text-center text-xs text-slate-400">Loading resources...</div>
          ) : resources.length === 0 ? (
            <div className="text-center py-16 bg-white rounded-3xl border border-slate-100 p-8">
              <p className="text-sm font-semibold text-slate-500">No resources available in this category.</p>
            </div>
          ) : (
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
              {resources.map((res) => (
                <div key={res.id} className="bg-white rounded-3xl p-6 border border-slate-100 shadow-xs flex flex-col justify-between">
                  <div>
                    <div className="flex items-center justify-between mb-3">
                      <span className="text-[10px] font-black uppercase px-2.5 py-0.5 rounded-full bg-teal-50 text-teal-800 border border-teal-200">
                        {res.resourceType || 'ARTICLE'}
                      </span>
                      {res.isPremium && (
                        <span className="text-[10px] font-black uppercase px-2 py-0.5 rounded-full bg-amber-100 text-amber-900">
                          PREMIUM
                        </span>
                      )}
                    </div>
                    <h3 className="font-extrabold text-base text-slate-900 mb-2">{res.title}</h3>
                    <p className="text-xs text-slate-600 leading-relaxed line-clamp-3 mb-4">
                      {res.description || res.content}
                    </p>
                  </div>
                  <div className="pt-3 border-t border-slate-100 flex items-center justify-between">
                    <span className="text-[11px] text-slate-400">{res.categoryName || 'Psychoeducation'}</span>
                    <button
                      onClick={() => alert(`Accessing "${res.title}":\n\n${res.content || res.description}`)}
                      className="px-3 py-1.5 rounded-xl bg-teal-600 text-white text-xs font-bold hover:bg-teal-700 transition"
                    >
                      Read Guide
                    </button>
                  </div>
                </div>
              ))}
            </div>
          )}
        </div>
      )}

      {/* TAB 4: MEDICAL PROFILE */}
      {currentTab === 'profile' && (
        <div className="bg-white rounded-3xl p-8 border border-slate-100 shadow-xs max-w-2xl mx-auto space-y-6">
          <div>
            <h2 className="text-xl font-black text-slate-900 tracking-tight">Patient Medical Profile</h2>
            <p className="text-xs text-slate-500 mt-1">
              Your clinical details and emergency contact are confidential under doctor-patient privilege.
            </p>
          </div>

          {profileMsg && (
            <div className="p-3 bg-teal-50 border border-teal-200 text-teal-800 rounded-xl text-xs flex items-center gap-2">
              <CheckCircle2 className="w-4 h-4 text-teal-600" />
              <span>{profileMsg}</span>
            </div>
          )}

          <form onSubmit={handleSaveProfile} className="space-y-4">
            <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
              <div>
                <label className="block text-xs font-bold text-slate-700 mb-1">Full Name</label>
                <input
                  type="text"
                  required
                  value={profileForm.fullName}
                  onChange={(e) => setProfileForm({ ...profileForm, fullName: e.target.value })}
                  className="w-full px-3 py-2 bg-slate-50 border border-slate-200 rounded-xl text-xs font-medium"
                />
              </div>

              <div>
                <label className="block text-xs font-bold text-slate-700 mb-1">Contact Phone</label>
                <input
                  type="tel"
                  required
                  value={profileForm.phone}
                  onChange={(e) => setProfileForm({ ...profileForm, phone: e.target.value })}
                  className="w-full px-3 py-2 bg-slate-50 border border-slate-200 rounded-xl text-xs font-medium"
                />
              </div>
            </div>

            <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
              <div>
                <label className="block text-xs font-bold text-slate-700 mb-1">Date of Birth</label>
                <input
                  type="date"
                  value={profileForm.dateOfBirth}
                  onChange={(e) => setProfileForm({ ...profileForm, dateOfBirth: e.target.value })}
                  className="w-full px-3 py-2 bg-slate-50 border border-slate-200 rounded-xl text-xs font-medium"
                />
              </div>

              <div>
                <label className="block text-xs font-bold text-slate-700 mb-1">Gender</label>
                <select
                  value={profileForm.gender}
                  onChange={(e) => setProfileForm({ ...profileForm, gender: e.target.value })}
                  className="w-full px-3 py-2 bg-slate-50 border border-slate-200 rounded-xl text-xs font-medium"
                >
                  <option value="Male">Male</option>
                  <option value="Female">Female</option>
                  <option value="Other">Other</option>
                  <option value="Prefer not to say">Prefer not to say</option>
                </select>
              </div>
            </div>

            <div>
              <label className="block text-xs font-bold text-slate-700 mb-1">
                Emergency Contact (Bangladesh Relative / Friend)
              </label>
              <input
                type="tel"
                value={profileForm.emergencyContact}
                onChange={(e) => setProfileForm({ ...profileForm, emergencyContact: e.target.value })}
                placeholder="018XXXXXXXX"
                className="w-full px-3 py-2 bg-slate-50 border border-slate-200 rounded-xl text-xs font-medium"
              />
            </div>

            <div>
              <label className="block text-xs font-bold text-slate-700 mb-1">
                Past Medical / Psychiatric History Notes
              </label>
              <textarea
                rows={3}
                value={profileForm.medicalHistoryNotes}
                onChange={(e) => setProfileForm({ ...profileForm, medicalHistoryNotes: e.target.value })}
                placeholder="Any prior diagnoses, medication, or therapy history..."
                className="w-full p-3 bg-slate-50 border border-slate-200 rounded-xl text-xs"
              />
            </div>

            <button
              type="submit"
              disabled={profileSaving}
              className="py-2.5 px-6 rounded-xl bg-teal-600 hover:bg-teal-700 text-white font-extrabold text-xs shadow-md transition flex items-center justify-center gap-2"
            >
              {profileSaving ? 'Saving...' : 'Update Medical Profile'}
            </button>
          </form>
        </div>
      )}

      {/* Doctor Notes Modal */}
      {viewingNotes && (
        <div className="fixed inset-0 z-50 flex items-center justify-center p-4 bg-slate-950/60 backdrop-blur-xs">
          <div className="bg-white rounded-3xl max-w-md w-full p-6 shadow-2xl border border-slate-100 space-y-4">
            <div className="flex items-center justify-between pb-3 border-b border-slate-100">
              <h3 className="font-extrabold text-base text-slate-900">Clinician Session Notes</h3>
              <button onClick={() => setViewingNotes(null)} className="text-slate-400 hover:text-slate-700 text-xs font-bold">
                ✕ Close
              </button>
            </div>
            <p className="text-xs text-slate-700 leading-relaxed whitespace-pre-wrap">
              {viewingNotes}
            </p>
          </div>
        </div>
      )}

      {/* Modals */}
      {bookingDoctor && (
        <BookingModal
          doctor={bookingDoctor}
          onClose={() => setBookingDoctor(null)}
          onBookingComplete={handleBookingSuccess}
        />
      )}

      {activePaymentAppointment && (
        <BkashPaymentModal
          appointment={activePaymentAppointment}
          onClose={() => setActivePaymentAppointment(null)}
          onSuccess={handlePaymentSuccess}
        />
      )}

      {activeRescheduleAppointment && (
        <RescheduleModal
          appointment={activeRescheduleAppointment}
          onClose={() => setActiveRescheduleAppointment(null)}
          onSuccess={() => {
            setActiveRescheduleAppointment(null);
            loadAppointments();
          }}
        />
      )}

    </div>
  );
}
