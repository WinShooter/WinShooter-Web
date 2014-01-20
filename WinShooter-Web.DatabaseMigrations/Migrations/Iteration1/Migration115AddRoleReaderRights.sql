/* Role Reader */
insert into Roles (Id, RoleName) values ('2a846801-5ae3-4318-8c54-68709f15f8a9', 'CompetitionReader')

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionReader'), (select Id from Rights where Name='ReadCompetition'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionReader'), (select Id from Rights where Name='ReadClub'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionReader'), (select Id from Rights where Name='ReadPatrol'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionReader'), (select Id from Rights where Name='ReadWeapon'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionReader'), (select Id from Rights where Name='ReadStation'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionReader'), (select Id from Rights where Name='ReadShooter'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionReader'), (select Id from Rights where Name='ReadTeam'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionReader'), (select Id from Rights where Name='ReadTeamToCompetitor'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionReader'), (select Id from Rights where Name='ReadCompetitorResult'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionReader'), (select Id from Rights where Name='GetCompetitionResults'))
