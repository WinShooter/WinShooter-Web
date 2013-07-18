﻿using FluentMigrator;

namespace WinShooter_Web.DatabaseMigrations.Migrations.Iteration1
{
    [Migration(108)]
    public class Migration108CreateTeamsTable : Migration
    {
        public const string TeamsTableName = "Teams";

        public override void Up()
        {
            Create.Table(TeamsTableName)
                .WithColumn("Id").AsGuid().PrimaryKey().Indexed()
                .WithColumn("Name").AsString()
                .WithColumn("ClubId").AsGuid()
                .WithColumn("Weaponclass").AsInt32()
                .WithColumn("CompetitionId").AsGuid();
        }

        public override void Down()
        {
            Delete.Table(TeamsTableName);
        }
    }
}