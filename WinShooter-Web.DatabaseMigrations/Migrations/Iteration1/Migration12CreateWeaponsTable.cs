using FluentMigrator;

namespace WinShooter_Web.DatabaseMigrations.Migrations.Iteration1
{
    [Migration(12)]
    public class Migration12CreateWeaponsTable : Migration
    {
        private const string WeaponsTableName = "Weapons";

        public override void Up()
        {
            Create.Table(WeaponsTableName)
                .WithColumn("Id").AsGuid().PrimaryKey().Indexed()
                .WithColumn("Manufacturer").AsString()
                .WithColumn("Model").AsString()
                .WithColumn("Caliber").AsString()
                .WithColumn("Class").AsInt32()
                .WithColumn("LastUpdated").AsDateTime();
        }

        public override void Down()
        {
            Delete.Table(WeaponsTableName);
        }
    }
}
