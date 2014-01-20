/* Role resultwriter */
insert into Roles (Id, RoleName) values ('B8AD70F8-EDBE-4F47-B4B7-C63B699EAE8C', 'CompetitionResultWriter')
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionResultWriter'), (select Id from Rights where Name='ReadCompetition'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionResultWriter'), (select Id from Rights where Name='ReadClub'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionResultWriter'), (select Id from Rights where Name='ReadPatrol'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionResultWriter'), (select Id from Rights where Name='ReadWeapon'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionResultWriter'), (select Id from Rights where Name='ReadStation'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionResultWriter'), (select Id from Rights where Name='ReadShooter'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionResultWriter'), (select Id from Rights where Name='ReadTeam'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionResultWriter'), (select Id from Rights where Name='ReadTeamToCompetitor'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionResultWriter'), (select Id from Rights where Name='CreateCompetitorResult'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionResultWriter'), (select Id from Rights where Name='ReadCompetitorResult'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionResultWriter'), (select Id from Rights where Name='UpdateCompetitorResult'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionResultWriter'), (select Id from Rights where Name='DeleteCompetitorResult'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionResultWriter'), (select Id from Rights where Name='GetCompetitionResults'))
