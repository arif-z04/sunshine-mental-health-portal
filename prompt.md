# Sunshine Mental Health Portal

## Production-Ready Mental Health Counseling & Appointment Platform — Master Development Prompt

You are a senior full-stack software engineer, software architect, UI/UX designer, QA engineer, security engineer, and DevOps engineer.

Your task is to transform the existing **Sunshine Mental Health Portal** project into a **complete, production-ready Bangladesh-based mental health counseling platform**.

The platform allows patients/users to discover mental health professionals, view doctor profiles, book counseling appointments, manage appointments, and communicate with the platform. It also includes a comprehensive **Admin Dashboard** for managing doctors, users, appointments, content, and platform operations.

Do NOT treat this as a simple academic CRUD project.

Treat it as a real-world healthcare/counseling SaaS product that could eventually be deployed publicly.

---

# 1. PRIMARY OBJECTIVE

Build and polish:

> **Sunshine Mental Health Portal**

A modern Bangladesh-focused mental health counseling platform where users can:

* Create an account
* Log in securely
* Browse mental health professionals
* Search/filter doctors
* View detailed doctor profiles
* View available consultation schedules
* Book appointments
* Manage appointments
* Cancel/reschedule appointments where appropriate
* View appointment history
* Manage their profile
* Receive notifications
* Access relevant mental-health resources

Administrators should have a powerful dashboard to:

* Manage users
* Manage doctors/counselors
* Approve/reject doctors
* Manage appointments
* Manage schedules
* Manage content/resources
* View platform analytics
* Manage reports/issues
* Manage system settings
* Monitor platform activity

---

# 2. FIRST: AUDIT THE EXISTING PROJECT

Before changing code:

1. Thoroughly inspect the entire repository.
2. Understand the existing architecture.
3. Identify:

   * frontend architecture
   * backend architecture
   * database architecture
   * authentication system
   * authorization/RBAC
   * API structure
   * components
   * pages
   * services
   * database models
   * existing tests
   * configuration
   * environment variables
   * deployment configuration
4. Read all existing documentation.
5. Identify incomplete features.
6. Identify bugs.
7. Identify duplicated code.
8. Identify security vulnerabilities.
9. Identify poor UX patterns.
10. Identify performance bottlenecks.
11. Identify accessibility problems.
12. Identify mobile responsiveness problems.

Do NOT blindly rewrite the project.

Preserve good existing architecture where possible.

If something needs to be redesigned, explain the reason internally through code/documentation and implement the better architecture.

---

# 3. DEVELOPMENT PRINCIPLES

Follow these principles throughout the project:

### Code Quality

* Clean architecture
* SOLID principles
* DRY
* KISS
* Separation of concerns
* Strong typing where supported
* Reusable components
* Reusable services
* Meaningful naming
* Small maintainable functions
* Avoid unnecessary abstraction
* Avoid duplicated business logic
* Avoid magic numbers/strings

### Production Quality

Everything should be written as if another professional developer will maintain this project for years.

Do not leave:

* TODO placeholders
* fake functionality
* broken buttons
* empty pages
* console errors
* mock data where real data should exist
* unfinished components
* dead routes
* unused imports
* unused dependencies
* debugging logs
* temporary hacks

---

# 4. DESIGN DIRECTION

Create a **modern, trustworthy, calming healthcare UI**.

The visual identity should communicate:

* safety
* professionalism
* empathy
* privacy
* trust
* calmness
* accessibility

## Primary design direction

Use a:

> **Modern White Material UI + Soft Wellness Accent**

The interface should be predominantly white/light.

Use subtle accent colors such as:

* soft green
* teal
* calming blue
* very subtle warm accent colors where appropriate

Avoid:

* overly saturated colors
* excessive gradients
* glassmorphism everywhere
* excessive shadows
* visually noisy layouts
* generic dashboard templates
* childish mental-health illustrations
* excessive animations

Use whitespace intelligently.

---

# 5. DESIGN SYSTEM

Create a consistent design system.

Define:

### Typography

Use a modern highly readable font.

Recommended hierarchy:

* Display
* H1
* H2
* H3
* H4
* Body
* Body Small
* Caption
* Button
* Label

Ensure excellent readability on mobile.

### Colors

Create semantic design tokens:

```text
primary
secondary
success
warning
error
info
background
surface
surfaceVariant
textPrimary
textSecondary
border
divider
```

Do not hardcode colors throughout components.

### Spacing

Use a consistent spacing scale.

### Border Radius

Use modern but restrained rounded corners.

### Shadows

Use subtle elevation.

### Components

Create reusable:

* Button
* IconButton
* Input
* Select
* DatePicker
* TimePicker
* Modal
* Drawer
* Card
* DoctorCard
* AppointmentCard
* StatusBadge
* Avatar
* EmptyState
* LoadingState
* ErrorState
* Skeleton
* Toast/Snackbar
* ConfirmationDialog
* Pagination
* DataTable
* Breadcrumb
* Tabs
* Tooltip

---

# 6. LANDING / HERO PAGE

Create a premium landing page.

The hero section should immediately communicate the product.

Suggested messaging:

> **Your mental health deserves care.**

Supporting text:

> Connect with trusted mental health professionals and take the next step toward a healthier, happier you.

Primary CTA:

> Find a Counselor

Secondary CTA:

> Book an Appointment

The hero should contain a professional healthcare/wellness visual.

Do not make the hero overly crowded.

Include:

* strong headline
* supporting description
* CTA buttons
* trust indicators
* subtle decorative elements
* professional imagery/illustration

---

# 7. LANDING PAGE SECTIONS

Build a complete marketing website.

Recommended sections:

## Hero

Strong value proposition + CTA.

## How It Works

Example:

1. Find a Counselor
2. Choose a Schedule
3. Book a Session
4. Begin Your Journey

## Why Sunshine?

Highlight:

