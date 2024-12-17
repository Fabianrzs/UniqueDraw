CREATE TABLE [dbo].[_MigrationHistory] (
    [MigrationId]    NVARCHAR (150) NOT NULL,
    [ProductVersion] NVARCHAR (32)  NOT NULL,
    CONSTRAINT [PK__MigrationHistory] PRIMARY KEY CLUSTERED ([MigrationId] ASC)
);

