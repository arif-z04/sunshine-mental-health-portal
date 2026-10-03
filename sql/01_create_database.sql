-- =============================================================================
-- SCRIPT 01: DATABASE CREATION & USER CONFIGURATION
-- Project: Sunshine Mental Health Counseling System (Bangladesh)
-- Description: Creates the sunshine_user role and sunshine_db database.
-- =============================================================================

-- Step 1: Create dedicated database user if not exists
DO
$do$
BEGIN
   IF NOT EXISTS (
      SELECT FROM pg_catalog.pg_roles WHERE rolname = 'sunshine_user'
   ) THEN
      CREATE ROLE sunshine_user WITH LOGIN PASSWORD 'SunshinePass123!';
      RAISE NOTICE 'Role sunshine_user created successfully.';
   ELSE
      ALTER ROLE sunshine_user WITH PASSWORD 'SunshinePass123!';
      RAISE NOTICE 'Role sunshine_user password updated.';
   END IF;
END
$do$;

-- Step 2: Grant permissions to create databases
ALTER ROLE sunshine_user CREATEDB;

-- Step 3: Create the database
SELECT 'CREATE DATABASE sunshine_db WITH OWNER = sunshine_user ENCODING = ''UTF8'''
WHERE NOT EXISTS (SELECT FROM pg_database WHERE datname = 'sunshine_db')\gexec

-- Step 4: Grant database privileges
GRANT ALL PRIVILEGES ON DATABASE sunshine_db TO sunshine_user;

-- Step 5: Connect and configure schema default privileges
\c sunshine_db postgres

GRANT ALL ON SCHEMA public TO sunshine_user;
GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO sunshine_user;
GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA public TO sunshine_user;

ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT ALL ON TABLES TO sunshine_user;
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT ALL ON SEQUENCES TO sunshine_user;

\echo 'Database sunshine_db and user sunshine_user successfully configured!';
