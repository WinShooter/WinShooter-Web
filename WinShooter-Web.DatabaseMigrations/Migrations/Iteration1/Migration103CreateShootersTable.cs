using FluentMigrator;

namespace WinShooter_Web.DatabaseMigrations.Migrations.Iteration1
{
    [Migration(103)]
    public class Migration103CreateShootersTable : Migration
    {
        private const string ShootersTableName = "Shooters";

        public override void Up()
        {
            Create.Table(ShootersTableName)
                .WithColumn("Id").AsGuid().PrimaryKey().Indexed()
                .WithColumn("CompetitionId").AsGuid().Indexed()
                .WithColumn("CardNumber").AsString()
                .WithColumn("Surname").AsString()
                .WithColumn("Givenname").AsString()
                .WithColumn("Email").AsString()
                .WithColumn("ClubId").AsGuid()
                .WithColumn("Paid").AsInt32()
                .WithColumn("Class").AsInt32()
                .WithColumn("HasArrived").AsBoolean()
                .WithColumn("SendResultsByEmail").AsBoolean()
                .WithColumn("LastUpdated").AsDateTime();
        }

        public override void Down()
        {
            Delete.Table(ShootersTableName);
        }
    }
}
