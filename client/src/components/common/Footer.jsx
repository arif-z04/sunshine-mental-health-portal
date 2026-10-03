import React from 'react';
import { Link } from 'react-router-dom';
import { ShieldCheck, Heart, Phone, MapPin, Clock } from 'lucide-react';

export default function Footer() {
  return (
    <footer className="bg-slate-900 text-slate-300 pt-16 pb-12 border-t border-slate-800 mt-auto">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-10 mb-12">
          
          {/* Brand Info */}
          <div className="space-y-4">
            <div className="flex items-center gap-2.5">
              <div className="w-9 h-9 rounded-xl bg-gradient-to-tr from-teal-500 to-amber-400 flex items-center justify-center text-white text-lg font-black">
                ☀️
              </div>
              <span className="text-xl font-black tracking-tight text-white">
                Sunshine<span className="text-amber-400">.</span>BD
              </span>
            </div>
            <p className="text-xs text-slate-400 leading-relaxed">
              Sunshine Mental Health Portal provides confidential, accredited mental telehealth and psychological counseling across all 64 districts of Bangladesh.
            </p>
            <div className="flex items-center gap-2 text-xs text-teal-400 font-medium">
              <ShieldCheck className="w-4 h-4 text-teal-400 shrink-0" />
              <span>BMDC Certified & Supervised Clinicians</span>
            </div>
          </div>

          {/* Quick Navigation */}
          <div>
            <h4 className="text-xs font-bold uppercase tracking-wider text-slate-200 mb-4">
              Telehealth Portals
            </h4>
            <ul className="space-y-2.5 text-xs text-slate-400">
              <li>
                <Link to="/patient" className="hover:text-teal-400 transition">
                  Patient Care Portal & Booking
                </Link>
              </li>
              <li>
                <Link to="/doctor" className="hover:text-teal-400 transition">
                  Counselor & Doctor Practice Portal
                </Link>
              </li>
              <li>
                <Link to="/admin/login" className="hover:text-amber-400 transition">
                  Administrator Console (Isolated Login)
                </Link>
              </li>
              <li>
                <a href="/#cbt-library" className="hover:text-teal-400 transition">
                  CBT Digital Psychoeducation Vault
                </a>
              </li>
            </ul>
          </div>

          {/* Bangladesh Emergency Hotlines */}
          <div>
            <h4 className="text-xs font-bold uppercase tracking-wider text-amber-400 mb-4 flex items-center gap-1.5">
              <Phone className="w-3.5 h-3.5" />
              Bangladesh 24/7 Crisis Helplines
            </h4>
            <ul className="space-y-2 text-xs text-slate-300">
              <li className="bg-slate-800/80 p-2.5 rounded-lg border border-slate-700/60">
                <span className="font-bold text-white block">কান পেতে রই (Kaan Pete Roi)</span>
                <span className="text-[11px] text-slate-400">Emotional support & suicide prevention: </span>
                <a href="tel:+8801779554391" className="text-teal-400 font-bold hover:underline">
                  +8801779554391
                </a>
              </li>
              <li className="flex justify-between py-1 border-b border-slate-800">
                <span>National Emergency:</span>
                <strong className="text-amber-350 text-white">999</strong>
              </li>
              <li className="flex justify-between py-1 border-b border-slate-800">
                <span>Shastho Batayon:</span>
                <strong className="text-white">16263</strong>
              </li>
              <li className="flex justify-between py-1">
                <span>NIMH Mental Hotline:</span>
                <strong className="text-white">09612-600600</strong>
              </li>
            </ul>
          </div>

          {/* Platform Standards */}
          <div>
            <h4 className="text-xs font-bold uppercase tracking-wider text-slate-200 mb-4">
              Bangladesh Telehealth Standards
            </h4>
            <div className="space-y-3 text-xs text-slate-400">
              <div className="flex items-start gap-2">
                <Clock className="w-4 h-4 text-slate-400 mt-0.5 shrink-0" />
                <span>Operating in <strong>Asia/Dhaka (BST, UTC+06:00)</strong> standard schedule.</span>
              </div>
              <div className="flex items-start gap-2">
                <span className="font-bold text-teal-400 shrink-0">৳ BDT:</span>
                <span>All consultation fees and MFS transactions processed securely in Bangladeshi Taka.</span>
              </div>
              <p className="text-[11px] text-slate-500 pt-1">
                Notice: Sunshine Telehealth is not a substitute for in-person emergency hospital care in acute life-threatening situations.
              </p>
            </div>
          </div>

        </div>

        {/* Bottom Bar */}
        <div className="pt-8 border-t border-slate-800/80 flex flex-col sm:flex-row items-center justify-between gap-4 text-xs text-slate-400">
          <p>© {new Date().getFullYear()} Sunshine Mental Health Portal Bangladesh. All rights reserved.</p>
          <div className="flex items-center gap-4 text-xs">
            <span className="flex items-center gap-1 text-slate-400">
              Made with <Heart className="w-3.5 h-3.5 text-red-500 inline fill-red-500" /> for mental wellbeing in Bangladesh
            </span>
          </div>
        </div>
      </div>
    </footer>
  );
}
