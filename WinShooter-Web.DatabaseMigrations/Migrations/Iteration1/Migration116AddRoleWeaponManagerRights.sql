/* Weapon Manager */
insert into Roles (Id, RoleName) values ('DFF79A10-37AA-4F6C-934A-4EF927556148', 'SystemWeaponManager')

insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemWeaponManager'), (select Id from Rights where Name='CreateWeapon'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemWeaponManager'), (select Id from Rights where Name='ReadWeapon'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemWeaponManager'), (select Id from Rights where Name='UpdateWeapon'))
insert into RoleRightsInfo (RoleId, RightId) values ((select Id from Roles where RoleName='SystemWeaponManager'), (select Id from Rights where Name='DeleteWeapon'))
