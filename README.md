## Pegasus


### Information
A slapped together emulator for Virindi Integrator 2. This code was an attempt at creating an emulator for Virindi Integrator 2. Various people have asked for the source code since hosting ceased, so here it is.

## Prerequisites

- Docker
- Docker Compose
- Git
- PostgreSQL (if running without Docker)

## Project Structure

```
Pegasus/
├── docker-compose.yml    # Docker compose configuration
├── init-db-script.sql   # Database initialization script
├── Pegasus/            
│   ├── Dockerfile      # Docker build configuration
│   ├── Config.json     # Application configuration
│   └── ...            # Application source files
└── README.md
```

## Database Setup

### Option 1: Manual PostgreSQL Setup
```sql
$ PGPASSWORD=adminpassword psql -U postgres -h hostname -p 5432 -d template1 --set=sslmode=require
psql 10.7
SSL connection (protocol: TLSv1.2, cipher: ECDHE-RSA-AES256-GCM-SHA384, bits: 256, compression: off)

defaultdb=> create database pegasus;
CREATE DATABASE
defaultdb=> create user pegasus with encrypted password 'somelongasspassword';
CREATE ROLE
defaultdb=> grant all privileges on database pegasus to pegasus;
GRANT
```

### Option 2: Docker Setup

The database is automatically initialized when using Docker Compose with the following configuration:
- Database: pegasus
- Username: pegasus
- Password: somelongasspassword (configurable in docker-compose.yml)

## Configuration

1. Copy `Config.example.json` to `Config.json` and adjust settings if needed:
```bash
cp Config.example.json Config.json
```

2. Environment variables (set in docker-compose.yml):
```
PEGASUS_DB_HOSTNAME=postgres
PEGASUS_DB_PORT=5432
PEGASUS_DB_DATABASE=pegasus
PEGASUS_DB_USERNAME=pegasus
PEGASUS_DB_PASSWORD=somelongasspassword
```

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
   - Port 5432 already in use: Change the port mapping in docker-compose.yml
   - Database connection issues: Ensure PostgreSQL container is healthy

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

This project is licensed under the terms of the license included in the repository. 