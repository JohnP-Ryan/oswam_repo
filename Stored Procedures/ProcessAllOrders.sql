-- =============================================================================================
-- Create Stored Procedure Template for Azure SQL Database and Azure SQL Data Warehouse Database
-- =============================================================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		John Ryan
-- Create date: 4/17/17
-- Description:	Changes all 'Waiting' order statuses to 'Fulfilled'
-- =============================================
ALTER PROCEDURE [dbo].[ProcessAllOrders] 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Orders SET OrderStatus = 'Fulfilled' Where OrderStatus = 'Waiting'
END
GO
