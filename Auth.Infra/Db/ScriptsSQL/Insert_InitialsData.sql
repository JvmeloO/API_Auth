---------------------------------------------------------------------------------------------------------------------------
/* Insert Initials Data */
---------------------------------------------------------------------------------------------------------------------------

USE authdb

INSERT INTO Roles(RoleName)
VALUES('Administrador')

--INSERT INTO UserRoles(UserId, RoleId)
--VALUES(1, 1)

INSERT INTO EmailTypes(TypeName)
VALUES('CodigoRecuperaSenha')

--INSERT INTO EmailTemplates(TemplateName, EmailSubject, Content, ContentIsHtml, EmailTypeId)
--VALUES('TemplateRecuperaSenha', 'Código Recuperação de Senha', 'TemplateHtml', 1, 1)