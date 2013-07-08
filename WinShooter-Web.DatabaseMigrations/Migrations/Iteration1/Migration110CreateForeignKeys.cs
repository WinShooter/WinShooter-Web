using FluentMigrator;

namespace WinShooter_Web.DatabaseMigrations.Migrations.Iteration1
{
    [Migration(110)]
    public class Migration110CreateForeignKeys : Migration
    {
        private const string ForeignKeyTeamsTeamsToCompetitorsName = "FK_Teams_TeamsToCompetitors";
        private const string ForeignKeyCompetitorsTeamsToCompetitorsName = "FK_Competitors_TeamsToCompetitors";
        private const string ForeignKeyClubsTeamsName = "FK_Clubs_Teams";
        private const string ForeignKeyCompetitorsCompetitorResultsName = "FK_Competitors_CompetitorResults";
        private const string ForeignKeyStationsCompetitorResultsName = "FK_Stations_CompetitorResults";
        private const string ForeignKeyPatrolsCompetitorsName = "FK_Patrols_Competitors";
        private const string ForeignKeyShootersCompetitorsName = "FK_Shooters_Competitors";
        private const string ForeignKeyWeaponsCompetitorsName = "FK_Weapons_Competitors";
        private const string ForeignKeyCompetitionsCompetitorsName = "FK_Competitions_Competitors";
        private const string ForeignKeyCompetitionPatrolsName = "FK_Competition_Patrols";
        private const string ForeignKeyClubsShootersName = "FK_Clubs_Shooters";
        private const string ForeignKeyCompetitionStationsName = "FK_Competition_Stations";

        public override void Up()
        {
            Create.ForeignKey(ForeignKeyTeamsTeamsToCompetitorsName)
                .FromTable("TeamToCompetitor").ForeignColumn("TeamId")
                .ToTable("Teams").PrimaryColumn("Id");

            Create.ForeignKey(ForeignKeyCompetitorsTeamsToCompetitorsName)
                .FromTable("TeamToCompetitor").ForeignColumn("CompetitorId")
                .ToTable("Competitors").PrimaryColumn("Id");

            Create.ForeignKey(ForeignKeyClubsTeamsName)
                .FromTable("Teams").ForeignColumn("ClubId")
                .ToTable("Clubs").PrimaryColumn("Id");
            
            Create.ForeignKey(ForeignKeyCompetitorsCompetitorResultsName)
                .FromTable("CompetitorResults").ForeignColumn("CompetitorId")
                .ToTable("Competitors").PrimaryColumn("Id");

            Create.ForeignKey(ForeignKeyStationsCompetitorResultsName)
                .FromTable("CompetitorResults").ForeignColumn("StationId")
                .ToTable("Stations").PrimaryColumn("Id");

            Create.ForeignKey(ForeignKeyPatrolsCompetitorsName)
                .FromTable("Competitors").ForeignColumn("PatrolId")
                .ToTable("Patrols").PrimaryColumn("Id");

            Create.ForeignKey(ForeignKeyShootersCompetitorsName)
                .FromTable("Competitors").ForeignColumn("ShooterId")
                .ToTable("Shooters").PrimaryColumn("Id");

            Create.ForeignKey(ForeignKeyWeaponsCompetitorsName)
                .FromTable("Competitors").ForeignColumn("WeaponId")
                .ToTable("Weapons").PrimaryColumn("Id");

            Create.ForeignKey(ForeignKeyCompetitionsCompetitorsName)
                .FromTable("Competitors").ForeignColumn("CompetitionId")
                .ToTable("Competition").PrimaryColumn("Id");

            Create.ForeignKey(ForeignKeyCompetitionPatrolsName)
                .FromTable("Patrols").ForeignColumn("CompetitionId")
                .ToTable("Competition").PrimaryColumn("Id");

            Create.ForeignKey(ForeignKeyCompetitionStationsName)
                .FromTable("Stations").ForeignColumn("CompetitionId")
                .ToTable("Competition").PrimaryColumn("Id");

            Create.ForeignKey(ForeignKeyClubsShootersName)
                .FromTable("Shooters").ForeignColumn("ClubId")
                .ToTable("Clubs").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.ForeignKey(ForeignKeyTeamsTeamsToCompetitorsName);
            Delete.ForeignKey(ForeignKeyCompetitorsTeamsToCompetitorsName);
            Delete.ForeignKey(ForeignKeyClubsTeamsName);
            Delete.ForeignKey(ForeignKeyCompetitorsCompetitorResultsName);
            Delete.ForeignKey(ForeignKeyStationsCompetitorResultsName);
            Delete.ForeignKey(ForeignKeyPatrolsCompetitorsName);
            Delete.ForeignKey(ForeignKeyShootersCompetitorsName);
            Delete.ForeignKey(ForeignKeyWeaponsCompetitorsName);
            Delete.ForeignKey(ForeignKeyCompetitionsCompetitorsName);
            Delete.ForeignKey(ForeignKeyCompetitionPatrolsName);
            Delete.ForeignKey(ForeignKeyClubsShootersName);
            Delete.ForeignKey(ForeignKeyCompetitionStationsName);
        }
    }
}
