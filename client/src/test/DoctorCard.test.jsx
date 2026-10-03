import React from 'react';
import { describe, it, expect, vi } from 'vitest';
import { render, screen, fireEvent } from '@testing-library/react';
import DoctorCard from '../components/patient/DoctorCard';

describe('DoctorCard Component', () => {
  const mockDoctor = {
    id: 1,
    fullName: 'Dr. Tanvir Ahmed',
    specialization: 'Adult Psychiatry',
    qualification: 'MBBS, MD (Psychiatry), BMDC #A-12345',
    experienceYears: 8,
    consultationFee: 1500,
    paymentPolicy: 'ADVANCE',
    bio: 'Specialist in mood disorders and depression.'
  };

  it('renders doctor name, specialization, and BMDC badge', () => {
    render(<DoctorCard doctor={mockDoctor} onSelectDoctor={() => {}} />);
    expect(screen.getByText('Dr. Tanvir Ahmed')).toBeInTheDocument();
    expect(screen.getByText('Adult Psychiatry')).toBeInTheDocument();
    expect(screen.getByText('Verified BMDC')).toBeInTheDocument();
  });

  it('formats fee properly in Bangladeshi Taka (৳ BDT)', () => {
    render(<DoctorCard doctor={mockDoctor} onSelectDoctor={() => {}} />);
    expect(screen.getByText('৳1,500')).toBeInTheDocument();
  });

  it('triggers onSelectDoctor when Book Session is clicked', () => {
    const handleSelect = vi.fn();
    render(<DoctorCard doctor={mockDoctor} onSelectDoctor={handleSelect} />);
    const bookButton = screen.getByRole('button', { name: /Book Session/i });
    fireEvent.click(bookButton);
    expect(handleSelect).toHaveBeenCalledWith(mockDoctor);
  });
});
