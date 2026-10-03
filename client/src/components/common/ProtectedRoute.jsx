import React from 'react';
import { Navigate, useLocation, Link } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';
import { ShieldAlert, ArrowLeft } from 'lucide-react';

export default function ProtectedRoute({ children, allowedRoles = [] }) {
  const { user, isAuthenticated, loading } = useAuth();
  const location = useLocation();

  if (loading) {
    return (
      <div className="min-h-[60vh] flex items-center justify-center">
        <div className="flex flex-col items-center gap-3">
          <div className="w-10 h-10 border-4 border-teal-600 border-t-transparent rounded-full animate-spin"></div>
          <p className="text-sm font-semibold text-slate-500">Checking authorization...</p>
        </div>
      </div>
    );
  }

  // Not authenticated
  if (!isAuthenticated) {
    if (allowedRoles.includes('ADMIN')) {
      return <Navigate to="/admin/login" state={{ from: location }} replace />;
    }
    return <Navigate to="/login" state={{ from: location }} replace />;
  }

  // Role validation
  if (allowedRoles.length > 0 && !allowedRoles.includes(user.role)) {
    return (
      <div className="max-w-md mx-auto my-16 p-8 bg-white border border-red-100 rounded-2xl shadow-xl text-center">
        <div className="w-14 h-14 bg-red-50 text-red-600 rounded-2xl flex items-center justify-center mx-auto mb-4">
          <ShieldAlert className="w-8 h-8" />
        </div>
        <h2 className="text-xl font-black text-slate-900 mb-2">Access Restricted</h2>
        <p className="text-xs text-slate-600 mb-6 leading-relaxed">
          You are signed in as a <span className="font-bold text-teal-700">{user.role}</span>.
          This portal requires one of the following roles: <span className="font-semibold">{allowedRoles.join(', ')}</span>.
        </p>
        <div className="flex flex-col gap-2">
          {user.role === 'PATIENT' && (
            <Link
              to="/patient"
              className="w-full py-2.5 px-4 bg-teal-600 text-white font-bold rounded-xl text-xs hover:bg-teal-700 transition"
            >
              Go to Patient Portal
            </Link>
          )}
          {user.role === 'DOCTOR' && (
            <Link
              to="/doctor"
              className="w-full py-2.5 px-4 bg-teal-600 text-white font-bold rounded-xl text-xs hover:bg-teal-700 transition"
            >
              Go to Doctor Practice Portal
            </Link>
          )}
          {user.role === 'ADMIN' && (
            <Link
              to="/admin"
              className="w-full py-2.5 px-4 bg-amber-600 text-white font-bold rounded-xl text-xs hover:bg-amber-700 transition"
            >
              Go to Admin Console
            </Link>
          )}
          <Link
            to="/"
            className="w-full py-2.5 px-4 bg-slate-100 text-slate-700 font-bold rounded-xl text-xs hover:bg-slate-200 transition inline-flex items-center justify-center gap-1.5"
          >
            <ArrowLeft className="w-3.5 h-3.5" /> Return Home
          </Link>
        </div>
      </div>
    );
  }

  return children;
}