* Verified Professionals
* Easy Appointment Booking
* Confidential Experience
* Flexible Scheduling
* Bangladesh-focused service

## Featured Counselors

Show selected professionals.

Each card should include:

* photo
* name
* designation
* specialization
* experience
* rating
* consultation fee
* availability
* View Profile
* Book Appointment

## Mental Health Categories

Examples:

* Anxiety
* Depression
* Stress
* Relationship Issues
* Academic Pressure
* Career Stress
* Sleep Problems
* Family Counseling
* Child & Adolescent Counseling

## Statistics / Trust Section

Examples:

* Registered Counselors
* Sessions Completed
* Users Supported
* Cities Covered

Do NOT use fake statistics in production.

If real statistics are unavailable, use appropriate alternative content.

## Testimonials

Implement realistic data architecture.

Clearly avoid presenting fabricated testimonials as real.

## Mental Health Resources

Include:

* Articles
* Guides
* Self-care resources
* Emergency information

## FAQ

Include common questions.

## Emergency Support

Very important.

Clearly communicate that the platform is **not an emergency service**.

Provide appropriate Bangladesh emergency/crisis guidance without making unsupported medical claims.

## Footer

Include:

* About
* Contact
* Privacy Policy
* Terms
* FAQ
* Mental Health Resources
* Doctor Login
* Admin Login
* Social links
* Bangladesh contact information

---

# 8. NAVIGATION

Create a clean responsive navigation bar.

Desktop:

```text
Logo | Home | Counselors | Resources | About | Contact | Login | Book Appointment
```

Authenticated users should see appropriate dashboard/profile controls.

Mobile:

Use a polished drawer/menu.

Test the mobile drawer thoroughly.

Ensure:

* correct open/close behavior
* overlay behavior
* keyboard accessibility
* scroll locking
* route navigation
* active state
* no layout overflow

---

# 9. AUTHENTICATION

Implement production-grade authentication.

Roles:

```text
PATIENT
COUNSELOR
ADMIN
```

Potentially:

```text
SUPER_ADMIN
```

if the architecture requires it.

Implement:

* registration
* login
* logout
* password hashing
* password reset
* email verification if supported
* session/token management
* protected routes
* role-based access control
* account status
* secure password policies

Never store plaintext passwords.

Never expose sensitive authentication data to the frontend.

---

# 10. USER/PATIENT EXPERIENCE

Create a dedicated patient dashboard.

Dashboard should include:

### Overview

* upcoming appointment
* recent appointments
* recommended counselors
* notifications
* quick actions

### Appointments

Users should be able to:

* view upcoming appointments
* view past appointments
* view appointment details
* cancel appointment
* reschedule where allowed
* see appointment status

Statuses:

```text
PENDING
CONFIRMED
COMPLETED
CANCELLED
REJECTED
NO_SHOW
```

Use consistent status badges.

---

# 11. COUNSELOR DIRECTORY

Create a powerful counselor discovery experience.

Features:

* search
* specialization filter
* gender filter where appropriate
* language filter
* experience filter
* availability filter
* consultation fee range
* rating
* location/mode
* sorting

Search should be performant.

Do not load the entire database unnecessarily.

Use pagination/server-side filtering where appropriate.

---

# 12. COUNSELOR PROFILE

Create a premium professional profile page.

Include:

* profile image
* name
* credentials
* designation
* specialization
* experience
* languages
* consultation fee
* professional bio
* qualifications
* availability
* session type
* location
* rating/reviews if implemented
* appointment booking CTA

The profile should feel trustworthy and professional.

---

# 13. APPOINTMENT BOOKING

Build a complete appointment booking flow.

Example:

```text
Choose Counselor
        ↓
Select Session Type
        ↓
Select Date
        ↓
Select Available Time
        ↓
Review Appointment
        ↓
Confirm Booking
        ↓
Booking Confirmation
```

Prevent:

* double booking
* invalid dates
* past appointments
* unavailable time slots
* race conditions
* duplicate submissions

Implement server-side validation.

Do not rely only on frontend validation.

---

# 14. SCHEDULING SYSTEM

Counselors should be able to manage:

* working days
* working hours
* breaks
* unavailable dates
* leave
* session duration
* consultation type

The system should automatically generate available slots.

Consider:

```text
30 minutes
45 minutes
60 minutes
```

Avoid hardcoding scheduling logic into UI components.

Keep scheduling business logic in a dedicated service/module.

---

# 15. BANGLADESH-SPECIFIC REQUIREMENTS

Design the platform specifically for Bangladesh.

Support:

* BDT (৳)
* Bangladesh phone number validation
* Bangladesh timezone
* Bangladesh date/time formatting
* English-first UI with architecture ready for Bangla localization
* Bangla-friendly font support
* Bangladesh-based counselor information
* local address formatting

Use:

```text
Asia/Dhaka
```

where timezone handling is required.

Do not assume UTC/local browser time for appointment logic.

---

# 16. ONLINE / OFFLINE SESSION TYPES

Support configurable consultation modes such as:

* Online
* In-person

For online sessions, design the architecture so a secure meeting URL can be associated with an appointment.

Do not expose meeting links to unauthorized users.

---

# 17. ADMIN DASHBOARD

Create a professional admin dashboard.

The dashboard should NOT look like a generic template.

Use a clean Material UI design.

### Overview

Show:

* total users
* active counselors
* pending counselor approvals
* upcoming appointments
* completed sessions
* cancelled sessions
* revenue if payment functionality exists
* platform activity

Use charts only when they communicate useful information.

Examples:

* appointments over time
* user growth
* counselor growth
* appointment status distribution
* popular counseling categories

---

# 18. ADMIN USER MANAGEMENT

Admins should be able to:

* search users
* filter users
* view user profile
* activate/deactivate account
* manage roles where authorized
* view appointment history
* view account status
* perform appropriate administrative actions

Do not expose unnecessary sensitive information.

---

