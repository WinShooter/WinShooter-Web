using FluentMigrator;

namespace WinShooter_Web.DatabaseMigrations.Migrations.Iteration1
{
    [Migration(100)]
    public class Migration100CreateCompetitionTable : Migration
    {
        private const string CompetitionTableName = "Competition";

        public override void Up()
        {
            Create.Table(CompetitionTableName)
                .WithColumn("Id").AsGuid().PrimaryKey().Indexed()
                .WithColumn("Name").AsString()
                .WithColumn("StartDate").AsDateTime()
                .WithColumn("CompetitionType").AsInt32()
                .WithColumn("UseNorwegianCount").AsBoolean()
                .WithColumn("IsPublic").AsBoolean();
        }

        public override void Down()
        {
            Delete.Table(CompetitionTableName);
        }
    }
}
