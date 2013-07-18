using FluentMigrator;

namespace WinShooter_Web.DatabaseMigrations.Migrations.Iteration1
{
    [Migration(107)]
    public class Migration107CreateCompetitorResultsTable : Migration
    {
        public const string CompetitorResultsTableName = "CompetitorResults";

        public override void Up()
        {
            Create.Table(CompetitorResultsTableName)
                .WithColumn("Id").AsGuid().PrimaryKey().Indexed()
                .WithColumn("CompetitorId").AsGuid()
                .WithColumn("StationId").AsGuid()
                .WithColumn("Points").AsInt32()
                .WithColumn("TargetHits").AsInt32();
        }

        public override void Down()
        {
            Delete.Table(CompetitorResultsTableName);
        }
    }
}
