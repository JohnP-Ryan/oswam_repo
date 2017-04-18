USE [OSWAM_Data]
GO
/****** Object:  StoredProcedure [dbo].[GetAverageFillTimeWaiting]    Script Date: 4/17/2017 8:26:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		John Ryan
-- Create date: 4/17/17
-- Description:	Calculates total fill time of 'Waiting' orders then divides by number of 'Waiting' orders
-- =============================================
ALTER PROCEDURE [dbo].[GetAverageFillTimeWaiting]
	
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

	IF((Select Count(Distinct OrderNumber) From Orders Where OrderStatus = 'Waiting') <> 0)
	BEGIN
		Select @NumOrders = Count(Distinct OrderNumber) From Orders Where OrderStatus = 'Waiting' -- Total number of uncompleted orders
		Select @AverageFillTime = @TotalFillTime/ @NumOrders
		Select @AverageFillTime as AverageFillTime
		Return 0
	END
	ELSE
		Select @AverageFillTime = 0
		Select @AverageFillTime as AverageFillTime
		Return 0
END
