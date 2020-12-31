USE master; 

IF EXISTS (SELECT name 
FROM master.dbo.sysdatabases 
WHERE ('[' + name + ']' = 'TourOfHeroes' 
OR name = 'TourOfHeroes'))
BEGIN
	DROP DATABASE [TourOfHeroes];
END
GO

CREATE DATABASE [TourOfHeroes];
GO

USE [TourOfHeroes];

--Create tables
CREATE TABLE [Power] (
	[Power_ID] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Power_Name] NVARCHAR(50) NOT NULL,
	[Power_Description] NVARCHAR(1000) NULL
);
GO

CREATE TABLE [Universe] (
	[Universe_ID] INT PRIMARY KEY IDENTITY (1, 1) NOT NULL,
	[Universe_Name] NVARCHAR(250) NOT NULL,
	[Logo_Url] NVARCHAR(MAX) NULL
);
GO

CREATE TABLE [Hero] (
    [Hero_ID] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Hero_Name] NVARCHAR(50) NOT NULL,
	[Power_Level] NVARCHAR(100) NULL,
	[Picture_Url] NVARCHAR(MAX) NULL,
	[Universe_ID] INT NOT NULL,
	FOREIGN KEY([Universe_ID]) REFERENCES[Universe]([Universe_ID])
);
GO

CREATE TABLE [Hero_Power_Association] (
	[HPA_ID] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	[Hero_ID] INT NOT NULL,
	[Power_ID] INT NOT NULL,
	FOREIGN KEY ([Hero_ID]) REFERENCES[Hero]([Hero_ID]),
	FOREIGN KEY ([Power_ID]) REFERENCES [Power](Power_ID)

);
GO

CREATE TABLE [Hero_Bios] (
	[Hero_Bio_ID] INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
	[Hero_Bio] NVARCHAR(MAX) NOT NULL,
	[Hero_ID] INT NOT NULL,
	[Order] INT NOT NULL,
	[Header] NVARCHAR(25) NOT NULL,
	FOREIGN KEY ([Hero_ID]) REFERENCES [Hero]([Hero_ID])
);
GO

--TODO: Add where clause to do where @variable is either null or is not null and table value = @variable

--Create stored procedures
CREATE PROCEDURE [Hero_Get] 
	@Hero_ID INT = NULL,
	@Hero_Name NVARCHAR(50) = NULL,
	@Power_Level NVARCHAR(100) = NULL,
	@Picture_Url NVARCHAR(1000) = NULL,
	@Universe_ID INT = NULL
AS
BEGIN
	IF @Hero_ID IS NOT NULL
	BEGIN
		SELECT * FROM [Hero]
		WHERE [Hero_ID] = @Hero_ID;
	END
	ELSE
	BEGIN
		SELECT
			Hero_Id,
			Hero_Name
		FROM [Hero]
		WHERE ((@Hero_Name IS NULL) OR (@Hero_Name IS NOT NULL AND [Hero_Name] = @Hero_Name))
		AND ((@Power_Level IS NULL) OR (@Power_Level IS NOT NULL AND [Power_Level] = @Power_Level))
		AND ((@Picture_Url IS NULL) OR (@Picture_Url IS NOT NULL AND [Picture_Url] = @Picture_Url))
		AND ((@Universe_ID IS NULL OR @Universe_ID = 0) OR (@Universe_ID IS NOT NULL AND [Universe_ID] = @Universe_ID));
	END
END
GO

CREATE PROCEDURE [Hero_Put] 
	@Hero_ID INT = NULL OUT,
	@Hero_Name NVARCHAR(50) = NULL,
	@Power_Level NVARCHAR(100) = NULL,
	@Picture_Url NVARCHAR(1000) = NULL,
	@Universe_ID INT = NULL
