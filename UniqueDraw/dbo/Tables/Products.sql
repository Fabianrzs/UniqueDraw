CREATE TABLE [dbo].[Products] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [Name]           NVARCHAR (MAX)   NOT NULL,
    [Description]    NVARCHAR (MAX)   NULL,
    [ClientId]       UNIQUEIDENTIFIER NOT NULL,
    [CreatedOn]      DATETIME2 (7)    DEFAULT (getdate()) NOT NULL,
    [LastModifiedOn] DATETIME2 (7)    NOT NULL,
    [IsActive]       BIT              NOT NULL,
    [Price]          DECIMAL (18, 2)  DEFAULT ((0.0)) NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Products_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_Products_ClientId]
    ON [dbo].[Products]([ClientId] ASC);

