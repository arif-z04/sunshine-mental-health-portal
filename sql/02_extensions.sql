-- =============================================================================
-- SCRIPT 02: DATABASE EXTENSIONS
-- Project: Sunshine Mental Health Counseling System (Bangladesh)
-- Description: Enables required PostgreSQL extensions (uuid-ossp, pgcrypto).
-- =============================================================================

\c sunshine_db sunshine_user

CREATE EXTENSION IF NOT EXISTS "uuid-ossp";
CREATE EXTENSION IF NOT EXISTS "pgcrypto";

\echo 'PostgreSQL extensions enabled successfully!';
