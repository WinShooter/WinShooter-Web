using FluentMigrator;

namespace WinShooter_Web.DatabaseMigrations.Migrations.Iteration1
{
    [Migration(19)]
    public class Migration19CreateTeamToCompetitorTable : Migration
    {
        public const string TeamToCompetitorTableName = "TeamToCompetitor";

        public override void Up()
        {
            Create.Table(TeamToCompetitorTableName)
                .WithColumn("Id").AsGuid()
                .WithColumn("TeamId").AsGuid()
                .WithColumn("CompetitorId").AsGuid();
        }

        public override void Down()
        {
            Delete.Table(TeamToCompetitorTableName);
        }
    }
}
