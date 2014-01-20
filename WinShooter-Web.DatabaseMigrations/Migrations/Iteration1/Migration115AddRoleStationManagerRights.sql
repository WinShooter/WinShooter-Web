/* Role station manager */
insert into Roles (Id, RoleName) values ('D03870A9-C138-4788-A200-BDA757E17905', 'CompetitionStationManager')
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionStationManager'), (select Id from Rights where Name='ReadCompetition'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionStationManager'), (select Id from Rights where Name='CreateStation'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionStationManager'), (select Id from Rights where Name='ReadStation'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionStationManager'), (select Id from Rights where Name='UpdateStation'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionStationManager'), (select Id from Rights where Name='DeleteStation'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionShooterManager'), (select Id from Rights where Name='GetCompetitionResults'))

/* Role System Admin */
insert into Roles (Id, RoleName) values ('1AFA2DB4-3D41-4356-9E5A-09B879D1B16C', 'SystemAdmin')
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='ReadCompetition'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='UpdateCompetition'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='DeleteCompetition'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='CreateClub'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='ReadClub'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='UpdateClub'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='DeleteClub'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='CreatePatrol'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='ReadPatrol'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='UpdatePatrol'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='DeletePatrol'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='CreateWeapon'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='ReadWeapon'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='UpdateWeapon'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='DeleteWeapon'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='CreateUser'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='ReadPublicUser'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='ReadPublicUser'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='UpdateUser'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='DeleteUser'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='CreateStation'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='ReadStation'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='UpdateStation'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='DeleteStation'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='CreateShooter'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='ReadShooter'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='UpdateShooter'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='DeleteShooter'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='CreateTeam'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='ReadTeam'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='UpdateTeam'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='DeleteTeam'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='CreateTeamToCompetitor'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='ReadTeamToCompetitor'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='UpdateTeamToCompetitor'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='DeleteTeamToCompetitor'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='CreateCompetitorResult'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='ReadCompetitorResult'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='UpdateCompetitorResult'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='DeleteCompetitorResult'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='GetCompetitionResults'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='CreateUserCompetitionRole'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='ReadUserCompetitionRole'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='UpdateUserCompetitionRole'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='DeleteUserCompetitionRole'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='CreateUserSystemRole'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='ReadUserSystemRole'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='UpdateUserSystemRole'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemAdmin'), (select Id from Rights where Name='DeleteUserSystemRole'))

/* Weapon Manager */
insert into Roles (Id, RoleName) values ('DFF79A10-37AA-4F6C-934A-4EF927556148', 'SystemWeaponManager')

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemWeaponManager'), (select Id from Rights where Name='CreateWeapon'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemWeaponManager'), (select Id from Rights where Name='ReadWeapon'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemWeaponManager'), (select Id from Rights where Name='UpdateWeapon'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemWeaponManager'), (select Id from Rights where Name='DeleteWeapon'))

/* Club Manager */
insert into Roles (Id, RoleName) values ('5494735A-114A-4D00-B8B0-EA82CE94A6B1', 'SystemClubManager')

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemClubManager'), (select Id from Rights where Name='CreateClub'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemClubManager'), (select Id from Rights where Name='ReadClub'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemClubManager'), (select Id from Rights where Name='UpdateClub'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemClubManager'), (select Id from Rights where Name='DeleteClub'))
