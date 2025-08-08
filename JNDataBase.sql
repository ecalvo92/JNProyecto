USE [master]
GO

CREATE DATABASE [JNDataBase]
GO

USE [JNDataBase]
GO

CREATE TABLE [dbo].[TCarrito](
	[IdCarrito] [bigint] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [bigint] NOT NULL,
	[IdProducto] [bigint] NOT NULL,
	[Cantidad] [int] NOT NULL,
	[Fecha] [datetime] NOT NULL,
 CONSTRAINT [PK_TCarrito] PRIMARY KEY CLUSTERED 
(
	[IdCarrito] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
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

CREATE TABLE [dbo].[TProducto](
	[IdProducto] [bigint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](255) NOT NULL,
	[Precio] [decimal](10, 2) NOT NULL,
	[Inventario] [int] NOT NULL,
	[Imagen] [varchar](255) NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_TProducto] PRIMARY KEY CLUSTERED 
(
	[IdProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TRol](
	[IdRol] [int] IDENTITY(1,1) NOT NULL,
	[NombreRol] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TRol] PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TUsuario](
	[IdUsuario] [bigint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](255) NOT NULL,
	[Correo] [varchar](100) NOT NULL,
	[Identificacion] [varchar](20) NOT NULL,
	[Contrasenna] [varchar](255) NOT NULL,
	[Estado] [bit] NOT NULL,
	[IdRol] [int] NOT NULL,
 CONSTRAINT [PK_TUsuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[TCarrito] ON 
GO
INSERT [dbo].[TCarrito] ([IdCarrito], [IdUsuario], [IdProducto], [Cantidad], [Fecha]) VALUES (1, 2, 1, 3, CAST(N'2025-08-07T20:54:32.420' AS DateTime))
GO
INSERT [dbo].[TCarrito] ([IdCarrito], [IdUsuario], [IdProducto], [Cantidad], [Fecha]) VALUES (2, 2, 2, 2, CAST(N'2025-08-07T20:54:44.500' AS DateTime))
GO
INSERT [dbo].[TCarrito] ([IdCarrito], [IdUsuario], [IdProducto], [Cantidad], [Fecha]) VALUES (3, 2, 3, 1, CAST(N'2025-08-07T20:54:45.783' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[TCarrito] OFF
GO

SET IDENTITY_INSERT [dbo].[TError] ON 
GO
INSERT [dbo].[TError] ([IdError], [FechaHora], [DescripcionError], [Origen], [IdUsuario]) VALUES (1, CAST(N'2025-06-19T18:41:45.287' AS DateTime), N'Procedure or function ''ValidarInicioSesion'' expects parameter ''@Contrasenna'', which was not supplied.', N'/api/Home/Index', 0)
GO
INSERT [dbo].[TError] ([IdError], [FechaHora], [DescripcionError], [Origen], [IdUsuario]) VALUES (2, CAST(N'2025-06-19T19:20:10.573' AS DateTime), N'Could not find stored procedure ''ValidarInicioSesion2''.', N'/api/Home/Index', 0)
GO
INSERT [dbo].[TError] ([IdError], [FechaHora], [DescripcionError], [Origen], [IdUsuario]) VALUES (3, CAST(N'2025-07-17T18:23:49.467' AS DateTime), N'Object reference not set to an instance of an object.', N'/api/Home/Index', 0)
GO
SET IDENTITY_INSERT [dbo].[TError] OFF
GO

SET IDENTITY_INSERT [dbo].[TProducto] ON 
GO
INSERT [dbo].[TProducto] ([IdProducto], [Nombre], [Descripcion], [Precio], [Inventario], [Imagen], [Estado]) VALUES (1, N'Ps5', N'Play Station 5', CAST(700.00 AS Decimal(10, 2)), 6, N'/productos/1.png', 1)
GO
INSERT [dbo].[TProducto] ([IdProducto], [Nombre], [Descripcion], [Precio], [Inventario], [Imagen], [Estado]) VALUES (2, N'Ps4', N'Play Station 4', CAST(400.00 AS Decimal(10, 2)), 2, N'/productos/2.png', 1)
GO
INSERT [dbo].[TProducto] ([IdProducto], [Nombre], [Descripcion], [Precio], [Inventario], [Imagen], [Estado]) VALUES (3, N'ps 2', N'Play Station 2', CAST(1200.00 AS Decimal(10, 2)), 10, N'/productos/3.png', 1)
GO
SET IDENTITY_INSERT [dbo].[TProducto] OFF
GO

SET IDENTITY_INSERT [dbo].[TRol] ON 
GO
INSERT [dbo].[TRol] ([IdRol], [NombreRol]) VALUES (1, N'Usuario Regular')
GO
INSERT [dbo].[TRol] ([IdRol], [NombreRol]) VALUES (2, N'Usuario Administrador')
GO
SET IDENTITY_INSERT [dbo].[TRol] OFF
GO

SET IDENTITY_INSERT [dbo].[TUsuario] ON 
GO
INSERT [dbo].[TUsuario] ([IdUsuario], [Nombre], [Correo], [Identificacion], [Contrasenna], [Estado], [IdRol]) VALUES (1, N'SEBASTIAN HERNANDEZ JIMENEZ', N'shernandez30307@ufide.ac.cr', N'402530307', N'kFBKAQ5IFs59aQ6uT5zSEg==', 1, 2)
GO
INSERT [dbo].[TUsuario] ([IdUsuario], [Nombre], [Correo], [Identificacion], [Contrasenna], [Estado], [IdRol]) VALUES (2, N'EDUARDO JOSE CALVO CASTILLO', N'ecalvo90415@ufide.ac.cr', N'304590415', N'zuIPiReYqBazwDmqy/SEBA==', 1, 1)
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

ALTER TABLE [dbo].[TCarrito]  WITH CHECK ADD  CONSTRAINT [FK_TCarrito_TProducto] FOREIGN KEY([IdProducto])
REFERENCES [dbo].[TProducto] ([IdProducto])
GO
ALTER TABLE [dbo].[TCarrito] CHECK CONSTRAINT [FK_TCarrito_TProducto]
GO

ALTER TABLE [dbo].[TCarrito]  WITH CHECK ADD  CONSTRAINT [FK_TCarrito_TUsuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[TUsuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[TCarrito] CHECK CONSTRAINT [FK_TCarrito_TUsuario]
GO

ALTER TABLE [dbo].[TUsuario]  WITH CHECK ADD  CONSTRAINT [FK_TUsuario_TRol] FOREIGN KEY([IdRol])
REFERENCES [dbo].[TRol] ([IdRol])
GO
ALTER TABLE [dbo].[TUsuario] CHECK CONSTRAINT [FK_TUsuario_TRol]
GO

CREATE PROCEDURE [dbo].[ActualizarContrasenna]
	@IdUsuario bigint,
	@Contrasenna varchar(255)
AS
BEGIN
	
	UPDATE	TUsuario
	SET		Contrasenna = @Contrasenna
	WHERE	IdUsuario = @IdUsuario

END
GO

CREATE PROCEDURE [dbo].[ActualizarDatosUsuario]
	@IdRol int,
	@Estado bit,
	@IdUsuario bigint
AS
BEGIN

	UPDATE	TUsuario
	SET		IdRol = @IdRol,
			Estado = @Estado
	WHERE	IdUsuario = @IdUsuario

END
GO

CREATE PROCEDURE [dbo].[ActualizarProducto]
	@IdProducto BIGINT,
	@Nombre varchar(50),
	@Descripcion varchar(255),
	@Precio decimal(10,2),
	@Inventario int
AS
BEGIN

	UPDATE	dbo.TProducto
	SET		Nombre = @Nombre,
			Descripcion = @Descripcion,
			Precio = @Precio,
			Inventario = @Inventario
	WHERE	IdProducto = @IdProducto

END
GO

CREATE PROCEDURE [dbo].[ActualizarUsuario]
	@Identificacion varchar(20),
	@Nombre varchar(255),
	@Correo varchar(100),
	@IdUsuario bigint
AS
BEGIN
	
	IF NOT EXISTS(SELECT 1 FROM dbo.TUsuario
				  WHERE Identificacion = @Identificacion
					AND Correo = @Correo
					AND IdUsuario != @IdUsuario)
	BEGIN

		UPDATE	TUsuario
		SET		Identificacion = @Identificacion,
				Nombre = @Nombre,
				Correo =  @Correo
		WHERE	IdUsuario = @IdUsuario

	END

END
GO

CREATE PROCEDURE [dbo].[ConsultarProducto]
	@IdProducto BIGINT
AS
BEGIN

	SELECT	IdProducto,
			Nombre,
			Descripcion,
			Precio,
			Inventario,
			Imagen,
			Estado
	  FROM	dbo.TProducto
	  WHERE IdProducto = @IdProducto
	
END
GO

CREATE PROCEDURE [dbo].[ConsultarProductos]

AS
BEGIN

	SELECT	IdProducto,
			Nombre,
			Descripcion,
			Precio,
			Inventario,
			Imagen,
			Estado
	  FROM	dbo.TProducto
	  ORDER BY IdProducto ASC
	
END
GO

CREATE PROCEDURE [dbo].[ConsultarRoles]

AS
BEGIN

	SELECT	IdRol,
			NombreRol
	  FROM	dbo.TRol
	
END
GO

CREATE PROCEDURE [dbo].[ConsultarUsuario]
	@IdUsuario BIGINT
AS
BEGIN

	SELECT	IdUsuario,
			Nombre,
			Correo,
			Identificacion,
			Estado,
			U.IdRol,
			R.NombreRol
	  FROM	dbo.TUsuario U
	  INNER JOIN dbo.TRol R ON U.IdRol = R.IdRol
	WHERE	IdUsuario = @IdUsuario
	
END
GO

CREATE PROCEDURE [dbo].[ConsultarUsuarios]

AS
BEGIN

	SELECT	IdUsuario,
			Nombre,
			Correo,
			Identificacion,
			Estado,
			U.IdRol,
			R.NombreRol
	  FROM	dbo.TUsuario U
	  INNER JOIN dbo.TRol R ON U.IdRol = R.IdRol
	  ORDER BY IdUsuario ASC
	
END
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

CREATE PROCEDURE [dbo].[RegistrarProducto]
	@Nombre varchar(50),
	@Descripcion varchar(255),
	@Precio decimal(10,2),
	@Inventario int
AS
BEGIN

	DECLARE @RUTA VARCHAR(255) = '/productos/'
	
	INSERT INTO dbo.TProducto(Nombre,Descripcion,Precio,Inventario,Imagen,Estado)
    VALUES (@Nombre,@Descripcion,@Precio,@Inventario,'',1)

	DECLARE @IdProductoGenerado BIGINT = @@IDENTITY

	UPDATE	dbo.TProducto
	SET		Imagen = @RUTA + CONVERT(VARCHAR,@IdProductoGenerado) + '.png'
	WHERE	IdProducto = @IdProductoGenerado

	SELECT @IdProductoGenerado 'IdProducto'

END
GO

CREATE PROCEDURE [dbo].[RegistrarProductoCarrito]
	@IdUsuario bigint,
	@IdProducto bigint
AS
BEGIN

	IF NOT EXISTS(SELECT 1 FROM TCarrito 
				  WHERE IdUsuario = @IdUsuario 
					AND IdProducto = @IdProducto)
	BEGIN

		INSERT INTO dbo.TCarrito(IdUsuario,IdProducto,Cantidad,Fecha)
		VALUES (@IdUsuario,@IdProducto,1,GETDATE())

	END
	ELSE
	BEGIN
		
		UPDATE	dbo.TCarrito
		SET		Cantidad = Cantidad + 1,
				Fecha = GETDATE()
		WHERE	IdUsuario = @IdUsuario 
			AND IdProducto = @IdProducto

	END

END
GO

CREATE PROCEDURE [dbo].[RegistrarUsuario]
	@Nombre varchar(255),
	@Correo varchar(100),
	@Identificacion varchar(20),
	@Contrasenna varchar(255),
	@Estado bit
AS
BEGIN
	
	IF NOT EXISTS(SELECT 1 FROM dbo.TUsuario
				  WHERE Identificacion = @Identificacion
					OR Correo = @Correo)
	BEGIN

		INSERT INTO dbo.TUsuario (Nombre,Correo,Identificacion,Contrasenna,Estado,IdRol)
		VALUES (@Nombre,@Correo,@Identificacion,@Contrasenna,@Estado,1)

	END

END
GO

CREATE PROCEDURE [dbo].[ValidarCorreo]
	@Correo varchar(100)
AS
BEGIN

	SELECT	IdUsuario,
			Nombre,
			Correo,
			Identificacion,
			Estado
	  FROM	dbo.TUsuario
	WHERE	Correo = @Correo
		AND Estado = 1
	
END
GO

CREATE PROCEDURE [dbo].[ValidarInicioSesion]
	@Correo varchar(100),
	@Contrasenna varchar(255)
AS
BEGIN

	SELECT	IdUsuario,
			Nombre,
			Correo,
			Identificacion,
			Estado,
			U.IdRol,
			R.NombreRol
	  FROM	dbo.TUsuario U
	  INNER JOIN dbo.TRol R ON U.IdRol = R.IdRol
	WHERE	Correo = @Correo
		AND Contrasenna = @Contrasenna
		AND Estado = 1
	
END
GO