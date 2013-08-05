/* Rights */
insert into Rights (Id, Name) values ('ff4cf230-2bbe-4019-b24e-6770e887bc8f', 'CreateCompetition')
insert into Rights (Id, Name) values ('190b0590-790f-4175-b489-aec8cde5247e', 'DeleteCompetition')

insert into Rights (Id, Name) values ('bf0d2977-6ad9-461b-8314-806716113367', 'ReadCompetitors')
insert into Rights (Id, Name) values ('79f52384-a1f0-4502-9cee-b277f6e426f2', 'UpdateCompetitors')
insert into Rights (Id, Name) values ('d838dca6-6cb8-41e9-94bc-d9ee2ce3c86e', 'DeleteCompetitors')

insert into Rights (Id, Name) values ('dd61f0a9-36e9-468b-a15e-eba5996e1683', 'AddClub')
insert into Rights (Id, Name) values ('7d710ee1-b8c8-4dff-9b5c-77c6d9aea32e', 'DeleteClub')
insert into Rights (Id, Name) values ('942a97d7-7632-43a4-b03f-ba4acbcc0149', 'UpdateClub')

insert into Rights (Id, Name) values ('98e5a194-5124-4198-b5be-1976460286be', 'ReadCompetitorResults')
insert into Rights (Id, Name) values ('6ef37d38-c55a-4bf4-9d01-c757533a8340', 'AddCompetitorResults')
insert into Rights (Id, Name) values ('ab34ab67-f48d-44c1-b7de-81ce60c98c40', 'UpdateCompetitorResults')
insert into Rights (Id, Name) values ('f67b15e9-2fd3-4d99-8015-3fa92c0f65a5', 'DeleteCompetitorResults')

insert into Rights (Id, Name) values ('9c57c1b8-b281-4f8c-a747-4cf1ed5badb4', 'GetCompetitionResults')

/* Role Owner */
insert into Roles (Id, Role) values ('21e30b68-2b58-4252-9858-7f5cbcc29a66', 'CompetitionOwner')
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where Role='CompetitionOwner'), (select Id from Rights where Name='DeleteCompetition'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where Role='CompetitionOwner'), (select Id from Rights where Name='ReadCompetitors'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where Role='CompetitionOwner'), (select Id from Rights where Name='UpdateCompetitors'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where Role='CompetitionOwner'), (select Id from Rights where Name='DeleteCompetitors'))

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where Role='CompetitionOwner'), (select Id from Rights where Name='ReadCompetitorResults'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where Role='CompetitionOwner'), (select Id from Rights where Name='AddCompetitorResults'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where Role='CompetitionOwner'), (select Id from Rights where Name='UpdateCompetitorResults'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where Role='CompetitionOwner'), (select Id from Rights where Name='DeleteCompetitorResults'))

/* Role Reader */
insert into Roles (Id, Role) values ('2a846801-5ae3-4318-8c54-68709f15f8a9', 'Reader')

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where Role='Reader'), (select Id from Rights where Name='ReadCompetitors'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where Role='Reader'), (select Id from Rights where Name='GetCompetitionResults'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where Role='Reader'), (select Id from Rights where Name='ReadCompetitorResults'))

