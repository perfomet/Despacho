/********************************************************|
* CREADO POR: David Perdig�n ****************************
* FECHA Y HORA: 07/01/2020 23:00 ************************
********************************************************/

USE master

IF EXISTS(SELECT 1 FROM sys.databases WHERE name LIKE 'Despacho') BEGIN
	DROP DATABASE Despacho
END

CREATE DATABASE Despacho

USE Despacho

CREATE TABLE Cliente (
	ClienteId INT PRIMARY KEY IDENTITY(2001, 1),
	Nombre VARCHAR(255) NOT NULL,
	RUT VARCHAR(12) NOT NULL
)

SET IDENTITY_INSERT Cliente ON

INSERT INTO Cliente (ClienteId, Nombre, RUT) VALUES (1, 'Cliente de pruebas', '11111111-1')

SET IDENTITY_INSERT Cliente OFF

CREATE TABLE Perfil (
	PerfilId INT PRIMARY KEY IDENTITY,
	Descripcion VARCHAR(50) NOT NULL UNIQUE
)

SET IDENTITY_INSERT Perfil ON

INSERT INTO Perfil (PerfilId, Descripcion) VALUES (1, 'SuperUsuario') 
INSERT INTO Perfil (PerfilId, Descripcion) VALUES (2, 'Administrador') 
INSERT INTO Perfil (PerfilId, Descripcion) VALUES (3, 'Planificador') 
INSERT INTO Perfil (PerfilId, Descripcion) VALUES (4, 'Cliente') 
INSERT INTO Perfil (PerfilId, Descripcion) VALUES (5, 'Bodega') 

SET IDENTITY_INSERT Perfil OFF

CREATE TABLE Usuario (
	UsuarioId INT PRIMARY KEY IDENTITY(1001, 1),
	Username VARCHAR(15) NOT NULL UNIQUE,
	Password VARCHAR(15) NOT NULL,
	Nombres VARCHAR(255) NOT NULL,
	ApellidoPaterno VARCHAR(255) NULL,
	ApellidoMaterno VARCHAR(50) NULL,
	Email VARCHAR(255) NOT NULL,
	PerfilId INT NOT NULL FOREIGN KEY REFERENCES Perfil (PerfilId),
	ClienteId INT NULL FOREIGN KEY REFERENCES Cliente (ClienteId)
)

INSERT INTO Usuario (PerfilId, Username, Password, Nombres, ApellidoPaterno, ApellidoMaterno, Email, ClienteId) 
VALUES (1, 'dperdigon', '123', 'David', 'Perdig�n', 'Garc�a', 'davidperdigon.g@gmail.com', NULL)
INSERT INTO Usuario (PerfilId, Username, Password, Nombres, ApellidoPaterno, ApellidoMaterno, Email, ClienteId)  
VALUES (1, 'fbalmaceda', '123', 'Fancisco', 'Balmaseda', 'Neyra', 'francisco.vivacuba@gmail.com', NULL)
INSERT INTO Usuario (PerfilId, Username, Password, Nombres, ApellidoPaterno, ApellidoMaterno, Email, ClienteId)  
VALUES (2, 'administrador', '123', 'Usuario', 'Administrador', NULL, 'pruebas@prueba.cl', NULL)
INSERT INTO Usuario (PerfilId, Username, Password, Nombres, ApellidoPaterno, ApellidoMaterno, Email, ClienteId)  
VALUES (3, 'planificador', '123', 'Usuario', 'Planificador', NULL, 'pruebas@prueba.cl', NULL)
INSERT INTO Usuario (PerfilId, Username, Password, Nombres, ApellidoPaterno, ApellidoMaterno, Email, ClienteId)  
VALUES (4, 'cliente', '123', 'Usuario', 'Cliente', NULL, 'pruebas@prueba.cl', NULL)
INSERT INTO Usuario (PerfilId, Username, Password, Nombres, ApellidoPaterno, ApellidoMaterno, Email, ClienteId)  
VALUES (5, 'bodega', '123', 'Usuario', 'Bodega', NULL, 'pruebas@prueba.cl', NULL)

