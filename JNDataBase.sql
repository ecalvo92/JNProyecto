USE [master]
GO

CREATE DATABASE [JNDataBase]
GO

USE [JNDataBase]
GO

CREATE TABLE [dbo].[TError](
	[IdError] [bigint] IDENTITY(1,1) NOT NULL,
	[FechaHora] [datetime] NOT NULL,
	[DescripcionError] [varchar](max) NOT NULL,
	[Origen] [varchar](255) NOT NULL,
	[IdUsuario] [bigint] NOT NULL,
 CONSTRAINT [PK_TError] PRIMARY KEY CLUSTERED 
(
	[IdError] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[TUsuario](
	[IdUsuario] [bigint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](255) NOT NULL,
	[Correo] [varchar](100) NOT NULL,
	[Identificacion] [varchar](20) NOT NULL,
	[Contrasenna] [varchar](10) NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_TUsuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[TError] ON 
GO
INSERT [dbo].[TError] ([IdError], [FechaHora], [DescripcionError], [Origen], [IdUsuario]) VALUES (1, CAST(N'2025-06-19T18:41:45.287' AS DateTime), N'Procedure or function ''ValidarInicioSesion'' expects parameter ''@Contrasenna'', which was not supplied.', N'/api/Home/Index', 0)
GO
INSERT [dbo].[TError] ([IdError], [FechaHora], [DescripcionError], [Origen], [IdUsuario]) VALUES (2, CAST(N'2025-06-19T19:20:10.573' AS DateTime), N'Could not find stored procedure ''ValidarInicioSesion2''.', N'/api/Home/Index', 0)
GO
SET IDENTITY_INSERT [dbo].[TError] OFF
GO

SET IDENTITY_INSERT [dbo].[TUsuario] ON 
GO
INSERT [dbo].[TUsuario] ([IdUsuario], [Nombre], [Correo], [Identificacion], [Contrasenna], [Estado]) VALUES (1, N'Eduardo Calvo Castillo', N'ecalvo90415@ufide.ac.cr', N'304590415', N'90415', 1)
GO
SET IDENTITY_INSERT [dbo].[TUsuario] OFF
GO

ALTER TABLE [dbo].[TUsuario] ADD  CONSTRAINT [uk_Correo] UNIQUE NONCLUSTERED 
(
	[Correo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

ALTER TABLE [dbo].[TUsuario] ADD  CONSTRAINT [uk_NombreUsuario] UNIQUE NONCLUSTERED 
(
	[Identificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

CREATE PROCEDURE [dbo].[RegistrarError]
	@DescripcionError varchar(max),
	@Origen varchar(255),
	@IdUsuario bigint
AS
BEGIN

	INSERT INTO dbo.TError (FechaHora,DescripcionError,Origen,IdUsuario)
	VALUES (GETDATE(),@DescripcionError,@Origen,@IdUsuario)

END
GO

CREATE PROCEDURE [dbo].[RegistrarUsuario]
	@Nombre varchar(255),
	@Correo varchar(100),
	@Identificacion varchar(20),
	@Contrasenna varchar(10),
	@Estado bit
AS
BEGIN
	
	IF NOT EXISTS(SELECT 1 FROM dbo.TUsuario
				  WHERE Identificacion = @Identificacion
					OR Correo = @Correo)
	BEGIN

		INSERT INTO dbo.TUsuario (Nombre,Correo,Identificacion,Contrasenna,Estado)
		VALUES (@Nombre,@Correo,@Identificacion,@Contrasenna,@Estado)

	END

END
GO

CREATE PROCEDURE [dbo].[ValidarInicioSesion]
	@Correo varchar(100),
	@Contrasenna varchar(10)
AS
BEGIN

	SELECT	IdUsuario,
			Nombre,
			Correo,
			Identificacion,
			Estado
	  FROM	dbo.TUsuario
	WHERE	Correo = @Correo
		AND Contrasenna = @Contrasenna
		AND Estado = 1
	
END
GO