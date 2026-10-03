const API_BASE = import.meta.env.VITE_API_BASE || (typeof window !== 'undefined' && window.location?.port === '5173' ? '' : 'http://localhost:5000') + '/api';

export class ApiError extends Error {
  constructor(message, status, data) {
    super(message);
    this.name = 'ApiError';
    this.status = status;
    this.data = data;
  }
}

async function request(endpoint, options = {}) {
  const token = localStorage.getItem('sunshine_token');
  const headers = {
    'Content-Type': 'application/json',
    ...(token ? { 'Authorization': `Bearer ${token}` } : {}),
    ...options.headers,
  };

  const config = {
    ...options,
    headers,
  };

  const url = `${API_BASE}${endpoint.startsWith('/') ? endpoint : `/${endpoint}`}`;

  try {
    const response = await fetch(url, config);

    // Auto-logout on 401 Unauthorized
    if (response.status === 401) {
      localStorage.removeItem('sunshine_token');
      localStorage.removeItem('sunshine_user');
      window.dispatchEvent(new CustomEvent('sunshine_unauthorized'));
    }

    const contentType = response.headers.get('content-type');
    let data = null;
    if (contentType && contentType.includes('application/json')) {
      data = await response.json();
    } else {
      data = await response.text();
    }

    if (!response.ok) {
      const errorMessage = (data && data.message) || (data && data.error) || (typeof data === 'string' && data) || `Request failed with HTTP status ${response.status}`;
      throw new ApiError(errorMessage, response.status, data);
    }

    return data;
  } catch (err) {
    if (err instanceof ApiError) throw err;
    throw new ApiError(err.message || 'Network error occurred. Please verify backend is running on port 5000.', 0, null);
  }
}

// 1. Authentication
export const authApi = {
  login: async (email, password) => {
    const res = await request('/auth/login', {
      method: 'POST',
      body: JSON.stringify({ email, password }),
    });
    if (res.token) {
      localStorage.setItem('sunshine_token', res.token);
      localStorage.setItem('sunshine_user', JSON.stringify(res.user));
    }
    return res;
  },
  adminLogin: async (email, password) => {
    const res = await request('/admin/login', {
      method: 'POST',
      body: JSON.stringify({ email, password }),
    });
    if (res.token) {
      localStorage.setItem('sunshine_token', res.token);
      localStorage.setItem('sunshine_user', JSON.stringify(res.user));
    }
    return res;
  },
  registerPatient: async (data) => {
    const res = await request('/auth/register/patient', {
      method: 'POST',
      body: JSON.stringify(data),
    });
    if (res.token) {
      localStorage.setItem('sunshine_token', res.token);
      localStorage.setItem('sunshine_user', JSON.stringify(res.user));
    }
    return res;
  },
  registerDoctor: async (data) => {
    const res = await request('/auth/register/doctor', {
      method: 'POST',
      body: JSON.stringify(data),
    });
    if (res.token) {
      localStorage.setItem('sunshine_token', res.token);
      localStorage.setItem('sunshine_user', JSON.stringify(res.user));
    }
    return res;
  },
  me: async () => {
    return request('/auth/me');
  },
  logout: () => {
    localStorage.removeItem('sunshine_token');
    localStorage.removeItem('sunshine_user');
  }
};

// 2. Patient Services
export const patientApi = {
  getDoctors: async (params = {}) => {
    const q = new URLSearchParams();
    if (params.specialization) q.append('specialization', params.specialization);
    if (params.paymentPolicy) q.append('paymentPolicy', params.paymentPolicy);
    if (params.availableOnly !== undefined) q.append('availableOnly', params.availableOnly);
    const qs = q.toString();
    return request(`/patient/doctors${qs ? `?${qs}` : ''}`);
  },
  getDoctorById: async (id) => {
    return request(`/patient/doctors/${id}`);
  },
  getDoctorSlots: async (doctorId, date) => {
    return request(`/patient/doctors/${doctorId}/slots?date=${date}`);
  },
  bookAppointment: async (bookingData) => {
    return request('/patient/appointments/book', {
      method: 'POST',
      body: JSON.stringify(bookingData),
    });
  },
  getMyAppointments: async () => {
    return request('/patient/appointments');
  },
  getAppointmentById: async (id) => {
    return request(`/patient/appointments/${id}`);
  },
  rescheduleAppointment: async (id, data) => {
    return request(`/patient/appointments/${id}/reschedule`, {
      method: 'POST',
      body: JSON.stringify(data),
    });
  },
  cancelAppointment: async (id) => {
    return request(`/patient/appointments/${id}/cancel`, {
      method: 'POST',
    });
  },
  getProfile: async () => {
    return request('/patient/profile');
  },
  updateProfile: async (data) => {
    return request('/patient/profile', {
      method: 'PUT',
      body: JSON.stringify(data),
    });
  },
  getSubscriptions: async () => {
    return request('/patient/subscriptions');
  },
  processPayment: async (data) => {
    return request('/patient/payments/process', {
      method: 'POST',
      body: JSON.stringify(data),
    });
  },
  getCategories: async () => {
    return request('/patient/resources/categories');
  },
  getResources: async (categoryId = null, search = '') => {
    const q = new URLSearchParams();
    if (categoryId) q.append('categoryId', categoryId);
    if (search) q.append('search', search);
    const qs = q.toString();
    return request(`/patient/resources${qs ? `?${qs}` : ''}`);
  },
  accessResource: async (id) => {
    return request(`/patient/resources/${id}/access`, {
      method: 'POST',
    });
  },
  getNotifications: async () => {
    return request('/patient/notifications');
  },
  markNotificationRead: async (id) => {
    return request(`/patient/notifications/${id}/read`, {
      method: 'PATCH',
    });
  }
};

