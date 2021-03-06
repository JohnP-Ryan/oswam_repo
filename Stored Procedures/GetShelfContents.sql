USE [OSWAM_Data]
GO
/****** Object:  StoredProcedure [dbo].[GetShelfContents]    Script Date: 4/14/2017 3:48:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		John Ryan
-- Create date: 4/4/17
-- Description:	Returns Product ID's that are placed on a shelf at input coords
-- =============================================
ALTER PROCEDURE [dbo].[GetShelfContents]
	-- Add the parameters for the stored procedure here
	@InputLocX int,
	@InputLocY int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF (@InputLocX IS NOT NULL AND @InputLocY IS NOT NULL)
	BEGIN
		Select ProductID, Quantity
		From LocalInventory
		Inner Join Shelf ON LocalInventory.ShelfID = Shelf.ID
		Where Shelf.LocationX = @InputLocX AND Shelf.LocationY = @InputLocY
	END
END