# 19. ADMIN COUNSELOR MANAGEMENT

Implement:

* counselor listing
* pending approval
* approve
* reject
* suspend
* activate
* edit profile
* verify credentials/status
* manage specialties
* manage availability

Approval actions should be audited.

---

# 20. ADMIN APPOINTMENT MANAGEMENT

Admin should be able to:

* view appointments
* search
* filter
* inspect details
* change status where appropriate
* handle cancellations
* monitor scheduling conflicts

Never allow administrative actions to bypass important business rules accidentally.

---

# 21. CONTENT MANAGEMENT

Create a manageable resource system.

Admins should be able to manage:

* articles
* mental health resources
* categories
* FAQs
* announcements

Resources should support:

* title
* slug
* description
* content
* author
* category
* publish status
* publication date
* cover image
* SEO metadata

---

# 22. NOTIFICATION SYSTEM

Design a reusable notification architecture.

Notifications may include:

* appointment booked
* appointment confirmed
* appointment cancelled
* appointment reminder
* counselor approval
* account-related notifications

Create notification preferences where appropriate.

---

# 23. SECURITY

Treat this as a healthcare-adjacent application.

Implement strong security practices.

### Protect against:

* SQL injection
* XSS
* CSRF where applicable
* broken access control
* IDOR
* insecure direct object references
* brute-force login attacks
* privilege escalation
* mass assignment
* unsafe file uploads
* sensitive data exposure

Implement:

* server-side authorization
* input validation
* output sanitization
* rate limiting
* secure headers
* secure cookies where applicable
* proper CORS
* password hashing
* audit logging

Never trust client-provided role information.

---

# 24. PRIVACY

Mental-health-related information can be highly sensitive.

Follow data minimization.

Only collect information that is actually required.

Avoid putting sensitive information into:

* URLs
* logs
* analytics events
* error messages
* frontend local storage unnecessarily

Do not expose patient information to other patients.

Ensure users can only access their own appointments and information.

---

# 25. ACCESSIBILITY

Target WCAG 2.1 AA-level practices.

Implement:

* keyboard navigation
* visible focus states
* semantic HTML
* accessible forms
* labels
* aria attributes where necessary
* adequate contrast
* screen-reader-friendly navigation
* accessible modals
* accessible dropdowns
* reduced-motion support

Do not use color alone to communicate status.

---

# 26. RESPONSIVE DESIGN

The application must work properly on:

* mobile
* tablet
* laptop
* desktop
* large screens

Test at minimum:

```text
320px
375px
390px
414px
768px
1024px
1280px
1440px
1920px
```

Pay particular attention to:

* navbar
* mobile drawer
* tables
* appointment booking
* forms
* cards
* dashboard
* charts
* modals

No horizontal overflow.

---

# 27. PERFORMANCE OPTIMIZATION

Optimize both frontend and backend.

Frontend:

* lazy loading
* code splitting
* image optimization
* caching
* efficient rendering
* pagination
* skeleton loading
* avoid unnecessary API requests

Backend:

* database indexing
* efficient queries
* pagination
* caching where useful
* connection pooling
* optimized joins
* avoid N+1 queries

Do not optimize prematurely.

Measure first.

---

# 28. DATABASE QUALITY

Review the entire database design.

Ensure:

* proper normalization
* appropriate relationships
* foreign keys
* unique constraints
* indexes
* timestamps
* soft deletion where appropriate
* status fields
* audit fields

Review every query for correctness and performance.

Important entities may include:

```text
User
Patient
Counselor
Specialization
Qualification
Availability
Appointment
AppointmentStatus
Notification
Review
Resource
ResourceCategory
FAQ
AuditLog
```

Do not blindly create all of these if the existing architecture has better equivalents.

---

# 29. API DESIGN

Create clean APIs.

Follow consistent conventions for:

```text
GET
POST
PUT/PATCH
DELETE
```

Use appropriate HTTP status codes.

Standardize API responses.

For example:

```json
{
  "success": true,
  "data": {},
  "message": "..."
}
```

and errors:

```json
{
  "success": false,
  "error": {
    "code": "...",
    "message": "...",
    "details": {}
  }
}
```

Do not expose stack traces in production.

---

# 30. ERROR HANDLING

Every important operation should have:

* loading state
* success state
* empty state
* error state

Create user-friendly error messages.

Avoid messages such as:

```text
500 Internal Server Error
```

as the only visible information.

Instead:

> Something went wrong while booking your appointment. Please try again.

Technical details should remain in logs.

---

# 31. LOADING STATES

Never leave users wondering whether something is happening.

Implement:

* skeletons
* spinners where appropriate
* disabled submit buttons
* optimistic updates only when safe

Prevent duplicate form submissions.

---

# 32. FORMS

All forms should have:

* validation
* clear labels
* helpful errors
* required indicators
* proper keyboard navigation
* loading state
* success feedback
* server-side validation

Validate both frontend and backend.

---

# 33. SEARCH AND FILTERING

Search interfaces should:

* debounce requests where appropriate
* preserve filters
* support pagination
* provide empty results states
* support clearing filters
* avoid unnecessary network calls

Use server-side filtering for large datasets.

---

# 34. SEO

For public pages implement:

* meaningful page titles
* meta descriptions
* Open Graph metadata
* canonical URLs where appropriate
* semantic HTML
* clean URLs
* sitemap
* robots configuration
* structured metadata where appropriate

Use SEO-friendly routes such as:

```text
/counselors
/counselors/:slug
/resources
/resources/:slug
/about
/contact
```

---

# 35. TESTING

Testing is mandatory.

Do not claim the project is production-ready without testing.

Implement appropriate levels of testing.

## Unit Tests

Test:

* authentication logic
* appointment logic
* scheduling logic
* validation
* permissions
* utility functions
* pricing/fee calculations if applicable

## Integration Tests

Test:

