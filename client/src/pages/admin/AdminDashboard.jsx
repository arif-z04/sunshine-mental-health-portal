import React, { useState, useEffect } from 'react';
import { useAuth } from '../../context/AuthContext';
import { adminApi } from '../../api/apiClient';
import { formatBDT } from '../../utils/dateUtils';
import {
  Shield, Users, Stethoscope, Calendar,
  CheckCircle2, RefreshCw, Activity, FileText
} from 'lucide-react';

export default function AdminDashboard() {
  const { user: _user } = useAuth();
  const [activeTab, setActiveTab] = useState('metrics'); // 'metrics', 'doctors', 'users', 'appointments', 'audits'

  // Dashboard Metrics
  const [metrics, setMetrics] = useState(null);
  const [loadingMetrics, setLoadingMetrics] = useState(false);

  // Doctors Verification Queue
  const [doctors, setDoctors] = useState([]);
  const [loadingDoctors, setLoadingDoctors] = useState(false);

  // Users Management
  const [users, setUsers] = useState([]);
  const [loadingUsers, setLoadingUsers] = useState(false);
  const [userRoleFilter, setUserRoleFilter] = useState('');
  const [userSearch, setUserSearch] = useState('');

  // Appointments Ledger
  const [appointments, setAppointments] = useState([]);
  const [loadingAppointments, setLoadingAppointments] = useState(false);

  // System Audit Logs
  const [auditLogs, setAuditLogs] = useState([]);
  const [loadingAudits, setLoadingAudits] = useState(false);

  // Messages
  const [actionMessage, setActionMessage] = useState('');

  const loadMetrics = async () => {
    setLoadingMetrics(true);
    try {
      const data = await adminApi.getMetrics();
      setMetrics(data);
    } catch (err) {
      console.error(err);
    } finally {
      setLoadingMetrics(false);
    }
  };

  const loadDoctors = async () => {
    setLoadingDoctors(true);
    try {
      const data = await adminApi.getDoctors();
      setDoctors(data || []);
    } catch (err) {
      console.error(err);
    } finally {
      setLoadingDoctors(false);
    }
  };

  const loadUsers = async () => {
    setLoadingUsers(true);
    try {
      const data = await adminApi.getUsers(userRoleFilter, userSearch);
      setUsers(data || []);
    } catch (err) {
      console.error(err);
    } finally {
      setLoadingUsers(false);
    }
  };

  const loadAppointments = async () => {
    setLoadingAppointments(true);
    try {
      const data = await adminApi.getAppointments();
      setAppointments(data || []);
    } catch (err) {
      console.error(err);
    } finally {
      setLoadingAppointments(false);
    }
  };

  const loadAudits = async () => {
    setLoadingAudits(true);
    try {
      const data = await adminApi.getAuditLogs(100);
      setAuditLogs(data || []);
    } catch (err) {
      console.error(err);
    } finally {
      setLoadingAudits(false);
    }
  };

  useEffect(() => {
    loadMetrics();
  }, []);

  useEffect(() => {
    if (activeTab === 'doctors') loadDoctors();
    if (activeTab === 'users') loadUsers();
    if (activeTab === 'appointments') loadAppointments();
    if (activeTab === 'audits') loadAudits();
  }, [activeTab, userRoleFilter, userSearch]);

  const handleVerifyDoctor = async (doctorId, currentStatus) => {
    try {
      await adminApi.verifyDoctor(doctorId, !currentStatus);
      setActionMessage(`Doctor #${doctorId} availability and BMDC verification set to ${!currentStatus}`);
      loadDoctors();
    } catch (err) {
      alert('Verification error: ' + err.message);
    }
  };

  const handleToggleUserStatus = async (userId, currentStatus) => {
    try {
      await adminApi.updateUserStatus(userId, !currentStatus);
      setActionMessage(`User #${userId} status set to ${!currentStatus ? 'Active' : 'Suspended'}`);
      loadUsers();
    } catch (err) {
      alert('Failed to update user status: ' + err.message);
    }
  };

  return (
    <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8 space-y-8">
      
      {/* Admin Header */}
      <div className="bg-slate-900 rounded-3xl p-6 sm:p-8 text-white flex flex-col md:flex-row md:items-center justify-between gap-6 shadow-xl">
        <div className="flex items-center gap-4">
          <div className="w-14 h-14 rounded-2xl bg-amber-500/20 border border-amber-500/40 text-amber-400 font-black text-2xl flex items-center justify-center shrink-0">
            <Shield className="w-8 h-8" />
          </div>
          <div>
            <div className="flex items-center gap-2">
              <h1 className="text-xl sm:text-2xl font-black tracking-tight">
                Sunshine Platform Operations
              </h1>
              <span className="text-[10px] font-black uppercase px-2.5 py-0.5 rounded-full bg-amber-500/20 text-amber-300 border border-amber-500/30">
                Admin Console
              </span>
            </div>
            <p className="text-xs text-slate-400 mt-1">
              Bangladesh Mental Health Telehealth Network Administration
            </p>
          </div>
        </div>

        {/* Tab Navigation */}
        <div className="flex flex-wrap items-center gap-1.5 p-1.5 bg-slate-800 rounded-2xl">
          {[
            { id: 'metrics', label: 'Overview', icon: Activity },
            { id: 'doctors', label: 'BMDC Queue', icon: Stethoscope },
            { id: 'users', label: 'Users', icon: Users },
            { id: 'appointments', label: 'Appointments', icon: Calendar },
            { id: 'audits', label: 'Audit Trail', icon: FileText }
          ].map((t) => {
            const Icon = t.icon;
            const isActive = activeTab === t.id;
            return (
              <button
                key={t.id}
                onClick={() => {
                  setActiveTab(t.id);
                  setActionMessage('');
                }}
                className={`py-2 px-3 rounded-xl text-xs font-bold transition-all flex items-center gap-1.5 ${
                  isActive
                    ? 'bg-amber-500 text-slate-950 font-black shadow-xs'
                    : 'text-slate-300 hover:text-white'
                }`}
              >
                <Icon className="w-3.5 h-3.5" />
                {t.label}
              </button>
            );
          })}
        </div>
      </div>

      {actionMessage && (
        <div className="p-3 bg-emerald-50 border border-emerald-200 text-emerald-800 rounded-xl text-xs flex items-center gap-2">
          <CheckCircle2 className="w-4 h-4 text-emerald-600 shrink-0" />
          <span>{actionMessage}</span>
        </div>
      )}

      {/* TAB 1: OVERVIEW METRICS */}
      {activeTab === 'metrics' && (
        <div className="space-y-6">
          <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-5">
            <div className="bg-white p-6 rounded-3xl border border-slate-100 shadow-xs">
              <span className="text-xs font-bold text-slate-400 block uppercase">Total Revenue Processed</span>
              <span className="text-3xl font-black text-slate-900 mt-2 block">
                {formatBDT(metrics?.totalRevenue)}
              </span>
              <span className="text-[11px] text-teal-600 font-semibold">Processed via bKash / Nagad / Cards</span>
            </div>

            <div className="bg-white p-6 rounded-3xl border border-slate-100 shadow-xs">
              <span className="text-xs font-bold text-slate-400 block uppercase">Active Telehealth Sessions</span>
              <span className="text-3xl font-black text-teal-600 mt-2 block">
                {metrics?.activeAppointments ?? 0}
              </span>
              <span className="text-[11px] text-slate-500">Confirmed upcoming appointments</span>
            </div>

            <div className="bg-white p-6 rounded-3xl border border-slate-100 shadow-xs">
              <span className="text-xs font-bold text-slate-400 block uppercase">Active Subscriptions</span>
              <span className="text-3xl font-black text-amber-600 mt-2 block">
                {metrics?.activeSubscriptions ?? 0}
              </span>
              <span className="text-[11px] text-slate-500">CBT Digital Vault Memberships</span>
            </div>

            <div className="bg-white p-6 rounded-3xl border border-slate-100 shadow-xs">
              <span className="text-xs font-bold text-slate-400 block uppercase">Registered Doctors</span>
              <span className="text-3xl font-black text-slate-800 mt-2 block">
                {metrics?.totalDoctors ?? 0}
              </span>
              <span className="text-[11px] text-slate-500">BMDC accredited specialists</span>
            </div>

            <div className="bg-white p-6 rounded-3xl border border-slate-100 shadow-xs">
              <span className="text-xs font-bold text-slate-400 block uppercase">Registered Patients</span>
              <span className="text-3xl font-black text-slate-800 mt-2 block">
                {metrics?.totalPatients ?? 0}
              </span>
              <span className="text-[11px] text-slate-500">Total client accounts</span>
            </div>

            <div className="bg-white p-6 rounded-3xl border border-slate-100 shadow-xs">
              <span className="text-xs font-bold text-slate-400 block uppercase">CBT Resources</span>
              <span className="text-3xl font-black text-slate-800 mt-2 block">
                {metrics?.totalResources ?? 0}
              </span>
              <span className="text-[11px] text-slate-500">Clinical guides & audio modules</span>
            </div>
          </div>
        </div>
      )}

      {/* TAB 2: BMDC VERIFICATION QUEUE */}
      {activeTab === 'doctors' && (
        <div className="space-y-4">
          <div className="flex items-center justify-between">
            <h2 className="text-xl font-black text-slate-900 tracking-tight">
              Clinician & BMDC Credential Verification Queue
            </h2>
            <button
              onClick={loadDoctors}
              className="text-xs font-bold text-teal-700 hover:text-teal-800 flex items-center gap-1.5 px-3 py-1.5 rounded-lg bg-teal-50 border border-teal-200"
            >
              <RefreshCw className="w-3.5 h-3.5" /> Refresh List
            </button>
          </div>

          {loadingDoctors ? (
            <div className="py-16 text-center text-xs text-slate-400">Loading clinicians...</div>
          ) : doctors.length === 0 ? (
            <div className="bg-white rounded-3xl p-12 text-center border border-slate-100">
              <p className="text-sm font-semibold text-slate-500">No doctors registered yet.</p>
            </div>
          ) : (
            <div className="bg-white rounded-3xl border border-slate-100 shadow-xs overflow-hidden">
              <div className="overflow-x-auto">
                <table className="w-full text-left text-xs">
                  <thead className="bg-slate-50 border-b border-slate-100 text-slate-600 uppercase font-black tracking-wider text-[10px]">
                    <tr>
                      <th className="py-3.5 px-4">Doctor</th>
                      <th className="py-3.5 px-4">Specialization</th>
                      <th className="py-3.5 px-4">BMDC Qualification</th>
                      <th className="py-3.5 px-4">Fee & Policy</th>
                      <th className="py-3.5 px-4">Verification Status</th>
                      <th className="py-3.5 px-4 text-right">Action</th>
                    </tr>
                  </thead>
                  <tbody className="divide-y divide-slate-100">
                    {doctors.map((doc) => (
                      <tr key={doc.id} className="hover:bg-slate-50/80 transition-colors">
                        <td className="py-4 px-4 font-bold text-slate-800">
                          <div>{doc.fullName}</div>
                          <div className="text-[10px] text-slate-400 font-normal">{doc.email}</div>
                        </td>
                        <td className="py-4 px-4 text-slate-700 font-semibold">{doc.specialization}</td>
                        <td className="py-4 px-4 text-slate-600">{doc.qualification || 'MBBS, MD'}</td>
                        <td className="py-4 px-4 font-bold text-slate-800">
                          {formatBDT(doc.consultationFee)}
                          <span className="block text-[10px] text-slate-400 font-normal">{doc.paymentPolicy}</span>
                        </td>
                        <td className="py-4 px-4">
                          <span className={`text-[10px] font-black uppercase px-2 py-0.5 rounded-full border ${
                            doc.isAvailable
                              ? 'bg-emerald-50 text-emerald-800 border-emerald-200'
                              : 'bg-amber-50 text-amber-800 border-amber-200'
                          }`}>
                            {doc.isAvailable ? 'VERIFIED ACTIVE' : 'PENDING APPROVAL'}
                          </span>
                        </td>
                        <td className="py-4 px-4 text-right">
                          <button
                            onClick={() => handleVerifyDoctor(doc.id, doc.isAvailable)}
                            className={`py-1.5 px-3 rounded-xl font-black text-[11px] shadow-xs transition ${
                              doc.isAvailable
                                ? 'bg-red-50 text-red-700 border border-red-200 hover:bg-red-100'
                                : 'bg-emerald-600 text-white hover:bg-emerald-700'
                            }`}
                          >
                            {doc.isAvailable ? 'Unverify / Suspend' : 'Approve BMDC'}
                          </button>
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            </div>
          )}
        </div>
      )}

      {/* TAB 3: USER MANAGEMENT */}
      {activeTab === 'users' && (
        <div className="space-y-4">
          <div className="flex flex-col sm:flex-row sm:items-center justify-between gap-4">
            <h2 className="text-xl font-black text-slate-900 tracking-tight">
              User Accounts & Role Permissions
            </h2>

            <div className="flex items-center gap-2">
              <input
                type="text"
                value={userSearch}
                onChange={(e) => setUserSearch(e.target.value)}
                placeholder="Search by name, email..."
                className="px-3 py-1.5 bg-white border border-slate-200 rounded-xl text-xs"
              />
              <select
                value={userRoleFilter}
                onChange={(e) => setUserRoleFilter(e.target.value)}
                className="px-3 py-1.5 bg-white border border-slate-200 rounded-xl text-xs font-semibold"
              >
                <option value="">All Roles</option>
                <option value="PATIENT">PATIENT</option>
                <option value="DOCTOR">DOCTOR</option>
                <option value="ADMIN">ADMIN</option>
              </select>
            </div>
          </div>

          {loadingUsers ? (
            <div className="py-16 text-center text-xs text-slate-400">Loading user accounts...</div>
          ) : (
            <div className="bg-white rounded-3xl border border-slate-100 shadow-xs overflow-hidden">
              <div className="overflow-x-auto">
                <table className="w-full text-left text-xs">
                  <thead className="bg-slate-50 border-b border-slate-100 text-slate-600 uppercase font-black tracking-wider text-[10px]">
                    <tr>
                      <th className="py-3.5 px-4">User</th>
                      <th className="py-3.5 px-4">Role</th>
                      <th className="py-3.5 px-4">Phone</th>
                      <th className="py-3.5 px-4">Status</th>
                      <th className="py-3.5 px-4 text-right">Action</th>
                    </tr>
                  </thead>
                  <tbody className="divide-y divide-slate-100">
                    {users.map((u) => (
                      <tr key={u.id} className="hover:bg-slate-50/80 transition-colors">
                        <td className="py-4 px-4 font-bold text-slate-800">
                          <div>{u.fullName || 'Anonymous'}</div>
                          <div className="text-[10px] text-slate-400 font-normal">{u.email}</div>
                        </td>
                        <td className="py-4 px-4">
                          <span className="text-[10px] font-black uppercase px-2 py-0.5 rounded-full bg-slate-100 text-slate-700 border border-slate-200">
                            {u.role}
                          </span>
                        </td>
                        <td className="py-4 px-4 text-slate-600">{u.phoneNumber || 'N/A'}</td>
                        <td className="py-4 px-4">
                          <span className={`text-[10px] font-black uppercase px-2 py-0.5 rounded-full border ${
                            u.isActive
                              ? 'bg-emerald-50 text-emerald-800 border-emerald-200'
                              : 'bg-red-50 text-red-800 border-red-200'
                          }`}>
                            {u.isActive ? 'ACTIVE' : 'DEACTIVATED'}
                          </span>
                        </td>
                        <td className="py-4 px-4 text-right">
                          <button
                            onClick={() => handleToggleUserStatus(u.id, u.isActive)}
                            className={`py-1.5 px-3 rounded-xl font-black text-[11px] shadow-xs transition ${
                              u.isActive
                                ? 'bg-red-50 text-red-700 border border-red-200 hover:bg-red-100'
                                : 'bg-emerald-600 text-white hover:bg-emerald-700'
                            }`}
                          >
                            {u.isActive ? 'Deactivate' : 'Activate User'}
                          </button>
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            </div>
          )}
        </div>
      )}

      {/* TAB 4: APPOINTMENTS LEDGER */}
      {activeTab === 'appointments' && (
        <div className="space-y-4">
          <div className="flex items-center justify-between">
            <h2 className="text-xl font-black text-slate-900 tracking-tight">
              Master Platform Consultation Ledger
            </h2>
            <button
              onClick={loadAppointments}
              className="text-xs font-bold text-teal-700 hover:text-teal-800 flex items-center gap-1.5 px-3 py-1.5 rounded-lg bg-teal-50 border border-teal-200"
            >
              <RefreshCw className="w-3.5 h-3.5" /> Refresh List
            </button>
          </div>

          {loadingAppointments ? (
            <div className="py-16 text-center text-xs text-slate-400">Loading appointments...</div>
          ) : (
            <div className="bg-white rounded-3xl border border-slate-100 shadow-xs overflow-hidden">
              <div className="overflow-x-auto">
                <table className="w-full text-left text-xs">
                  <thead className="bg-slate-50 border-b border-slate-100 text-slate-600 uppercase font-black tracking-wider text-[10px]">
                    <tr>
                      <th className="py-3.5 px-4">#ID</th>
                      <th className="py-3.5 px-4">Patient</th>
                      <th className="py-3.5 px-4">Doctor</th>
                      <th className="py-3.5 px-4">Date & Time</th>
                      <th className="py-3.5 px-4">Status</th>
                      <th className="py-3.5 px-4">Fee</th>
                    </tr>
                  </thead>
                  <tbody className="divide-y divide-slate-100">
                    {appointments.map((apt) => (
                      <tr key={apt.id} className="hover:bg-slate-50/80 transition-colors">
                        <td className="py-4 px-4 font-mono font-bold text-slate-400">#{apt.id}</td>
                        <td className="py-4 px-4 font-bold text-slate-800">{apt.patientName}</td>
                        <td className="py-4 px-4 font-semibold text-slate-700">{apt.doctorName}</td>
                        <td className="py-4 px-4 text-slate-600">
                          {apt.appointmentDate} at <strong>{apt.startTime}</strong>
                        </td>
                        <td className="py-4 px-4">
                          <span className={`text-[10px] font-black uppercase px-2 py-0.5 rounded-full border ${
                            apt.status === 'CONFIRMED'
                              ? 'bg-emerald-50 text-emerald-800 border-emerald-200'
                              : apt.status === 'HELD'
                              ? 'bg-amber-50 text-amber-900 border-amber-200'
                              : 'bg-slate-100 text-slate-600 border-slate-200'
                          }`}>
                            {apt.status}
                          </span>
                        </td>
                        <td className="py-4 px-4 font-black text-slate-800">
                          {formatBDT(apt.fee || apt.doctorFee)}
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            </div>
          )}
        </div>
      )}

      {/* TAB 5: SYSTEM AUDIT LOGS */}
      {activeTab === 'audits' && (
        <div className="space-y-4">
          <div className="flex items-center justify-between">
            <h2 className="text-xl font-black text-slate-900 tracking-tight">
              Live System Audit Trail
            </h2>
            <button
              onClick={loadAudits}
              className="text-xs font-bold text-teal-700 hover:text-teal-800 flex items-center gap-1.5 px-3 py-1.5 rounded-lg bg-teal-50 border border-teal-200"
            >
              <RefreshCw className="w-3.5 h-3.5" /> Refresh Logs
            </button>
          </div>

          {loadingAudits ? (
            <div className="py-16 text-center text-xs text-slate-400">Loading audit trail...</div>
          ) : (
            <div className="bg-white rounded-3xl border border-slate-100 shadow-xs overflow-hidden">
              <div className="overflow-x-auto">
                <table className="w-full text-left text-xs font-mono">
                  <thead className="bg-slate-50 border-b border-slate-100 text-slate-600 uppercase font-black tracking-wider text-[10px]">
                    <tr>
                      <th className="py-3.5 px-4">Timestamp (UTC)</th>
                      <th className="py-3.5 px-4">Action</th>
                      <th className="py-3.5 px-4">Entity</th>
                      <th className="py-3.5 px-4">Actor</th>
                      <th className="py-3.5 px-4">IP Address</th>
                      <th className="py-3.5 px-4">Details</th>
                    </tr>
                  </thead>
                  <tbody className="divide-y divide-slate-100">
                    {auditLogs.map((log) => (
                      <tr key={log.id} className="hover:bg-slate-50/80 transition-colors">
                        <td className="py-3 px-4 text-slate-400 whitespace-nowrap text-[11px]">
                          {log.createdAt ? new Date(log.createdAt).toLocaleString('en-GB') : 'N/A'}
                        </td>
                        <td className="py-3 px-4 font-bold text-teal-700">{log.action}</td>
                        <td className="py-3 px-4 text-slate-600">{log.entityType}</td>
                        <td className="py-3 px-4 text-slate-800">{log.userEmail || 'System'}</td>
                        <td className="py-3 px-4 text-slate-400">{log.ipAddress || '127.0.0.1'}</td>
                        <td className="py-3 px-4 text-slate-600 max-w-xs truncate" title={log.details}>
                          {log.details}
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            </div>
          )}
        </div>
      )}

    </div>
  );
}
