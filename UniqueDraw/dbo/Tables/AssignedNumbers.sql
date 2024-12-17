CREATE TABLE [dbo].[AssignedNumbers] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [ClientId]       UNIQUEIDENTIFIER NOT NULL,
    [UserId]         UNIQUEIDENTIFIER NOT NULL,
    [Number]         NVARCHAR (MAX)   NOT NULL,
    [RaffleId]       UNIQUEIDENTIFIER NOT NULL,
    [CreatedOn]      DATETIME2 (7)    DEFAULT (getdate()) NOT NULL,
    [LastModifiedOn] DATETIME2 (7)    NOT NULL,
    [IsActive]       BIT              NOT NULL,
    CONSTRAINT [PK_AssignedNumbers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AssignedNumbers_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([Id]),
    CONSTRAINT [FK_AssignedNumbers_Raffles_RaffleId] FOREIGN KEY ([RaffleId]) REFERENCES [dbo].[Raffles] ([Id]),
    CONSTRAINT [FK_AssignedNumbers_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_AssignedNumbers_UserId]
    ON [dbo].[AssignedNumbers]([UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AssignedNumbers_RaffleId]
    ON [dbo].[AssignedNumbers]([RaffleId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_AssignedNumbers_ClientId]
    ON [dbo].[AssignedNumbers]([ClientId] ASC);

