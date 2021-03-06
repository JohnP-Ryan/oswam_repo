USE [OSWAM_Data]
GO
/****** Object:  StoredProcedure [dbo].[SortVolume]    Script Date: 4/14/2017 3:16:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		John Ryan
-- Create date: 4/xx/17
-- Description:	Sort Items onto closest shelves by smallest volume
-- =============================================
ALTER PROCEDURE [dbo].[SortVolume] 
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ProductID char(10);
	DECLARE @ProductVolume decimal(29,6);
	DECLARE @ProductWeight decimal(9,2);
	DECLARE @ShelfID int;
	DECLARE @ShelfVolumeLeft decimal(9,2);
	DECLARE @ShelfWeightLeft decimal(9,2);

	DELETE From LocalInventory --clear all current item placement data
	UPDATE Product SET NumPlaced = 0 --Set placed items to 0
	UPDATE Shelf SET ContainsItems = 0 --empty all shelves
	UPDATE Shelf SET availableWeight = (Select PreferenceValue From Preference Where ID = 3) WHERE CellType = 1 --reset weight
	UPDATE Shelf SET availableVolume = (Select PreferenceValue From Preference Where ID = 6) WHERE CellType = 1 --reset volume

	IF NOT EXISTS(Select TOP 1 ID From Shelf Where ContainsItems = 0 AND CellType = 1) -- make sure there is at least one empty shelf
		Return 1

	Select TOP 1 @ShelfID = ID From Shelf Where ContainsItems = 0 AND CellType = 1 Order By PackDistance ASC --Select first shelf
	Select @ShelfVolumeLeft = availableVolume From Shelf Where ID = @ShelfID --Seed remaining volume
	Select @ShelfWeightLeft = availableWeight From Shelf Where ID = @ShelfID --Seed remaining weight
	UPDATE Shelf SET ContainsItems = 1 Where ID = @ShelfID

	WHILE EXISTS (Select TOP 1 ID From Product Where NumPlaced < Quantity) --loop while products yet to be placed
	BEGIN
		--Select product by desired sort method
		Select @ProductID = ID From Product Where NumPlaced < Quantity ORDER BY Volume DESC
		Select @ProductVolume = Volume From Product Where ID = @ProductID
		Select @ProductWeight = Weight From Product Where ID = @ProductID

		--Place product on closest available shelf
		IF (@ShelfVolumeLeft >= @ProductVolume AND @ShelfWeightLeft >= @ProductWeight)--shelf can fit item
		BEGIN
			IF NOT EXISTS(Select ShelfID From LocalInventory Where ShelfID = @ShelfID AND ProductID = @ProductID) --check to see if same product already on shelf
			BEGIN
				INSERT INTO LocalInventory (ShelfID, ProductID, Quantity) VALUES (@ShelfID, @ProductID, 1); --create LocalInventory record
			END
			ELSE
			BEGIN
				UPDATE LocalInventory SET Quantity = Quantity + 1 Where ShelfID = @ShelfID AND ProductID = @ProductID
			END

			UPDATE Product SET NumPlaced = NumPlaced + 1 Where ID = @ProductID --Increase placement counter
			SET @ShelfVolumeLeft -= @ProductVolume
			SET @ShelfWeightLeft -= @ProductWeight
		END
		ELSE --Shelf out of space/weight
		BEGIN
			--Save capacity info before switching to new shelf
			UPDATE Shelf SET availableVolume = @ShelfVolumeLeft Where ID = @ShelfID
			UPDATE Shelf SET availableWeight = @ShelfWeightLeft Where ID = @ShelfID


			IF NOT EXISTS (Select TOP 1 ID From Shelf Where ContainsItems = 0) --No shelves left for some reason
				Return 1

			Select TOP 1 @ShelfID = ID From Shelf Where ContainsItems = 0 Order By PackDistance ASC --Select next closest open shelf
			Select @ShelfVolumeLeft = availableVolume From Shelf Where ID = @ShelfID
			Select @ShelfWeightLeft = availableWeight From Shelf Where ID = @ShelfID
			UPDATE Shelf SET ContainsItems = 1 Where ID = @ShelfID
			Continue; --loop back, will select same product
		END
	END

	--Save capacity info before exiting proc
	UPDATE Shelf SET availableVolume = @ShelfVolumeLeft Where ID = @ShelfID
	UPDATE Shelf SET availableWeight = @ShelfWeightLeft Where ID = @ShelfID

END