* authentication flow
* booking flow
* cancellation
* counselor approval
* role-based access
* database interactions

## API Tests

Test:

* success responses
* validation errors
* unauthorized requests
* forbidden requests
* missing resources
* malformed requests
* duplicate operations

## End-to-End Tests

Test critical user journeys:

### Patient

```text
Register
→ Login
→ Find Counselor
→ View Profile
→ Select Slot
→ Book Appointment
→ View Appointment
→ Cancel/Reschedule
```

### Counselor

```text
Login
→ View Dashboard
→ Configure Availability
→ View Appointment
→ Update Appointment
```

### Admin

```text
Login
→ Dashboard
→ Review Counselor
→ Approve Counselor
→ Manage Users
→ Manage Appointments
```

---

# 36. TEST EDGE CASES

Explicitly test:

* double booking
* two users attempting the same slot
* expired session
* invalid appointment ID
* unauthorized appointment access
* cancelled appointment
* unavailable counselor
* past date
* timezone boundary
* invalid phone number
* duplicate email
* duplicate registration
* invalid password
* empty search
* no search results
* deleted counselor
* suspended user
* network failure
* API timeout

---

# 37. SECURITY TESTING

Perform a security review.

Check:

* authentication bypass
* authorization bypass
* privilege escalation
* IDOR
* injection
* XSS
* CSRF
* rate limiting
* insecure file uploads
* sensitive information leakage
* exposed secrets
* insecure API endpoints

Never include secrets in source control.

---

# 38. ENVIRONMENT CONFIGURATION

Separate:

```text
development
test
production
```

Use environment variables for:

* database credentials
* API keys
* secrets
* email configuration
* external service credentials
* payment configuration
* application URLs

Provide:

```text
.env.example
```

Never commit real secrets.

---

# 39. LOGGING

Implement structured logging.

Logs should help diagnose:

* authentication failures
* API errors
* booking failures
* database errors
* important administrative actions

Never log:

* passwords
* tokens
* unnecessary patient information
* sensitive mental health information

---

# 40. AUDIT LOGGING

Administrative and security-sensitive actions should be auditable.

Examples:

```text
Counselor approved
Counselor rejected
User suspended
Appointment modified
Admin login
Role changed
Resource published
```

Store:

* actor
* action
* target
* timestamp
* relevant metadata

Avoid storing sensitive information unnecessarily.

---

# 41. ADMIN UX

The admin dashboard should feel like a professional SaaS administration platform.

Use:

* sidebar
* top navigation
* breadcrumbs
* responsive tables
* filters
* search
* pagination
* confirmation dialogs
* status badges
* charts
* analytics cards

Do not overload the dashboard.

Prioritize information hierarchy.

---

# 42. EMPTY STATES

Every list needs a meaningful empty state.

Examples:

> No upcoming appointments

with a useful CTA:

> Find a Counselor

Avoid blank screens.

---

# 43. MICRO-INTERACTIONS

Use subtle animations.

Examples:

* button hover
* card hover
* page transitions
* modal transitions
* skeleton transitions
* toast animation

Animations should be:

* fast
* subtle
* purposeful

Respect:

```text
prefers-reduced-motion
```

---

# 44. MOBILE-FIRST QUALITY

Do not simply shrink desktop UI.

Design mobile experiences intentionally.

Particularly optimize:

* counselor cards
* appointment booking
* date/time selection
* dashboard navigation
* forms
* mobile drawer
* tables

For tables, use responsive alternatives rather than forcing users to horizontally scroll whenever possible.

---

# 45. DATA VALIDATION

Implement strict validation for:

* email
* phone
* password
* appointment dates
* time slots
* counselor information
* user profile
* resource content
* IDs
* query parameters

Never rely solely on client-side validation.

---

# 46. IMAGE AND FILE HANDLING

If profile pictures or resource images are supported:

Implement:

* file type validation
* file size limits
* safe filenames
* image optimization
* appropriate storage strategy
* authorization checks

Never trust uploaded file extensions.

---

# 47. PAYMENT ARCHITECTURE

If payment is included in the existing project or planned:

Design the system so Bangladesh-compatible payment providers can be integrated later.

Potential architecture should allow providers such as:

* bKash
* Nagad
* SSLCommerz

Do not implement fake payment confirmation.

If payment is not currently required, keep the architecture extensible without adding unnecessary complexity.

---

# 48. INTERNATIONALIZATION

Architecture should be ready for:

```text
English
Bangla
```

Even if the first production version launches in English.

Avoid hardcoding user-visible strings directly throughout business logic.

---

# 49. CONTENT QUALITY

All visible copy should feel professional.

Avoid placeholder text such as:

```text
Lorem ipsum
Test User
Doctor 1
Sample text
Coming soon
```

unless it is explicitly part of a development-only state.

Use Bangladesh-relevant realistic content structure.

Do not fabricate real medical professionals.

Use clearly identified demo data when demo data is required.

---

# 50. MEDICAL SAFETY

This is a counseling platform, not an AI medical diagnosis system.

Do not introduce functionality that:

* diagnoses mental illness
* guarantees treatment outcomes
* replaces professional medical care
* provides dangerous medical advice

Clearly communicate appropriate limitations.

For emergency situations, provide appropriate emergency guidance and encourage users to seek immediate professional/emergency assistance.

---

# 51. ACCESS CONTROL MATRIX

Create a clear permission model.

Example:

| Feature                 | Patient | Counselor | Admin |
| ----------------------- | ------: | --------: | ----: |
| View counselors         |       ✓ |         ✓ |     ✓ |
| Book appointment        |       ✓ |         ✗ |     ✓ |
| Manage own appointments |       ✓ |         ✓ |     ✓ |
| Manage availability     |       ✗ |         ✓ |     ✓ |
| Manage users            |       ✗ |         ✗ |     ✓ |
| Manage counselors       |       ✗ |         ✗ |     ✓ |
| Manage resources        |       ✗ |         ✗ |     ✓ |
| Platform analytics      |       ✗ |   Limited |     ✓ |
| System settings         |       ✗ |         ✗ |     ✓ |

