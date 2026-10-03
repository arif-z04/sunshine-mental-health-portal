import React from 'react';
import { describe, it, expect } from 'vitest';
import { render, screen } from '@testing-library/react';
import App from '../App';

describe('App Routing & Layout', () => {
  it('renders landing page with hero title and brand logo', () => {
    render(<App />);
    const brandElements = screen.getAllByText(/Sunshine/i);
    expect(brandElements.length).toBeGreaterThanOrEqual(1);
    expect(screen.getByText(/Confidential Mental Healthcare for/i)).toBeInTheDocument();
  });

  it('renders navigation links for public users', () => {
    render(<App />);
    expect(screen.getByText('Home')).toBeInTheDocument();
    expect(screen.getByText('Find Counselors')).toBeInTheDocument();
    expect(screen.getByText('Sign In')).toBeInTheDocument();
    expect(screen.getByText('Get Started')).toBeInTheDocument();
  });

  it('renders crisis hotline banner across the application', () => {
    render(<App />);
    const hotlines = screen.getAllByText(/কান পেতে রই/i);
    expect(hotlines.length).toBeGreaterThanOrEqual(1);
    expect(hotlines[0]).toBeInTheDocument();
  });
});
