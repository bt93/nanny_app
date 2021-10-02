USE NannyDB
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jason Howie
-- Create date: 10/2/2021
-- Description:	Gets the caretake by ID
-- =============================================
CREATE PROCEDURE GetCaretakerByID
	-- Add the parameters for the stored procedure here
	@CaretakerID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *
    FROM caretaker WITH(NOLOCK)
    JOIN address ON caretaker.address_id = address.address_id
    WHERE caretaker_id = @CaretakerID
END
GO
