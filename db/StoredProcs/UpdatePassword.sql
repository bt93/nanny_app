USE NannyDB
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jason Howie
-- Create date: 10/2/2021
-- Description:	Updates the users password
-- =============================================
Alter PROCEDURE UpdatePassword 
	-- Add the parameters for the stored procedure here
	@Password NVARCHAR(60),
	@Salt NVARCHAR(20),
	@CaretakerID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE caretaker
		SET password = @Password,
        salt = @Salt
        WHERE caretaker_id = @CaretakerID
END
GO
