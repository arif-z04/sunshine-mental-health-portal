import React from 'react';
import { ShieldCheck, Clock, Award, Star, Calendar } from 'lucide-react';
import { formatBDT } from '../../utils/dateUtils';

export default function DoctorCard({ doctor, onSelectDoctor }) {
  const isPostPayment = doctor.paymentPolicy === 'POST_PAYMENT';

  return (
    <div className="bg-white rounded-3xl p-6 border border-slate-100 shadow-sm hover:shadow-md transition-all flex flex-col justify-between group">
      <div>
        {/* Top Badges */}
        <div className="flex items-center justify-between gap-2 mb-4">
          <span className="inline-flex items-center gap-1 text-[11px] font-extrabold uppercase px-2.5 py-0.5 rounded-full bg-teal-50 text-teal-800 border border-teal-100">
            <ShieldCheck className="w-3.5 h-3.5 text-teal-600" />
            Verified BMDC
          </span>

          <span className={`text-[10px] font-extrabold uppercase px-2.5 py-0.5 rounded-full border ${
            isPostPayment
              ? 'bg-emerald-50 text-emerald-800 border-emerald-200'
              : 'bg-amber-50 text-amber-800 border-amber-200'
          }`}>
            {isPostPayment ? 'Post-Payment' : 'Advance Hold'}
          </span>
        </div>

        {/* Doctor Header */}
        <div className="flex items-start gap-4 mb-4">
          <div className="w-14 h-14 rounded-2xl bg-gradient-to-tr from-teal-500 to-teal-700 text-white font-black text-xl flex items-center justify-center shadow-md shadow-teal-700/20 shrink-0">
            {doctor.fullName?.charAt(3) || doctor.fullName?.charAt(0) || 'D'}
          </div>
          <div>
            <h3 className="font-extrabold text-base text-slate-900 group-hover:text-teal-700 transition-colors">
              {doctor.fullName}
            </h3>
            <p className="text-xs font-bold text-teal-600 mb-1">
              {doctor.specialization}
            </p>
            <p className="text-[11px] text-slate-500 line-clamp-1">
              {doctor.qualification || 'MBBS, FCPS / MD (Psychiatry)'}
            </p>
          </div>
        </div>

        {/* Doctor Stats */}
        <div className="grid grid-cols-2 gap-2 py-3 border-y border-slate-100 text-xs mb-4">
          <div className="flex items-center gap-1.5 text-slate-600">
            <Award className="w-4 h-4 text-amber-500" />
            <span><strong>{doctor.experienceYears || 5}+</strong> yrs exp</span>
          </div>
          <div className="flex items-center gap-1.5 text-slate-600 justify-end">
            <Star className="w-4 h-4 text-amber-400 fill-amber-400" />
            <span><strong>4.9</strong> (120+ reviews)</span>
          </div>
        </div>

        {/* Bio preview */}
        <p className="text-xs text-slate-600 line-clamp-2 mb-4 leading-relaxed">
          {doctor.bio || 'Compassionate and evidence-based mental healthcare with specialized focus on CBT, anxiety, and depression management.'}
        </p>
      </div>

      {/* Pricing & Booking Action */}
      <div className="pt-4 border-t border-slate-100 flex items-center justify-between">
        <div>
          <span className="text-[10px] font-bold text-slate-400 uppercase tracking-wider block">
            Consultation Fee
          </span>
          <span className="text-lg font-black text-slate-900">
            {formatBDT(doctor.consultationFee)}
          </span>
        </div>

        <button
          onClick={() => onSelectDoctor(doctor)}
          className="inline-flex items-center gap-1.5 px-4 py-2.5 rounded-xl bg-teal-600 hover:bg-teal-700 text-white font-extrabold text-xs shadow-sm shadow-teal-600/20 active:scale-95 transition"
        >
          <Calendar className="w-3.5 h-3.5" />
          Book Session
        </button>
      </div>
    </div>
  );
}
