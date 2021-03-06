USE [OSWAM_Data]
GO
/****** Object:  StoredProcedure [dbo].[GetPreferenceValue]    Script Date: 4/14/2017 3:15:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		John Ryan
-- Create date: 3/xx/17
-- Description:	Select a stored preference value by its name.
-- =============================================
ALTER PROCEDURE [dbo].[GetPreferenceValue] 
	-- Add the parameters for the stored procedure here
	@PreferenceKey varchar(40)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TOP 1 PreferenceValue From Preference Where PreferenceKey = @PreferenceKey
END
