using FluentMigrator;

namespace WinShooter_Web.DatabaseMigrations.Migrations.Iteration1
{
    [Migration(11)]
    public class Migration11CreateStationsTable : Migration
    {
        private const string StationsTableName = "Stations";

        public override void Up()
        {
            Create.Table(StationsTableName)
                .WithColumn("Id").AsGuid()
                .WithColumn("CompetitionId").AsGuid()
                .WithColumn("NumberOfTargets").AsInt32()
                .WithColumn("NumberOfShots").AsInt32()
                .WithColumn("Points").AsInt32()
                .WithColumn("Distinguish").AsBoolean()
                .WithColumn("StationNumber").AsInt32();
        }

        public override void Down()
        {
            Delete.Table(StationsTableName);
        }
    }
}