// 3. Doctor Services
export const doctorApi = {
  getProfile: async () => {
    return request('/doctor/profile');
  },
  updateProfile: async (data) => {
    return request('/doctor/profile', {
      method: 'PUT',
      body: JSON.stringify(data),
    });
  },
  updatePaymentPolicy: async (paymentPolicy) => {
    return request('/doctor/policy', {
      method: 'PATCH',
      body: JSON.stringify({ paymentPolicy }),
    });
  },
  getSchedules: async () => {
    return request('/doctor/schedules');
  },
  saveSchedules: async (schedules) => {
    return request('/doctor/schedules', {
      method: 'POST',
      body: JSON.stringify(schedules),
    });
  },
  getAppointments: async () => {
    return request('/doctor/appointments');
  },
  updateAppointmentStatus: async (id, status, notes = '') => {
    return request(`/doctor/appointments/${id}/status`, {
      method: 'PATCH',
      body: JSON.stringify({ status, notes }),
    });
  }
};

// 4. Admin Services
export const adminApi = {
  getMetrics: async () => {
    return request('/admin/metrics');
  },
  getUsers: async (role = '', search = '') => {
    const q = new URLSearchParams();
    if (role) q.append('role', role);
    if (search) q.append('search', search);
    const qs = q.toString();
    return request(`/admin/users${qs ? `?${qs}` : ''}`);
  },
  updateUserStatus: async (id, isActive) => {
    return request(`/admin/users/${id}/status`, {
      method: 'PATCH',
      body: JSON.stringify({ isActive }),
    });
  },
  getDoctors: async () => {
    return request('/admin/doctors');
  },
  verifyDoctor: async (id, isAvailable) => {
    return request(`/admin/doctors/${id}/verify`, {
      method: 'PATCH',
      body: JSON.stringify({ isAvailable }),
    });
  },
  getAppointments: async () => {
    return request('/admin/appointments');
  },
  updateAppointmentStatus: async (id, status, notes = '') => {
    return request(`/admin/appointments/${id}/status`, {
      method: 'PATCH',
      body: JSON.stringify({ status, notes }),
    });
  },
  getPlans: async () => {
    return request('/admin/plans');
  },
  createPlan: async (plan) => {
    return request('/admin/plans', {
      method: 'POST',
      body: JSON.stringify(plan),
    });
  },
  updatePlan: async (id, plan) => {
    return request(`/admin/plans/${id}`, {
      method: 'PUT',
      body: JSON.stringify(plan),
    });
  },
  getResources: async () => {
    return request('/admin/resources');
  },
  createResource: async (resource) => {
    return request('/admin/resources', {
      method: 'POST',
      body: JSON.stringify(resource),
    });
  },
  updateResource: async (id, resource) => {
    return request(`/admin/resources/${id}`, {
      method: 'PUT',
      body: JSON.stringify(resource),
    });
  },
  deleteResource: async (id) => {
    return request(`/admin/resources/${id}`, {
      method: 'DELETE',
    });
  },
  getPayments: async () => {
    return request('/admin/payments');
  },
  refundPayment: async (id) => {
    return request(`/admin/payments/${id}/refund`, {
      method: 'POST',
    });
  },
  getAuditLogs: async (limit = 100) => {
    return request(`/admin/audit-logs?limit=${limit}`);
  }
};
