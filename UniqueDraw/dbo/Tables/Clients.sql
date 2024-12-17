CREATE TABLE [dbo].[Clients] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [Name]           NVARCHAR (MAX)   NOT NULL,
    [ApiKey]         NVARCHAR (MAX)   NOT NULL,
    [UserName]       NVARCHAR (MAX)   NULL,
    [Password]       NVARCHAR (MAX)   NULL,
    [CreatedOn]      DATETIME2 (7)    DEFAULT (getdate()) NOT NULL,
    [LastModifiedOn] DATETIME2 (7)    NOT NULL,
    [IsActive]       BIT              NOT NULL,
    CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED ([Id] ASC)
);

