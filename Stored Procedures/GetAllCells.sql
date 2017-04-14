USE [OSWAM_Data]
GO
/****** Object:  StoredProcedure [dbo].[GetAllCells]    Script Date: 4/14/2017 3:15:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		John Ryan
-- Create date: 3/xx/17
-- Description:	Select all stored cell data.
-- =============================================
ALTER PROCEDURE [dbo].[GetAllCells]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * From Shelf
END
