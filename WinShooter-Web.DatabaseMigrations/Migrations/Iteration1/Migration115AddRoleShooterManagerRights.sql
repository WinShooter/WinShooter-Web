/* Role ShooterManager */
insert into Roles (Id, RoleName) values ('77950D5B-F398-4666-8607-7C838208FF66', 'CompetitionShooterManager')
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionShooterManager'), (select Id from Rights where Name='ReadCompetition'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionShooterManager'), (select Id from Rights where Name='CreateClub'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionShooterManager'), (select Id from Rights where Name='ReadClub'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionShooterManager'), (select Id from Rights where Name='UpdateClub'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionShooterManager'), (select Id from Rights where Name='DeleteClub'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionShooterManager'), (select Id from Rights where Name='CreatePatrol'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionShooterManager'), (select Id from Rights where Name='ReadPatrol'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionShooterManager'), (select Id from Rights where Name='UpdatePatrol'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionShooterManager'), (select Id from Rights where Name='DeletePatrol'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionShooterManager'), (select Id from Rights where Name='CreateWeapon'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionShooterManager'), (select Id from Rights where Name='ReadWeapon'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionShooterManager'), (select Id from Rights where Name='UpdateWeapon'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionShooterManager'), (select Id from Rights where Name='DeleteWeapon'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionShooterManager'), (select Id from Rights where Name='ReadStation'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionShooterManager'), (select Id from Rights where Name='CreateShooter'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionShooterManager'), (select Id from Rights where Name='ReadShooter'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionShooterManager'), (select Id from Rights where Name='UpdateShooter'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionShooterManager'), (select Id from Rights where Name='DeleteShooter'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionShooterManager'), (select Id from Rights where Name='CreateTeam'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionShooterManager'), (select Id from Rights where Name='ReadTeam'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionShooterManager'), (select Id from Rights where Name='UpdateTeam'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionShooterManager'), (select Id from Rights where Name='DeleteTeam'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionShooterManager'), (select Id from Rights where Name='CreateTeamToCompetitor'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionShooterManager'), (select Id from Rights where Name='ReadTeamToCompetitor'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionShooterManager'), (select Id from Rights where Name='UpdateTeamToCompetitor'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionShooterManager'), (select Id from Rights where Name='DeleteTeamToCompetitor'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionShooterManager'), (select Id from Rights where Name='CreateCompetitorResult'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionShooterManager'), (select Id from Rights where Name='ReadCompetitorResult'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionShooterManager'), (select Id from Rights where Name='UpdateCompetitorResult'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionShooterManager'), (select Id from Rights where Name='DeleteCompetitorResult'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionShooterManager'), (select Id from Rights where Name='GetCompetitionResults'))
