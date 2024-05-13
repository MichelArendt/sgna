using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using ServerAPI.Server.Models.Settings;
using System;

namespace ServerAPI.Server.Services
{
    public enum DatabaseProvider { MySQL, SQLServer };

    public class AppSettingsService
    {
        string environment;

        public AppSettingsService(IConfiguration configuration)
        {
            //Configuration = configuration;
            environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? environment;
            AppSettings = configuration.GetSection("AppSettings").Get<AppSettingsModel>();
            DatabaseProvider = GetDatabaseProvider();
            ConnectionString = GetDatabaseConnectionString();
        }

        //private IConfiguration Configuration { get; }
        public AppSettingsModel AppSettings { get; set; }
        public DatabaseProvider DatabaseProvider { get; set; }
        public string ConnectionString { get; set; }

        private DatabaseProvider GetDatabaseProvider()
        {
            switch (AppSettings.Database.Type)
            {
                case "SQLServer":
                    return DatabaseProvider.SQLServer;

                default:
                    return DatabaseProvider.MySQL;
            }
        }

        private string GetDatabaseConnectionString()
        {
            switch (DatabaseProvider)
            {
                case DatabaseProvider.SQLServer:
                    return AppSettings.Database.ConnectionString.SQLServer;

                default: // MySQL
                    return AppSettings.Database.ConnectionString.MySQL;
            }
        }

        public bool EnvironmentIsDevelopment()
        {
            return environment == "Development";
        }

        public DbContextOptionsBuilder GetDatabaseOptions()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            GetDatabaseOptions(ref optionsBuilder);

            return optionsBuilder;
        }

        public void GetDatabaseOptions(ref DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.
                UseMySql(ConnectionString,
                    mySqlOptions =>
                    {
                        mySqlOptions
                            .CharSetBehavior(CharSetBehavior.AppendToAllColumns)
                            .AnsiCharSet(CharSet.Utf8mb4)
                            .UnicodeCharSet(CharSet.Utf8mb4);
                    }

            );
        }
    }
}
