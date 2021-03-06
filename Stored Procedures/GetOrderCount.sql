USE [OSWAM_Data]
GO
/****** Object:  StoredProcedure [dbo].[GetOrderCount]    Script Date: 4/17/2017 8:40:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		John Ryan
-- Create date: 3/xx/17
-- Description:	Select all OrderNumber, order size and Status
-- =============================================
ALTER PROCEDURE [dbo].[GetOrderCount]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select OrderNumber, count(*) as Products, OrderStatus FROM Orders GROUP BY OrderNumber, OrderStatus ORDER BY OrderStatus DESC
END
