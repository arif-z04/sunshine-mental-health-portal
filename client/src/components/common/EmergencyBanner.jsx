import React from 'react';
import { PhoneCall, AlertTriangle } from 'lucide-react';

export default function EmergencyBanner() {
  return (
    <aside aria-label="Crisis Support" className="bg-amber-50 border-b border-amber-200 px-4 py-2 text-xs text-amber-900 transition-colors">
      <div className="max-w-7xl mx-auto flex flex-wrap items-center justify-center gap-x-4 gap-y-1.5 text-center">
        <span className="inline-flex items-center gap-1 font-extrabold bg-amber-200 text-amber-950 px-2.5 py-0.5 rounded-full text-[11px] tracking-wide uppercase shadow-xs">
          <AlertTriangle className="w-3.5 h-3.5 text-amber-800" />
          জরুরি সহায়তা (Bangladesh 24/7 Crisis)
        </span>
        <span className="leading-relaxed">
          মানসিক সংকট বা চরম উদ্বেগে তাৎক্ষণিক যোগাযোগ করুন:
          <strong className="mx-1">কান পেতে রই (Kaan Pete Roi):</strong>
          <a
            href="tel:+8801779554391"
            className="underline font-bold text-amber-950 hover:text-amber-700 transition-colors inline-flex items-center gap-0.5"
          >
            <PhoneCall className="w-3 h-3 inline" /> +8801779554391
          </a>
          <span className="mx-2 text-amber-300 hidden sm:inline">|</span>
          <strong className="mx-1">জরুরি সেবা:</strong> <span className="font-bold text-amber-950">999</span>
          <span className="mx-2 text-amber-300 hidden sm:inline">|</span>
          <strong className="mx-1">স্বাস্থ্য বাতায়ন:</strong> <span className="font-bold text-amber-950">16263</span>
          <span className="mx-2 text-amber-300 hidden sm:inline">|</span>
          <strong className="mx-1">NIMH:</strong> <span className="font-bold text-amber-950">09612-600600</span>
        </span>
      </div>
    </aside>
  );
}
