CREATE TABLE [dbo].[Users] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [ClientId]       UNIQUEIDENTIFIER NOT NULL,
    [Name]           NVARCHAR (MAX)   NOT NULL,
    [Email]          NVARCHAR (MAX)   NULL,
    [Phone]          NVARCHAR (MAX)   NULL,
    [CreatedOn]      DATETIME2 (7)    DEFAULT (getdate()) NOT NULL,
    [LastModifiedOn] DATETIME2 (7)    NOT NULL,
    [IsActive]       BIT              NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Users_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Users_ClientId]
    ON [dbo].[Users]([ClientId] ASC);