CREATE TABLE TipoSolicitud (
	TipoSolicitudId INT PRIMARY KEY IDENTITY,
	Descripcion VARCHAR(100) NOT NULL UNIQUE,
	Observaciones VARCHAR(255) NULL
)

SET IDENTITY_INSERT TipoSolicitud ON

INSERT INTO TipoSolicitud (TipoSolicitudId, Descripcion, Observaciones) VALUES (1, 'Despacho (Entrega)', 'Cliente retira producto en la bodega de MiLogistic')
INSERT INTO TipoSolicitud (TipoSolicitudId, Descripcion, Observaciones) VALUES (2, 'Despacho y Distribuci�n', 'Enviar producto desde bodegas MiLogistic y llevarlo al cliente final')
INSERT INTO TipoSolicitud (TipoSolicitudId, Descripcion, Observaciones) VALUES (3, 'Retiro (Entrega en bodega MiLogistic)', 'Busca equipo en la ubicaci�n del cliente y lo lleva a una bodega de MiLogistic')
INSERT INTO TipoSolicitud (TipoSolicitudId, Descripcion, Observaciones) VALUES (4, 'Traslado', 'Solo entre bodegas MiLogistic')
INSERT INTO TipoSolicitud (TipoSolicitudId, Descripcion, Observaciones) VALUES (5, 'Distribuci�n', 'Busca producto en x lugar y entrega al cliente final')

SET IDENTITY_INSERT TipoSolicitud OFF

CREATE TABLE EmpresaTransporte (
	EmpresaTransporteId INT PRIMARY KEY IDENTITY,
	Nombre VARCHAR(255) NOT NULL UNIQUE,
	EsPropia BIT NOT NULL,
)

SET IDENTITY_INSERT EmpresaTransporte ON

INSERT INTO EmpresaTransporte (EmpresaTransporteId, Nombre, EsPropia) VALUES (1, 'MiLogistic', 1)
INSERT INTO EmpresaTransporte (EmpresaTransporteId, Nombre, EsPropia) VALUES (2, 'Otra empresa externa', 0)

SET IDENTITY_INSERT EmpresaTransporte OFF 

CREATE TABLE Camion (
	Patente VARCHAR(10) PRIMARY KEY,
	Descripcion VARCHAR(255) NOT NULL,
	EmpresaTransporteId INT NOT NULL FOREIGN KEY REFERENCES EmpresaTransporte (EmpresaTransporteId)
	UNIQUE (Descripcion, EmpresaTransporteId)
)

INSERT INTO Camion VALUES ('KXLS-90', 'Cami�n de Prueba 1', 1)
INSERT INTO Camion VALUES ('CFDB-82', 'Cami�n de Prueba 2', 2)

CREATE TABLE EstadoEquipo (
	EstadoEquipoId INT PRIMARY KEY IDENTITY,
	Descripcion VARCHAR(255) NOT NULL UNIQUE
)

SET IDENTITY_INSERT EstadoEquipo ON

INSERT INTO EstadoEquipo (EstadoEquipoId, Descripcion) VALUES (1, 'Usado')
INSERT INTO EstadoEquipo (EstadoEquipoId, Descripcion) VALUES (2, 'Nuevo')
INSERT INTO EstadoEquipo (EstadoEquipoId, Descripcion) VALUES (3, 'Reparado (Usado)')
INSERT INTO EstadoEquipo (EstadoEquipoId, Descripcion) VALUES (4, 'De baja')

SET IDENTITY_INSERT EstadoEquipo OFF 

CREATE TABLE UnidadMedida (
	UnidadMedidaId INT PRIMARY KEY IDENTITY,
	Descripcion VARCHAR(10) NOT NULL UNIQUE
)

SET IDENTITY_INSERT UnidadMedida ON

INSERT INTO UnidadMedida (UnidadMedidaId, Descripcion) VALUES (1, 'M2')
INSERT INTO UnidadMedida (UnidadMedidaId, Descripcion) VALUES (2, 'UND')

SET IDENTITY_INSERT UnidadMedida OFF 

CREATE TABLE Clasificacion (
	ClasificacionId INT PRIMARY KEY IDENTITY,
	Cantidad DECIMAL(18, 1) NOT NULL,
	UnidadMedidaId INT NOT NULL FOREIGN KEY REFERENCES UnidadMedida (UnidadMedidaId),
	UNIQUE (Cantidad, UnidadMedidaId)
)

