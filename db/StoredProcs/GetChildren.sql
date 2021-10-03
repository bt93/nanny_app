SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jason Howie
-- Create date: 10/3/2021
-- Description:	Retrieves the Children given the CaretakerID
-- =============================================
CREATE PROCEDURE GetChildren 
	-- Add the parameters for the stored procedure here
	@CaretakerID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT child.* FROM child WITH(NOLOCK)
	JOIN child_caretaker ON child.child_id = child_caretaker.child_id
    JOIN caretaker ON child_caretaker.caretaker_id = caretaker.caretaker_id
	WHERE caretaker.caretaker_id = @CaretakerID
    AND child.active = 1
	GROUP BY child.child_id, child.first_name, child.last_name, 
	child.gender, child.date_of_birth, child.needs_diapers,
	child.rate_per_hour, child.image_url, child.active;
END
GO
