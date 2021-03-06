USE [OSWAM_Data]
GO
/****** Object:  StoredProcedure [dbo].[GetTotalStoredItemNum]    Script Date: 4/14/2017 3:48:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		John Ryan
-- Create date: 4/7/17
-- Description:	Returns total stored item number
-- =============================================
ALTER PROCEDURE [dbo].[GetTotalStoredItemNum]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Sum(Quantity) as TotalStoredItemNum FROM Product
END