SET IDENTITY_INSERT Clasificacion ON

INSERT INTO Clasificacion (ClasificacionId, Cantidad, UnidadMedidaId) VALUES (1, 0.5, 1)
INSERT INTO Clasificacion (ClasificacionId, Cantidad, UnidadMedidaId) VALUES (2, 1, 1)
INSERT INTO Clasificacion (ClasificacionId, Cantidad, UnidadMedidaId) VALUES (3, 2, 1)
INSERT INTO Clasificacion (ClasificacionId, Cantidad, UnidadMedidaId) VALUES (4, 3, 1)

SET IDENTITY_INSERT Clasificacion OFF 

CREATE TABLE Prioridad (
	PrioridadId INT PRIMARY KEY IDENTITY,
	Descripcion VARCHAR(10) NOT NULL UNIQUE
)

SET IDENTITY_INSERT Prioridad ON

INSERT INTO Prioridad (PrioridadId, Descripcion) VALUES (1, 'Alta')
INSERT INTO Prioridad (PrioridadId, Descripcion) VALUES (2, 'Media')
INSERT INTO Prioridad (PrioridadId, Descripcion) VALUES (3, 'Baja')

SET IDENTITY_INSERT Prioridad OFF 

CREATE TABLE UnidadNegocio (
	UnidadNegocioId INT PRIMARY KEY IDENTITY,
	Descripcion VARCHAR(255) NOT NULL,
	ClienteId INT NOT NULL FOREIGN KEY REFERENCES Cliente (ClienteId),
	UNIQUE (Descripcion, ClienteId)
)

SET IDENTITY_INSERT UnidadNegocio ON

INSERT INTO UnidadNegocio (UnidadNegocioId, Descripcion, ClienteId) VALUES (1, 'Zona Sur', 1)
INSERT INTO UnidadNegocio (UnidadNegocioId, Descripcion, ClienteId) VALUES (2, 'Zona Norte', 1)
INSERT INTO UnidadNegocio (UnidadNegocioId, Descripcion, ClienteId) VALUES (3, 'Zona Oriente', 1)

SET IDENTITY_INSERT UnidadNegocio OFF 

CREATE TABLE Gerencia (
	GerenciaId INT PRIMARY KEY IDENTITY,
	Descripcion VARCHAR(255) NOT NULL,
	ClienteId INT NOT NULL FOREIGN KEY REFERENCES Cliente (ClienteId),
	UNIQUE (Descripcion, ClienteId)
)

SET IDENTITY_INSERT Gerencia ON

INSERT INTO Gerencia (GerenciaId, Descripcion, ClienteId) VALUES (1, 'Fr�o', 1)
INSERT INTO Gerencia (GerenciaId, Descripcion, ClienteId) VALUES (2, 'Calor', 1)

SET IDENTITY_INSERT Gerencia OFF

CREATE TABLE Enlace (
	EnlaceId INT PRIMARY KEY IDENTITY,
	Descripcion VARCHAR(255) NOT NULL UNIQUE
)

CREATE TABLE TipoDocumento (
	TipoDocumentoId INT PRIMARY KEY IDENTITY,
	Descripcion VARCHAR(255) NOT NULL UNIQUE
)

SET IDENTITY_INSERT TipoDocumento ON

INSERT INTO TipoDocumento (TipoDocumentoId, Descripcion) VALUES (1, 'Boleta')
INSERT INTO TipoDocumento (TipoDocumentoId, Descripcion) VALUES (2, 'Factura')
INSERT INTO TipoDocumento (TipoDocumentoId, Descripcion) VALUES (3, 'Gu�a Despacho')

SET IDENTITY_INSERT TipoDocumento OFF

