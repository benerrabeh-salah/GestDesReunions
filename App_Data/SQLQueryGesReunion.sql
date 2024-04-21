-- Création de la base de données "GesReunions"
CREATE DATABASE GesReunions;
GO

-- Utilisation de la base de données "GesReunions"
USE GesReunions;
GO

-- Création de la table "UserTbl"
CREATE TABLE [dbo].[UserTbl] (
    [Id]         INT          IDENTITY (1, 1) NOT NULL,
    [username]   VARCHAR (50) NULL,
    [password]   VARCHAR (50) NULL,
    [isActive]   BIT          NULL,
    [isAdmin]    BIT          NULL,
    [consult_]   BIT          NULL,
    [input_]     BIT          NULL,
    [edit_]      BIT          NULL,
    [delete_]    BIT          NULL,
    [print_]     BIT          NULL,
    [nbAccess]   INT          NULL,
    [lastAccess] DATETIME     NULL,
	[instruction_] BIT        NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO

-- Création de la table "ReunionTbl"
CREATE TABLE [dbo].[ReunionTbl] (
    [Id]                   INT            IDENTITY (1, 1) NOT NULL,
    [date_reunion]         DATE           NULL,
    [sujet_reunion]        NVARCHAR (MAX) NULL,
    [division]             VARCHAR (20)   NULL,
    [recommendation]       NVARCHAR (MAX) NULL,
    [proch_reunion]        DATE           NULL,
    [saisi_par]            VARCHAR (10)   NULL,
    [date_de_saisie]       DATETIME       NULL,
    [date_de_modification] DATETIME       NULL,
	[cadre]                VARCHAR (50)   NULL,
	[objet]                NVARCHAR (250) NULL,
	[idcadre]              INT            NULL,
	[cout_cadre]           NVARCHAR(max)  NULL,
    [secteur]              NVARCHAR (50)  NULL,
    [partenaire]           NVARCHAR (50)  NULL,
	[contribution_partenaire] NVARCHAR(max) NULL,
    [statut]               VARCHAR (50)   NULL,
	[etat_avancement]      NVARCHAR (max) NULL,
	[supprimer]            VARCHAR(10)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO




-- Création de la table "StatutTbl"
CREATE TABLE [dbo].[StatutTbl] (
    [Id]     INT          IDENTITY (1, 1) NOT NULL,
    [status] VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

-- Création de la table "PartenaireTbl"
CREATE TABLE [dbo].[PartenaireTbl] (
    [Id]             INT          IDENTITY (1, 1) NOT NULL,
    [nom_partenaire] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

-- Création de la table "SecteurTbl"
CREATE TABLE [dbo].[SecteurTbl] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [nom_secteur] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

Create table [dbo].[DivisionTbl] (
 [Id]          INT          IDENTITY (1, 1) NOT NULL,
 [nom_division] varchar(20) not null
 PRIMARY KEY CLUSTERED ([Id] ASC)
);


Create table [dbo].[InstructionTbl] (
 [Id]  INT PRIMARY KEY Identity (1,1)  NOT NULL,
 [IdReunion]  INT  NOT NULL,
 [Objet] NVARCHAR(max) NOT NULL
);


CREATE TABLE UploadedFiles (
    FileID INT PRIMARY KEY IDENTITY,
	ReunionId INT NOT NULL,
    FileName_ NVARCHAR(255) ,
    FileContent_ VARBINARY(MAX)
);


select * from UploadedFiles