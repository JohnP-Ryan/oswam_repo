USE [OSWAM_Data]
GO
/****** Object:  StoredProcedure [dbo].[GenerateQuantities]    Script Date: 4/14/2017 3:15:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		John Ryan
-- Create date: 3/xx/17
-- Description:	Generate random product quantities until total volume reaches user-input percentage. 
-- =============================================
ALTER PROCEDURE [dbo].[GenerateQuantities]
	-- Add the parameters for the stored procedure here
	@fillPercentage int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @MaxTotalVolume int;
	DECLARE @ShelfCubicInchVolume int;
	DECLARE @NumShelves int;
	DECLARE @CurrentVolume int;
	DECLARE @SelectedProductID int;
	DECLARE @TargetVolume int;

	SELECT @NumShelves = PreferenceValue FROM Preference WHERE ID = 0;
	SELECT @ShelfCubicInchVolume = PreferenceValue FROM Preference WHERE ID = 6;
	SET @MaxTotalVolume = (@ShelfCubicInchVolume * @NumShelves);
	SELECT @CurrentVolume = SUM(Volume*Quantity) FROM Product;

	SET @TargetVolume = ((@MaxTotalVolume / 100) * @fillPercentage);

	WHILE (@CurrentVolume < @TargetVolume)
	BEGIN
		UPDATE Product
		SET Quantity = Quantity + 1
		WHERE ID IN (SELECT TOP 1 PERCENT ID FROM Product ORDER BY NEWID())

		SELECT @CurrentVolume = SUM(Volume*Quantity) FROM Product;
	END;
END
