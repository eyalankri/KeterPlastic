/************************************************************
 * Updated by: Eyal.
 * Time: 07/07/2020 18:25:24
 ************************************************************/

CREATE DATABASE [keter-store];
GO
 
USE [keter-store]
GO

/****** Object:  Table [dbo].[Product]    Script Date: 06/07/2020 10:20:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- drop table  Product
CREATE TABLE [dbo].[Product]
(
	[ProductId]              [int] IDENTITY(1, 1) NOT NULL,
	[ProductName]            [nvarchar](100) NOT NULL,
	[ProductDescription]     [nvarchar](MAX) NULL,
	[ProductPriceUsd]        [decimal](10, 4) NOT NULL,
	[ProductFileName]        [varchar](40) NULL,
	[DateCreated]            [datetime] DEFAULT GETDATE() NOT NULL,
	[LastUpdated]            [datetime] DEFAULT GETDATE() NOT NULL
)
GO



CREATE PROCEDURE Product_Insert
	@ProductName NVARCHAR(100),
	@ProductDescription NVARCHAR(MAX),
	@ProductPriceUsd DECIMAL(10, 4),
	@ProductFileName VARCHAR(40)
AS
	INSERT INTO Product
	  (
	    ProductName,
	    ProductDescription,
	    ProductPriceUsd,
	    ProductFileName
	  )
	VALUES
	  (
	    @ProductName,
	    @ProductDescription,
	    @ProductPriceUsd,
	    @ProductFileName
	  )
GO	


CREATE PROCEDURE Product_Select
AS
	SELECT ProductId,
	       ProductName,
	       ProductDescription,
	       ProductPriceUsd,
	       ProductFileName,
	       DateCreated,
	       LastUpdated
	FROM   Product
GO


CREATE PROCEDURE Product_Delete_ProductId
	@ProductId INT
AS
	DELETE 
	FROM   Product
	WHERE  ProductId = @ProductId
GO	


CREATE PROCEDURE Product_Update_ProductId
	@ProductId INT,
	@ProductName NVARCHAR(100),
	@ProductDescription NVARCHAR(MAX),
	@ProductPriceUsd DECIMAL(10, 4),
	@ProductFileName VARCHAR(40) = NULL
AS
	UPDATE Product
	SET    ProductName            = @ProductName,
	       ProductDescription     = @ProductDescription,
	       ProductPriceUsd        = @ProductPriceUsd,
	       ProductFileName        = ISNULL(@ProductFileName, ProductFileName)
	WHERE  ProductId              = @ProductId
GO
	
	
