SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
USE NannyDB
GO
-- =============================================
-- Author:		Jason Howie
-- Create date: 10/2/2021
-- Description:	Retrieves all caretakers 
-- =============================================
ALTER PROCEDURE GetAllCaretakers
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT * FROM caretaker with(nolock)
		JOIN address ON caretaker.address_id = address.address_id
END
GO