CREATE TABLE SolicitudDespacho (
	SolicitudDespachoId	INT PRIMARY KEY IDENTITY (1001, 1),
	TipoSolicitudId INT NOT NULL FOREIGN KEY REFERENCES TipoSolicitud (TipoSolicitudId),
	FechaSolicitud DATETIME NOT NULL,
	FechaRecepcion DATETIME NOT NULL,
	Denominacion VARCHAR(255) NOT NULL,
	Material VARCHAR(50) NOT NULL,
	Marca VARCHAR(50) NOT NULL,
	BodegaOrigen VARCHAR(10) NOT NULL,
	EstadoEquipoId INT NOT NULL FOREIGN KEY REFERENCES EstadoEquipo (EstadoEquipoId),
	ClasificacionId INT NOT NULL FOREIGN KEY REFERENCES Clasificacion (ClasificacionId),
	NumeroCliente VARCHAR(50) NOT NULL,
	NombreCliente VARCHAR(255) NOT NULL,
	DireccionCliente VARCHAR(255) NOT NULL,
	--ComunaId INT NOT NULL FOREIGN KEY REFERENCES Comuna (ComunaId), -- ESTA ES LA COLUMNA CORRECTA DE COMUNA PERO SE HABILITAR� CUANDO TENGAMOS LA TABLA EN EL SCRIPT
	Comuna VARCHAR(100) NOT NULL,
	NumeroTelefonoContacto VARCHAR(15) NULL,
	RUT VARCHAR(12) NOT NULL,
	Proyecto VARCHAR(100) NOT NULL,
	PrioridadId INT NOT NULL FOREIGN KEY REFERENCES Prioridad (PrioridadId),
	UnidadNegocioId INT NOT NULL FOREIGN KEY REFERENCES UnidadNegocio (UnidadNegocioId),
	GerenciaId INT NOT NULL FOREIGN KEY REFERENCES Gerencia (GerenciaId),
	ObservacionAof VARCHAR(MAX) NULL,
	NumeroPlaca	INT NOT NULL,
	FechaDespacho DATETIME NOT NULL,
	PatenteCamion VARCHAR(10) NOT NULL FOREIGN KEY REFERENCES Camion (Patente),
	LlamadaDiaAnterior BIT NOT NULL,
	ComentariosLlamada VARCHAR(MAX) NULL,
	EnlaceId INT NOT NULL FOREIGN KEY REFERENCES Enlace (EnlaceId),
	NumeroDocumento	INT NOT NULL,
	NumeroEntrega INT NOT NULL,
	FechaEntregaDocumento DATETIME NOT NULL,
	FechaRecepcionDocumento	DATETIME NOT NULL,
	Folio INT NOT NULL,
	TipoDocumentoId INT NOT NULL FOREIGN KEY REFERENCES TipoDocumento (TipoDocumentoId),
	Concrecion BIT NULL,
	NombreConcrecion VARCHAR(255) NULL,
	RUTConcrecion VARCHAR(12) NULL,
	MotivoNoConcrecion VARCHAR(MAX) NULL,
	RetiroReal INT NULL
)
CREATE TABLE [Codificador_Region](
	[IdRegion] [int] IDENTITY(1,1) NOT NULL,
	[Region] [nvarchar](100) NULL,
 CONSTRAINT [PK_Codificador_Region] PRIMARY KEY CLUSTERED 
(
	[IdRegion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

INSERT INTO [Codificador_Region] ([Region]) VALUES ('TARAPAC�')
INSERT INTO [Codificador_Region] ([Region]) VALUES ('ANTOFAGASTA')
INSERT INTO [Codificador_Region] ([Region]) VALUES ('ATACAMA')
INSERT INTO [Codificador_Region] ([Region]) VALUES ('COQUIMBO')
INSERT INTO [Codificador_Region] ([Region]) VALUES ('REGI�N DE VALPARA�SO')
INSERT INTO [Codificador_Region] ([Region]) VALUES ('REGI�N DEL LIBERTADOR BERNARDO O�HIGGINS')
INSERT INTO [Codificador_Region] ([Region]) VALUES ('REGI�N DEL MAULE')
INSERT INTO [Codificador_Region] ([Region]) VALUES ('REGI�N DEL BIOBIO')
INSERT INTO [Codificador_Region] ([Region]) VALUES ('REGI�N DE LA ARAUCAN�A')
INSERT INTO [Codificador_Region] ([Region]) VALUES ('REGI�N DE LOS LAGOS')
INSERT INTO [Codificador_Region] ([Region]) VALUES ('REGI�N DE AIS�N DEL GENERAL CARLOS IBA�EZ DEL CAMPO')
INSERT INTO [Codificador_Region] ([Region]) VALUES ('REGI�N DE MAGALLANES Y DE LA ANT�RTICA CHILENA')
INSERT INTO [Codificador_Region] ([Region]) VALUES ('REGI�N METROPOLITANA')
INSERT INTO [Codificador_Region] ([Region]) VALUES ('REGI�N DE LOS R�OS')
INSERT INTO [Codificador_Region] ([Region]) VALUES ('ARICA Y PARINACOTA')
INSERT INTO [Codificador_Region] ([Region]) VALUES ('�UBLE')

CREATE TABLE [Codificador_Provincia](
	[idregion] [int] NOT NULL,
	[idprovincia] [int] IDENTITY(1,1) NOT NULL,
	[idordenprovincia] [int] NOT NULL,
	[provincia] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Codificador_Provincia] PRIMARY KEY CLUSTERED 
(
	[idprovincia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Codificador_Provincia]  WITH CHECK ADD  CONSTRAINT [FK_Codificador_Provincia_Codificador_Region] FOREIGN KEY([idregion])
REFERENCES [Codificador_Region] ([IdRegion])
GO

ALTER TABLE [Codificador_Provincia] CHECK CONSTRAINT [FK_Codificador_Provincia_Codificador_Region]

INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (1, 1,'IQUIQUE')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (1, 2,'TAMARUGAL')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (2, 1,'ANTOFAGASTA')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (2, 2,'EL LOA')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (2, 3,'TOCOPILLA')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (3, 1,'COPIAP�')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (3, 2,'CHA�ARAL')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (3, 3,'HUASCO')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (4, 4,'ELQUI')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (4, 5,'CHOAPA')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (4, 6,'LIMAR�')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (5, 1,'VALPARA�SO')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (5, 2,'ISLA DE PASCUA')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (5, 3,'LOS ANDES')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (5, 4,'PETORCA')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (5, 5,'QUILLOTA')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (5, 6,'SAN ANTONIO')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (5, 7,'SAN FELIPE DE ACONCAGUA')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (5, 8,'MARGA MARGA')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (6, 1,'CACHAPOAL')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (6, 2,'CARDENAL CARO')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (6, 3,'COLCHAGUA')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (7, 1,'TALCA')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (7, 2,'CAUQUENES')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (7, 3,'CURIC�')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (7, 4,'LINARES')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (8, 1,'CONCEPCI�N')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (8, 2,'ARAUCO')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (8, 3,'BIOB�O')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (9, 1,'CAUT�N')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (9, 2,'MALLECO')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (10, 1,'LLANQUIHUE')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (10, 2,'CHILO�')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (10, 3,'OSORNO')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (10, 4,'PALENA')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (11, 1,'COIHAIQUE')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (11, 2,'AIS�N')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (11, 3,'CAPIT�N PRAT')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (11, 4,'GENERAL CARRERA')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (12, 1,'MAGALLANES')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (12, 2,'ANT�RTICA CHILENA')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (12, 3,'TIERRA DEL FUEGO')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (12, 4,'�LTIMA ESPERANZA')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (13, 1,'SANTIAGO')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (13, 2,'CORDILLERA')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (13, 3,'CHACABUCO')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (13, 4,'MAIPO')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (13, 5,'MELIPILLA')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (13, 6,'TALAGANTE')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (14, 1,'VALDIVIA')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (14, 2,'RANCO')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (15, 1,'ARICA')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (15, 2,'PARINACOTA')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (16, 1,'DIGUILL�N')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (16, 2,'ITATA')
INSERT INTO [Codificador_Provincia] ([idregion], [idordenprovincia],[provincia]) VALUES (16, 3,'PUNILLA')
)
CREATE TABLE [Codificador_Comuna](
	[idcomuna] [int] IDENTITY(1,1) NOT NULL,
	[idregion] [int] NULL,
	[idprovincia] [int] NULL,
	[comuna] [nvarchar](50) NULL,
 CONSTRAINT [PK_Codificador_Comuna] PRIMARY KEY CLUSTERED 
(
	[idcomuna] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [Codificador_Comuna]  WITH CHECK ADD  CONSTRAINT [FK_Codificador_Comuna_Codificador_Provincia] FOREIGN KEY([idprovincia])
REFERENCES [Codificador_Provincia] ([idprovincia])
GO

ALTER TABLE [Codificador_Comuna] CHECK CONSTRAINT [FK_Codificador_Comuna_Codificador_Provincia]
GO

ALTER TABLE [Codificador_Comuna]  WITH CHECK ADD  CONSTRAINT [FK_Codificador_Comuna_Codificador_Region] FOREIGN KEY([idregion])
REFERENCES [dbo].[Codificador_Region] ([IdRegion])
GO

ALTER TABLE [Codificador_Comuna] CHECK CONSTRAINT [FK_Codificador_Comuna_Codificador_Region]
GO

INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (1, 1,'IQUIQUE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (1, 1,'ALTO HOSPICIO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (1, 2,'POZO ALMONTE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (1, 2,'CAMI�A')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (1, 2,'COLCHANE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (1, 2,'HUARA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (1, 2,'PICA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (2, 3,'ANTOFAGASTA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (2, 3,'MEJILLONES')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (2, 3,'SIERRA GORDA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (2, 3,'TALTAL')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (2, 4,'CALAMA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (2, 4,'OLLAG�E')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (2, 4,'SAN PEDRO DE ATACAMA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (2, 5,'TOCOPILLA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (2, 5,'MAR�A ELENA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (3, 6,'COPIAP�')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (3, 6,'CALDERA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (3, 6,'TIERRA AMARILLA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (3, 7,'CHA�ARAL')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (3, 7,'DIEGO DE ALMAGROL')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (3, 8,'VALLENAR')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (3, 8,'ALTO DEL CARMEN')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (3, 8,'FREIRINA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (3, 8,'HUASCO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (4, 9,'LA SERENA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (4, 9,'COQUIMBO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (4, 9,'ANDACOLLO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (4, 9,'LA HIGUERA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (4, 9,'PAIGUANO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (4, 9,'VICU�A')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (4, 10,'ILLAPEL')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (4, 10,'CANELA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (4, 10,'LOS VILOS')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (4, 10,'SALAMANCA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (4, 11,'OVALLE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (4, 11,'COMBARBAL�')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (4, 11,'MONTE PATRIA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (4, 11,'PUNITAQUI')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (4, 11,'R�O HURTADO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (5, 12,'VALPARA�SO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (5, 12,'CASABLANCA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (5, 12,'CONC�N')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (5, 12,'JUAN FERN�NDEZ')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (5, 12,'PUCHUNCAV�')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (5, 12,'QUINTERO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (5, 12,'QUINTERO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (5, 13,'ISLA DE PASCUA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (5, 14,'LOS ANDES')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (5, 14,'CALLE LARGA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (5, 14,'RINCONADA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (5, 14,'SAN ESTEBAN')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (6,20,'RANCAGUA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (6,20,'CODEGUA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (6,20,'COINCO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (6,20,'COLTAUCO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (6,20,'DO�IHUE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (6,20,'GRANEROS')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (6,20,'LAS CABRAS')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (6,20,'MACHAL�')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (6,20,'MALLOA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (6,20,'MOSTAZAL')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (6,20,'OLIVAR')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (6,20,'PEUMO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (6,20,'PICHIDEGUA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (6,20,'QUINTA DE TILCOCO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (6,20,'RENGO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (6,20,'REQU�NOA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (6,20,'SAN VICENTE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (6,21,'PICHILEMU')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (6,21,'LA ESTRELLA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (6,21,'LITUECHE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (6,21,'MARCHIHUE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (6,21,'NAVIDAD')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (6,21,'PAREDONES')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (6,22,'SAN FERNANDO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (6,22,'CH�PICA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (6,22,'CHIMBARONGO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (6,22,'LOLOL')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (6,22,'NANCAGUA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (6,22,'PALMILLA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (6,22,'PERALILLO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (6,22,'PLACILLA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (6,22,'PUMANQUE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (6,22,'SANTA CRUZ')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (7,23,'TALCA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (7,23,'CONSTITUCI�N')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (7,23,'CUREPTO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (7,23,'EMPEDRADO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (7,23,'MAULE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (7,23,'PELARCO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (7,23,'PENCAHUE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (7,23,'R�O CLARO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (7,23,'SAN CLEMENTE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (7,23,'SAN RAFAEL')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (7,24,'CAUQUENES')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (7,24,'CHANCO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (7,24,'PELLUHUE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (7,25,'CURIC�')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (7,25,'HUALA��')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (7,25,'LICANT�N')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (7,25,'MOLINA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (7,25,'RAUCO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (7,25,'ROMERAL')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (7,25,'SAGRADA FAMILIA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (7,25,'TENO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (7,25,'VICHUQU�N')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (7,26,'LINARES')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (7,26,'COLB�N')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (7,26,'LONGAV�')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (7,26,'PARRAL')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (7,26,'RETIRO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (7,26,'SAN JAVIER')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (7,26,'VILLA ALEGRE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (7,26,'YERBAS BUENAS')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (8,27,'CONCEPCI�N')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (8,27,'CORONEL')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (8,27,'CHIGUAYANTE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (8,27,'FLORIDA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (8,27,'HUALQUI')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (8,27,'LOTA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (8,27,'PENCO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (8,27,'SAN PEDRO DE LA PAZ')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (8,27,'SANTA JUANA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (8,27,'TALCAHUANO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (8,27,'TOM�')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (8,27,'HUALP�N')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (8,28,'LEBU')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (8,28,'ARAUCO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (8,28,'CA�ETE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (8,28,'CONTULMO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (8,28,'CURANILAHUE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (8,28,'LOS ALAMOS')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (8,28,'TIR�A')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (8,29,'LOS ANGELES')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (8,29,'ANTUCO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (8,29,'CABRERO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (8,29,'LAJA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (8,29,'MULCH�N')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (8,29,'NACIMIENTO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (8,29,'NEGRETE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (8,29,'QUILACO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (8,29,'QUILLECO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (8,29,'SAN ROSENDO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (8,29,'SANTA B�RBARA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (8,29,'TUCAPEL')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (8,29,'TUCAPEL')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (8,29,'ALTO BIOB�O')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (9,30,'TEMUCO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (9,30,'CARAHUE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (9,30,'CUNCO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (9,30,'CURARREHUE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (9,30,'FREIRE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (9,30,'GALVARINO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (9,30,'GORBEA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (9,30,'LAUTARO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (9,30,'LONCOCHE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (9,30,'MELIPEUCO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (9,30,'NUEVA IMPERIAL')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (9,30,'PADRE LAS CASAS')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (9,30,'PERQUENCO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (9,30,'PITRUFQU�N')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (9,30,'PUC�N')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (9,30,'SAAVEDRA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (9,30,'TEODORO SCHMIDT')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (9,30,'TOLT�N')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (9,30,'VILC�N')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (9,30,'VILLARRICA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (9,30,'CHOLCHOL')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (9,31,'ANGOL')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (9,31,'COLLIPULLI')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (9,31,'CURACAUT�N')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (9,31,'ERCILLA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (9,31,'LONQUIMAY')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (9,31,'LOS SAUCES')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (9,31,'LUMACO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (9,31,'PUR�N')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (9,31,'RENAICO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (9,31,'TRAIGU�N')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (9,31,'VICTORIA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (10,32,'PUERTO MONTT')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (10,32,'CALBUCO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (10,32,'COCHAM�')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (10,32,'FRESIA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (10,32,'FRUTILLAR')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (10,32,'LOS MUERMOS')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (10,32,'LLANQUIHUE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (10,32,'MAULL�N')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (10,32,'PUERTO VARAS')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (10,33,'CASTRO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (10,33,'ANCUD')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (10,33,'CHONCHI')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (10,33,'CURACO DE V�LEZ')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (10,33,'DALCAHUE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (10,33,'PUQUELD�N')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (10,33,'QUEIL�N')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (10,33,'QUELL�N')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (10,33,'QUEMCHI')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (10,33,'QUINCHAO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (10,34,'OSORNO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (10,34,'PUERTO OCTAY')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (10,34,'PURRANQUE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (10,34,'PUYEHUE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (10,34,'R�O NEGRO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (10,34,'SAN JUAN DE LA COSTA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (10,34,'SAN PABLO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (10,35,'CHAIT�N')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (10,35,'FUTALEUF�')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (10,35,'HUALAIHU�')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (10,35,'PALENA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (11,36,'COIHAIQUE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (11,36,'LAGO VERDE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (11,37,'AIS�N')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (11,37,'CISNES')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (11,37,'GUAITECAS')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (11,38,'COCHRANE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (11,38,'O�HIGGINS')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (11,38,'TORTEL')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (11,39,'CHILE CHICO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (11,39,'R�O IBA�EZ')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (12,40,'PUNTA ARENAS')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (12,40,'LAGUNA BLANCA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (12,40,'R�O VERDE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (12,40,'SAN GREGORIO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (12,41,'CABO DE HORNOS')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (12,42,'PORVENIR')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (12,42,'PRIMAVERA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (12,42,'TIMAUKEL')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (12,43,'NATALES')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (12,43,'TORRES DEL PAINE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,44,'SANTIAGO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,44,'CERRILLOS')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,44,'CERRO NAVIA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,44,'CONCHAL�')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,44,'EL BOSQUE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,44,'ESTACI�N CENTRAL')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,44,'HUECHURABA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,44,'INDEPENDENCIA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,44,'LA CISTERNA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,44,'LA FLORIDA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,44,'LA GRANJA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,44,'LA PINTANA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,44,'LA REINA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,44,'LAS CONDES')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,44,'LO BARNECHEA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,44,'LO ESPEJO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,44,'LO PRADO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,44,'MACUL')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,44,'MAIP�')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,44,'�U�OA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,44,'PEDRO AGUIRRE CERDA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,44,'PE�ALOL�N')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,44,'PROVIDENCIA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,44,'PUDAHUEL')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,44,'QUILICURA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,44,'QUINTA NORMAL')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,44,'RECOLETA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,44,'RENCA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,44,'SAN JOAQU�N')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,44,'SAN MIGUEL')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,44,'SAN RAM�N')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,44,'VITACURA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,45,'PUENTE ALTO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,45,'PIRQUE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,45,'SAN JOS� DE MAIPO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,46,'COLINA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,46,'LAMPA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,46,'TILTIL')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,47,'SAN BERNARDO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,47,'BUIN')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,47,'CALERA DE TANGO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,47,'PAINE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,48,'MELIPILLA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,48,'ALHU�')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,48,'CURACAV�')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,48,'MAR�A PINTO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,48,'SAN PEDRO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,49,'TALAGANTE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,49,'EL MONTE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,49,'ISLA DE MAIPO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,49,'PADRE HURTADO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (13,49,'PE�AFLOR')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (14,50,'VALDIVIA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (14,50,'CORRAL')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (14,50,'LANCO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (14,50,'LOS LAGOS')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (14,50,'M�FIL')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (14,50,'MARIQUINA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (14,50,'PAILLACO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (14,50,'PANGUIPULLI')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (14,51,'LA UNI�N')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (14,51,'FUTRONO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (14,51,'LAGO RANCO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (14,51,'R�O BUENO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (15,52,'ARICA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (15,52,'CAMARONES')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (15,53,'PUTRE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (15,53,'GENERAL LAGOS')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (16,54,'CHILL�N')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (16,54,'BULNES')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (16,54,'CHILL�N VIEJO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (16,54,'EL CARMEN')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (16,54,'PEMUCO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (16,54,'PINTO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (16,54,'QUILL�N')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (16,54,'SAN IGNACIO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (16,54,'YUNGAY')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (16,55,'QUIRIHUE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (16,55,'COBQUECURA')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (16,55,'COELEMU')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (16,55,'NINHUE')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (16,55,'PORTEZUELO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (16,55,'RANQUIL')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (16,55,'TREGUACO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (16,56,'SAN CARLOS')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (16,56,'COIHUECO')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (16,56,'�IQU�N')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (16,56,'SAN FABI�N')
INSERT INTO [Codificador_Comuna] ([idregion], [idprovincia], [comuna]) VALUES  (16,56,'SAN NICOL�S')


GO