Adjust this according to the existing architecture.

---

# 52. PRODUCTION BUILD

The final project must:

* build successfully
* start successfully
* pass tests
* have no critical console errors
* have no broken routes
* have no missing assets
* have no unresolved imports
* have no TypeScript/build errors if applicable
* have no database migration errors
* work on a clean environment

---

# 53. DEPLOYMENT READINESS

Prepare:

```text
README.md
.env.example
deployment documentation
database setup documentation
migration instructions
seed instructions
testing instructions
development instructions
production instructions
```

Document:

```text
Prerequisites
Installation
Environment variables
Database setup
Migrations
Seed data
Development
Testing
Build
Production deployment
Troubleshooting
```

---

# 54. DOCUMENTATION

Update/create:

```text
README.md
docs/
```

Potential documentation:

```text
docs/
├── architecture.md
├── database.md
├── api.md
├── authentication.md
├── authorization.md
├── testing.md
├── deployment.md
├── development-guide.md
└── security.md
```

Documentation should be understandable to a beginner developer while still being technically correct.

Explain:

* project structure
* how features work
* how to add a feature
* how authentication works
* how appointments work
* how scheduling works
* how the database is structured
* how to run tests

---

# 55. CODE COMMENTS

Do not over-comment obvious code.

Comments should explain:

* why something exists
* complex business logic
* important security decisions
* scheduling rules
* non-obvious performance decisions

---

# 56. PERFORMANCE AUDIT

After implementation:

Measure and improve:

* page load performance
* API response time
* database query performance
* bundle size
* image sizes
* unnecessary renders
* unnecessary API calls

Do not optimize based solely on assumptions.

---

# 57. FINAL QA PASS

After completing development, perform a complete QA pass.

Check every route.

Check every button.

Check every form.

Check every API endpoint.

Check every role.

Check mobile.

Check desktop.

Check error states.

Check empty states.

Check loading states.

Check authentication.

Check authorization.

Check appointment booking.

Check scheduling.

Check admin operations.

Check database migrations.

Check production build.

---

# 58. BUG-FIX LOOP

Do not stop after finding bugs.

For every discovered issue:

```text
Identify
→ Reproduce
→ Determine root cause
→ Fix
→ Add regression test
→ Re-test
```

Do not apply superficial fixes that hide the underlying problem.

---

# 59. FINAL PRODUCTION CHECKLIST

Before declaring completion, verify:

### UI

* [ ] Modern white Material UI
* [ ] Consistent design system
* [ ] Responsive
* [ ] Accessible
* [ ] Mobile navigation works
* [ ] Hero page polished
* [ ] Empty states
* [ ] Loading states
* [ ] Error states

### Authentication

* [ ] Registration
* [ ] Login
* [ ] Logout
* [ ] Password security
* [ ] Protected routes
* [ ] RBAC

### Patient

* [ ] Counselor discovery
* [ ] Counselor profiles
* [ ] Appointment booking
* [ ] Appointment management
* [ ] Profile management
* [ ] Notifications

### Counselor

* [ ] Dashboard
* [ ] Profile
* [ ] Availability
* [ ] Appointment management

### Admin

* [ ] Dashboard
* [ ] Analytics
* [ ] User management
* [ ] Counselor management
* [ ] Appointment management
* [ ] Content management
* [ ] Audit logs
* [ ] Settings

### Backend

* [ ] Validation
* [ ] Authorization
* [ ] Error handling
* [ ] Security
* [ ] Logging
* [ ] Performance

### Database

* [ ] Correct relationships
* [ ] Constraints
* [ ] Indexes
* [ ] Migrations
* [ ] Seed data
* [ ] No N+1 queries

### Testing

* [ ] Unit tests
* [ ] Integration tests
* [ ] API tests
* [ ] E2E tests
* [ ] Security tests
* [ ] Regression tests

### Production

* [ ] Production build succeeds
* [ ] No critical errors
* [ ] Environment configuration
* [ ] Deployment documentation
* [ ] Security review
* [ ] Performance review

---

# 60. IMPORTANT AGENT BEHAVIOR

You are operating as an autonomous senior development agent.

Do not merely tell me what should be changed.

**Actually inspect the repository and implement the changes.**

Do not stop after implementing the first obvious features.

Continuously:

```text
Inspect
→ Plan
→ Implement
→ Test
→ Debug
→ Refactor
→ Optimize
→ Re-test
```

When encountering an existing implementation:

* understand it first
* preserve useful work
* improve it where necessary
* avoid unnecessary rewrites

When something is ambiguous, prefer the most maintainable production-grade solution that fits the existing architecture.

Do not introduce unnecessary technologies merely to make the project appear more sophisticated.

---

# 61. FINAL DELIVERABLE

At the end, provide a concise engineering report containing:

### Architecture

What architecture is now used and why.

### Implemented Features

List the major completed features.

### UI/UX Improvements

List major visual and usability improvements.

### Security Improvements

List security protections implemented.

### Testing

Report:

```text
Unit tests: X passed
Integration tests: X passed
E2E tests: X passed
```

### Performance

Report major performance improvements and measurements where available.

### Database

Describe important schema/index/query improvements.

### Remaining Issues

Only list genuine remaining issues.

Do NOT claim something is production-ready if critical issues remain.

### How to Run

Provide exact commands for:

```text
install
development
database setup
migration
seed
test
build
production
```

---

# MOST IMPORTANT REQUIREMENT

The final result should feel like a **real, polished Bangladesh-based mental health counseling platform**, not a university CRUD assignment.

The quality bar is:

> **Production SaaS + modern healthcare UX + strong security + reliable appointment scheduling + professional admin dashboard + comprehensive testing.**