AS
BEGIN
	IF @Hero_ID IS NULL
	BEGIN
		IF @Hero_Name IS NULL
			RAISERROR ('Hero name can not be null when creating a new Hero', 0, 0);

		IF @Power_Level IS NULL
			RAISERROR ('Power level can not be null when creating a new Hero', 0, 0);

		IF @Universe_ID IS NULL
			RAISERROR ('A new hero must be assigned a universe', 0, 0);

		INSERT INTO [Hero] (Hero_Name, Power_Level, Picture_Url, Universe_ID) VALUES (
			@Hero_Name, @Power_Level, @Picture_Url, @Universe_ID
		);

		SET @Hero_ID = SCOPE_IDENTITY();
	END
	ELSE
	BEGIN
		UPDATE [Hero] SET
			Hero_Name = CASE WHEN (@Hero_Name IS NOT NULL) THEN @Hero_Name ELSE Hero_Name END,
			Power_Level = CASE WHEN (@Power_Level IS NOT NULL) THEN @Power_Level ELSE Power_Level END,
			Picture_Url = CASE WHEN (@Picture_Url IS NOT NULL) THEN @Picture_Url ELSE Picture_Url END,
			Universe_ID = CASE WHEN (@Universe_ID IS NOT NULL) THEN @Universe_ID ELSE Universe_ID END
			WHERE [Hero_ID] = @Hero_ID;
	END
	
	RETURN;
END
GO

CREATE PROCEDURE [Hero_Del]
	@Hero_ID INT OUTPUT
AS
BEGIN
	DELETE FROM [Hero] WHERE [Hero_ID] = @Hero_ID;
END
GO

CREATE PROCEDURE [Power_Get] 
	@Power_ID INT = NULL,
	@Power_Name NVARCHAR(50) = NULL,
	@Power_Description NVARCHAR(100) = NULL
AS
BEGIN
	SELECT * FROM [Power]
	WHERE ((@Power_ID IS NULL) OR (@Power_ID IS NOT NULL AND [Power_ID] = @Power_ID))
	AND ((@Power_Name IS NULL) OR (@Power_Name IS NOT NULL AND [Power_Name] = @Power_Name))
	AND ((@Power_Description IS NULL) OR (@Power_Description IS NOT NULL AND [Power_Description] = @Power_Description));
END
GO

CREATE PROCEDURE [Power_Put] 
	@Power_ID INT = NULL OUTPUT,
	@Power_Name NVARCHAR(50) = NULL,
	@Power_Description NVARCHAR(100) = NULL
AS
BEGIN
	IF @Power_ID IS NULL
	BEGIN
		IF @Power_Name IS NULL
			RAISERROR ('Power name can not be null when creating a new Power', 0, 0);

		IF @Power_Description IS NULL
			RAISERROR ('Power description can not be null when creating a new Power', 0, 0);

		INSERT INTO [Power] (Power_Name, Power_Description) VALUES (
			@Power_Name, @Power_Description
		);

		SET @Power_ID = SCOPE_IDENTITY();
	END
	ELSE
	BEGIN
		UPDATE [Power] SET
			Power_Name = CASE WHEN (@Power_Name IS NOT NULL) THEN @Power_Name ELSE Power_Name END,
			Power_Description = CASE WHEN (@Power_Description IS NOT NULL) THEN @Power_Description ELSE Power_Description END
			WHERE [Power_ID] = @Power_ID;
	END
	
	RETURN;
END
GO

CREATE PROCEDURE [Power_Del]
	@Power_ID INT OUTPUT
AS
BEGIN
	DELETE FROM [Power] WHERE [Power_ID] = @Power_ID;
END
GO

CREATE PROCEDURE [Universe_Get] 
	@Universe_ID INT = NULL,
	@Universe_Name NVARCHAR(50) = NULL
AS
BEGIN
	SELECT * FROM [Universe]
	WHERE ((@Universe_ID IS NULL OR @Universe_ID = 0) OR (@Universe_ID IS NOT NULL AND [Universe_ID] = @Universe_ID))
	AND ((@Universe_Name IS NULL) OR (@Universe_Name IS NOT NULL AND [Universe_Name] = @Universe_Name))
