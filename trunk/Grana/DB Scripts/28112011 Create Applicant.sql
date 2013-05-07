CREATE TABLE dbo.Applicant
	(
	ApplicantId int NOT NULL IDENTITY (1, 1),
	FirstName varchar(50) NOT NULL,
	MiddleNames varchar(50) NULL,
	LastName varchar(50) NOT NULL,
	Gender varchar(50) NOT NULL,
	BirthDate smalldatetime NOT NULL,
	HomeStatus bit NOT NULL,
	MaritalStatus varchar(50) NOT NULL,
	NumberOfDependants int NOT NULL,
	OwnCar bit NOT NULL,
	CarLicense varchar(50) NULL,
	SocialSecurityNumber varchar(50) NOT NULL,
	IdentityNumber varchar(50) NOT NULL,
	UserId uniqueidentifier NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Applicant ADD CONSTRAINT
	PK_Applicant PRIMARY KEY CLUSTERED 
	(
	ApplicantId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Applicant ADD CONSTRAINT
	FK_Membership_Applicant FOREIGN KEY
	(
	UserId
	) REFERENCES dbo.aspnet_Membership
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO


--Create Employment Table

GO
CREATE TABLE dbo.Employment
	(
	EmploymentId int NOT NULL IDENTITY (1, 1),
	EmploymentStatus varchar(50) NOT NULL,
	EmployersName varchar(50) NOT NULL,
	MonthlyIncome int NOT NULL,
	Industry varchar(50) NOT NULL,
	TimeAtEmployer int NOT NULL,
	Position varchar(150) NOT NULL,
	WorkPhone varchar(50) NULL,
	DirectDebit bit NOT NULL,
	IncomeFrequency varchar(50) NULL,
	PayDate datetime NOT NULL,
	DateAdded datetime NOT NULL,
	UserAdderId uniqueidentifier NOT NULL,
	ApplicantId int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Employment ADD CONSTRAINT
	PK_Table_1 PRIMARY KEY CLUSTERED 
	(
	EmploymentId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Employment ADD CONSTRAINT
	FK_User_Employment FOREIGN KEY
	(
	UserAdderId
	) REFERENCES dbo.aspnet_Membership
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Employment ADD CONSTRAINT
	FK_Applicant_Employment FOREIGN KEY
	(
	ApplicantId
	) REFERENCES dbo.Applicant
	(
	ApplicantId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

--CREATE Address table

CREATE TABLE dbo.Address
	(
	AddressId int NOT NULL IDENTITY (1, 1),
	Postcode varchar(50) NOT NULL,
	State varchar(50) NOT NULL,
	City varchar(50) NOT NULL,
	Neighborhood varchar(50) NOT NULL,
	Address varchar(200) NOT NULL,
	Number varchar(50) NOT NULL,
	Complement varchar(50) NULL,
	DateAdded datetime NOT NULL,
	UserAdderId uniqueidentifier NOT NULL,
	ApplicantId int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Address ADD CONSTRAINT
	PK_Address PRIMARY KEY CLUSTERED 
	(
	AddressId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Address ADD CONSTRAINT
	FK_User_Address FOREIGN KEY
	(
	UserAdderId
	) REFERENCES dbo.aspnet_Membership
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Address ADD CONSTRAINT
	FK_Applicant_Address FOREIGN KEY
	(
	ApplicantId
	) REFERENCES dbo.Applicant
	(
	ApplicantId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

--ContactType

CREATE TABLE dbo.ContactType
	(
	ContactTypeId int NOT NULL IDENTITY (1, 1),
	Name varchar(50) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.ContactType ADD CONSTRAINT
	PK_ContactType PRIMARY KEY CLUSTERED 
	(
	ContactTypeId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
--Add Types

INSERT INTO [Grana].[dbo].[ContactType]
           ([Name])
     VALUES
           ('MobilePhone')
GO
INSERT INTO [Grana].[dbo].[ContactType]
           ([Name])
     VALUES
           ('HomePhone')
GO
INSERT INTO [Grana].[dbo].[ContactType]
           ([Name])
     VALUES
           ('EmailAddress')
GO


--CREATE Contact Table

GO
CREATE TABLE dbo.Contact
	(
	ContactId int NOT NULL IDENTITY (1, 1),
	ContactTypeId int NOT NULL,
	ContactDetail varchar(150) NOT NULL,
	DateAdded datetime NOT NULL,
	ApplicantId int NOT NULL,
	UserAdderId uniqueidentifier NOT NULL,
	ApplicationId int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Contact ADD CONSTRAINT
	PK_Contact PRIMARY KEY CLUSTERED 
	(
	ContactId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Contact ADD CONSTRAINT
	FK_ContactType_Contact FOREIGN KEY
	(
	ContactTypeId
	) REFERENCES dbo.ContactType
	(
	ContactTypeId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Contact ADD CONSTRAINT
	FK_Applicant_Contact FOREIGN KEY
	(
	ApplicantId
	) REFERENCES dbo.Applicant
	(
	ApplicantId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Contact ADD CONSTRAINT
	FK_User_Contact FOREIGN KEY
	(
	UserAdderId
	) REFERENCES dbo.aspnet_Membership
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
GO	 
	ALTER TABLE dbo.Contact ADD CONSTRAINT
	FK_Applicantion_Contact FOREIGN KEY
	(
	ApplicationId
	) REFERENCES dbo.Application
	(
	ApplicationId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	

GO

CREATE TABLE dbo.BankAgency (
 BankAgencyId int IDENTITY(1, 1),
 Cnpj float,
 SequencialCnpj float,
 DvCnpj float,
 EntityName nvarchar(255),
 Segment nvarchar(255),
 AgencyCode float,
 AgencyName nvarchar(255),
 Address nvarchar(255),
 Complement nvarchar(255),
 Neighborhood nvarchar(255),
 Postcode nvarchar(255),
 City nvarchar(255),
 State nvarchar(255),
 StartDate datetime,
 Ddd float,
 PhoneNumber float
)
ALTER TABLE dbo.BankAgency ADD CONSTRAINT
	PK_BankAgency PRIMARY KEY CLUSTERED 
	(
	BankAgencyId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO



CREATE TABLE dbo.BankAccount
	(
	BankAccountId int NOT NULL IDENTITY (1, 1),
	AccountHolder varchar(80) NOT NULL,
	BankName varchar(50) NOT NULL,
	BankAgencyCode varchar(50) NOT NULL,
	AccountNumber varchar(50) NOT NULL,
	DateAdded datetime NOT NULL,
	UserAdderId uniqueidentifier NOT NULL,
	ApplicantId int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.BankAccount ADD CONSTRAINT
	PK_BankAccount PRIMARY KEY CLUSTERED 
	(
	BankAccountId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.BankAccount ADD CONSTRAINT
	FK_User_BankAccount FOREIGN KEY
	(
	UserAdderId
	) REFERENCES dbo.aspnet_Membership
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.BankAccount ADD CONSTRAINT
	FK_Applicant_BankAccount FOREIGN KEY
	(
	ApplicantId
	) REFERENCES dbo.Applicant
	(
	ApplicantId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO


--CREATE BankCard table

GO
CREATE TABLE dbo.BankCard
	(
	BankCardId int NOT NULL IDENTITY (1, 1),
	CardHolder varchar(50) NULL,
	CardNumber varchar(30) NOT NULL,
	ExpiryDate smalldatetime NOT NULL,
	StartDate smalldatetime NULL,
	SecurityCode varchar(4) NULL,
	DateAdded datetime NOT NULL,
	UserAdderId uniqueidentifier NOT NULL,
	ApplicantId int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.BankCard ADD CONSTRAINT
	PK_BankCard PRIMARY KEY CLUSTERED 
	(
	BankCardId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.BankCard ADD CONSTRAINT
	FK_User_BankCard FOREIGN KEY
	(
	UserAdderId
	) REFERENCES dbo.aspnet_Membership
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.BankCard ADD CONSTRAINT
	FK_Applicant_BankCard FOREIGN KEY
	(
	ApplicantId
	) REFERENCES dbo.Applicant
	(
	ApplicantId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

--CREATE ApplicationStatus table

CREATE TABLE dbo.ApplicationStatus
	(
	ApplicationStatusText varchar(50) NOT NULL,
	Description varchar(50) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.ApplicationStatus ADD CONSTRAINT
	PK_ApplicationStatus PRIMARY KEY CLUSTERED 
	(
	ApplicationStatus
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO


--CREATE application table


CREATE TABLE dbo.Application
	(
	ApplicantionId int NOT NULL IDENTITY (1, 1),
	ApplicantId int NOT NULL,
	AffiliateSource varchar(50) NULL,
	ApplicationIp varchar(50) NULL,
	AppStatus varchar(50) NOT NULL,
	Amount smallmoney NOT NULL,
	PaybackDate smalldatetime NOT NULL,
	ApplicationDate datetime NOT NULL,
	InterestRate float(53) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Application ADD CONSTRAINT
	PK_Application PRIMARY KEY CLUSTERED 
	(
	ApplicantionId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Application ADD CONSTRAINT
	FK_ApplicationStatus_Application FOREIGN KEY
	(
	AppStatus
	) REFERENCES dbo.ApplicationStatus
	(
	ApplicationStatusText
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Application ADD CONSTRAINT
	FK_Applicant_Application FOREIGN KEY
	(
	ApplicantId
	) REFERENCES dbo.Applicant
	(
	ApplicantId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

--Insert ApplicationStatus 

INSERT INTO [Grana].[dbo].[ApplicationStatus]
           ([ApplicationStatusText]
           ,[Description])
     VALUES
           ('New'
           ,'New application status')
GO

INSERT INTO [Grana].[dbo].[ApplicationStatus]
           ([ApplicationStatusText]
           ,[Description])
     VALUES
           ('Approved'
           ,'Approved application status')
GO

INSERT INTO [Grana].[dbo].[ApplicationStatus]
           ([ApplicationStatusText]
           ,[Description])
     VALUES
           ('Declined'
           ,'Approved application status')
GO

INSERT INTO [Grana].[dbo].[ApplicationStatus]
           ([ApplicationStatusText]
           ,[Description])
     VALUES
           ('Undecided'
           ,'Undecided, pending application status')
GO


--Note Table

CREATE TABLE dbo.Note
	(
	NoteId int NOT NULL IDENTITY (1, 1),
	UserAdderId uniqueidentifier NOT NULL,
	ApplicantId int NOT NULL,
	ApplicationId int NOT NULL,
	Text varchar(250) NOT NULL,
	DateAdded datetime NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Note ADD CONSTRAINT
	PK_Note PRIMARY KEY CLUSTERED 
	(
	NoteId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Note ADD CONSTRAINT
	FK_User_Note FOREIGN KEY
	(
	UserAdderId
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Note ADD CONSTRAINT
	FK_Applicant_Note FOREIGN KEY
	(
	ApplicantId
	) REFERENCES dbo.Applicant
	(
	ApplicantId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Note ADD CONSTRAINT
	FK_Application_Note FOREIGN KEY
	(
	ApplicationId
	) REFERENCES dbo.Application
	(
	ApplicationId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

--DOCUMENT TABLE

CREATE TABLE dbo.Document
	(
	DocumentId int NOT NULL IDENTITY (1, 1),
	DocumentName varchar(150) NOT NULL,
	DocumentPath varchar(250) NOT NULL,
	DateAdded datetime NOT NULL,
	ApplicantId int NOT NULL,
	ApplicationId int NOT NULL,
	UserAdderId uniqueidentifier NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Document ADD CONSTRAINT
	PK_Document PRIMARY KEY CLUSTERED 
	(
	DocumentId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Document ADD CONSTRAINT
	FK_User_Document FOREIGN KEY
	(
	UserAdderId
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Document ADD CONSTRAINT
	FK_Application_Document FOREIGN KEY
	(
	ApplicationId
	) REFERENCES dbo.Application
	(
	ApplicationId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Document ADD CONSTRAINT
	FK_Applicant_Document FOREIGN KEY
	(
	ApplicantId
	) REFERENCES dbo.Applicant
	(
	ApplicantId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

--Reason tables
CREATE TABLE dbo.Reason
	(
	ReasonId int NOT NULL IDENTITY (1, 1),
	Description varchar(250) NOT NULL,
	Status varchar(50) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Reason ADD CONSTRAINT
	PK_Reason PRIMARY KEY CLUSTERED 
	(
	ReasonId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

CREATE TABLE dbo.ReasonLog
	(
	ReasonLogId int NOT NULL IDENTITY (1, 1),
	ReasonId int NOT NULL,
	ApplicationId int NOT NULL,
	UserAdderId uniqueidentifier NOT NULL,
	DateLogged datetime NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.ReasonLog ADD CONSTRAINT
	PK_ReasonLog PRIMARY KEY CLUSTERED 
	(
	ReasonLogId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.ReasonLog ADD CONSTRAINT
	FK_Reason_ReasonLog FOREIGN KEY
	(
	ReasonId
	) REFERENCES dbo.Reason
	(
	ReasonId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.ReasonLog ADD CONSTRAINT
	FK_Application_ReasonLog FOREIGN KEY
	(
	ApplicationId
	) REFERENCES dbo.Application
	(
	ApplicationId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.ReasonLog ADD CONSTRAINT
	FK_aspnet_Users_ReasonLog FOREIGN KEY
	(
	UserAdderId
	) REFERENCES dbo.aspnet_Users
	(
	UserId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO