SET XACT_ABORT ON;
BEGIN TRANSACTION;
GO

--CREATE DATABASE [ODataGraphQLDb]
--GO 

BEGIN TRANSACTION ODataGraph

USE [ODataGraphQLDb]
GO

IF NOT EXISTS (SELECT * FROM sys.tables
WHERE name = N'Author' AND type = 'U')
BEGIN
	CREATE TABLE [ODataGraphQLDb].[dbo].[Author]
	(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Surname] [nvarchar](150) NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_Author_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
))
END	
GO

IF NOT EXISTS (SELECT * FROM sys.tables
WHERE name = N'Book' AND type = 'U')
BEGIN
CREATE TABLE [ODataGraphQLDb].[dbo].[Book](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Published] [bit] NOT NULL,
	[Genre] [nvarchar](150) NOT NULL,
	[AuthorId] [int] NOT NULL,
 CONSTRAINT [PK_Book_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
),
 CONSTRAINT [AK_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
))
END	
GO

ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Author_Id] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Author] ([Id])
GO

ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Author_Id]
GO

INSERT INTO [ODataGraphQLDb].[dbo].[Author] ([Name], [Surname], [Email])
VALUES
 (N'Kuda', N'Mushore', N'kmkuda@gmail.com'), 	 
 (N'Patrick',N'Moloney', N'pmoloney@gmail.com'),
 (N'Susan', N'Kruger', N'sk@mail.com'),
 (N'Donald', N'Jones', N'djones@mail.com'),
 (N'Tendai', N'Shumba', N'ten@mail.com'),
 (N'Amos', N'Marks', N'amos@mail.com'),
 (N'William', N'Nichol', N'nichol@mail.com'),
 (N'Jana', N'Dos Santos', N'jana@mail.com'),
 (N'Nomsa', N'Mabuza', N'nomsa@mail.com');

 INSERT INTO [ODataGraphQLDb].[dbo].[Book] ([Name], [Published], [Genre], [AuthorId])
VALUES
 (N'Rest v OData v GraphQL', 0 , N'Technology', 1), 	 
 (N'Angular Concepts', 0 , N'Technology', 2),
 (N'AspNetCore for begginers', 0 , N'Technology', 1),
 (N'A Dance with the stars', 0 , N'Fiction', 3),
 (N'The Hitman', 0 , N'Fiction', 1),
 (N'A long winding road', 0 , N'Fiction', 4),
 (N'Journey to the core', 0 , N'Fiction', 2),
 (N'Monsters inc', 0 , N'Fiction', 4),
 (N'Legend of the north', 0 , N'Fiction', 5),
 (N'Tales of the south', 0 , N'Fiction', 5),
 (N'Dragon fire', 0 , N'Fiction', 7);

 COMMIT