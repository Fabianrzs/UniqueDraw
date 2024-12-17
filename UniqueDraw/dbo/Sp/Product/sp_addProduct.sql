CREATE PROCEDURE sp_addProduct
    @Id UNIQUEIDENTIFIER,
    @Name VARCHAR(50),
    @Description VARCHAR(MAX),
    @Price DECIMAL,
    @ClientId UNIQUEIDENTIFIER,
    @CreatedOn DATETIME,
    @LastModifiedOn DATETIME = NULL,
    @IsActive INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO [dbo].[Products] (Id, [Name], [Description], Price, ClientId, CreatedOn, LastModifiedOn, IsActive)
        VALUES (@Id, @Name, @Description, @Price, @ClientId, @CreatedOn, @LastModifiedOn, @IsActive);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END