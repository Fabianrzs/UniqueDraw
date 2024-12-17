CREATE PROCEDURE [dbo].[sp_updateProduct]
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

        UPDATE [dbo].[Products]
        SET 
            [Name] = ISNULL(@Name, [Name]),
            [Description] = ISNULL(@Description, [Description]),
            Price = ISNULL(@Price, Price),
            ClientId = ISNULL(@ClientId, ClientId),
            LastModifiedOn = ISNULL(@LastModifiedOn, LastModifiedOn),
            IsActive = ISNULL(@IsActive, [IsActive])
        WHERE
            Id = @Id;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END

