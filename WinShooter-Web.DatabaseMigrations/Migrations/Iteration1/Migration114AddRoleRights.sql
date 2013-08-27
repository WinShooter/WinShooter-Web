/* Rights */
insert into Rights (Id, Name) values ('ff4cf230-2bbe-4019-b24e-6770e887bc8f', 'CreateCompetition')
insert into Rights (Id, Name) values ('126F6075-91F0-4626-9561-7EC817EE0BC9', 'ReadCompetition')
insert into Rights (Id, Name) values ('796CF1EF-015B-4DCF-B61A-4B4D30A12214', 'UpdateCompetition')
insert into Rights (Id, Name) values ('190b0590-790f-4175-b489-aec8cde5247e', 'DeleteCompetition')

insert into Rights (Id, Name) values ('dd61f0a9-36e9-468b-a15e-eba5996e1683', 'CreateClub')
insert into Rights (Id, Name) values ('7d710ee1-b8c8-4dff-9b5c-77c6d9aea32e', 'ReadClub')
insert into Rights (Id, Name) values ('942a97d7-7632-43a4-b03f-ba4acbcc0149', 'UpdateClub')
insert into Rights (Id, Name) values ('97FB96CE-D326-4580-80B6-4F1EBC775AF2', 'DeleteClub')

insert into Rights (Id, Name) values ('4409DB2C-3F3C-439F-A89B-F69F366D61FA', 'CreatePatrol')
insert into Rights (Id, Name) values ('18A6F811-4793-4955-AEBA-8A93AC962BC4', 'ReadPatrol')
insert into Rights (Id, Name) values ('D4F86D58-52AB-4182-9853-22F3B3A70106', 'UpdatePatrol')
insert into Rights (Id, Name) values ('7C5058E8-F5F1-4904-B095-1EF0D585CDDF', 'DeletePatrol')

insert into Rights (Id, Name) values ('7FBA4318-6129-4CEF-8DEA-556B00CFCED5', 'CreateWeapon')
insert into Rights (Id, Name) values ('00CCB030-1E81-4AAD-834C-56E0BB58F44A', 'ReadWeapon')
insert into Rights (Id, Name) values ('26387570-21A4-4F92-B1BD-69DA367EBD13', 'DeleteWeapon')
insert into Rights (Id, Name) values ('4AD4E5A4-25BA-4FFF-8446-8C1DDF63786B', 'UpdateWeapon')

insert into Rights (Id, Name) values ('980D30FB-ADB0-43A0-9A40-B8F7CF0D8E22', 'CreateStation')
insert into Rights (Id, Name) values ('7F35CAE5-41DC-4DBD-B521-5BE28C33FE67', 'ReadStation')
insert into Rights (Id, Name) values ('FFA34DB5-C3AE-44FC-AE03-27BB421362AD', 'DeleteStation')
insert into Rights (Id, Name) values ('149B9EFD-D06E-475E-8CE1-793053E2DACA', 'UpdateStation')

insert into Rights (Id, Name) values ('FA0D5D3E-30C1-4FB1-8A85-29E29D94B23C', 'CreateShooter')
insert into Rights (Id, Name) values ('40793FC8-93FA-4656-888B-9D8E5E7EBCA4', 'ReadShooter')
insert into Rights (Id, Name) values ('ADA93D79-5204-4DE6-9567-457C4B212E23', 'UpdateShooter')
insert into Rights (Id, Name) values ('AC50DDB0-9FDD-4D44-BAB0-FD4436354A13', 'DeleteShooter')

insert into Rights (Id, Name) values ('79f52384-a1f0-4502-9cee-b277f6e426f2', 'CreateCompetitor')
insert into Rights (Id, Name) values ('bf0d2977-6ad9-461b-8314-806716113367', 'ReadCompetitor')
insert into Rights (Id, Name) values ('DDCA152F-4268-4775-B9CE-D94204F06279', 'UpdateCompetitor')
insert into Rights (Id, Name) values ('d838dca6-6cb8-41e9-94bc-d9ee2ce3c86e', 'DeleteCompetitor')

