USE [OSWAM_Data]
GO
/****** Object:  StoredProcedure [dbo].[GetTotalStoredItemVolume]    Script Date: 4/14/2017 3:16:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		John Ryan
-- Create date: 4/12/17
-- Description:	Return sum of stored item volume
-- =============================================
ALTER PROCEDURE [dbo].[GetTotalStoredItemVolume]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Sum(Volume * Quantity)/1728 From Product
END
