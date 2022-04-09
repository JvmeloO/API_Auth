---------------------------------------------------------------------------------------------------------------------------
/* Insert Initials Data */
---------------------------------------------------------------------------------------------------------------------------

USE authdb

INSERT INTO Roles(RoleName)
VALUES('Administrador')

--INSERT INTO UserRoles(UserId, RoleId)
--VALUES(1, 1)

INSERT INTO EmailsTypes(EmailTypeName)
VALUES('CodigoRecuperaSenha')