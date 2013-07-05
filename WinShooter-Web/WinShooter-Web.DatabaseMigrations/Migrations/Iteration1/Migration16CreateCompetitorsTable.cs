using FluentMigrator;

namespace WinShooter_Web.DatabaseMigrations.Migrations.Iteration1
{
    [Migration(16)]
    public class Migration16CreateCompetitorsTable : Migration
    {
        public const string CompetitorsTableName = "Competitors";

        public override void Up()
        {
            Create.Table(CompetitorsTableName)
                .WithColumn("Id").AsGuid()
                .WithColumn("ShooterId").AsGuid()
                .WithColumn("ShooterClass").AsInt32()
                .WithColumn("WeaponId").AsGuid()
                .WithColumn("PatrolId").AsGuid()
                .WithColumn("Lane").AsInt32()
                .WithColumn("FinalShootingPlace").AsInt32()
                .WithColumn("CompetitionId").AsGuid();
        }

        public override void Down()
        {
            Delete.Table(CompetitorsTableName);
        }
    }
}
