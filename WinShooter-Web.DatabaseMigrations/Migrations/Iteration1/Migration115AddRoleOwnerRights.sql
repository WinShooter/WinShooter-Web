/* Role Owner */
insert into Roles (Id, RoleName) values ('21e30b68-2b58-4252-9858-7f5cbcc29a66', 'CompetitionOwner')
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='ReadCompetition'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='UpdateCompetition'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='DeleteCompetition'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='ReadClub'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='CreatePatrol'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='ReadPatrol'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='UpdatePatrol'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='DeletePatrol'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='ReadWeapon'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='ReadPublicUser'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='CreateStation'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='ReadStation'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='UpdateStation'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='DeleteStation'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='CreateShooter'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='ReadShooter'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='UpdateShooter'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='DeleteShooter'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='CreateTeam'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='ReadTeam'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='UpdateTeam'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='DeleteTeam'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='CreateTeamToCompetitor'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='ReadTeamToCompetitor'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='UpdateTeamToCompetitor'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='DeleteTeamToCompetitor'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='CreateCompetitorResult'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='ReadCompetitorResult'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='UpdateCompetitorResult'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='DeleteCompetitorResult'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='GetCompetitionResults'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='CreateUserCompetitionRole'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='ReadUserCompetitionRole'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='UpdateUserCompetitionRole'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='DeleteUserCompetitionRole'))
