using FluentMigrator;

namespace WinShooter_Web.DatabaseMigrations.Migrations.Iteration1
{
    [Migration(106)]
    public class Migration106CreateCompetitorsTable : Migration
    {
        public const string CompetitorsTableName = "Competitors";

        public override void Up()
        {
            Create.Table(CompetitorsTableName)
                .WithColumn("Id").AsGuid().PrimaryKey().Indexed()
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
