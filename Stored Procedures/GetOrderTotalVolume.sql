USE [OSWAM_Data]
GO
/****** Object:  StoredProcedure [dbo].[GetOrderTotalVolume]    Script Date: 4/14/2017 3:15:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		John Ryan
-- Create date: 3/xx/17
-- Description:	Sum the items' volume within a given order (cu ft).
-- =============================================
ALTER PROCEDURE [dbo].[GetOrderTotalVolume]
	-- Add the parameters for the stored procedure here
	@OrderID varchar(36)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF (@OrderID IS NOT NULL)
		Select (SUM(Volume)/1728) As VolumeCubicFT From Orders INNER JOIN Product ON Product.ID = Orders.ProductID Where OrderNumber = @OrderID
END
