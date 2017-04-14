USE [OSWAM_Data]
GO
/****** Object:  StoredProcedure [dbo].[GetAllPreferences]    Script Date: 4/14/2017 3:15:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		John Ryan
-- Create date: 3/xx/17
-- Description:	Select all preferences
-- =============================================
ALTER PROCEDURE [dbo].[GetAllPreferences]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * From Preference
END
