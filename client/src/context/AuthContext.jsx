import React, { createContext, useContext, useState, useEffect } from 'react';
import { authApi } from '../api/apiClient';

const AuthContext = createContext(null);

export function AuthProvider({ children }) {
  const [user, setUser] = useState(() => {
    try {
      const saved = localStorage.getItem('sunshine_user');
      return saved ? JSON.parse(saved) : null;
    } catch {
      return null;
    }
  });
  const [token, setToken] = useState(() => localStorage.getItem('sunshine_token'));
  const [loading, setLoading] = useState(true);

  // Sync state if unauthorized event is fired
  useEffect(() => {
    const handleUnauthorized = () => {
      setUser(null);
      setToken(null);
    };
    window.addEventListener('sunshine_unauthorized', handleUnauthorized);
    return () => window.removeEventListener('sunshine_unauthorized', handleUnauthorized);
  }, []);

  // Verify and refresh user info on initial mount if token exists
  useEffect(() => {
    let isMounted = true;
    async function loadUser() {
      if (!token) {
        setLoading(false);
        return;
      }
      try {
        const userData = await authApi.me();
        if (isMounted) {
          setUser(userData);
          localStorage.setItem('sunshine_user', JSON.stringify(userData));
        }
      } catch (err) {
        if (isMounted) {
          // Token invalid or expired
          authApi.logout();
          setUser(null);
          setToken(null);
        }
      } finally {
        if (isMounted) setLoading(false);
      }
    }
    loadUser();
    return () => { isMounted = false; };
  }, [token]);

  const login = async (email, password) => {
    const res = await authApi.login(email, password);
    setToken(res.token);
    setUser(res.user);
    return res;
  };

  const adminLogin = async (email, password) => {
    const res = await authApi.adminLogin(email, password);
    setToken(res.token);
    setUser(res.user);
    return res;
  };

  const registerPatient = async (data) => {
    const res = await authApi.registerPatient(data);
    setToken(res.token);
    setUser(res.user);
    return res;
  };

  const registerDoctor = async (data) => {
    const res = await authApi.registerDoctor(data);
    setToken(res.token);
    setUser(res.user);
    return res;
  };

  const logout = () => {
    authApi.logout();
    setToken(null);
    setUser(null);
  };

  const refreshUser = async () => {
    try {
      const userData = await authApi.me();
      setUser(userData);
      localStorage.setItem('sunshine_user', JSON.stringify(userData));
      return userData;
    } catch {
      return null;
    }
  };

  const value = {
    user,
    token,
    loading,
    isAuthenticated: !!token && !!user,
    isPatient: user?.role === 'PATIENT',
    isDoctor: user?.role === 'DOCTOR',
    isAdmin: user?.role === 'ADMIN',
    login,
    adminLogin,
    registerPatient,
    registerDoctor,
    logout,
    refreshUser,
  };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

export function useAuth() {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error('useAuth must be used within an AuthProvider');
  }
  return context;
}
