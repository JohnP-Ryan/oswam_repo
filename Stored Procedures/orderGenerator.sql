USE [OSWAM_Data]
GO
/****** Object:  StoredProcedure [dbo].[orderGenerator]    Script Date: 4/27/2017 6:39:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		John Ryan
-- Create date: 4-xx-17
-- Description:	Generate a random order with user-input item size.
--              Only selects items with sufficient quantity.
-- =============================================
ALTER PROCEDURE [dbo].[orderGenerator]
	@orderSize int
AS
BEGIN
	DECLARE @OrderStatus varchar(15);
	SET @OrderStatus = 'Waiting';

	DECLARE @OrderNum uniqueidentifier = NEWID();


	INSERT INTO Orders (ProductID, OrderStatus, OrderNumber)
	SELECT TOP (@orderSize) ID, @OrderStatus, @OrderNum
	FROM [dbo].[Product]
	Where Quantity > NumReserved
	ORDER BY newid()


	UPDATE Product
	SET NumReserved = NumReserved + 1
	Where ID IN (Select ProductID From Orders Where OrderNumber = @OrderNum)

END