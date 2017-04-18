USE [OSWAM_Data]
GO
/****** Object:  StoredProcedure [dbo].[GetAverageFillTime]    Script Date: 4/17/2017 5:20:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		John Ryan
-- Create date: 4/6/17
-- Description:	Calculates total fill time then divides by number of orders
-- =============================================
ALTER PROCEDURE [dbo].[GetAverageFillTime]
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @TotalFillTime decimal(9,2);
	DECLARE @NumOrders decimal(9,2);
	DECLARE @AverageFillTime decimal(9,2);


	Select @TotalFillTime = (Sum(PackDistance)/51261 + Sum(Quantity * 5)) 
	From Orders
	Inner Join LocalInventory ON Orders.ProductID = LocalInventory.ProductID
	Inner Join Shelf ON LocalInventory.ShelfID = Shelf.ID
	Where OrderStatus = 'Waiting'


	Select @NumOrders = Count(Distinct OrderNumber) From Orders -- Total number of orders


	Select @AverageFillTime = @TotalFillTime/ @NumOrders

	Select @AverageFillTime as AverageFillTime

END
