import React, { useState, useEffect } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { ShieldCheck, HeartHandshake, PhoneCall, Calendar, Video, Clock, CheckCircle2, ChevronRight, HelpCircle } from 'lucide-react';
import { patientApi } from '../../api/apiClient';
import DoctorCard from '../../components/patient/DoctorCard';
import BookingModal from '../../components/patient/BookingModal';
import BkashPaymentModal from '../../components/patient/BkashPaymentModal';

export default function LandingPage() {
  const [doctors, setDoctors] = useState([]);
  const [loading, setLoading] = useState(true);
  const [selectedDoctor, setSelectedDoctor] = useState(null);
  const [activePaymentAppointment, setActivePaymentAppointment] = useState(null);
  const [filterSpecialization, setFilterSpecialization] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    let isMounted = true;
    async function loadDoctors() {
      try {
        const data = await patientApi.getDoctors({ availableOnly: true });
        if (isMounted) setDoctors(data || []);
      } catch (err) {
        console.error('Failed to load doctors', err);
      } finally {
        if (isMounted) setLoading(false);
      }
    }
    loadDoctors();
    return () => { isMounted = false; };
  }, []);

  const specializations = [
    'All Specializations',
    'Clinical Psychology',
    'CBT Specialist',
    'Depression & Mood',
    'Anxiety & Trauma',
    'Child & Adolescent',
    'Marriage & Family'
  ];

  const filteredDoctors = doctors.filter((doc) => {
    if (!filterSpecialization || filterSpecialization === 'All Specializations') return true;
    return doc.specialization?.toLowerCase().includes(filterSpecialization.toLowerCase());
  });

  const handleBookingComplete = (appointment) => {
    setSelectedDoctor(null);
    if (appointment.status === 'HELD' || appointment.doctorPaymentPolicy !== 'POST_PAYMENT') {
      setActivePaymentAppointment(appointment);
    } else {
      navigate('/patient?tab=appointments');
    }
  };

  const handlePaymentSuccess = () => {
    setActivePaymentAppointment(null);
    navigate('/patient?tab=appointments');
  };

  return (
    <div className="space-y-20 pb-20">
      
      {/* Hero Section */}
      <section className="relative overflow-hidden bg-gradient-to-b from-teal-50/60 via-white to-slate-50 pt-16 pb-20 border-b border-slate-100">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="grid grid-cols-1 lg:grid-cols-12 gap-12 items-center">
            
            <div className="lg:col-span-7 space-y-6 text-left">
              <div className="inline-flex items-center gap-2 px-3 py-1 rounded-full bg-teal-100/80 text-teal-800 text-xs font-bold">
                <span className="w-2 h-2 rounded-full bg-teal-600 animate-pulse"></span>
                First Accredited Mental Telehealth Network in Bangladesh
              </div>

              <h1 className="text-4xl sm:text-5xl lg:text-6xl font-black text-slate-900 tracking-tight leading-[1.15]">
                Confidential Mental Healthcare for <span className="text-teal-600 underline decoration-amber-400 decoration-wavy decoration-2">Every Mind</span> in Bangladesh.
              </h1>

              <p className="text-base sm:text-lg text-slate-600 leading-relaxed max-w-2xl">
                Connect with verified BMDC-certified psychiatrists and licensed clinical psychologists across Bangladesh from the safety of your home. Secure sessions, local MFS (bKash/Nagad), and complete medical confidentiality.
              </p>

              {/* Action Buttons */}
              <div className="flex flex-wrap items-center gap-4 pt-2">
                <Link
                  to="/patient"
                  className="px-6 py-3.5 rounded-2xl bg-teal-600 hover:bg-teal-700 text-white font-extrabold text-sm shadow-lg shadow-teal-600/25 active:scale-95 transition-all inline-flex items-center gap-2"
                >
                  <Calendar className="w-4 h-4" />
                  Find a Counselor Now
                </Link>

                <a
                  href="tel:+8801779554391"
                  className="px-6 py-3.5 rounded-2xl bg-amber-50 hover:bg-amber-100 text-amber-900 border border-amber-300 font-extrabold text-sm active:scale-95 transition-all inline-flex items-center gap-2"
                >
                  <PhoneCall className="w-4 h-4 text-amber-700" />
                  Crisis Support (কান পেতে রই)
                </a>
              </div>

              {/* Trust Indicators */}
              <div className="grid grid-cols-3 gap-4 pt-6 border-t border-slate-200/60 max-w-lg">
                <div>
                  <div className="text-2xl font-black text-slate-900">100%</div>
                  <div className="text-xs text-slate-500 font-medium">Confidential & Encrypted</div>
                </div>
                <div>
                  <div className="text-2xl font-black text-slate-900">৳ BDT</div>
                  <div className="text-xs text-slate-500 font-medium">bKash & Nagad MFS</div>
                </div>
                <div>
                  <div className="text-2xl font-black text-slate-900">64</div>
                  <div className="text-xs text-slate-500 font-medium">Districts Covered</div>
                </div>
              </div>
            </div>

            {/* Hero Card Visual */}
            <div className="lg:col-span-5 relative">
              <div className="bg-white rounded-3xl p-6 shadow-xl border border-slate-100 space-y-4">
                <div className="flex items-center justify-between pb-3 border-b border-slate-100">
                  <div className="flex items-center gap-2">
                    <span className="w-3 h-3 rounded-full bg-emerald-500"></span>
                    <span className="text-xs font-bold text-slate-700">Telehealth Queue Live</span>
                  </div>
                  <span className="text-[11px] font-bold text-teal-700 bg-teal-50 px-2 py-0.5 rounded-full">
                    Asia/Dhaka (BST)
                  </span>
                </div>

                <div className="bg-slate-50 rounded-2xl p-4 space-y-3 border border-slate-100">
                  <div className="flex items-center gap-3">
                    <div className="w-12 h-12 rounded-xl bg-teal-600 text-white font-black flex items-center justify-center text-lg">
                      ☀️
                    </div>
                    <div>
                      <h4 className="text-sm font-bold text-slate-900">Confidential Telehealth Session</h4>
                      <p className="text-xs text-slate-500">End-to-End Encrypted WebRTC</p>
                    </div>
                  </div>
                  <div className="flex items-center justify-between text-xs pt-2 border-t border-slate-200/60 text-slate-600">
                    <span className="flex items-center gap-1"><Clock className="w-3.5 h-3.5 text-teal-600" /> 45 Mins</span>
                    <span className="flex items-center gap-1"><Video className="w-3.5 h-3.5 text-teal-600" /> HD Video</span>
                    <span className="flex items-center gap-1"><ShieldCheck className="w-3.5 h-3.5 text-teal-600" /> HIPAA & BMDC</span>
                  </div>
                </div>

                <div className="p-4 rounded-2xl bg-amber-50 border border-amber-200 space-y-1">
                  <div className="text-xs font-bold text-amber-950 flex items-center gap-1.5">
                    <Clock className="w-3.5 h-3.5 text-amber-700" />
                    15-Minute Advance Hold Protection
                  </div>
                  <p className="text-[11px] text-amber-900 leading-relaxed">
                    Zero double-booking guarantee. When you reserve a slot, our database holds it exclusively for 15 minutes while you confirm with bKash or Nagad.
                  </p>
                </div>
              </div>
            </div>

          </div>
        </div>
      </section>

      {/* Specialist Directory Section */}
      <section id="therapists" className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="flex flex-col md:flex-row md:items-end justify-between gap-4 mb-8">
          <div>
            <span className="text-xs font-black uppercase tracking-wider text-teal-600 block mb-1">
              Verified Practitioners
            </span>
            <h2 className="text-3xl font-black text-slate-900 tracking-tight">
              Featured Counselors & Psychiatrists
            </h2>
            <p className="text-sm text-slate-600 mt-1">
              Certified clinicians available for instant appointment booking and telehealth consultation.
            </p>
          </div>

          <Link
            to="/patient"
            className="text-xs font-bold text-teal-700 hover:text-teal-800 inline-flex items-center gap-1 shrink-0"
          >
            View All Counselors <ChevronRight className="w-4 h-4" />
          </Link>
        </div>

        {/* Specialization Filter Pills */}
        <div className="flex items-center gap-2 overflow-x-auto pb-4 mb-6">
          {specializations.map((spec) => (
            <button
              key={spec}
              onClick={() => setFilterSpecialization(spec)}
              className={`px-3.5 py-1.5 rounded-full text-xs font-bold whitespace-nowrap transition-all ${
                (filterSpecialization === spec || (!filterSpecialization && spec === 'All Specializations'))
                  ? 'bg-teal-600 text-white shadow-xs'
                  : 'bg-white border border-slate-200 text-slate-600 hover:border-slate-300'
              }`}
            >
              {spec}
            </button>
          ))}
        </div>

        {/* Doctors Grid */}
        {loading ? (
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
            {[1, 2, 3].map((i) => (
              <div key={i} className="h-64 bg-slate-100 rounded-3xl animate-pulse"></div>
            ))}
          </div>
        ) : filteredDoctors.length === 0 ? (
          <div className="text-center py-12 bg-white rounded-3xl border border-dashed border-slate-200 p-8">
            <p className="text-sm font-semibold text-slate-500">No counselors found for the selected specialization.</p>
          </div>
        ) : (
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
            {filteredDoctors.slice(0, 6).map((doctor) => (
              <DoctorCard
                key={doctor.id}
                doctor={doctor}
                onSelectDoctor={(doc) => setSelectedDoctor(doc)}
              />
            ))}
          </div>
        )}
      </section>

      {/* How It Works Section */}
      <section id="how-it-works" className="bg-slate-100/70 py-16 border-y border-slate-200/60">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="text-center max-w-2xl mx-auto mb-12">
            <span className="text-xs font-black uppercase tracking-wider text-teal-600 block mb-1">
              Seamless Telehealth Process
            </span>
            <h2 className="text-3xl font-black text-slate-900 tracking-tight">
              How Sunshine Telehealth Works
            </h2>
            <p className="text-sm text-slate-600 mt-2">
              Three simple, confidential steps to access compassionate mental health support from anywhere in Bangladesh.
            </p>
          </div>

          <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
            {[
              {
                step: '01',
                title: 'Select Counselor & Time',
                desc: 'Browse BMDC-verified specialists, filter by clinical expertise, and pick an available 45-minute slot in Asia/Dhaka time.',
                icon: Calendar,
              },
              {
                step: '02',
                title: 'Advance Hold & Local MFS',
                desc: 'Your slot is held exclusively for 15 minutes. Complete payment seamlessly via bKash, Nagad, Rocket, or local cards.',
                icon: Clock,
              },
              {
                step: '03',
                title: 'Join Encrypted Session',
                desc: 'Receive instant confirmation and join your encrypted, private video or audio consultation directly from your browser.',
                icon: Video,
              },
            ].map((card) => (
              <div key={card.step} className="bg-white rounded-3xl p-8 border border-slate-200/70 shadow-xs relative">
                <div className="text-3xl font-black text-teal-100 absolute right-6 top-6">
                  {card.step}
                </div>
                <div className="w-12 h-12 rounded-2xl bg-teal-50 text-teal-700 flex items-center justify-center mb-6">
                  <card.icon className="w-6 h-6" />
                </div>
                <h3 className="text-lg font-bold text-slate-900 mb-2">{card.title}</h3>
                <p className="text-xs text-slate-600 leading-relaxed">{card.desc}</p>
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* CBT Vault Callout */}
      <section id="cbt-library" className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="bg-gradient-to-r from-teal-800 to-teal-950 rounded-3xl p-8 sm:p-12 text-white relative overflow-hidden shadow-xl">
          <div className="relative z-10 max-w-2xl space-y-4">
            <span className="px-3 py-1 rounded-full bg-teal-700 text-teal-200 text-xs font-bold uppercase tracking-wider inline-block">
              Psychoeducation & Digital Self-Care
            </span>
            <h2 className="text-3xl sm:text-4xl font-black tracking-tight leading-tight">
              Sunshine CBT & Guided Mindfulness Vault
            </h2>
            <p className="text-sm text-teal-100 leading-relaxed">
              Explore evidence-based Cognitive Behavioral Therapy exercises, audio-guided relaxation in Bengali and English, worksheets, and clinical guides curated for anxiety, sleep, and emotional regulation.
            </p>
            <div className="pt-2">
              <Link
                to="/patient?tab=resources"
                className="px-6 py-3 rounded-2xl bg-amber-500 hover:bg-amber-600 text-slate-950 font-black text-xs shadow-md transition-all inline-flex items-center gap-2"
              >
                Access CBT Resource Library
                <ChevronRight className="w-4 h-4" />
              </Link>
            </div>
          </div>
        </div>
      </section>

      {/* FAQ Section */}
      <section id="faq" className="max-w-4xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="text-center mb-10">
          <h2 className="text-2xl sm:text-3xl font-black text-slate-900 tracking-tight">
            Frequently Asked Questions
          </h2>
          <p className="text-xs text-slate-500 mt-1">Everything you need to know about our mental telehealth platform in Bangladesh.</p>
        </div>

        <div className="space-y-4">
          {[
            {
              q: 'How does the 15-minute advance hold work?',
              a: 'When you select an appointment slot with an advance-payment counselor, our system locks that time for exactly 15 minutes. During this period, no other patient can book it while you finalize payment through bKash, Nagad, or Rocket.'
            },
            {
              q: 'Is my mental health information confidential?',
              a: 'Yes. All consultations and clinical records comply strictly with medical confidentiality standards and BMDC ethics. Only you and your treating counselor have access to your clinical notes.'
            },
            {
              q: 'What if I need to cancel or reschedule?',
              a: 'You can reschedule your appointment to another available slot directly from your Patient Care Portal at any time before the session begins.'
            },
            {
              q: 'What should I do in an emergency crisis?',
              a: 'Sunshine is an outpatient telehealth platform. If you or someone you know is in immediate danger or experiencing severe crisis, please immediately call Kaan Pete Roi (+8801779554391), National Emergency (999), or visit the nearest hospital emergency room.'
            }
          ].map((faq, idx) => (
            <div key={idx} className="bg-white rounded-2xl p-6 border border-slate-200/80 shadow-xs">
              <h4 className="text-sm font-bold text-slate-900 mb-2 flex items-center gap-2">
                <HelpCircle className="w-4 h-4 text-teal-600 shrink-0" />
                {faq.q}
              </h4>
              <p className="text-xs text-slate-600 leading-relaxed pl-6">
                {faq.a}
              </p>
            </div>
          ))}
        </div>
      </section>

      {/* Modals */}
      {selectedDoctor && (
        <BookingModal
          doctor={selectedDoctor}
          onClose={() => setSelectedDoctor(null)}
          onBookingComplete={handleBookingComplete}
        />
      )}

      {activePaymentAppointment && (
        <BkashPaymentModal
          appointment={activePaymentAppointment}
          onClose={() => setActivePaymentAppointment(null)}
          onSuccess={handlePaymentSuccess}
        />
      )}

    </div>
  );
}
