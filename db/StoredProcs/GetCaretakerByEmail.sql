SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE NannyDB
GO

-- =============================================
-- Author:		Jason Howie
-- Create date: 10/2/2021
-- Description:	Retrieves a single caretaker by their email address
-- =============================================
Alter PROCEDURE GetCaretakerByEmail
	-- Add the parameters for the stored procedure here
	@EmailAddress NVARCHAR(120) 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *
	FROM caretaker WITH(NOLOCK)
	JOIN address ON caretaker.address_id = address.address_id
	WHERE email_address = @EmailAddress;

END
GO
