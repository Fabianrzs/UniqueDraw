CREATE TABLE [dbo].[Raffles] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [Name]           NVARCHAR (MAX)   NOT NULL,
    [StartDate]      DATETIME2 (7)    NOT NULL,
    [EndDate]        DATETIME2 (7)    NOT NULL,
    [ClientId]       UNIQUEIDENTIFIER NOT NULL,
    [CreatedOn]      DATETIME2 (7)    DEFAULT (getdate()) NOT NULL,
    [LastModifiedOn] DATETIME2 (7)    NOT NULL,
    [IsActive]       BIT              NOT NULL,
    CONSTRAINT [PK_Raffles] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Raffles_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Raffles_ClientId]
    ON [dbo].[Raffles]([ClientId] ASC);