insert into Rights (Id, Name) values ('FAA15DDA-8B82-4C3D-8316-0D15F5B13984', 'CreateTeam')
insert into Rights (Id, Name) values ('8758CF85-3CD2-4AA8-9040-E269E8B51C2E', 'ReadTeam')
insert into Rights (Id, Name) values ('BC33C323-14A6-4600-B66B-D1E36D4ACC8A', 'UpdateTeam')
insert into Rights (Id, Name) values ('DA4A981F-4F52-4807-8E22-55B588CCE1DE', 'DeleteTeam')

insert into Rights (Id, Name) values ('C511B8A5-0A60-4234-81A6-51B85E77D878', 'CreateTeamToCompetitor')
insert into Rights (Id, Name) values ('52DC479D-BFF6-42F1-A47E-52CD62B4379C', 'ReadTeamToCompetitor')
insert into Rights (Id, Name) values ('785581A8-DEC0-4CFD-B3FE-7AF63628A05A', 'UpdateTeamToCompetitor')
insert into Rights (Id, Name) values ('71FD0255-6992-4ACF-BCBE-488BAD56EC7E', 'DeleteTeamToCompetitor')

insert into Rights (Id, Name) values ('6ef37d38-c55a-4bf4-9d01-c757533a8340', 'CreateCompetitorResult')
insert into Rights (Id, Name) values ('98e5a194-5124-4198-b5be-1976460286be', 'ReadCompetitorResult')
insert into Rights (Id, Name) values ('ab34ab67-f48d-44c1-b7de-81ce60c98c40', 'UpdateCompetitorResult')
insert into Rights (Id, Name) values ('f67b15e9-2fd3-4d99-8015-3fa92c0f65a5', 'DeleteCompetitorResult')

insert into Rights (Id, Name) values ('9c57c1b8-b281-4f8c-a747-4cf1ed5badb4', 'GetCompetitionResults')

insert into Rights (Id, Name) values ('D872C5E1-FDF6-4746-A1D4-28C62367896F', 'CreateUserCompetitionRole')
insert into Rights (Id, Name) values ('420A58ED-A14A-4AFD-B3E1-61D1DF9EA874', 'ReadUserCompetitionRole')
insert into Rights (Id, Name) values ('5E4C2FAA-56FA-4377-8A41-BCA459BF8242', 'UpdateUserCompetitionRole')
insert into Rights (Id, Name) values ('697BD26D-6AB8-4378-A4F4-2A283A395046', 'DeleteUserCompetitionRole')

/* Role Owner */
insert into Roles (Id, RoleName) values ('21e30b68-2b58-4252-9858-7f5cbcc29a66', 'CompetitionOwner')
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='ReadCompetition'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='UpdateCompetition'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='DeleteCompetition'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='CreateClub'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='ReadClub'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='UpdateClub'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='DeleteClub'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='CreatePatrol'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='ReadPatrol'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='UpdatePatrol'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='DeletePatrol'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='CreateWeapon'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='ReadWeapon'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='UpdateWeapon'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionOwner'), (select Id from Rights where Name='DeleteWeapon'))

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

/* Role Owner */
insert into Roles (Id, RoleName) values ('D03870A9-C138-4788-A200-BDA757E17905', 'CompetitionStationManager')
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionStationManager'), (select Id from Rights where Name='ReadCompetition'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionStationManager'), (select Id from Rights where Name='CreateStation'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionStationManager'), (select Id from Rights where Name='ReadStation'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionStationManager'), (select Id from Rights where Name='UpdateStation'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionStationManager'), (select Id from Rights where Name='DeleteStation'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='CompetitionShooterManager'), (select Id from Rights where Name='GetCompetitionResults'))
