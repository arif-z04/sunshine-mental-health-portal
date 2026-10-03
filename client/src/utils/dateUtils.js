// Bangladesh Time & Currency Utilities

export const DHAKA_TIMEZONE = 'Asia/Dhaka';

/**
 * Format a currency amount in Bangladeshi Taka (৳ BDT)
 */
export function formatBDT(amount) {
  if (amount === null || amount === undefined) return '৳0';
  return `৳${Number(amount).toLocaleString('en-BD')}`;
}

/**
 * Format ISO date string into Bangladesh local readable date
 */
export function formatDhakaDate(dateString) {
  if (!dateString) return 'N/A';
  const date = new Date(dateString);
  return new Intl.DateTimeFormat('en-GB', {
    timeZone: DHAKA_TIMEZONE,
    day: 'numeric',
    month: 'short',
    year: 'numeric',
  }).format(date);
}

/**
 * Format ISO date string into Bangladesh 12-hour time (e.g. "03:30 PM BST")
 */
export function formatDhakaTime(dateString) {
  if (!dateString) return 'N/A';
  const date = new Date(dateString);
  const timeStr = new Intl.DateTimeFormat('en-US', {
    timeZone: DHAKA_TIMEZONE,
    hour: 'numeric',
    minute: '2-digit',
    hour12: true,
  }).format(date);
  return `${timeStr} BST`;
}

/**
 * Format date & time together
 */
export function formatDhakaDateTime(dateString) {
  if (!dateString) return 'N/A';
  return `${formatDhakaDate(dateString)} at ${formatDhakaTime(dateString)}`;
}

/**
 * Calculate remaining hold time for an appointment (15-min advance hold policy)
 * Returns { expired: boolean, minutes: number, seconds: number, formatted: string }
 */
export function getHoldRemainingTime(createdAtIso, holdDurationMinutes = 15) {
  if (!createdAtIso) return { expired: true, minutes: 0, seconds: 0, formatted: '00:00' };

  const createdMs = new Date(createdAtIso).getTime();
  const expiresMs = createdMs + holdDurationMinutes * 60 * 1000;
  const nowMs = Date.now();
  const diffMs = expiresMs - nowMs;

  if (diffMs <= 0) {
    return { expired: true, minutes: 0, seconds: 0, formatted: '00:00' };
  }

  const totalSeconds = Math.floor(diffMs / 1000);
  const minutes = Math.floor(totalSeconds / 60);
  const seconds = totalSeconds % 60;
  const formatted = `${String(minutes).padStart(2, '0')}:${String(seconds).padStart(2, '0')}`;

  return { expired: false, minutes, seconds, formatted };
}
