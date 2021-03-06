USE [OSWAM_Data]
GO
/****** Object:  StoredProcedure [dbo].[CreateShelf]    Script Date: 4/14/2017 3:14:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		John Ryan
-- Create date: 3/xx/17
-- Description:	Create a cell based on any valid information supplied (coord required), 
--              other values filed with configured defaults if not specified.
-- =============================================
ALTER PROCEDURE [dbo].[CreateShelf]
	-- Add the parameters for the stored procedure here
	@LocationX INT,
	@LocationY INT,
	@availableVolume decimal(9,2),
	@availableWeight decimal(9,2),
	@CellType bit

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	--Pull Configured default values (used if not supplied)
	DECLARE @EmptyShelfVolume AS DECIMAL(9,2)
	DECLARE @EmptyShelfWeight AS DECIMAL(9,2)

	SELECT @EmptyShelfVolume = PreferenceValue FROM [Preference] WHERE ID = 6
	SELECT @EmptyShelfWeight = PreferenceValue FROM [Preference] WHERE ID = 3


	--Validate parameter input values
	IF (@CellType IS NULL OR NOT(@CellType <> 1 OR @CellType <> 0)) -- CellType not set, return error
		Return 1

	IF (@LocationX IS NULL OR @LocationY IS NULL OR @LocationX = -1 OR @LocationY = -1) --Invalid coord data, return error
		Return 2

	--Delete an existing entry if exists
	IF EXISTS(Select 1 From Shelf Where LocationX = @LocationX AND LocationY = @LocationY) --Entry exists at coords, delete first, let delete handle placement decrement
		EXEC DeleteShelf NULL, @LocationX, @LocationY


	--Execute proper insert
	IF (@CellType = 1) --Cell is Shelf
		BEGIN
			IF(@availableVolume IS NULL OR @availableVolume = -1 OR @availableWeight IS NULL OR @availableWeight = -1)--Null volume or weight, use defaults
				BEGIN
					INSERT INTO Shelf (LocationX, LocationY, availableVolume, availableWeight, CellType) VALUES (@LocationX, @LocationY, @EmptyShelfVolume, @EmptyShelfWeight, 1)
				END
			ELSE
				BEGIN
					INSERT INTO Shelf (LocationX, LocationY, availableVolume, availableWeight, CellType) VALUES (@LocationX, @LocationY, @availableVolume, @availableWeight, 1)
				END
			UPDATE Preference SET PreferenceValue = PreferenceValue + 1 WHERE ID = 9
		END
	ELSE -- Cell is Packing station
		BEGIN
			INSERT INTO Shelf (LocationX, LocationY, availableVolume, availableWeight, CellType) VALUES (@LocationX, @LocationY, 0, 0, 0)
			UPDATE Preference SET PreferenceValue = PreferenceValue + 1 WHERE ID = 10
		END
END
