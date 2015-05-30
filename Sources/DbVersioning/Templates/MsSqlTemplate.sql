-- Start checking possibility of using
DECLARE @newDbVersion       VARCHAR(11)
SET @newDbVersion = '0.1'

DECLARE @currentDbVersion       VARCHAR(11)
SET @currentDbVersion = dbo.F_GetCurrentDbVersionAsString()

DECLARE @errorMessage NVARCHAR(500);

IF dbo.F_CompareDbVersionWithCurrent(@newDbVersion) <> 1
BEGIN
    SET @errorMessage = 'Cannot install script ' + @newDbVersion + '. DB version ' + @currentDbVersion + ' expected.'
    RAISERROR(@errorMessage, 20, -1) WITH LOG
END
GO
-- End checking possibility of using

-- BEGIN CHANGES
-- SQL CHANGES
-- END CHANGES

-- Start writing new version info 
GO
IF @@ERROR <> 0
BEGIN
    DECLARE @errorMessage VARCHAR(MAX)
    SET @errorMessage = ERROR_MESSAGE()
    RAISERROR(@errorMessage, 16, 1)
END
ELSE
BEGIN
    DECLARE @newDbVersion       varchar(11)
    SET @newDbVersion = '0.1'
    BEGIN
        EXEC P_AppendDbVersionInfo @newDbVersion, '{Notes}'
         
        PRINT 'Completed successfully'
    END
END
GO
-- End writing new version info