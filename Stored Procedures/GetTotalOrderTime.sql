USE [OSWAM_Data]
GO
/****** Object:  StoredProcedure [dbo].[GetTotalOrderTime]    Script Date: 4/27/2017 6:25:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		John Ryan
-- Create date: 4/4/17
-- Description:	Returns Order Fulfillment time, input OrderNumber
-- =============================================
ALTER PROCEDURE [dbo].[GetTotalOrderTime]
	-- Add the parameters for the stored procedure here
	@InputOrderNum varchar(36)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF (@InputOrderNum IS NOT NULL)
	BEGIN
		Select (Sum(PackDistance)/51261 + Sum(Quantity * 5)) AS TotalFillTime --total fill time for order
		From Orders
		Inner Join LocalInventory ON Orders.ProductID = LocalInventory.ProductID
		Inner Join Shelf ON LocalInventory.ShelfID = Shelf.ID
		Where OrderNumber = @InputOrderNum
	END
END
