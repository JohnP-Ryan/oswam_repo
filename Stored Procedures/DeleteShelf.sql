USE [OSWAM_Data]
GO
/****** Object:  StoredProcedure [dbo].[DeleteShelf]    Script Date: 4/14/2017 3:15:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		John Ryan
-- Create date: 3/xx/17
-- Description:	Delete shelf if given valid ID or coordinates.
-- =============================================
ALTER PROCEDURE [dbo].[DeleteShelf]
	-- Add the parameters for the stored procedure here
	@ID int,
	@LocationX int,
	@LocationY int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF (@ID IS NOT NULL and @ID <> -1)
		BEGIN
			IF EXISTS (Select * FROM Shelf WHERE ID = @ID)
				BEGIN
					DECLARE @CellType int
					SELECT @CellType = CellType FROM Shelf WHERE ID = @ID

					IF @CellType = 1
						UPDATE Preference SET PreferenceValue = PreferenceValue - 1 WHERE ID = 9
					ELSE
						UPDATE Preference SET PreferenceValue = PreferenceValue - 1 WHERE ID = 10

					DELETE FROM Shelf WHERE ID = @ID
				END
		END
	ELSE IF (@LocationX <> -1 AND @LocationY <> -1)
		BEGIN
			IF EXISTS (Select * FROM Shelf WHERE LocationX = @LocationX and LocationY = @LocationY)
				BEGIN
					DECLARE @CellType2 int
					SELECT @CellType2 = CellType FROM Shelf WHERE LocationX = @LocationX and LocationY = @LocationY

					IF @CellType2 = 1
						UPDATE Preference SET PreferenceValue = PreferenceValue - 1 WHERE ID = 9
					ELSE
						UPDATE Preference SET PreferenceValue = PreferenceValue - 1 WHERE ID = 10

					DELETE FROM Shelf WHERE LocationX = @LocationX and LocationY = @LocationY
				END
		END
	ELSE
		RETURN 1
END