Prioritize correctness, usability, security, accessibility, maintainability, and reliability over adding unnecessary features.




# 62. MANDATORY TECHNOLOGY STACK

The project MUST use the following technology stack.

Do NOT replace the stack with another framework or database unless explicitly instructed.

## Frontend

```text
HTML5
CSS3
JavaScript
Material UI / Material Design principles
```

Use modern, semantic HTML5, maintainable CSS, and clean JavaScript.

The frontend must communicate with the ASP.NET backend through properly designed APIs.

Do not put database logic inside frontend JavaScript.

---

## Backend

```text
ASP.NET Core
C#
Entity Framework Core
```

Use ASP.NET Core for the backend/API.

Use Entity Framework Core for:

* database access
* entity mapping
* relationships
* migrations
* querying
* transactions where appropriate

Follow clean separation between:

```text
Controller/API
    ↓
Service / Business Logic
    ↓
Repository/Data Access where justified
    ↓
Entity Framework Core
    ↓
PostgreSQL
```

Do not put complex business logic directly inside controllers.

---

## Database

```text
PostgreSQL
```

PostgreSQL is the mandatory production database.

Do NOT switch to:

* MySQL
* MariaDB
* SQL Server
* SQLite

unless explicitly instructed.

---

# 63. DATABASE-FIRST DEVELOPMENT

This is a very important requirement.

Before building the application features, **prepare the PostgreSQL database/server structure first**.

The database must be designed based on the actual requirements of the Sunshine Mental Health Portal.

Do not start by creating random EF Core entities and allowing the database design to evolve without planning.

First determine:

* entities
* relationships
* primary keys
* foreign keys
* constraints
* indexes
* unique constraints
* status values
* timestamps
* audit requirements
* soft-delete requirements where appropriate

Then prepare the database.

---

# 64. SQL DIRECTORY

Create a dedicated directory:

```text
sql/
```

Store the database-related SQL scripts there.

Recommended structure:

```text
sql/
├── 01_create_database.sql
├── 02_extensions.sql
├── 03_schema.sql
├── 04_indexes.sql
├── 05_constraints.sql
├── 06_seed_data.sql
├── 07_test_queries.sql
└── README.md
```

Adjust the structure if the existing project architecture requires something better, but the SQL scripts MUST remain organized inside `sql/`.

---

# 65. DATABASE SQL SCRIPTS

The SQL files should contain real executable PostgreSQL SQL.

Do not create documentation-only SQL.

The scripts should be:

* correctly ordered
* reproducible
* readable
* maintainable
* safe to execute
* properly commented

Where appropriate, make scripts idempotent.

For example, avoid blindly executing:

```sql
CREATE TABLE users (...);
```

if the script may reasonably be executed more than once.

Use appropriate PostgreSQL mechanisms such as:

```sql
CREATE TABLE IF NOT EXISTS
```

where appropriate.

Do not use `IF NOT EXISTS` blindly when it could hide schema problems.

---

# 66. DATABASE SCHEMA

Design a normalized relational schema.

At minimum, evaluate the need for entities such as:

```text
users
roles
user_roles
patient_profiles
counselor_profiles
specializations
counselor_specializations
qualifications
counselor_qualifications
availability
appointments
appointment_statuses
appointment_types
notifications
reviews
resources
resource_categories
faqs
audit_logs
```

Do NOT blindly create every table above.

Create only the tables that are justified by the application's requirements.

Use appropriate:

* `PRIMARY KEY`
* `FOREIGN KEY`
* `UNIQUE`
* `NOT NULL`
* `CHECK`
* indexes

---

# 67. POSTGRESQL DESIGN QUALITY

Use PostgreSQL appropriately.

Consider:

* UUID vs integer IDs
* `TIMESTAMPTZ` for appointment timestamps
* appropriate numeric types for BDT prices
* indexes for frequently queried columns
* unique constraints for emails
* transaction safety
* foreign-key behavior
* cascading rules

For example, appointment times should not be stored in a way that loses timezone information.

The application's primary timezone is:

```text
Asia/Dhaka
```

---

# 68. APPOINTMENT DATABASE INTEGRITY

Appointment scheduling is one of the most important parts of this system.

The database must help prevent double booking.

Do not rely solely on frontend checks.

Do not rely solely on:

```text
"Check if slot exists"
→
"Then insert appointment"
```

because concurrent requests can still cause conflicts.

Design proper database/application-level protection against race conditions.

Use appropriate:

* transactions
* constraints
* locking where necessary
* unique indexes/constraints where applicable

Test concurrent booking scenarios.

---

# 69. BANGLADESH SEED DATA

Create realistic **Bangladesh-specific seed data**.

Store the seed data in:

```text
sql/06_seed_data.sql
```

and/or the appropriate EF Core seed mechanism.

The seed data should represent a realistic Bangladesh deployment.

Do NOT use obviously fake generic data such as:

```text
John Doe
Jane Doe
Doctor 1
Test User
Sample Hospital
```

unless specifically marked as test data.

Use Bangladesh-relevant examples.

---

# 70. BANGLADESH COUNSELOR SEED DATA

Create a reasonable set of demo counselors/professionals.

Use clearly fictional/demo identities to avoid impersonating real professionals.

For example, names can be Bangladesh-appropriate:

```text
Dr. Samira Rahman
Dr. Farhan Ahmed
Dr. Nusrat Jahan
Dr. Mahmud Hasan
Dr. Tahmina Akter
```

These must be treated as **demo/seed professionals**, not real people.

Include realistic:

* designation
* specialization
* years of experience
* qualifications
* languages
* consultation fee in BDT
* location
* consultation mode
* availability

Example specializations:

```text
Clinical Psychology
Counseling Psychology
Child & Adolescent Counseling
Relationship Counseling
Stress Management
Anxiety Management
Depression Support
Academic Counseling
Career Counseling
Family Counseling
```

Do not make unsupported claims about real people.

---

