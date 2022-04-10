---------------------------------------------------------------------------------------------------------------------------
/* Create Database */
---------------------------------------------------------------------------------------------------------------------------

CREATE DATABASE authdb;
GO

---------------------------------------------------------------------------------------------------------------------------
/* Create Initials Tables */
---------------------------------------------------------------------------------------------------------------------------

USE authdb;

CREATE TABLE Users(UserId INT NOT NULL IDENTITY(1,1),
				   Username VARCHAR(20) NOT NULL,
				   Email VARCHAR(50) NOT NULL,
				   Password VARCHAR(100) NOT NULL,
				   CONSTRAINT PK_Users PRIMARY KEY (UserId),
				   CONSTRAINT UQ_Users UNIQUE (Username, Email));

CREATE TABLE Roles(RoleId INT NOT NULL IDENTITY(1,1),
			       RoleName VARCHAR(20) NOT NULL,
				   CONSTRAINT PK_Roles PRIMARY KEY (RoleId),
				   CONSTRAINT UQ_Roles UNIQUE (RoleName));

CREATE TABLE UserRoles(UserId INT NOT NULL,
                       RoleId INT NOT NULL,
					   CONSTRAINT PK_UserRoles PRIMARY KEY (UserId, RoleId),
					   CONSTRAINT FK_UserRoles_UserId FOREIGN KEY (UserId) REFERENCES Users(UserId),
					   CONSTRAINT FK_UserRoles_RoleId FOREIGN KEY (RoleId) REFERENCES Roles(RoleId),
					   CONSTRAINT UQ_UserRoles UNIQUE (UserId, RoleId));

CREATE TABLE EmailTypes(EmailTypeId INT NOT NULL IDENTITY(1,1),
					    TypeName VARCHAR(30) NOT NULL,
					    CONSTRAINT PK_EmailTypes PRIMARY KEY (EmailTypeId),
					    CONSTRAINT UQ_EmailTypes UNIQUE (TypeName))

CREATE TABLE EmailTemplates(EmailTemplateId INT NOT NULL IDENTITY(1,1),
							TemplateName VARCHAR(20) NOT NULL,
							EmailSubject VARCHAR(100) NOT NULL,
							Content VARCHAR(MAX) NOT NULL,
							ContentIsHtml BIT NOT NULL,
							EmailTypeId INT NOT NULL,
							CONSTRAINT PK_EmailTemplates PRIMARY KEY (EmailTemplateId),
							CONSTRAINT UQ_EmailTemplates UNIQUE (TemplateName),
							CONSTRAINT FK_EmailTemplates_EmailTypeId FOREIGN KEY (EmailTypeId) REFERENCES EmailTypes(EmailTypeId),
							CONSTRAINT CK_EmailTemplates_Content CHECK (DATALENGTH([Content]) <= 400000))						

CREATE TABLE EmailSents(EmailSentId INT NOT NULL IDENTITY(1,1),
						SenderEmail VARCHAR(50) NOT NULL,
						RecipientEmail VARCHAR(50) NOT NULL,
						SendDate DATETIME NOT NULL,
						VerificationCode VARCHAR(10) NULL,
						ValidatedCode BIT NULL,
						EmailTemplateId INT NOT NULL,
						CONSTRAINT PK_EmailSents PRIMARY KEY (EmailSentId),
						CONSTRAINT FK_EmailSents_EmailTemplateId FOREIGN KEY (EmailTemplateId) REFERENCES EmailTemplates(EmailTemplateId))