import React from 'react';
import { describe, it, expect, vi } from 'vitest';
import { render, screen, fireEvent } from '@testing-library/react';
import BkashPaymentModal from '../components/patient/BkashPaymentModal';

describe('BkashPaymentModal Component', () => {
  const mockAppointment = {
    id: 10,
    doctorName: 'Dr. Tanvir Ahmed',
    fee: 1500,
    createdAt: new Date().toISOString()
  };

  it('renders MFS Checkout header and BDT amount', () => {
    render(<BkashPaymentModal appointment={mockAppointment} onClose={() => {}} onSuccess={() => {}} />);
    expect(screen.getByText(/MFS Checkout Bangladesh/i)).toBeInTheDocument();
    expect(screen.getByText('৳1,500')).toBeInTheDocument();
  });

  it('renders payment method options for bKash, Nagad, and Rocket', () => {
    render(<BkashPaymentModal appointment={mockAppointment} onClose={() => {}} onSuccess={() => {}} />);
    expect(screen.getByRole('button', { name: 'bKash' })).toBeInTheDocument();
    expect(screen.getByRole('button', { name: 'Nagad' })).toBeInTheDocument();
    expect(screen.getByRole('button', { name: 'Rocket' })).toBeInTheDocument();
  });

  it('displays 15-minute advance hold countdown timer', () => {
    render(<BkashPaymentModal appointment={mockAppointment} onClose={() => {}} onSuccess={() => {}} />);
    expect(screen.getByText(/15-Min Advance Hold Policy/i)).toBeInTheDocument();
  });

  it('allows switching to Nagad payment method', () => {
    render(<BkashPaymentModal appointment={mockAppointment} onClose={() => {}} onSuccess={() => {}} />);
    const nagadBtn = screen.getByRole('button', { name: 'Nagad' });
    fireEvent.click(nagadBtn);
    expect(screen.getByText(/Nagad Account Number/i)).toBeInTheDocument();
  });
});