# 71. BANGLADESH LOCATIONS

Seed data should use realistic Bangladesh locations.

Examples:

```text
Dhaka
Chattogram
Khulna
Rajshahi
Sylhet
Barishal
Rangpur
Mymensingh
```

Use realistic district/city relationships where appropriate.

Do not use random foreign addresses.

---

# 72. BANGLADESH PHONE NUMBERS

Use fictional Bangladesh-format phone numbers for seed data.

Do not use real people's phone numbers.

The application should validate Bangladesh phone formats appropriately.

Example format:

```text
+8801XXXXXXXXX
```

or an appropriate local format.

---

# 73. BDT PRICING

All counselor consultation fees in seed data should use:

```text
BDT / ৳
```

Example:

```text
৳800
৳1000
৳1200
৳1500
```

Do not use USD unless explicitly required.

---

# 74. SEED APPOINTMENTS

Create realistic demo appointment data.

Include a mixture of:

```text
PENDING
CONFIRMED
COMPLETED
CANCELLED
```

Ensure appointment dates and relationships are valid.

Do not create impossible appointments such as:

* appointments for nonexistent counselors
* appointments for nonexistent patients
* overlapping appointments
* invalid counselor availability
* invalid foreign keys

---

# 75. SEED USERS

Create appropriate demo accounts for:

```text
Patient
Counselor
Admin
```

Clearly document the development credentials in the development documentation.

IMPORTANT:

These credentials are development/demo credentials only.

Never use real passwords.

Never include production credentials.

Passwords must still be stored using the application's real password hashing mechanism.

---

# 76. EF CORE AND DATABASE CONNECTION

After preparing the PostgreSQL database design, properly connect the application through Entity Framework Core.

Configure:

```text
ASP.NET Core
        ↓
EF Core
        ↓
Npgsql
        ↓
PostgreSQL
```

Use the official PostgreSQL EF Core provider:

```text
Npgsql.EntityFrameworkCore.PostgreSQL
```

Use configuration through environment variables/user secrets rather than hardcoding credentials.

---

# 77. DATABASE CONNECTION TEST

Create an explicit database connection test.

The application must be able to verify:

```text
ASP.NET Core
      ↓
EF Core
      ↓
Npgsql
      ↓
PostgreSQL
```

successfully.

Test:

* application startup
* database connection
* migrations/schema
* basic query
* insert
* update
* delete where appropriate
* transaction
* relationship query

Do not consider the backend complete until these work.

---

# 78. END-TO-END CONNECTION TESTING

Test the complete chain:

```text
Browser
   ↓
HTML/CSS/JavaScript
   ↓
HTTP/API
   ↓
ASP.NET Core
   ↓
Service Layer
   ↓
Entity Framework Core
   ↓
Npgsql
   ↓
PostgreSQL
```

Every important user operation must be tested through the actual application flow.

For example:

```text
Patient clicks "Book Appointment"
        ↓
JavaScript sends API request
        ↓
ASP.NET receives request
        ↓
Authorization checked
        ↓
Validation performed
        ↓
Appointment service executes
        ↓
EF Core transaction
        ↓
PostgreSQL
        ↓
API response
        ↓
Frontend updates UI
```

Do not test only individual pieces in isolation.

---

# 79. API CONNECTION TESTING

Test every important API endpoint.

For each endpoint verify:

### Success

* correct request
* correct authentication
* correct response
* correct database operation

### Validation

* missing fields
* invalid values
* malformed IDs
* invalid dates
* invalid time slots

### Authentication

* unauthenticated request
* expired authentication
* invalid authentication

### Authorization

* patient accessing another patient's data
* counselor accessing unauthorized resources
* patient attempting admin operation
* counselor attempting admin operation

### Database

* valid record
* missing record
* duplicate record
* constraint violation
* transaction failure

---

# 80. UI TESTING — DESKTOP WEB

Perform proper UI testing on desktop browsers.

At minimum test:

```text
Chrome
Firefox
Edge
```

Test important viewport sizes including:

```text
1280 × 720
1366 × 768
1440 × 900
1920 × 1080
```

Verify:

* layout
* spacing
* typography
* buttons
* forms
* dropdowns
* dialogs
* tables
* charts
* navigation
* routing
* loading states
* error states
* empty states
* notifications
* appointment booking

---

# 81. UI TESTING — MOBILE WEB

Mobile testing is mandatory.

Do NOT assume that responsive CSS means the mobile UI is working.

Test actual mobile viewport behavior.

At minimum test:

```text
320 × 568
375 × 667
390 × 844
414 × 896
```

Test:

* mobile navbar
* mobile drawer
* hamburger menu
* overlay
* scrolling
* touch targets
* forms
* date picker
* time picker
* counselor cards
* appointment booking
* dashboard
* notifications
* modals
* tables
* buttons
* footer

---

# 82. MOBILE NAVIGATION TESTING

Pay special attention to the mobile navigation drawer.

Test:

```text
Open drawer
→ Navigate
→ Close drawer
→ Browser back
→ Open again
→ Navigate to another route
→ Resize viewport
→ Rotate viewport if supported
```

Verify:

* drawer opens correctly
* drawer closes correctly
* overlay works
* body scrolling is handled correctly
* navigation works
* active route is displayed
* drawer does not remain stuck
* no duplicate overlays
* no horizontal overflow
* no JavaScript errors

---

# 83. RESPONSIVE BREAKPOINT TESTING

Do not test only at standard desktop/mobile sizes.

Test intermediate widths:

```text
320
360
375
390
414
480
600
768
820
900
1024
1280
1440
1920
```

Look for:

* overflowing text
* broken grids
* overlapping buttons
* oversized cards
* broken navigation
* table overflow
* modal overflow
* clipped content
* inconsistent spacing

Fix the underlying responsive design rather than adding random media-query patches.

---

# 84. UI FUNCTIONAL TESTING

