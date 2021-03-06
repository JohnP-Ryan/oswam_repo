USE [OSWAM_Data]
GO
/****** Object:  StoredProcedure [dbo].[GetShelfItemTotals]    Script Date: 4/14/2017 3:15:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		John Ryan
-- Create date: 4/3/17
-- Description:	Returns Total Unique Item ID's on a shelf and Overall Total Items
-- =============================================
ALTER PROCEDURE  [dbo].[GetShelfItemTotals]
	@ShelfID int, 
	@ShelfLocX int,
	@ShelfLocY int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF (@ShelfID IS NOT NULL)
	BEGIN
		Select Count(ProductID) AS 'Unique Items', Sum(Quantity) AS	'Total Items'
		From LocalInventory 
		INNER JOIN Shelf ON LocalInventory.ShelfID = Shelf.ID
		Where ID = @ShelfID
	END
	ELSE IF (@ShelfLocX IS NOT NULL AND @ShelfLocY IS NOT NULL)
	BEGIN
		Select Count(ProductID) AS 'Unique Items', Sum(Quantity) AS	'Total Items'
		From LocalInventory 
		INNER JOIN Shelf ON LocalInventory.ShelfID = Shelf.ID
		Where LocationX = @ShelfLocX AND LocationY = @ShelfLocY
	END
	ELSE
		Return 1
END
