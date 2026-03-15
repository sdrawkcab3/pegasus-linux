## Pegasus


### Information
A slapped together emulator for Virindi Integrator 2. This code was an attempt at creating an emulator for Virindi Integrator 2. Various people have asked for the source code since hosting ceased, so here it is.

## Public Host
Check the wiki for more instruction. This public host will stay up as long as Oracle keeps their little ampere hosts free - thanks Oracle!

147.224.182.244 white.virindi.net

Make up a new username and password. 
There is no sign-up, but please do not use your account info from your server (or anything else that's real).

Working
- pretty blue nav lines
- fellowship stuff
- many commands and conveniences

Added
- Some security updates
- The client ONLY reacts to commands from yourself and "Friends". This means if you'd like to reliquish command of your toons to someone, you MUST add their VI username to your friends list. (Vital sharing, targeting is still shared)
- Prevent malicious oversize packets from crashing the host.

Not working
- Dungeon maps sorta work, but will never be as nice as UtilityBelt dungeon maps.
- There may be other things that broke when I updated to dotnet9, but at least it runs on aarch64 (aka RaspPi-ish SBCs)

## Self Host Prerequisites

- Docker
- Docker Compose
- Git
- PostgreSQL (if running without Docker)
  
## Project Structure

```
pegasus-linux/
├── docker-compose.yml    # Docker Compose configuration (app, db, cloudflared)
├── init-db-script.sql    # Database initialization script
├── .env.example          # Template for required environment variables
├── .env                  # Your local secrets — never commit this
├── .gitignore
├── README.md
└── Pegasus/
    ├── Dockerfile        # Docker build configuration
    ├── Config.json       # Application configuration
    └── ...               # Application source files
```

## Database Setup

### Option 1: Manual PostgreSQL Setup

Connect to your PostgreSQL instance and run:

```sql
CREATE DATABASE pegasus;
CREATE USER pegasus WITH ENCRYPTED PASSWORD 'your_password_here';
GRANT ALL PRIVILEGES ON DATABASE pegasus TO pegasus;
```

Use the same password you set as `POSTGRES_PASSWORD` in your `.env` file.

### Option 2: Docker Setup

The database is automatically initialized when using Docker Compose. Credentials are configured via a `.env` file (see [Configuration](#configuration) below).

## Configuration

1. Copy `Config.example.json` to `Config.json` and adjust settings if needed:
```bash
cp Config.example.json Config.json
```

2. Create a `.env` file from the example and fill in your values:
```bash
cp .env.example .env
```

```env
# A strong password for the PostgreSQL database
POSTGRES_PASSWORD=your_strong_password_here
```

Never commit `.env` to git — it is listed in `.gitignore`.

## Running the Application

### Using Docker (Recommended)

1. Clone the repository:
```bash
git clone https://github.com/sdrawkcab3/Pegasus-linux.git
cd Pegasus
```

2. Start the application using Docker Compose:
```bash
docker-compose up -d
```

This will:
- Build the .NET Core application
- Start PostgreSQL database
- Initialize the database schema
- Start the Pegasus application

3. View logs:
```bash
# View all logs
docker-compose logs -f

# View only Pegasus application logs
docker-compose logs -f pegasus-app

# View only database logs
docker-compose logs -f pegasus-db
```

4. Stop the application:
```bash
docker-compose down
```

### Development

#### Building
```bash
# Clean and rebuild
docker-compose down
docker system prune -f
docker-compose build --no-cache
docker-compose up -d
```

#### Database Management
- The database is automatically initialized using `init-db-script.sql`
- Data persists between restarts in the `postgres_data` volume
- To reset the database:
```bash
docker-compose down -v  # The -v flag removes volumes
```

## Troubleshooting

1. If the application fails to start, check:
   - Docker logs for errors
   - Database connection settings
   - Config.json exists and is properly formatted

2. Common issues:
   - Database connection issues: Ensure the `POSTGRES_PASSWORD` in `.env` matches what the app expects
   - Database connection issues: Ensure PostgreSQL container is healthy

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

This project is licensed under the terms of the license included in the repository. 
