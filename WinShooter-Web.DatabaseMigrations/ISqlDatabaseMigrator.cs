namespace WinShooter_Web.DatabaseMigrations
{
    public interface ISqlDatabaseMigrator
    {
        void MigrateToLatest(string connectionString);
    }
}