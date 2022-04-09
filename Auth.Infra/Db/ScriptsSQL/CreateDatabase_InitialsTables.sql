---------------------------------------------------------------------------------------------------------------------------
/* Create Database */
---------------------------------------------------------------------------------------------------------------------------

CREATE DATABASE authdb;

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

CREATE TABLE EmailType(EmailTypeId INT NOT NULL IDENTITY(1,1),
					   EmailTypeName VARCHAR(30) NOT NULL,
					   CONSTRAINT PK_EmailType PRIMARY KEY (EmailTypeId),
					   CONSTRAINT UQ_EmailType UNIQUE (EmailTypeName))

CREATE TABLE EmailsSents(EmailSentId INT NOT NULL IDENTITY(1,1),
						 EmailTypeId INT NOT NULL,
						 SenderEmail VARCHAR(50) NOT NULL,
						 RecipientEmail VARCHAR(50) NOT NULL,
						 SubjectEmail VARCHAR(100) NOT NULL,
						 Content VARCHAR(7000) NOT NULL,
						 ContentIsHtml BIT NOT NULL,
						 SendDate DATETIME NOT NULL,
						 VerificationCode INT NULL,
						 ValidatedCode BIT NULL
						 CONSTRAINT PK_EmailsSents PRIMARY KEY (EmailSentId),
						 CONSTRAINT FK_EmailsSents_EmailTypeId FOREIGN KEY (EmailTypeId) REFERENCES EmailType(EmailTypeId))