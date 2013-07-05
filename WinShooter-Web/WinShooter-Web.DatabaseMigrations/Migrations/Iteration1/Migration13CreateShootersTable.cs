using FluentMigrator;

namespace WinShooter_Web.DatabaseMigrations.Migrations.Iteration1
{
    [Migration(13)]
    public class Migration13CreateShootersTable : Migration
    {
        private const string ShootersTableName = "Shooters";

        public override void Up()
        {
            Create.Table(ShootersTableName)
                .WithColumn("Id").AsGuid()
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
