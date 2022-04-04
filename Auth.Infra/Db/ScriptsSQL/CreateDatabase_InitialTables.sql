---------------------------------------------------------------------------------------------------------------------------
/* Create Database */
---------------------------------------------------------------------------------------------------------------------------

CREATE DATABASE authdb;

---------------------------------------------------------------------------------------------------------------------------
/* Create Initial Tables */
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