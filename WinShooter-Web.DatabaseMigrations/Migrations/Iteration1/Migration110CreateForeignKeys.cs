// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Migration110CreateForeignKeys.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
//   This program is free software; you can redistribute it and/or
//   modify it under the terms of the GNU General Public License
//   as published by the Free Software Foundation; either version 2
//   of the License, or (at your option) any later version.
//   
//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY OR FITNESS FOR A PARTICULAR PURPOSE. See the
//   GNU General Public License for more details.
//   
//   You should have received a copy of the GNU General Public License
//   along with this program; if not, write to the Free Software
//   Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
// </copyright>
// <summary>
//   The database migration that creates foreign keys.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Web.DatabaseMigrations.Migrations.Iteration1
{
    using FluentMigrator;

    /// <summary>
    /// The database migration that creates foreign keys.
    /// </summary>
    [Migration(110)]
    public class Migration110CreateForeignKeys : Migration
    {
        /// <summary>
        /// The name of the foreign key for connecting table teams to table TeamsToCompetitors.
        /// </summary>
        private const string ForeignKeyTeamsTeamsToCompetitorsName = "FK_Teams_TeamsToCompetitors";

        /// <summary>
        /// The name of the foreign key for connecting table competitors to table TeamsToCompetitors.
        /// </summary>
        private const string ForeignKeyCompetitorsTeamsToCompetitorsName = "FK_Competitors_TeamsToCompetitors";

        /// <summary>
        /// The name of the foreign key for connecting clubs to teams.
        /// </summary>
        private const string ForeignKeyClubsTeamsName = "FK_Clubs_Teams";

        /// <summary>
        /// The name of the foreign key for connecting table Competitors to table CompetitorResults.
        /// </summary>
        private const string ForeignKeyCompetitorsCompetitorResultsName = "FK_Competitors_CompetitorResults";

        /// <summary>
        /// The name of the foreign key for connecting table stations to table CompetitorResults.
        /// </summary>
        private const string ForeignKeyStationsCompetitorResultsName = "FK_Stations_CompetitorResults";

        /// <summary>
        /// The name of the foreign key for connecting table Patrols to table Competitors.
        /// </summary>
        private const string ForeignKeyPatrolsCompetitorsName = "FK_Patrols_Competitors";

        /// <summary>
        /// The name of the foreign key for connecting table Shooters to table Competitions.
        /// </summary>
        private const string ForeignKeyShootersCompetitionName = "FK_Competition_Shooters";

        /// <summary>
        /// The name of the foreign key for connecting table Patrols to table Competitors.
        /// </summary>
        private const string ForeignKeyShootersCompetitorsName = "FK_Shooters_Competitors";

        /// <summary>
        /// The name of the foreign key for connecting table Shooters to Competitors.
        /// </summary>
        private const string ForeignKeyWeaponsCompetitorsName = "FK_Weapons_Competitors";

        /// <summary>
        /// The name of the foreign key for connecting table Competitions to Competitors.
        /// </summary>
        private const string ForeignKeyCompetitionsCompetitorsName = "FK_Competitions_Competitors";

        /// <summary>
        /// The name of the foreign key for connecting table Competitions to Competitors.
        /// </summary>
        private const string ForeignKeyCompetitionPatrolsName = "FK_Competition_Patrols";

        /// <summary>
        /// The name of the foreign key for connecting table Clubs to Shooters.
        /// </summary>
        private const string ForeignKeyClubsShootersName = "FK_Clubs_Shooters";

        /// <summary>
        /// The name of the foreign key for connecting Clubs to table Shooters.
        /// </summary>
        private const string ForeignKeyCompetitionStationsName = "FK_Competition_Stations";

        /// <summary>
        /// The function for upgrading the database.
        /// </summary>
        public override void Up()
        {
            this.Create.ForeignKey(ForeignKeyTeamsTeamsToCompetitorsName)
                .FromTable("TeamToCompetitor").ForeignColumn("TeamId")
                .ToTable("Teams").PrimaryColumn("Id");

            this.Create.ForeignKey(ForeignKeyCompetitorsTeamsToCompetitorsName)
                .FromTable("TeamToCompetitor").ForeignColumn("CompetitorId")
                .ToTable("Competitors").PrimaryColumn("Id");

            this.Create.ForeignKey(ForeignKeyClubsTeamsName)
                .FromTable("Teams").ForeignColumn("ClubId")
                .ToTable("Clubs").PrimaryColumn("Id");
            
            this.Create.ForeignKey(ForeignKeyCompetitorsCompetitorResultsName)
                .FromTable("CompetitorResults").ForeignColumn("CompetitorId")
                .ToTable("Competitors").PrimaryColumn("Id");

            this.Create.ForeignKey(ForeignKeyStationsCompetitorResultsName)
                .FromTable("CompetitorResults").ForeignColumn("StationId")
                .ToTable("Stations").PrimaryColumn("Id");

            this.Create.ForeignKey(ForeignKeyPatrolsCompetitorsName)
                .FromTable("Competitors").ForeignColumn("PatrolId")
                .ToTable("Patrols").PrimaryColumn("Id");

            this.Create.ForeignKey(ForeignKeyShootersCompetitionName)
                .FromTable("Shooters").ForeignColumn("CompetitionId")
                .ToTable("Competition").PrimaryColumn("Id");

            this.Create.ForeignKey(ForeignKeyShootersCompetitorsName)
                .FromTable("Competitors").ForeignColumn("ShooterId")
                .ToTable("Shooters").PrimaryColumn("Id");

            this.Create.ForeignKey(ForeignKeyWeaponsCompetitorsName)
                .FromTable("Competitors").ForeignColumn("WeaponId")
                .ToTable("Weapons").PrimaryColumn("Id");

            this.Create.ForeignKey(ForeignKeyCompetitionsCompetitorsName)
                .FromTable("Competitors").ForeignColumn("CompetitionId")
                .ToTable("Competition").PrimaryColumn("Id");

            this.Create.ForeignKey(ForeignKeyCompetitionPatrolsName)
                .FromTable("Patrols").ForeignColumn("CompetitionId")
                .ToTable("Competition").PrimaryColumn("Id");

            this.Create.ForeignKey(ForeignKeyCompetitionStationsName)
                .FromTable("Stations").ForeignColumn("CompetitionId")
                .ToTable("Competition").PrimaryColumn("Id");

            this.Create.ForeignKey(ForeignKeyClubsShootersName)
                .FromTable("Shooters").ForeignColumn("ClubId")
                .ToTable("Clubs").PrimaryColumn("Id");
        }

        /// <summary>
        /// The function for downgrading the database.
        /// </summary>
        public override void Down()
        {
            this.Delete.ForeignKey(ForeignKeyTeamsTeamsToCompetitorsName);
            this.Delete.ForeignKey(ForeignKeyCompetitorsTeamsToCompetitorsName);
            this.Delete.ForeignKey(ForeignKeyClubsTeamsName);
            this.Delete.ForeignKey(ForeignKeyCompetitorsCompetitorResultsName);
            this.Delete.ForeignKey(ForeignKeyStationsCompetitorResultsName);
            this.Delete.ForeignKey(ForeignKeyPatrolsCompetitorsName);
            this.Delete.ForeignKey(ForeignKeyShootersCompetitorsName);
            this.Delete.ForeignKey(ForeignKeyWeaponsCompetitorsName);
            this.Delete.ForeignKey(ForeignKeyCompetitionsCompetitorsName);
            this.Delete.ForeignKey(ForeignKeyCompetitionPatrolsName);
            this.Delete.ForeignKey(ForeignKeyClubsShootersName);
            this.Delete.ForeignKey(ForeignKeyCompetitionStationsName);
        }
    }
}
