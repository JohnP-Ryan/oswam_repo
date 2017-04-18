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
-- Description:	Delete all entries in Orders table
-- =============================================
CREATE PROCEDURE DeleteOrders
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Delete From Orders
END
GO
