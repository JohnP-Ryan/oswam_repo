USE [OSWAM_Data]
GO
/****** Object:  StoredProcedure [dbo].[GetInventoryProducts]    Script Date: 4/14/2017 3:48:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
-- =============================================
-- Author:		John Ryan
-- Create date: 2/12/17
-- Description:	Query inventory products by ID, Name, Weight and/or Price
-- =============================================
ALTER PROCEDURE [dbo].[GetInventoryProducts] 
	-- Add the parameters for the stored procedure here
	@ID char(10),
	@SearchName varchar(50),
	@WeightLow int,
	@WeightHigh int,
	@PriceLow int,
	@PriceHigh int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
	IF (@ID IS NOT NULL AND LEN(@ID) >= 1) 
		SELECT * FROM Product WHERE ID = @ID
	ELSE IF (@WeightHigh > 0 AND @WeightHigh > @WeightLow AND @PriceHigh > 0 AND @PriceHigh > @PriceLow) --everything is valid
	
		SELECT * 
		FROM Product 
		WHERE ItemName LIKE CONCAT('%', @SearchName , '%')
		AND Weight BETWEEN @WeightLow AND @WeightHigh 
		AND Price BETWEEN @PriceLow AND @PriceHigh
	ELSE IF (@WeightHigh > 0 AND @WeightHigh > @WeightLow AND (@PriceHigh <= 0 OR @PriceLow >= @PriceHigh)) --weight valid, price non-valid
	
		SELECT * 
		FROM Product 
		WHERE ItemName LIKE CONCAT('%', @SearchName , '%')
		AND Weight BETWEEN @WeightLow AND @WeightHigh 
	ELSE IF (@PriceHigh > 0 AND @PriceHigh > @PriceLow AND (@WeightHigh <= 0 OR @WeightLow >= @WeightHigh)) --price valid, weight non-valid

		SELECT * 
		FROM Product 
		WHERE ItemName LIKE CONCAT('%', @SearchName , '%')
		AND Price BETWEEN @PriceLow AND @PriceHigh
	ELSE IF (LEN(@SearchName) > 1)													--weight non-valid, price non-valid, name != null
		SELECT *
		FROM Product
		WHERE ItemName LIKE CONCAT('%', @SearchName , '%')
	ELSE														-- return everything
		SELECT *
		FROM Product
END
