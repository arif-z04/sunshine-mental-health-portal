import React from 'react';
import { describe, it, expect } from 'vitest';
import { render, screen } from '@testing-library/react';
import EmergencyBanner from '../components/common/EmergencyBanner';

describe('EmergencyBanner Component', () => {
  it('renders the Bangladesh 24/7 crisis badge', () => {
    render(<EmergencyBanner />);
    expect(screen.getByText(/জরুরি সহায়তা \(Bangladesh 24\/7 Crisis\)/i)).toBeInTheDocument();
  });

  it('renders Kaan Pete Roi crisis phone number', () => {
    render(<EmergencyBanner />);
    const hotline = screen.getByText('+8801779554391');
    expect(hotline).toBeInTheDocument();
    expect(hotline.closest('a')).toHaveAttribute('href', 'tel:+8801779554391');
  });

  it('renders National Emergency 999 and Shastho Batayon 16263', () => {
    render(<EmergencyBanner />);
    expect(screen.getByText('999')).toBeInTheDocument();
    expect(screen.getByText('16263')).toBeInTheDocument();
    expect(screen.getByText('09612-600600')).toBeInTheDocument();
  });
});
