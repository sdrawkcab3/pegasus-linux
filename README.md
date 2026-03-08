## Pegasus


### Information
A slapped together emulator for Virindi Integrator 2. This code was an attempt at creating an emulator for Virindi Integrator 2. Various people have asked for the source code since hosting ceased, so here it is.

## Prerequisites

- Docker
- Docker Compose
- Git
- PostgreSQL (if running without Docker)
- A [Cloudflare account](https://cloudflare.com) with a domain (for public access via Cloudflare Tunnel)

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

# Cloudflare Tunnel token (see Public Access section below)
CLOUDFLARE_TUNNEL_TOKEN=your_tunnel_token_here
```

Never commit `.env` to git — it is listed in `.gitignore`.

## Public Access via Cloudflare Tunnel

Cloudflare Tunnel exposes the app publicly without opening any ports on your router or firewall.

### Setup

1. Create a free [Cloudflare account](https://cloudflare.com) and add your domain.

2. Go to **Cloudflare Zero Trust dashboard** → Networks → Tunnels → **Create a tunnel**.

3. Choose **Cloudflared**, give it a name (e.g. `pegasus`), and copy the tunnel token into your `.env` as `CLOUDFLARE_TUNNEL_TOKEN`.

4. Under **Public Hostnames**, add a hostname and configure the service:
   - **Type:** `HTTP`
   - **URL:** `pegasus:13124`

   This uses the internal Docker network name — no host port exposure needed.

5. Start the stack:
```bash
docker compose up -d
```

The `cloudflared` container will connect automatically and your app will be available at your configured hostname over HTTPS.

## Running the Application

### Using Docker (Recommended)

1. Clone the repository:
```bash
git clone https://github.com/davesienkowski/Pegasus-linux.git
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