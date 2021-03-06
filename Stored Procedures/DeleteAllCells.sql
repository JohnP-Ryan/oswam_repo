USE [OSWAM_Data]
GO
/****** Object:  StoredProcedure [dbo].[DeleteAllCells]    Script Date: 4/14/2017 3:15:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		John Ryan
-- Create date: 3/21/17
-- Description:	Remove all records from [Shelf]
-- =============================================
ALTER PROCEDURE [dbo].[DeleteAllCells] 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Preference SET PreferenceValue = 0 WHERE ID = 9
	UPDATE Preference SET PreferenceValue = 0 WHERE ID = 10
	DELETE FROM Shelf
END