END
GO

CREATE PROCEDURE [Universe_Put] 
	@Universe_ID INT = NULL OUTPUT,
	@Universe_Name NVARCHAR(250) = NULL,
	@Logo_Url NVARCHAR(MAX) = NULL
AS
BEGIN
	IF @Universe_ID IS NULL
	BEGIN
		IF @Universe_Name IS NULL
			RAISERROR ('Universe name can not be null when creating a new Universe', 0, 0);
			
		INSERT INTO [Universe] (Universe_Name, Logo_Url) VALUES (
			@Universe_Name, @Logo_Url
		);

		SET @Universe_ID = SCOPE_IDENTITY();
	END
	ELSE
	BEGIN
		UPDATE [Universe] SET
			Universe_Name = CASE WHEN (@Universe_Name IS NOT NULL) THEN @Universe_Name ELSE Universe_Name END,
			Logo_Url = CASE WHEN (@Logo_Url IS NOT NULL) THEN @Logo_Url ELSE Logo_Url END
			WHERE [Universe_ID] = @Universe_ID;
	END
	
	RETURN;
END
GO

CREATE PROCEDURE [Universe_Del]
	@Universe_ID INT OUTPUT
AS
BEGIN
	DELETE FROM [Universe] WHERE [Universe_ID] = @Universe_ID;
END
GO

CREATE PROCEDURE [Hero_Bios_Get]
	@Hero_ID INT
AS
BEGIN
	SELECT 
		[Hero_Bio_ID],
		[Order],
		[Header],
		[Hero_Bio]
	FROM [Hero_Bios] WHERE [Hero_ID] = @Hero_Id
	ORDER BY [Order] ASC
END
GO

CREATE PROCEDURE [Hero_Bios_Put]
	@Hero_Bio_ID INT = NULL OUTPUT,
	@Hero_ID INT = NULL,
	@Order INT = NULL,
	@Hero_Bio VARCHAR(MAX) = NULL,
	@Header NVARCHAR(25) = NULL
AS
BEGIN
	IF @Hero_Bio_ID IS NULL
	BEGIN
		IF @Hero_ID IS NULL
			RAISERROR ('All bio entries must be assigned to a hero', 0, 0);

		IF @Order IS NULL
			SET @Order = 1;

		IF @Header IS NULL
			SET @Header = 'Bio';

		IF @Hero_Bio IS NULL
			RAISERROR ('All bio entries must be filled out', 0, 0);
			
		INSERT INTO [Hero_Bios] ([Hero_Bio], [Hero_ID], [Order], [Header]) VALUES (
			@Hero_Bio, @Hero_ID, @Order, @Header
		);

		SET @Hero_Bio_ID = SCOPE_IDENTITY();
	END
	ELSE
	BEGIN
		UPDATE [Hero_Bios] SET
			[Hero_Bio] = CASE WHEN (@Hero_Bio IS NOT NULL) THEN @Hero_Bio ELSE [Hero_Bio] END,
			[Order] = CASE WHEN (@Order IS NOT NULL) THEN @Order ELSE [Order] END,
			[Header] = CASE WHEN (@Header IS NOT NULL) THEN @Header ELSE [Header] END
			WHERE [Hero_Bio_ID] = @Hero_Bio_ID;
	END
END
GO

CREATE PROCEDURE [Hero_Bios_Del]
	@Hero_Bio_ID INT OUTPUT
AS
BEGIN
	DELETE FROM [Hero_Bios] WHERE [Hero_Bio_ID] = @Hero_Bio_ID;
END
GO

DECLARE @Hero_ID INT;
DECLARE @Power_ID INT;
DECLARE @Universe_ID INT;

EXEC [Power_Put]
	@Power_ID = @Power_ID OUTPUT,
	@Power_Name = 'Electrokinesis',
	@Power_Description = 'The ability to conduct and direct electricity and lightning psychokinetically.';

