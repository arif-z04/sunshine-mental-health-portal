import React, { useState } from 'react';
import { Link, useNavigate, useLocation } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';
import { Menu, X, Sun, LogOut, User, Shield, Stethoscope, HeartHandshake } from 'lucide-react';

export default function Navbar() {
  const { user, isAuthenticated, logout, isPatient, isDoctor, isAdmin } = useAuth();
  const [mobileMenuOpen, setMobileMenuOpen] = useState(false);
  const navigate = useNavigate();
  const location = useLocation();

  const handleLogout = () => {
    logout();
    navigate('/');
    setMobileMenuOpen(false);
  };

  const getPortalLink = () => {
    if (isAdmin) return { path: '/admin', label: 'Admin Console', icon: Shield };
    if (isDoctor) return { path: '/doctor', label: 'Doctor Portal', icon: Stethoscope };
    return { path: '/patient', label: 'Patient Portal', icon: HeartHandshake };
  };

  const portal = getPortalLink();

  return (
    <header className="bg-white/95 backdrop-blur-md border-b border-slate-100 sticky top-0 z-40">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 h-20 flex items-center justify-between">
        
        {/* Brand Logo */}
        <Link to="/" className="flex items-center gap-2.5 group">
          <div className="w-10 h-10 rounded-xl bg-gradient-to-tr from-teal-600 to-amber-500 flex items-center justify-center text-white text-xl font-black shadow-sm shadow-teal-600/20 group-hover:scale-105 transition-transform">
            ☀️
          </div>
          <div>
            <span className="text-xl font-black tracking-tight text-slate-900 group-hover:text-teal-700 transition-colors">
              Sunshine<span className="text-amber-500">.</span>BD
            </span>
            <span className="block text-[10px] tracking-wider uppercase font-bold text-teal-700">
              Mental Health Telehealth BD
            </span>
          </div>
        </Link>

        {/* Desktop Navigation Links */}
        <nav className="hidden md:flex items-center gap-6 text-sm font-semibold text-slate-600">
          <Link to="/" className={`hover:text-teal-600 transition ${location.pathname === '/' ? 'text-teal-600 font-bold' : ''}`}>
            Home
          </Link>
          <Link to="/patient" className={`hover:text-teal-600 transition ${location.pathname.startsWith('/patient') ? 'text-teal-600 font-bold' : ''}`}>
            Find Counselors
          </Link>
          <a href="/#how-it-works" className="hover:text-teal-600 transition">
            How It Works
          </a>
          <a href="/#cbt-library" className="hover:text-teal-600 transition">
            CBT Vault
          </a>
          <a href="/#faq" className="hover:text-teal-600 transition">
            FAQ
          </a>

          {/* Direct Role Portal Shortcut */}
          {isAuthenticated && (
            <Link
              to={portal.path}
              className="inline-flex items-center gap-1.5 px-3 py-1.5 rounded-lg bg-teal-50 text-teal-800 text-xs font-bold border border-teal-200 hover:bg-teal-100 transition"
            >
              <portal.icon className="w-3.5 h-3.5 text-teal-600" />
              {portal.label}
            </Link>
          )}
        </nav>

        {/* Desktop Auth Controls */}
        <div className="hidden sm:flex items-center gap-3">
          {isAuthenticated ? (
            <div className="flex items-center gap-3">
              <div className="text-right">
                <span className="block text-xs font-bold text-slate-800 leading-tight">
                  {user.fullName || user.email}
                </span>
                <span className="inline-block text-[10px] font-extrabold uppercase px-1.5 py-0.2 rounded bg-slate-100 text-slate-600 border border-slate-200">
                  {user.role}
                </span>
              </div>
              <button
                onClick={handleLogout}
                className="inline-flex items-center gap-1 text-xs font-bold text-slate-600 hover:text-red-600 px-3 py-2 rounded-lg hover:bg-red-50 border border-slate-200 transition"
                title="Log Out"
              >
                <LogOut className="w-3.5 h-3.5" />
                <span className="hidden lg:inline">Sign Out</span>
              </button>
            </div>
          ) : (
            <div className="flex items-center gap-2">
              <Link
                to="/login"
                className="text-xs font-bold text-slate-700 hover:text-teal-700 px-3 py-2 rounded-lg hover:bg-slate-50 transition"
              >
                Sign In
              </Link>
              <Link
                to="/register"
                className="text-xs font-extrabold bg-teal-600 hover:bg-teal-700 text-white px-4 py-2.5 rounded-xl shadow-sm shadow-teal-600/20 transition active:scale-95"
              >
                Get Started
              </Link>
            </div>
          )}
        </div>

        {/* Hamburger Button (Mobile) */}
        <button
          onClick={() => setMobileMenuOpen(!mobileMenuOpen)}
          className="md:hidden p-2 rounded-xl text-slate-600 hover:bg-slate-100 focus:outline-hidden"
          aria-label="Toggle mobile menu"
        >
          {mobileMenuOpen ? <X className="w-6 h-6" /> : <Menu className="w-6 h-6" />}
        </button>
      </div>

      {/* Mobile Drawer Menu */}
      {mobileMenuOpen && (
        <div className="md:hidden bg-white border-b border-slate-200 px-4 pt-3 pb-6 space-y-3 shadow-xl">
          <nav className="flex flex-col gap-1 text-sm font-semibold text-slate-700">
            <Link
              to="/"
              onClick={() => setMobileMenuOpen(false)}
              className="px-3 py-2 rounded-lg hover:bg-slate-50 text-slate-800"
            >
              Home
            </Link>
            <Link
              to="/patient"
              onClick={() => setMobileMenuOpen(false)}
              className="px-3 py-2 rounded-lg hover:bg-teal-50 text-teal-800 font-bold"
            >
              Find Counselors & Telehealth
            </Link>
            <Link
              to="/doctor"
              onClick={() => setMobileMenuOpen(false)}
              className="px-3 py-2 rounded-lg hover:bg-slate-50 text-slate-700"
            >
              Doctor Practice Portal
            </Link>
            <Link
              to="/admin/login"
              onClick={() => setMobileMenuOpen(false)}
              className="px-3 py-2 rounded-lg hover:bg-amber-50 text-amber-900 font-semibold"
            >
              Admin Portal
            </Link>
          </nav>

          <div className="pt-3 border-t border-slate-100">
            {isAuthenticated ? (
              <div className="space-y-2">
                <div className="px-3 py-1">
                  <div className="text-xs font-bold text-slate-800">{user.fullName || user.email}</div>
                  <div className="text-[11px] text-teal-700 font-semibold uppercase">{user.role} Account</div>
                </div>
                <button
                  onClick={handleLogout}
                  className="w-full text-left px-3 py-2 text-xs font-bold text-red-600 hover:bg-red-50 rounded-lg flex items-center gap-2"
                >
                  <LogOut className="w-4 h-4" /> Sign Out
                </button>
              </div>
            ) : (
              <div className="grid grid-cols-2 gap-2 pt-1">
                <Link
                  to="/login"
                  onClick={() => setMobileMenuOpen(false)}
                  className="text-center py-2 px-3 text-xs font-bold text-slate-700 bg-slate-100 rounded-lg"
                >
                  Sign In
                </Link>
                <Link
                  to="/register"
                  onClick={() => setMobileMenuOpen(false)}
                  className="text-center py-2 px-3 text-xs font-bold text-white bg-teal-600 rounded-lg"
                >
                  Get Started
                </Link>
              </div>
            )}
          </div>
        </div>
      )}
    </header>
  );
}
