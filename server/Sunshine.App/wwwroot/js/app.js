// Sunshine Mental Health Counseling System - Bangladesh Edition
const API_BASE = '/api';

const Sunshine = {
  currency: '৳',
  currencyCode: 'BDT',
  formatBDT: (amount) => `৳${Number(amount || 0).toLocaleString('en-BD')}`,
  getToken: () => localStorage.getItem('sunshine_token'),
  setAuth: (token, user) => {
    localStorage.setItem('sunshine_token', token);
    localStorage.setItem('sunshine_user', JSON.stringify(user));
  },
  getUser: () => {
    const u = localStorage.getItem('sunshine_user');
    return u ? JSON.parse(u) : null;
  },
  logout: () => {
    localStorage.removeItem('sunshine_token');
    localStorage.removeItem('sunshine_user');
    window.location.href = '/';
  },
  request: async (endpoint, options = {}) => {
    const headers = { 'Content-Type': 'application/json', ...(options.headers || {}) };
    const token = Sunshine.getToken();
    if (token) {
      headers['Authorization'] = `Bearer ${token}`;
    }

    try {
      const res = await fetch(`${API_BASE}${endpoint}`, { credentials: 'include', ...options, headers });
      const data = await res.json().catch(() => ({}));
      if (!res.ok) {
        throw new Error(data.message || `Request failed with status ${res.status}`);
      }
      return data;
    } catch (err) {
      Sunshine.toast(err.message, 'error');
      throw err;
    }
  },
  toast: (message, type = 'info') => {
    const container = document.getElementById('toast-container') || createToastContainer();
    const toast = document.createElement('div');
    const bgColors = {
      success: 'bg-emerald-600 text-white',
      error: 'bg-rose-600 text-white',
      warning: 'bg-amber-500 text-slate-900',
      info: 'bg-teal-600 text-white',
    };
    toast.className = `px-4 py-3 rounded-xl shadow-lg text-sm font-medium flex items-center justify-between gap-3 transform transition-all duration-300 translate-y-2 opacity-0 ${bgColors[type] || bgColors.info}`;
    toast.innerHTML = `
      <div class="flex items-center gap-2">
        <span>${message}</span>
      </div>
      <button class="text-xs opacity-75 hover:opacity-100 font-bold" onclick="this.parentElement.remove()">✕</button>
    `;
    container.appendChild(toast);
    setTimeout(() => {
      toast.classList.remove('translate-y-2', 'opacity-0');
    }, 10);
    setTimeout(() => {
      toast.classList.add('opacity-0', 'translate-y-2');
      setTimeout(() => toast.remove(), 300);
    }, 4000);
  }
};

function createToastContainer() {
  const div = document.createElement('div');
  div.id = 'toast-container';
  div.className = 'fixed bottom-5 right-5 z-50 flex flex-col gap-2 max-w-sm w-full';
  document.body.appendChild(div);
  return div;
}
