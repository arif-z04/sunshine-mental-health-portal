import React, { useState } from 'react';
import { Link, useNavigate, useLocation } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';
import { Lock, Mail, AlertCircle, ArrowRight, ShieldCheck, HeartHandshake, Stethoscope } from 'lucide-react';

export default function LoginPage() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [isSubmitting, setIsSubmitting] = useState(false);
  const { login } = useAuth();
  const navigate = useNavigate();
  const location = useLocation();

  const from = location.state?.from?.pathname || null;

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');
    setIsSubmitting(true);

    try {
      const res = await login(email.trim(), password);
      if (res.success) {
        if (from) {
          navigate(from, { replace: true });
        } else if (res.user.role === 'PATIENT') {
          navigate('/patient', { replace: true });
        } else if (res.user.role === 'DOCTOR') {
          navigate('/doctor', { replace: true });
        } else if (res.user.role === 'ADMIN') {
          navigate('/admin', { replace: true });
        } else {
          navigate('/', { replace: true });
        }
      } else {
        setError(res.message || 'Login failed. Please check your credentials.');
      }
    } catch (err) {
      setError(err.message || 'Unable to connect to authentication service.');
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleQuickFill = (quickEmail, quickPassword) => {
    setEmail(quickEmail);
    setPassword(quickPassword);
  };

  return (
    <div className="min-h-[75vh] flex items-center justify-center px-4 py-12">
      <div className="bg-white rounded-3xl max-w-md w-full p-8 shadow-xl border border-slate-100 space-y-6">
        
        {/* Header */}
        <div className="text-center space-y-2">
          <div className="w-12 h-12 rounded-2xl bg-teal-600 text-white flex items-center justify-center font-black text-2xl mx-auto shadow-md shadow-teal-600/20">
            ☀️
          </div>
          <h2 className="text-2xl font-black text-slate-900 tracking-tight">
            Sign In to Sunshine BD
          </h2>
          <p className="text-xs text-slate-500">
            Access your appointments, consultation ledger, and clinical records.
          </p>
        </div>

        {/* Demo Fast-Fill Buttons */}
        <div className="bg-slate-50 p-3 rounded-2xl border border-slate-200/80 space-y-2">
          <span className="text-[10px] font-extrabold text-slate-500 uppercase tracking-wider block text-center">
            Demo Credentials Quick-Fill
          </span>
          <div className="grid grid-cols-2 gap-2 text-xs font-bold">
            <button
              type="button"
              onClick={() => handleQuickFill('rahim@example.com', 'Patient123!')}
              className="py-1.5 px-2 rounded-xl bg-white border border-slate-200 text-slate-700 hover:border-teal-500 hover:text-teal-700 transition flex items-center justify-center gap-1"
            >
              <HeartHandshake className="w-3.5 h-3.5 text-teal-600" />
              Patient Demo
            </button>
            <button
              type="button"
              onClick={() => handleQuickFill('dr.anika@sunshine.bd', 'Doctor123!')}
              className="py-1.5 px-2 rounded-xl bg-white border border-slate-200 text-slate-700 hover:border-teal-500 hover:text-teal-700 transition flex items-center justify-center gap-1"
            >
              <Stethoscope className="w-3.5 h-3.5 text-teal-600" />
              Doctor Demo
            </button>
          </div>
        </div>

        {error && (
          <div className="p-3 bg-red-50 border border-red-200 text-red-700 rounded-xl text-xs flex items-start gap-2">
            <AlertCircle className="w-4 h-4 shrink-0 mt-0.5" />
            <span>{error}</span>
          </div>
        )}

        <form onSubmit={handleSubmit} className="space-y-4">
          <div>
            <label className="block text-xs font-bold text-slate-700 mb-1">
              Email Address
            </label>
            <div className="relative">
              <Mail className="w-4 h-4 absolute left-3.5 top-3 text-slate-400" />
              <input
                type="email"
                required
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                placeholder="name@example.com"
                className="w-full pl-10 pr-3 py-2.5 bg-slate-50 border border-slate-200 rounded-xl text-xs font-medium focus:outline-hidden focus:border-teal-500 focus:bg-white"
              />
            </div>
          </div>

          <div>
            <label className="block text-xs font-bold text-slate-700 mb-1">
              Password
            </label>
            <div className="relative">
              <Lock className="w-4 h-4 absolute left-3.5 top-3 text-slate-400" />
              <input
                type="password"
                required
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                placeholder="••••••••"
                className="w-full pl-10 pr-3 py-2.5 bg-slate-50 border border-slate-200 rounded-xl text-xs font-medium focus:outline-hidden focus:border-teal-500 focus:bg-white"
              />
            </div>
          </div>

          <button
            type="submit"
            disabled={isSubmitting}
            className="w-full py-3 px-4 rounded-xl bg-teal-600 hover:bg-teal-700 text-white font-extrabold text-xs shadow-md shadow-teal-600/20 active:scale-95 disabled:opacity-50 transition-all flex items-center justify-center gap-2"
          >
            {isSubmitting ? (
              <>
                <div className="w-4 h-4 border-2 border-white border-t-transparent rounded-full animate-spin"></div>
                Signing In...
              </>
            ) : (
              <>
                Sign In to Portal
                <ArrowRight className="w-4 h-4" />
              </>
            )}
          </button>
        </form>

        {/* Footer links */}
        <div className="pt-4 border-t border-slate-100 flex flex-col gap-2.5 text-center text-xs text-slate-500">
          <p>
            Don't have an account?{' '}
            <Link to="/register" className="text-teal-600 font-bold hover:underline">
              Create an account
            </Link>
          </p>
          <p className="text-[11px] text-slate-400">
            System Administrator?{' '}
            <Link to="/admin/login" className="text-amber-700 font-bold hover:underline">
              Go to Isolated Admin Portal
            </Link>
          </p>
        </div>

      </div>
    </div>
  );
}
