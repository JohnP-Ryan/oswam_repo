USE [OSWAM_Data]
GO
/****** Object:  StoredProcedure [dbo].[SetPreferenceValue]    Script Date: 4/17/2017 7:25:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		John Ryan
-- Create date: 3-xx-17
-- Description:	Save preference values, and ensure some constraints.
-- =============================================
ALTER PROCEDURE [dbo].[SetPreferenceValue] 
	-- Add the parameters for the stored procedure here
	@PreferenceID int,
	@NewPreferenceValue int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @ShelfSideLength int 
	DECLARE @ShelfHeight int
	DECLARE @NumShelves int
	DECLARE @NumPackingStations int
	DECLARE @NumPlacedShelves int
	DECLARE @NumPlacedStations int


	IF @PreferenceID = 11 --Prevent min order size being set over max
		BEGIN
			IF @NewPreferenceValue > (Select PreferenceValue From Preference Where ID = 12)
				Return 1
		END
	
	IF @PreferenceID = 12 --Prevent max order size being set under min
		BEGIN
			IF @NewPreferenceValue < (Select PreferenceValue From Preference Where ID = 11)
				Return 1
		END

	IF @PreferenceID <> 6 AND @PreferenceID <> 9 AND @PreferenceID <> 10 --Values that should never be set manually
		BEGIN
			UPDATE Preference SET PreferenceValue = @NewPreferenceValue WHERE ID = @PreferenceID
		END

    IF @PreferenceID = 1 OR @PreferenceID = 2 OR @PreferenceID = 6 --If changing shelf dimensions, recalc volume
		BEGIN
			SELECT @ShelfHeight = PreferenceValue FROM Preference WHERE ID = 1
			SELECT @ShelfSideLength = PreferenceValue FROM Preference WHERE ID = 2
			UPDATE Preference SET PreferenceValue = ((@ShelfSideLength * @ShelfSideLength * @ShelfHeight) * 1728) WHERE ID = 6
		END

	IF @PreferenceID = 4 OR @PreferenceID = 5 --If resizing the warehouse dimensions, delete all cells, fix outOfBounds issue
		BEGIN
			DELETE FROM Shelf
		END

	IF @PreferenceID = 0 OR @PreferenceID = 7 --If changing max cell nums, check placed num
		BEGIN
			SELECT @NumShelves = PreferenceValue FROM Preference WHERE ID = 0
			SELECT @NumPackingStations = PreferenceValue FROM Preference WHERE ID = 7
			SELECT @NumPlacedShelves = PreferenceValue FROM Preference WHERE ID = 9
			SELECT @NumPlacedStations = PreferenceValue FROM Preference WHERE ID = 10

			IF ((@NumShelves < @NumPlacedShelves) OR (@NumPackingStations < @NumPlacedStations)) --If placed > max, delete cells, reset grid
				EXEC DeleteAllCells
		END
END
