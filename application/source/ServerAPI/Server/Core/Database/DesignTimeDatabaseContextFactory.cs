namespace ServerAPI.Server.Database
{
    // A factory for creating derived DbContext instances.
    // Implement this interface to enable design-time services for context types that do not have a public default constructor.
    // At design-time, derived DbContext instances can be created in order to enable specific design-time experiences such as Migrations.
    // Design-time services will automatically discover implementations of this interface that are in the startup assembly or the same assembly as the derived context.
    //public class DesignTimeDatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    //{
    //    private IConfiguration configuration;
    //    private AppSettingsService appSettings;

    //    public DesignTimeDatabaseContextFactory(IConfiguration configuration, AppSettingsService appSettingsService)
    //    {
    //        this.configuration = configuration;
    //        appSettings = appSettingsService;
    //    }

    //    public DatabaseContext CreateDbContext(string[] args)
    //    {
    //        var builder = new DbContextOptionsBuilder<DatabaseContext>();

    //        builder
    //            .UseMySql(
    //                    appSettings.ConnectionString,
    //                    mysqlOptions => appSettings.GetDatabaseOptions()
    //                );

    //        return new DatabaseContext(appSettings.GetDatabaseOptions()/*, Configuration*/);
    //    }
    //}
}
