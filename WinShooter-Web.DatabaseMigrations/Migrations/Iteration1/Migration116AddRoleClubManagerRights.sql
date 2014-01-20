/* Club Manager */
insert into Roles (Id, RoleName) values ('5494735A-114A-4D00-B8B0-EA82CE94A6B1', 'SystemClubManager')

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemClubManager'), (select Id from Rights where Name='CreateClub'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemClubManager'), (select Id from Rights where Name='ReadClub'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemClubManager'), (select Id from Rights where Name='UpdateClub'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemClubManager'), (select Id from Rights where Name='DeleteClub'))
