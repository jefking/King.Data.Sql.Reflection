# King.Data.Sql.Reflection Docker Usage

This Docker image allows you to inspect SQL Server database schemas (stored procedures and tables) from a containerized environment.

## Building the Docker Image

```bash
docker build -t king-sql-reflection .
```

## Running the Container

### Basic Usage

```bash
docker run -e SQL_CONNECTION_STRING="Server=your_server;Database=your_db;User Id=your_user;Password=your_password;" king-sql-reflection
```

### With JSON Output

```bash
docker run -e SQL_CONNECTION_STRING="your_connection_string" -e OUTPUT_JSON=true king-sql-reflection
```

## Environment Variables

- `SQL_CONNECTION_STRING` (Required): The SQL Server connection string
- `OUTPUT_JSON` (Optional): Set to "true" to output results in JSON format

## Example Output

The console application will display:
1. All stored procedures with their parameters
2. All tables with their columns
3. Optional JSON representation of the schema

## Security Notes

- Never hardcode connection strings in Docker images
- Use environment variables or secrets management for production deployments
- Consider using Docker secrets or orchestration-specific secret management

## Networking

If connecting to a SQL Server on the host machine:
- On Linux: Use `--network host` or the host's IP address
- On Mac/Windows: Use `host.docker.internal` as the server name

Example:
```bash
docker run -e SQL_CONNECTION_STRING="Server=host.docker.internal;Database=MyDB;User Id=sa;Password=MyPassword;" king-sql-reflection
```