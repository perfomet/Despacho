/********************************************************|
* CREADO POR: David Perdigón ****************************
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
VALUES (1, 'dperdigon', '123', 'David', 'Perdigón', 'García', 'davidperdigon.g@gmail.com', NULL)
INSERT INTO Usuario (PerfilId, Username, Password, Nombres, ApellidoPaterno, ApellidoMaterno, Email, ClienteId)  
VALUES (1, 'fbalmaceda', '123', 'Fancisco', 'Balmaceda', 'Neira', 'francisco.vivacuba@gmail.com', NULL)
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
INSERT INTO TipoSolicitud (TipoSolicitudId, Descripcion, Observaciones) VALUES (2, 'Despacho y Distribución', 'Enviar producto desde bodegas MiLogistic y llevarlo al cliente final')
INSERT INTO TipoSolicitud (TipoSolicitudId, Descripcion, Observaciones) VALUES (3, 'Retiro (Entrega en bodega MiLogistic)', 'Busca equipo en la ubicación del cliente y lo lleva a una bodega de MiLogistic')
INSERT INTO TipoSolicitud (TipoSolicitudId, Descripcion, Observaciones) VALUES (4, 'Traslado', 'Solo entre bodegas MiLogistic')
INSERT INTO TipoSolicitud (TipoSolicitudId, Descripcion, Observaciones) VALUES (5, 'Distribución', 'Busca producto en x lugar y entrega al cliente final')

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

INSERT INTO Camion VALUES ('KXLS-90', 'Camión de Prueba 1', 1)
INSERT INTO Camion VALUES ('CFDB-82', 'Camión de Prueba 2', 2)

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

INSERT INTO Gerencia (GerenciaId, Descripcion, ClienteId) VALUES (1, 'Frío', 1)
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
INSERT INTO TipoDocumento (TipoDocumentoId, Descripcion) VALUES (3, 'Guía Despacho')

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
	--ComunaId INT NOT NULL FOREIGN KEY REFERENCES Comuna (ComunaId), -- ESTA ES LA COLUMNA CORRECTA DE COMUNA PERO SE HABILITARÁ CUANDO TENGAMOS LA TABLA EN EL SCRIPT
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
