USE [master]
GO

CREATE DATABASE [JNDataBase]
GO

USE [JNDataBase]
GO

CREATE TABLE [dbo].[TUsuario](
	[IdUsuario] [bigint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](255) NOT NULL,
	[Correo] [varchar](100) NOT NULL,
	[NombreUsuario] [varchar](20) NOT NULL,
	[Contrasenna] [varchar](10) NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_TUsuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[TUsuario] ADD  CONSTRAINT [uk_Correo] UNIQUE NONCLUSTERED 
(
	[Correo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

ALTER TABLE [dbo].[TUsuario] ADD  CONSTRAINT [uk_NombreUsuario] UNIQUE NONCLUSTERED 
(
	[NombreUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

CREATE PROCEDURE [dbo].[RegistrarUsuario]
	@Nombre varchar(255),
	@Correo varchar(100),
	@NombreUsuario varchar(20),
	@Contrasenna varchar(10),
	@Estado bit
AS
BEGIN
	
	IF NOT EXISTS(SELECT 1 FROM dbo.TUsuario
				  WHERE NombreUsuario = @NombreUsuario
					OR Correo = @Correo)
	BEGIN

		INSERT INTO dbo.TUsuario (Nombre,Correo,NombreUsuario,Contrasenna,Estado)
		VALUES (@Nombre,@Correo,@NombreUsuario,@Contrasenna,@Estado)

	END

END
GO

CREATE PROCEDURE [dbo].[ValidarInicioSesion]
	@NombreUsuario varchar(20),
	@Contrasenna varchar(10)
AS
BEGIN

	SELECT	IdUsuario,
			Nombre,
			Correo,
			NombreUsuario,
			Estado
	  FROM	dbo.TUsuario
	WHERE	NombreUsuario = @NombreUsuario
		AND Contrasenna = @Contrasenna
		AND Estado = 1
	
END
GO