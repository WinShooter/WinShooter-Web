using FluentMigrator;

namespace WinShooter_Web.DatabaseMigrations.Migrations.Iteration1
{
    [Migration(104)]
    public class Migration104CreateClubsTable : Migration
    {
        private const string ClubsTableName = "Clubs";

        public override void Up()
        {
            Create.Table(ClubsTableName)
                .WithColumn("Id").AsGuid().PrimaryKey().Indexed()
                .WithColumn("ClubId").AsString()
                .WithColumn("Name").AsString()
                .WithColumn("Country").AsString()
                .WithColumn("Email").AsString()
                .WithColumn("Plusgiro").AsString()
                .WithColumn("Bankgiro").AsString()
                .WithColumn("LastUpdated").AsDateTime();
        }

        public override void Down()
        {
            Delete.Table(ClubsTableName);
        }
    }
}
