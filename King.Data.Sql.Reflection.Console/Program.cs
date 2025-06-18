using System;
using System.Threading.Tasks;
using King.Data.Sql.Reflection;
using King.Data.Sql.Reflection.Models;
using Newtonsoft.Json;

namespace King.Data.Sql.Reflection.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connectionString = Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING");
            
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                System.Console.WriteLine("Error: SQL_CONNECTION_STRING environment variable is not set.");
                System.Console.WriteLine("Usage: docker run -e SQL_CONNECTION_STRING=\"your_connection_string\" [image_name]");
                Environment.Exit(1);
            }

            try
            {
                System.Console.WriteLine("King.Data.Sql.Reflection - SQL Server Schema Inspector");
                System.Console.WriteLine("========================================================");
                System.Console.WriteLine($"Connecting to SQL Server...");
                
                var reader = new SchemaReader(connectionString);
                
                // Get stored procedures
                System.Console.WriteLine("\nFetching Stored Procedures...");
                var procedures = await reader.Load(SchemaTypes.StoredProcedure);
                
                System.Console.WriteLine($"\nFound {procedures.Count()} stored procedures:");
                foreach (var proc in procedures)
                {
                    System.Console.WriteLine($"\n- {proc.Name} ({proc.Preface})");
                    if (proc.Variables?.Any() == true)
                    {
                        System.Console.WriteLine("  Parameters:");
                        foreach (var param in proc.Variables.OrderBy(v => v.Order))
                        {
                            System.Console.WriteLine($"    {param.ParameterName} {param.DataType}({param.MaxLength}) {(param.IsOutParameter == 1 ? "OUTPUT" : "")}");
                        }
                    }
                }
                
                // Get tables
                System.Console.WriteLine("\n\nFetching Tables...");
                var tables = await reader.Load(SchemaTypes.Table);
                
                System.Console.WriteLine($"\nFound {tables.Count()} tables:");
                foreach (var table in tables)
                {
                    System.Console.WriteLine($"\n- {table.Name} ({table.Preface})");
                    if (table.Variables?.Any() == true)
                    {
                        System.Console.WriteLine("  Columns:");
                        foreach (var column in table.Variables.OrderBy(v => v.Order))
                        {
                            var nullable = column.IsNullable == 1 ? "NULL" : "NOT NULL";
                            System.Console.WriteLine($"    {column.ParameterName} {column.DataType}({column.MaxLength}) {nullable}");
                        }
                    }
                }
                
                // Option to output as JSON
                var outputJson = Environment.GetEnvironmentVariable("OUTPUT_JSON");
                if (!string.IsNullOrWhiteSpace(outputJson) && outputJson.ToLower() == "true")
                {
                    System.Console.WriteLine("\n\nJSON Output:");
                    System.Console.WriteLine("============");
                    
                    var output = new 
                    {
                        StoredProcedures = procedures,
                        Tables = tables
                    };
                    
                    System.Console.WriteLine(JsonConvert.SerializeObject(output, Formatting.Indented));
                }
                
                System.Console.WriteLine("\n\nSchema inspection completed successfully!");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"\nError: {ex.Message}");
                System.Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                Environment.Exit(1);
            }
        }
    }
}