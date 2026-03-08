-- The database and user will be created by Docker's postgres initialization
-- based on the environment variables, so we only need to set additional permissions

-- Grant additional privileges if needed
GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO pegasus;
GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA public TO pegasus;
