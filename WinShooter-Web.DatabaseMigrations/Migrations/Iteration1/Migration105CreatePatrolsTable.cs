using FluentMigrator;

namespace WinShooter_Web.DatabaseMigrations.Migrations.Iteration1
{
    [Migration(105)]
    public class Migration105CreatePatrolsTable : Migration
    {
        public const string PatrolsTableName = "Patrols";

        public override void Up()
        {
            Create.Table(PatrolsTableName)
                .WithColumn("Id").AsGuid().PrimaryKey().Indexed()
                .WithColumn("PatrolId").AsInt32()
                .WithColumn("StartTime").AsDateTime()
                .WithColumn("CompetitionId").AsGuid()
                .WithColumn("PatrolClass").AsInt32()
                .WithColumn("StartTimeDisplay").AsDateTime();
        }

        public override void Down()
        {
            Delete.Table(PatrolsTableName);
        }
    }
}