Every interactive element must actually work.

Test:

* links
* buttons
* dropdowns
* tabs
* modals
* drawers
* forms
* search
* filters
* pagination
* sorting
* appointment booking
* cancellation
* profile editing
* login
* logout
* notifications

A button must never exist merely for visual appearance.

If a feature is not implemented, do not present it as functional.

---

# 85. UI AUTOMATED TESTING

Where practical, use browser automation for end-to-end testing.

Recommended tooling may include:

```text
Playwright
```

or another appropriate browser testing framework compatible with the project.

Automate critical flows.

At minimum:

### Patient E2E

```text
Open website
→ Register/Login
→ Browse counselors
→ Search/filter
→ Open counselor
→ Select appointment
→ Confirm booking
→ Verify appointment
```

### Counselor E2E

```text
Login
→ Open dashboard
→ View appointments
→ Manage availability
→ Update appointment
```

### Admin E2E

```text
Login
→ Open admin dashboard
→ View users
→ View counselors
→ Approve counselor
→ View appointments
```

---

# 86. VISUAL UI QA

Do a visual QA pass after functionality is complete.

Check for:

* inconsistent spacing
* inconsistent typography
* misaligned icons
* poor button sizes
* awkward card heights
* inconsistent border radius
* inconsistent shadows
* excessive whitespace
* cramped sections
* broken responsive layouts
* poor contrast
* inconsistent colors

The UI should look like one cohesive product.

---

# 87. BROWSER CONSOLE QA

Before completion:

Open browser developer tools and verify there are no unexpected:

```text
JavaScript errors
React/framework errors if applicable
network errors
404 assets
failed API requests
CORS errors
mixed-content errors
accessibility warnings where applicable
```

Do not ignore console errors simply because the page visually works.

---

# 88. NETWORK/API QA

Use browser Network tools or automated tests to verify:

* API requests use correct URLs
* HTTP methods are correct
* request payloads are correct
* response structures are correct
* authentication is correctly transmitted
* errors are handled
* duplicate requests are avoided
* failed requests recover appropriately

Do not leave unnecessary API calls running on every render/navigation.

---

# 89. DATABASE QUERY TESTING

Use:

```text
sql/07_test_queries.sql
```

for useful PostgreSQL verification queries.

Include queries to verify:

* users
* counselors
* specializations
* availability
* appointments
* appointment conflicts
* notifications
* resources
* relationships

Also include diagnostic queries useful during development.

---

# 90. FULL SYSTEM TEST

Before declaring the project finished, perform this exact conceptual test:

```text
START POSTGRESQL
        ↓
RUN DATABASE SQL
        ↓
VERIFY DATABASE
        ↓
START ASP.NET CORE
        ↓
VERIFY EF CORE CONNECTION
        ↓
VERIFY API
        ↓
OPEN FRONTEND
        ↓
VERIFY API CONNECTION
        ↓
LOGIN
        ↓
BROWSE COUNSELORS
        ↓
BOOK APPOINTMENT
        ↓
VERIFY DATABASE RECORD
        ↓
VERIFY APPOINTMENT IN DASHBOARD
        ↓
LOGIN AS COUNSELOR
        ↓
VERIFY APPOINTMENT
        ↓
LOGIN AS ADMIN
        ↓
VERIFY ADMIN DATA
        ↓
RUN AUTOMATED TESTS
        ↓
RUN UI TESTS
        ↓
RUN MOBILE RESPONSIVE TESTS
        ↓
FIX ALL CRITICAL ISSUES
```

---

# 91. FINAL QUALITY GATE

Do NOT say:

> "Project completed."

until the following have been verified:

### Database

* PostgreSQL works
* SQL scripts work
* schema is correct
* seed data works
* constraints work
* indexes exist
* EF Core connects correctly

### Backend

* ASP.NET Core starts
* APIs work
* validation works
* authentication works
* authorization works
* database operations work
* error handling works

### Frontend

* pages render correctly
* APIs connect correctly
* authentication works
* booking works
* forms work
* navigation works
* no critical console errors

### Desktop

* tested at multiple desktop sizes
* navigation works
* layouts work
* forms work
* dashboards work

### Mobile

* tested at multiple mobile sizes
* mobile navigation works
* booking works
* forms work
* cards work
* dashboards work
* no horizontal overflow

### Testing

* unit tests pass
* integration tests pass
* API tests pass
* E2E tests pass
* database tests pass
* responsive UI tests pass

---

# 92. FINAL TECHNICAL STACK SUMMARY

The final system MUST use:

```text
Frontend
├── HTML5
├── CSS3
├── JavaScript
└── Material Design / Material UI principles

Backend
├── ASP.NET Core
├── C#
└── Entity Framework Core

Database
└── PostgreSQL

PostgreSQL Provider
└── Npgsql.EntityFrameworkCore.PostgreSQL

Testing
├── Backend/API tests
├── Database tests
├── Integration tests
└── Browser/E2E testing
```

Do not introduce a different frontend framework or database unless explicitly instructed.

---

# 93. FINAL INSTRUCTION TO THE AI AGENT

The most important rule is:

> **Do not just make the application look production-ready. Make the entire system actually work end-to-end.**

The final quality must be evaluated across:

```text
UI
+
UX
+
Frontend
+
Backend
+
API
+
Authentication
+
Authorization
+
Database
+
EF Core
+
PostgreSQL
+
Security
+
Performance
+
Accessibility
+
Responsive Design
+
Automated Testing
+
Manual UI Testing
+
Mobile Testing
+
Documentation
```

Start with the **PostgreSQL database design and SQL scripts**, then establish the **EF Core/PostgreSQL connection**, then verify the **ASP.NET Core APIs**, then connect the frontend, and finally perform comprehensive **desktop + mobile UI testing**.

Do not skip stages.

Do not assume that something works because the code compiles.

**Actually run it, test it, find failures, fix them, and test again.**