EXEC [Universe_Put]
	@Universe_ID = @Universe_ID OUTPUT,
	@Universe_Name = 'A Certain Magical Index',
	@Logo_Url = 'https://upload.wikimedia.org/wikipedia/commons/thumb/9/95/To_aru_majutsu_no_index_logo.svg/800px-To_aru_majutsu_no_index_logo.svg.png';

EXEC [Hero_Put] 
	@Hero_ID = @Hero_ID OUTPUT,
	@Hero_Name = 'Mikoto Misaka',
	@Power_Level = 'Level 5, Partial Level 6 Shifted',
	@Picture_Url = 'https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/d573ac93-3845-4bff-9497-795de20dcf00/daolm4k-491e5fd9-6f72-4e88-afe7-f65faacce66d.png/v1/fill/w_1024,h_1890,strp/misaka_mikoto_hypnosis_rp__read_description__by_hypnolover12_daolm4k-fullview.png?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOiIsImlzcyI6InVybjphcHA6Iiwib2JqIjpbW3siaGVpZ2h0IjoiPD0xODkwIiwicGF0aCI6IlwvZlwvZDU3M2FjOTMtMzg0NS00YmZmLTk0OTctNzk1ZGUyMGRjZjAwXC9kYW9sbTRrLTQ5MWU1ZmQ5LTZmNzItNGU4OC1hZmU3LWY2NWZhYWNjZTY2ZC5wbmciLCJ3aWR0aCI6Ijw9MTAyNCJ9XV0sImF1ZCI6WyJ1cm46c2VydmljZTppbWFnZS5vcGVyYXRpb25zIl19.vtsYbHKWhRiokg2AruotYHN0cGDQgwliptS-lZyN0Po',
	@Universe_ID = @Universe_ID;

INSERT INTO [Hero_Power_Association] ([Hero_ID], [Power_ID]) VALUES (
	@Hero_ID,
	@Power_ID
);

EXEC [Hero_Bios_Put]
	@Hero_ID = @Hero_ID,
	@Order = 1,
	@Header = 'Bio',
	@Hero_Bio = 'The third strongest level 5 esper of Academy City. Mikoto is a powerful electromaster capable of launching large metallic objects at supersonic speed with electromagnetism, calling bolts of lightning, and manipulating electronics through electrical signals.'
GO

DECLARE @Hero_ID INT;
DECLARE @Power_ID INT;
DECLARE @Universe_ID INT;

EXEC [Power_Put]
	@Power_ID = @Power_ID OUTPUT,
	@Power_Name = 'One For All',
	@Power_Description = 'A power that is passed down from one generation to the next, growing more and more powerful with each successor. Allows the user to Smash through virtually anything.';

EXEC [Universe_Put]
	@Universe_ID = @Universe_ID OUTPUT,
	@Universe_Name = 'My Hero Academia',
	@Logo_Url = 'https://i.pinimg.com/originals/4c/67/51/4c67516ab6bf8f6ebda56b8bfb064d41.png';

EXEC [Hero_Put] 
	@Hero_ID = @Hero_ID OUTPUT,
	@Hero_Name = 'Izuku Midoriya',
	@Power_Level = 'Mountain level',
	@Picture_Url = 'https://vignette.wikia.nocookie.net/vsbattles/images/f/f4/Izuku_Midoriya_Full_Cowl.png/revision/latest?cb=20200510063038',
	@Universe_ID = @Universe_ID;

INSERT INTO [Hero_Power_Association] ([Hero_ID], [Power_ID]) VALUES (
	@Hero_ID,
	@Power_ID
);

EXEC [Hero_Bios_Put]
	@Hero_ID = @Hero_ID,
	@Order = 1,
	@Header = 'Bio',
	@Hero_Bio = 'Also known as Deku. Though born Quirkless, Izuku manages to catch the attention of the legendary hero All Might due to his innate heroism and a strong sense of justice and has since become his close pupil as well as a student in Class 1-A at U.A. High School. All Might passed on his Quirk to Izuku, making him the ninth holder of One For All.'
GO