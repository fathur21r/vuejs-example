USE [master]
GO
/****** Object:  Database [Test-VueJs]    Script Date: 26/08/2021 23:54:05 ******/
CREATE DATABASE [Test-VueJs]
GO

CREATE FUNCTION [dbo].[fn_DateConverter] 
(
	@Date		Datetime
)
RETURNS VARCHAR(100)
AS
BEGIN
	RETURN CONVERT(VARCHAR(100), @Date, 120)
END
GO
/****** Object:  Table [dbo].[Category]    Script Date: 26/08/2021 23:54:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](45) NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[LastModifiedDate] [datetime] NULL,
 CONSTRAINT [PK__Category__3214EC070F068717] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Post]    Script Date: 26/08/2021 23:54:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Title] [varchar](100) NOT NULL,
	[Content] [varchar](8000) NOT NULL,
	[IsActive] [bit] NULL,
	[PostingDate] [datetime] NULL,
 CONSTRAINT [PK__Post__3214EC07C3BA848D] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Router]    Script Date: 26/08/2021 23:54:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Router](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RouteName] [varchar](100) NOT NULL,
	[Url] [varchar](100) NOT NULL,
	[MenuName] [varchar](100) NULL,
	[Component] [varchar](500) NOT NULL,
 CONSTRAINT [PK_Router] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Post] ADD  CONSTRAINT [DF_Post_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Post] ADD  CONSTRAINT [DF__Post__PostingDat__398D8EEE]  DEFAULT (getdate()) FOR [PostingDate]
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteCategory]    Script Date: 26/08/2021 23:54:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
CREATE PROCEDURE [dbo].[sp_DeleteCategory]
	@Id INT
AS
BEGIN
	DELETE FROM Category WHERE Category.Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_DeletePost]    Script Date: 26/08/2021 23:54:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
CREATE PROCEDURE [dbo].[sp_DeletePost]
	@Id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	DELETE FROM Post WHERE Post.Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetCategory]    Script Date: 26/08/2021 23:54:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetCategory]
	@Name VARCHAR(8000)
	,@IsActiveState VARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT
		D1.[Id]
		,D1.[Name]
		,D1.[IsActive]
		,dbo.fn_DateConverter(D1.CreatedDate) CreatedDate
		,dbo.fn_DateConverter(D1.[LastModifiedDate]) LastModifiedDate
	FROM Category D1
	WHERE 1 = 1
		AND D1.Name like CONCAT('%', @Name, '%')
		AND CAST(D1.IsActive AS varchar(100)) like CONCAT('%', @IsActiveState, '%')
	ORDER BY D1.Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetCategoryById]    Script Date: 26/08/2021 23:54:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetCategoryById]
	@Id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT
		[Id]
		,[Name]
		,[IsActive]
		,dbo.fn_DateConverter(CreatedDate) CreatedDate
		,dbo.fn_DateConverter([LastModifiedDate]) LastModifiedDate
	FROM Category D1
	WHERE 1 = 1
		AND D1.Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetPost]    Script Date: 26/08/2021 23:54:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetPost]
	@CatId VARCHAR(4),
	@IsActiveState varchar(1)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT
		D1.Id,
        D2.Id AS CategoryId,
        D2.Name AS Category,
        D1.Content,
        dbo.fn_DateConverter(D1.PostingDate) PostingDate,
        D1.Title,
		d1.IsActive
	FROM Post D1
    JOIN Category D2 ON D1.CategoryId = D2.Id
	WHERE 1 = 1
		AND d2.IsActive = 1
		AND D1.CategoryId like CONCAT('%', @CatId, '%')
		AND D1.IsActive like CONCAT('%', @IsActiveState, '%')
	ORDER BY PostingDate DESC;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetPostById]    Script Date: 26/08/2021 23:54:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetPostById]
	@Id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT
		D1.Id,
        D2.Id AS CategoryId,
        D2.Name AS Category,
        D1.Content,
        dbo.fn_DateConverter(D1.PostingDate) PostingDate,
        D1.Title,
		d1.IsActive
	FROM Post D1
    JOIN Category D2 ON D1.CategoryId = D2.Id
	WHERE 1=1
		AND D2.IsActive = 1
		AND D1.Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_SaveCategory]    Script Date: 26/08/2021 23:54:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
CREATE PROCEDURE [dbo].[sp_SaveCategory]
	@Id VARCHAR(4) = '',
    @Name VARCHAR(255),
	@IsActive Bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;
	DECLARE @RowsAffected INT = 0
	IF @Id = '' 
		BEGIN
			INSERT INTO Category (Name, IsActive)
			VALUES(@Name, @IsActive);
			--SET @RowsAffected = @@ROWCOUNT
		END
    ELSE
		BEGIN
			UPDATE Category
				SET Category.Name = @Name,
					Category.IsActive = @IsActive,
					Category.LastModifiedDate = GETDATE()
			WHERE Category.Id = @Id;
			--SET @RowsAffected = @@ROWCOUNT
		END;
	
	--RETURN @RowsAffected
END
GO
/****** Object:  StoredProcedure [dbo].[sp_SavePost]    Script Date: 26/08/2021 23:54:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
CREATE PROCEDURE [dbo].[sp_SavePost]
	@Id VARCHAR(4),
    @CategoryId INT,
    @Content VARCHAR(8000),
    @Title VARCHAR(100),
	@IsActive bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	IF @Id = '' 
		BEGIN
			INSERT INTO Post (CategoryId, Content, Title, IsActive)
		VALUES(@CategoryId, @Content, @Title, @IsActive);
		END
    ELSE
		BEGIN
			UPDATE Post
			SET Post.CategoryId = @CategoryId,
				Post.Content = @Content,
                Post.Title = @Title,
				Post.IsActive = @IsActive
			WHERE Post.Id = @Id;
		END;
END
GO
INSERT INTO [dbo].[Router]([RouteName],[Url],[MenuName],[Component])
VALUES	('Home','/','Home','../views/Home.vue'),
		('Category','/Category','Category','../views/Category.vue'),
		('EditCategory','/Category/:id',NULL,'../views/EditCategory.vue'),
		('Post','/Post','Post','../views/Post.vue'),
		('EditPost','/Post/:id',NULL,'../views/EditPost.vue')

USE [master]
GO
ALTER DATABASE [Test-VueJs] SET  READ_WRITE 
GO


