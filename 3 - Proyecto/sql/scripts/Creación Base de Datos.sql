/********************************************************|
* CREADO POR: David Perdigón ****************************
* FECHA Y HORA: 07/01/2020 23:00 ************************
********************************************************/

USE master
GO

IF EXISTS(SELECT 1 FROM sys.databases WHERE name LIKE 'Despacho') BEGIN
	DROP DATABASE Despacho
END

CREATE DATABASE Despacho
GO

USE Despacho
GO

CREATE TABLE Cliente (
	ClienteId INT PRIMARY KEY IDENTITY(2001, 1),
	Codigo VARCHAR(50) NOT NULL,
	Nombre VARCHAR(255) NOT NULL,
	RUT VARCHAR(11) NULL,
	VRUT VARCHAR(1) NULL,
	Prefijo VARCHAR(10) NOT NULL,
	EstaActivo BIT NOT NULL
)
GO

SET IDENTITY_INSERT Cliente ON
GO

INSERT INTO Cliente (ClienteId, Codigo, Nombre, RUT, VRUT, Prefijo, EstaActivo) VALUES (2001, 'LOG. ANDINA', 'Andina', NULL, NULL, 'AND', 1)
INSERT INTO Cliente (ClienteId, Codigo, Nombre, RUT, VRUT, Prefijo, EstaActivo) VALUES (2002, 'LOG.EMBONOR', 'Embonor', NULL, NULL, 'EMB', 1)
INSERT INTO Cliente (ClienteId, Codigo, Nombre, RUT, VRUT, Prefijo, EstaActivo) VALUES (2003, 'LOG.MIMET', 'Servimet', NULL, NULL, '', 1)
INSERT INTO Cliente (ClienteId, Codigo, Nombre, RUT, VRUT, Prefijo, EstaActivo) VALUES (2004, 'LOG.STA ELENA', 'Soprople', NULL, NULL, 'SOP', 1)

SET IDENTITY_INSERT Cliente OFF
GO

CREATE TABLE Perfil (
	PerfilId INT PRIMARY KEY IDENTITY,
	Descripcion VARCHAR(50) NOT NULL UNIQUE,
	EstaActivo BIT NOT NULL
)
GO

SET IDENTITY_INSERT Perfil ON
GO

INSERT INTO Perfil (PerfilId, Descripcion, EstaActivo) VALUES (1, 'SuperUsuario', 1)
INSERT INTO Perfil (PerfilId, Descripcion, EstaActivo) VALUES (2, 'Administrador', 1) 
INSERT INTO Perfil (PerfilId, Descripcion, EstaActivo) VALUES (3, 'Planificador', 1) 
INSERT INTO Perfil (PerfilId, Descripcion, EstaActivo) VALUES (4, 'Cliente', 1)  
INSERT INTO Perfil (PerfilId, Descripcion, EstaActivo) VALUES (5, 'Bodega', 1) 

SET IDENTITY_INSERT Perfil OFF
GO

CREATE TABLE Usuario (
	UsuarioId INT PRIMARY KEY IDENTITY(1001, 1),
	Username VARCHAR(15) NOT NULL UNIQUE,
	Password VARCHAR(15) NOT NULL,
	Nombres VARCHAR(255) NOT NULL,
	ApellidoPaterno VARCHAR(255) NULL,
	ApellidoMaterno VARCHAR(50) NULL,
	Email VARCHAR(255) NOT NULL,
	PerfilId INT NOT NULL FOREIGN KEY REFERENCES Perfil (PerfilId),
	ClienteId INT NULL FOREIGN KEY REFERENCES Cliente (ClienteId),
	EstaActivo BIT NOT NULL
)
GO

INSERT INTO Usuario (PerfilId, Username, Password, Nombres, ApellidoPaterno, ApellidoMaterno, Email, ClienteId, EstaActivo) 
VALUES (1, 'dperdigon', '123', 'David', 'Perdigón', 'García', 'davidperdigon.g@gmail.com', NULL, 1)
INSERT INTO Usuario (PerfilId, Username, Password, Nombres, ApellidoPaterno, ApellidoMaterno, Email, ClienteId, EstaActivo)  
VALUES (1, 'fbalmaseda', '123', 'Fancisco', 'Balmaseda', 'Neyra', 'francisco.vivacuba@gmail.com', NULL, 1)
INSERT INTO Usuario (PerfilId, Username, Password, Nombres, ApellidoPaterno, ApellidoMaterno, Email, ClienteId, EstaActivo)  
VALUES (2, 'administrador', '123', 'Usuario', 'Administrador', NULL, 'pruebas@prueba.cl', NULL, 1)
INSERT INTO Usuario (PerfilId, Username, Password, Nombres, ApellidoPaterno, ApellidoMaterno, Email, ClienteId, EstaActivo)  
VALUES (3, 'planificador', '123', 'Usuario', 'Planificador', NULL, 'pruebas@prueba.cl', NULL, 1)
INSERT INTO Usuario (PerfilId, Username, Password, Nombres, ApellidoPaterno, ApellidoMaterno, Email, ClienteId, EstaActivo)  
VALUES (4, 'cliente', '123', 'Usuario', 'Cliente', NULL, 'pruebas@prueba.cl', 2003, 1)
INSERT INTO Usuario (PerfilId, Username, Password, Nombres, ApellidoPaterno, ApellidoMaterno, Email, ClienteId, EstaActivo)  
VALUES (5, 'bodega', '123', 'Usuario', 'Bodega', NULL, 'pruebas@prueba.cl', NULL, 1)

CREATE TABLE TipoSolicitud (
	TipoSolicitudId INT PRIMARY KEY IDENTITY,
	Descripcion VARCHAR(100) NOT NULL UNIQUE,
	Observaciones VARCHAR(255) NULL,
	EstaActivo BIT NOT NULL
)
GO

SET IDENTITY_INSERT TipoSolicitud ON
GO

INSERT INTO TipoSolicitud (TipoSolicitudId, Descripcion, Observaciones, EstaActivo)  
 VALUES (1, 'Despacho (Entrega)', 'Cliente retira producto en la bodega de MiLogistic', 1)
INSERT INTO TipoSolicitud (TipoSolicitudId, Descripcion, Observaciones, EstaActivo)  
 VALUES (2, 'Despacho y Distribución', 'Enviar producto desde bodegas MiLogistic y llevarlo al cliente final', 1)
INSERT INTO TipoSolicitud (TipoSolicitudId, Descripcion, Observaciones, EstaActivo)  
 VALUES (3, 'Retiro (Entrega en bodega MiLogistic)', 'Busca equipo en la ubicación del cliente y lo lleva a una bodega de MiLogistic', 1)
INSERT INTO TipoSolicitud (TipoSolicitudId, Descripcion, Observaciones, EstaActivo)  
 VALUES (4, 'Traslado', 'Solo entre bodegas MiLogistic', 1)
INSERT INTO TipoSolicitud (TipoSolicitudId, Descripcion, Observaciones, EstaActivo)  
 VALUES (5, 'Distribución', 'Busca producto en x lugar y entrega al cliente final', 1)

SET IDENTITY_INSERT TipoSolicitud OFF
GO

CREATE TABLE EmpresaTransporte (
	EmpresaTransporteId INT PRIMARY KEY IDENTITY,
	Nombre VARCHAR(255) NOT NULL UNIQUE,
	EsPropia BIT NOT NULL,
	EstaActivo BIT NOT NULL
)
GO

SET IDENTITY_INSERT EmpresaTransporte ON
GO

INSERT INTO EmpresaTransporte (EmpresaTransporteId, Nombre, EsPropia, EstaActivo) VALUES (1, 'MiLogistic', 1, 1)
INSERT INTO EmpresaTransporte (EmpresaTransporteId, Nombre, EsPropia, EstaActivo) VALUES (2, 'Otra empresa externa', 0, 1)

SET IDENTITY_INSERT EmpresaTransporte OFF 
GO

CREATE TABLE Camion (
	Patente VARCHAR(10) PRIMARY KEY,
	Descripcion VARCHAR(255) NOT NULL,
	EmpresaTransporteId INT NOT NULL FOREIGN KEY REFERENCES EmpresaTransporte (EmpresaTransporteId)
	UNIQUE (Descripcion, EmpresaTransporteId),
	EstaActivo BIT NOT NULL
)
GO

INSERT INTO Camion VALUES ('KXLS-90', 'Camión de Prueba 1', 1, 1)
INSERT INTO Camion VALUES ('CFDB-82', 'Camión de Prueba 2', 2, 1)

CREATE TABLE EstadoEquipo (
	EstadoEquipoId INT PRIMARY KEY IDENTITY,
	Descripcion VARCHAR(255) NOT NULL UNIQUE,
	EstaActivo BIT NOT NULL
)
GO

SET IDENTITY_INSERT EstadoEquipo ON
GO

INSERT INTO EstadoEquipo (EstadoEquipoId, Descripcion, EstaActivo) VALUES (1, 'Usado', 1)
INSERT INTO EstadoEquipo (EstadoEquipoId, Descripcion, EstaActivo) VALUES (2, 'Nuevo', 1)
INSERT INTO EstadoEquipo (EstadoEquipoId, Descripcion, EstaActivo) VALUES (3, 'Reparado (Usado)', 1)
INSERT INTO EstadoEquipo (EstadoEquipoId, Descripcion, EstaActivo) VALUES (4, 'De baja', 1)

SET IDENTITY_INSERT EstadoEquipo OFF 
GO

CREATE TABLE UnidadMedida (
	UnidadMedidaId INT PRIMARY KEY IDENTITY,
	Descripcion VARCHAR(10) NOT NULL UNIQUE,
	EstaActivo BIT NOT NULL
)
GO

SET IDENTITY_INSERT UnidadMedida ON
GO

INSERT INTO UnidadMedida (UnidadMedidaId, Descripcion, EstaActivo) VALUES (1, 'M2', 1)
INSERT INTO UnidadMedida (UnidadMedidaId, Descripcion, EstaActivo) VALUES (2, 'UND', 1)

SET IDENTITY_INSERT UnidadMedida OFF 
GO

CREATE TABLE Clasificacion (
	ClasificacionId INT PRIMARY KEY IDENTITY,
	Cantidad DECIMAL(18, 1) NOT NULL,
	UnidadMedidaId INT NOT NULL FOREIGN KEY REFERENCES UnidadMedida (UnidadMedidaId),
	UNIQUE (Cantidad, UnidadMedidaId),
	EstaActivo BIT NOT NULL
)
GO

SET IDENTITY_INSERT Clasificacion ON
GO

INSERT INTO Clasificacion (ClasificacionId, Cantidad, UnidadMedidaId, EstaActivo) VALUES (1, 0.5, 1, 1)
INSERT INTO Clasificacion (ClasificacionId, Cantidad, UnidadMedidaId, EstaActivo) VALUES (2, 1, 1, 1)
INSERT INTO Clasificacion (ClasificacionId, Cantidad, UnidadMedidaId, EstaActivo) VALUES (3, 2, 1, 1)
INSERT INTO Clasificacion (ClasificacionId, Cantidad, UnidadMedidaId, EstaActivo) VALUES (4, 3, 1, 1)

SET IDENTITY_INSERT Clasificacion OFF 
GO

CREATE TABLE Prioridad (
	PrioridadId INT PRIMARY KEY IDENTITY,
	Descripcion VARCHAR(10) NOT NULL UNIQUE,
	EstaActivo BIT NOT NULL
)
GO

SET IDENTITY_INSERT Prioridad ON
GO

INSERT INTO Prioridad (PrioridadId, Descripcion, EstaActivo) VALUES (1, 'Alta', 1)
INSERT INTO Prioridad (PrioridadId, Descripcion, EstaActivo) VALUES (2, 'Media', 1)
INSERT INTO Prioridad (PrioridadId, Descripcion, EstaActivo) VALUES (3, 'Baja', 1)

SET IDENTITY_INSERT Prioridad OFF 
GO

CREATE TABLE UnidadNegocio (
	UnidadNegocioId INT PRIMARY KEY IDENTITY,
	Descripcion VARCHAR(255) NOT NULL,
	ClienteId INT NOT NULL FOREIGN KEY REFERENCES Cliente (ClienteId),
	UNIQUE (Descripcion, ClienteId),
	EstaActivo BIT NOT NULL
)
GO

SET IDENTITY_INSERT UnidadNegocio ON
GO

INSERT INTO UnidadNegocio (UnidadNegocioId, Descripcion, ClienteId, EstaActivo) VALUES (1, 'Zona Sur', 2001, 1)
INSERT INTO UnidadNegocio (UnidadNegocioId, Descripcion, ClienteId, EstaActivo) VALUES (2, 'Zona Norte', 2001, 1)
INSERT INTO UnidadNegocio (UnidadNegocioId, Descripcion, ClienteId, EstaActivo) VALUES (3, 'Zona Oriente', 2001, 1)

SET IDENTITY_INSERT UnidadNegocio OFF 
GO

CREATE TABLE Gerencia (
	GerenciaId INT PRIMARY KEY IDENTITY,
	Descripcion VARCHAR(255) NOT NULL,
	ClienteId INT NOT NULL FOREIGN KEY REFERENCES Cliente (ClienteId),
	UNIQUE (Descripcion, ClienteId),
	EstaActivo BIT NOT NULL
)
GO

SET IDENTITY_INSERT Gerencia ON
GO

INSERT INTO Gerencia (GerenciaId, Descripcion, ClienteId, EstaActivo) VALUES (1, 'Frío', 2001, 1)
INSERT INTO Gerencia (GerenciaId, Descripcion, ClienteId, EstaActivo) VALUES (2, 'Calor', 2001, 1)

SET IDENTITY_INSERT Gerencia OFF
GO



CREATE TABLE TipoDocumento (
	TipoDocumentoId INT PRIMARY KEY IDENTITY,
	Descripcion VARCHAR(255) NOT NULL UNIQUE,
	EstaActivo BIT NOT NULL
)
GO

SET IDENTITY_INSERT TipoDocumento ON
GO

INSERT INTO TipoDocumento (TipoDocumentoId, Descripcion, EstaActivo) VALUES (1, 'Boleta', 1)
INSERT INTO TipoDocumento (TipoDocumentoId, Descripcion, EstaActivo) VALUES (2, 'Factura', 1)
INSERT INTO TipoDocumento (TipoDocumentoId, Descripcion, EstaActivo) VALUES (3, 'Guía Despacho', 1)

SET IDENTITY_INSERT TipoDocumento OFF
GO

CREATE TABLE Region (
	RegionId INT IDENTITY(1,1) PRIMARY KEY,
	Region VARCHAR(100) NULL,
	EstaActivo BIT NOT NULL
)
GO

INSERT INTO Region (Region, EstaActivo) VALUES ('TARAPACÁ', 1)
INSERT INTO Region (Region, EstaActivo) VALUES ('ANTOFAGASTA', 1)
INSERT INTO Region (Region, EstaActivo) VALUES ('ATACAMA', 1)
INSERT INTO Region (Region, EstaActivo) VALUES ('COQUIMBO', 1)
INSERT INTO Region (Region, EstaActivo) VALUES ('REGIÓN DE VALPARAÍSO', 1)
INSERT INTO Region (Region, EstaActivo) VALUES ('REGIÓN DEL LIBERTADOR BERNARDO O´HIGGINS', 1)
INSERT INTO Region (Region, EstaActivo) VALUES ('REGIÓN DEL MAULE', 1)
INSERT INTO Region (Region, EstaActivo) VALUES ('REGIÓN DEL BIOBIO', 1)
INSERT INTO Region (Region, EstaActivo) VALUES ('REGIÓN DE LA ARAUCANÍA', 1)
INSERT INTO Region (Region, EstaActivo) VALUES ('REGIÓN DE LOS LAGOS', 1)
INSERT INTO Region (Region, EstaActivo) VALUES ('REGIÓN DE AISÉN DEL GENERAL CARLOS IBAÑEZ DEL CAMPO', 1)
INSERT INTO Region (Region, EstaActivo) VALUES ('REGIÓN DE MAGALLANES Y DE LA ANTÁRTICA CHILENA', 1)
INSERT INTO Region (Region, EstaActivo) VALUES ('REGIÓN METROPOLITANA', 1)
INSERT INTO Region (Region, EstaActivo) VALUES ('REGIÓN DE LOS RÍOS', 1)
INSERT INTO Region (Region, EstaActivo) VALUES ('ARICA Y PARINACOTA', 1)
INSERT INTO Region (Region, EstaActivo) VALUES ('ÑUBLE', 1)

CREATE TABLE Provincia (
	ProvinciaId INT IDENTITY(1,1) PRIMARY KEY,
	RegionId INT NOT NULL FOREIGN KEY REFERENCES Region (RegionId),
	Orden INT NOT NULL,
	Provincia VARCHAR(50) NOT NULL,
	EstaActivo BIT NOT NULL
)
GO

INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (1, 1,'IQUIQUE', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (1, 2,'TAMARUGAL', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (2, 1,'ANTOFAGASTA', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (2, 2,'EL LOA', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (2, 3,'TOCOPILLA', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (3, 1,'COPIAPÓ', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (3, 2,'CHAÑARAL', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (3, 3,'HUASCO', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (4, 4,'ELQUI', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (4, 5,'CHOAPA', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (4, 6,'LIMARÍ', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (5, 1,'VALPARAÍSO', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (5, 2,'ISLA DE PASCUA', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (5, 3,'LOS ANDES', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (5, 4,'PETORCA', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (5, 5,'QUILLOTA', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (5, 6,'SAN ANTONIO', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (5, 7,'SAN FELIPE DE ACONCAGUA', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (5, 8,'MARGA MARGA', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (6, 1,'CACHAPOAL', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (6, 2,'CARDENAL CARO', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (6, 3,'COLCHAGUA', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (7, 1,'TALCA', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (7, 2,'CAUQUENES', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (7, 3,'CURICÓ', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (7, 4,'LINARES', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (8, 1,'CONCEPCIÓN', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (8, 2,'ARAUCO', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (8, 3,'BIOBÍO', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (9, 1,'CAUTÍN', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (9, 2,'MALLECO', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (10, 1,'LLANQUIHUE', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (10, 2,'CHILOÉ', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (10, 3,'OSORNO', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (10, 4,'PALENA', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (11, 1,'COIHAIQUE', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (11, 2,'AISÉN', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (11, 3,'CAPITÁN PRAT', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (11, 4,'GENERAL CARRERA', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (12, 1,'MAGALLANES', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (12, 2,'ANTÁRTICA CHILENA', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (12, 3,'TIERRA DEL FUEGO', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (12, 4,'ÚLTIMA ESPERANZA', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (13, 1,'SANTIAGO', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (13, 2,'CORDILLERA', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (13, 3,'CHACABUCO', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (13, 4,'MAIPO', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (13, 5,'MELIPILLA', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (13, 6,'TALAGANTE', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (14, 1,'VALDIVIA', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (14, 2,'RANCO', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (15, 1,'ARICA', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (15, 2,'PARINACOTA', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (16, 1,'DIGUILLÓN', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (16, 2,'ITATA', 1)
INSERT INTO Provincia (RegionId, Orden, Provincia, EstaActivo) VALUES (16, 3,'PUNILLA', 1)

CREATE TABLE Comuna(
	ComunaId INT IDENTITY(1,1) PRIMARY KEY,
	RegionId INT NULL FOREIGN KEY REFERENCES Region (RegionId),
	ProvinciaId INT NULL FOREIGN KEY REFERENCES Provincia (ProvinciaId),
	Comuna VARCHAR(50) NULL,
	EstaActivo BIT NOT NULL
)
GO

INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (1, 1,'IQUIQUE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (1, 1,'ALTO HOSPICIO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (1, 2,'POZO ALMONTE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (1, 2,'CAMIÑA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (1, 2,'COLCHANE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (1, 2,'HUARA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (1, 2,'PICA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (2, 3,'ANTOFAGASTA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (2, 3,'MEJILLONES', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (2, 3,'SIERRA GORDA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (2, 3,'TALTAL', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (2, 4,'CALAMA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (2, 4,'OLLAGÜE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (2, 4,'SAN PEDRO DE ATACAMA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (2, 5,'TOCOPILLA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (2, 5,'MARÍA ELENA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (3, 6,'COPIAPÓ', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (3, 6,'CALDERA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (3, 6,'TIERRA AMARILLA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (3, 7,'CHAÑARAL', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (3, 7,'DIEGO DE ALMAGRO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (3, 8,'VALLENAR', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (3, 8,'ALTO DEL CARMEN', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (3, 8,'FREIRINA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (3, 8,'HUASCO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (4, 9,'LA SERENA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (4, 9,'COQUIMBO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (4, 9,'ANDACOLLO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (4, 9,'LA HIGUERA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (4, 9,'PAIGUANO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (4, 9,'VICUÑA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (4, 10,'ILLAPEL', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (4, 10,'CANELA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (4, 10,'LOS VILOS', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (4, 10,'SALAMANCA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (4, 11,'OVALLE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (4, 11,'COMBARBALÁ', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (4, 11,'MONTE PATRIA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (4, 11,'PUNITAQUI', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (4, 11,'RÍO HURTADO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 12,'VALPARAÍSO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 12,'CASABLANCA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 12,'CONCÓN', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 12,'JUAN FERNÁNDEZ', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 12,'PUCHUNCAVÍ', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 12,'QUINTERO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 12,'QUINTERO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 13,'ISLA DE PASCUA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 14,'LOS ANDES', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 14,'CALLE LARGA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 14,'RINCONADA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 14,'SAN ESTEBAN', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 15,'LA LIGUA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 15,'CABILDO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 15,'PAPUDO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 15,'PETORCA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 15,'ZAPALLAR', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 15,'ZAPALLAR', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 16,'QUILLOTA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 16,'CALERA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 16,'HIJUELAS', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 16,'LA CRUZ', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 16,'NOGALES', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 17,'SAN ANTONIO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 17,'ALGARROBO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 17,'CARTAGENA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 17,'EL QUISCO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 17,'EL TABO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 17,'SANTO DOMINGO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 18,'SAN FELIPE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 18,'CATEMU', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 18,'LLAILLAY', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 18,'PANQUEHUE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 18,'PUTAENDO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 18,'SANTA MARÍA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 19,'QUILPUÉ', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 19,'LIMACHE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 19,'OLMUÉ', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (5, 19,'VILLA ALEMANA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (6,20,'RANCAGUA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (6,20,'CODEGUA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (6,20,'COINCO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (6,20,'COLTAUCO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (6,20,'DOÑIHUE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (6,20,'GRANEROS', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (6,20,'LAS CABRAS', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (6,20,'MACHALÍ', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (6,20,'MALLOA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (6,20,'MOSTAZAL', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (6,20,'OLIVAR', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (6,20,'PEUMO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (6,20,'PICHIDEGUA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (6,20,'QUINTA DE TILCOCO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (6,20,'RENGO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (6,20,'REQUÍNOA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (6,20,'SAN VICENTE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (6,21,'PICHILEMU', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (6,21,'LA ESTRELLA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (6,21,'LITUECHE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (6,21,'MARCHIHUE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (6,21,'NAVIDAD', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (6,21,'PAREDONES', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (6,22,'SAN FERNANDO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (6,22,'CHÉPICA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (6,22,'CHIMBARONGO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (6,22,'LOLOL', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (6,22,'NANCAGUA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (6,22,'PALMILLA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (6,22,'PERALILLO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (6,22,'PLACILLA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (6,22,'PUMANQUE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (6,22,'SANTA CRUZ', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (7,23,'TALCA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (7,23,'CONSTITUCIÓN', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (7,23,'CUREPTO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (7,23,'EMPEDRADO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (7,23,'MAULE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (7,23,'PELARCO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (7,23,'PENCAHUE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (7,23,'RÍO CLARO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (7,23,'SAN CLEMENTE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (7,23,'SAN RAFAEL', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (7,24,'CAUQUENES', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (7,24,'CHANCO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (7,24,'PELLUHUE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (7,25,'CURICÓ', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (7,25,'HUALAÑÉ', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (7,25,'LICANTÉN', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (7,25,'MOLINA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (7,25,'RAUCO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (7,25,'ROMERAL', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (7,25,'SAGRADA FAMILIA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (7,25,'TENO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (7,25,'VICHUQUÉN', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (7,26,'LINARES', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (7,26,'COLBÚN', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (7,26,'LONGAVÍ', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (7,26,'PARRAL', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (7,26,'RETIRO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (7,26,'SAN JAVIER', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (7,26,'VILLA ALEGRE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (7,26,'YERBAS BUENAS', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (8,27,'CONCEPCIÓN', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (8,27,'CORONEL', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (8,27,'CHIGUAYANTE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (8,27,'FLORIDA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (8,27,'HUALQUI', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (8,27,'LOTA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (8,27,'PENCO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (8,27,'SAN PEDRO DE LA PAZ', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (8,27,'SANTA JUANA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (8,27,'TALCAHUANO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (8,27,'TOMÉ', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (8,27,'HUALPÉN', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (8,28,'LEBU', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (8,28,'ARAUCO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (8,28,'CAÑETE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (8,28,'CONTULMO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (8,28,'CURANILAHUE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (8,28,'LOS ALAMOS', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (8,28,'TIRÚA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (8,29,'LOS ÁNGELES', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (8,29,'ANTUCO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (8,29,'CABRERO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (8,29,'LAJA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (8,29,'MULCHÉN', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (8,29,'NACIMIENTO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (8,29,'NEGRETE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (8,29,'QUILACO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (8,29,'QUILLECO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (8,29,'SAN ROSENDO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (8,29,'SANTA BÁRBARA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (8,29,'TUCAPEL', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (8,29,'TUCAPEL', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (8,29,'ALTO BIOBÍO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (9,30,'TEMUCO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (9,30,'CARAHUE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (9,30,'CUNCO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (9,30,'CURARREHUE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (9,30,'FREIRE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (9,30,'GALVARINO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (9,30,'GORBEA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (9,30,'LAUTARO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (9,30,'LONCOCHE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (9,30,'MELIPEUCO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (9,30,'NUEVA IMPERIAL', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (9,30,'PADRE LAS CASAS', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (9,30,'PERQUENCO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (9,30,'PITRUFQUÉN', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (9,30,'PUCÓN', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (9,30,'SAAVEDRA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (9,30,'TEODORO SCHMIDT', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (9,30,'TOLTÉN', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (9,30,'VILCÚN', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (9,30,'VILLARRICA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (9,30,'CHOLCHOL', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (9,31,'ANGOL', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (9,31,'COLLIPULLI', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (9,31,'CURACAUTÍN', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (9,31,'ERCILLA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (9,31,'LONQUIMAY', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (9,31,'LOS SAUCES', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (9,31,'LUMACO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (9,31,'PURÉN', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (9,31,'RENAICO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (9,31,'TRAIGUÉN', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (9,31,'VICTORIA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (10,32,'PUERTO MONTT', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (10,32,'CALBUCO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (10,32,'COCHAMÓ', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (10,32,'FRESIA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (10,32,'FRUTILLAR', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (10,32,'LOS MUERMOS', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (10,32,'LLANQUIHUE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (10,32,'MAULLÍN', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (10,32,'PUERTO VARAS', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (10,33,'CASTRO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (10,33,'ANCUD', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (10,33,'CHONCHI', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (10,33,'CURACO DE VÉLEZ', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (10,33,'DALCAHUE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (10,33,'PUQUELDÓN', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (10,33,'QUEILÉN', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (10,33,'QUELLÓN', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (10,33,'QUEMCHI', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (10,33,'QUINCHAO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (10,34,'OSORNO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (10,34,'PUERTO OCTAY', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (10,34,'PURRANQUE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (10,34,'PUYEHUE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (10,34,'RÍO NEGRO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (10,34,'SAN JUAN DE LA COSTA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (10,34,'SAN PABLO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (10,35,'CHAITÉN', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (10,35,'FUTALEUFÚ', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (10,35,'HUALAIHUÉ', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (10,35,'PALENA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (11,36,'COIHAIQUE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (11,36,'LAGO VERDE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (11,37,'AISÉN', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (11,37,'CISNES', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (11,37,'GUAITECAS', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (11,38,'COCHRANE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (11,38,'O´HIGGINS', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (11,38,'TORTEL', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (11,39,'CHILE CHICO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (11,39,'RÍO IBAÑEZ', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (12,40,'PUNTA ARENAS', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (12,40,'LAGUNA BLANCA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (12,40,'RÍO VERDE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (12,40,'SAN GREGORIO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (12,41,'CABO DE HORNOS', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (12,42,'PORVENIR', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (12,42,'PRIMAVERA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (12,42,'TIMAUKEL', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (12,43,'NATALES', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (12,43,'TORRES DEL PAINE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,44,'SANTIAGO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,44,'CERRILLOS', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,44,'CERRO NAVIA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,44,'CONCHALÍ', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,44,'EL BOSQUE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,44,'ESTACIÓN CENTRAL', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,44,'HUECHURABA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,44,'INDEPENDENCIA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,44,'LA CISTERNA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,44,'LA FLORIDA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,44,'LA GRANJA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,44,'LA PINTANA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,44,'LA REINA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,44,'LAS CONDES', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,44,'LO BARNECHEA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,44,'LO ESPEJO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,44,'LO PRADO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,44,'MACUL', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,44,'MAIPÚ', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,44,'ÑUÑOA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,44,'PEDRO AGUIRRE CERDA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,44,'PEÑALOLÉN', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,44,'PROVIDENCIA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,44,'PUDAHUEL', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,44,'QUILICURA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,44,'QUINTA NORMAL', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,44,'RECOLETA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,44,'RENCA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,44,'SAN JOAQUÍN', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,44,'SAN MIGUEL', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,44,'SAN RAMÓN', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,44,'VITACURA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,45,'PUENTE ALTO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,45,'PIRQUE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,45,'SAN JOSÉ DE MAIPO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,46,'COLINA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,46,'LAMPA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,46,'TILTIL', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,47,'SAN BERNARDO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,47,'BUIN', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,47,'CALERA DE TANGO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,47,'PAINE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,48,'MELIPILLA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,48,'ALHUÉ', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,48,'CURACAVÍ', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,48,'MARÍA PINTO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,48,'SAN PEDRO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,49,'TALAGANTE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,49,'EL MONTE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,49,'ISLA DE MAIPO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,49,'PADRE HURTADO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (13,49,'PEÑAFLOR', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (14,50,'VALDIVIA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (14,50,'CORRAL', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (14,50,'LANCO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (14,50,'LOS LAGOS', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (14,50,'MÁFIL', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (14,50,'MARIQUINA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (14,50,'PAILLACO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (14,50,'PANGUIPULLI', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (14,51,'LA UNIÓN', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (14,51,'FUTRONO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (14,51,'LAGO RANCO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (14,51,'RÍO BUENO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (15,52,'ARICA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (15,52,'CAMARONES', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (15,53,'PUTRE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (15,53,'GENERAL LAGOS', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (16,54,'CHILLÁN', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (16,54,'BULNES', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (16,54,'CHILLÁN VIEJO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (16,54,'EL CARMEN', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (16,54,'PEMUCO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (16,54,'PINTO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (16,54,'QUILLÓN', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (16,54,'SAN IGNACIO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (16,54,'YUNGAY', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (16,55,'QUIRIHUE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (16,55,'COBQUECURA', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (16,55,'COELEMU', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (16,55,'NINHUE', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (16,55,'PORTEZUELO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (16,55,'RANQUIL', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (16,55,'TREGUACO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (16,56,'SAN CARLOS', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (16,56,'COIHUECO', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (16,56,'ÑIQUÉN', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (16,56,'SAN FABIÁN', 1)
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna, EstaActivo) VALUES  (16,56,'SAN NICOLÁS', 1)

CREATE TABLE TipoPersonal (
	TipoPersonalId INT PRIMARY KEY IDENTITY(1, 1),
	Descripcion VARCHAR(50) NOT NULL
)

SET IDENTITY_INSERT TipoPersonal ON
GO

INSERT INTO TipoPersonal (TipoPersonalId, Descripcion) VALUES (1, 'Pioneta')
INSERT INTO TipoPersonal (TipoPersonalId, Descripcion) VALUES (2, 'Chofer')
INSERT INTO TipoPersonal (TipoPersonalId, Descripcion) VALUES (3, 'Enlace')

SET IDENTITY_INSERT TipoPersonal OFF

CREATE TABLE Personal (
	PersonalId INT PRIMARY KEY IDENTITY(1, 1),
	RUT INT NOT NULL UNIQUE,
	DV  CHAR(1) NOT NULL,
	Nombre VARCHAR(100) NOT NULL,
	PrimerApellido VARCHAR(100) NOT NULL,
	SegundoApellido VARCHAR(100) NOT NULL,
	Email VARCHAR(100) NOT NULL,
	TipoPersonalId INT NOT NULL FOREIGN KEY REFERENCES TipoPersonal (TipoPersonalId),
	EstaActivo BIT NOT NULL
)
GO

CREATE TABLE EstadoSolicitud (
	EstadoSolicitudId INT PRIMARY KEY IDENTITY,
	Descripcion VARCHAR(255) NOT NULL UNIQUE,
	EstaActivo BIT NOT NULL
)
GO

SET IDENTITY_INSERT EstadoSolicitud ON
GO

INSERT INTO EstadoSolicitud (EstadoSolicitudId, Descripcion, EstaActivo) VALUES (1, 'Solicitud Ingresada', 1) -- CLIENTE
INSERT INTO EstadoSolicitud (EstadoSolicitudId, Descripcion, EstaActivo) VALUES (2, 'Placas Ingresadas', 1) -- BODEGA
INSERT INTO EstadoSolicitud (EstadoSolicitudId, Descripcion, EstaActivo) VALUES (3, 'Planificado', 1) -- PLANIFICADOR
INSERT INTO EstadoSolicitud (EstadoSolicitudId, Descripcion, EstaActivo) VALUES (4, 'Documentado', 1) -- CLIENTE
INSERT INTO EstadoSolicitud (EstadoSolicitudId, Descripcion, EstaActivo) VALUES (5, 'Concreción', 1) -- BODEGA
INSERT INTO EstadoSolicitud (EstadoSolicitudId, Descripcion, EstaActivo) VALUES (6, 'Aprobado por Cliente', 1) -- CLIENTE

SET IDENTITY_INSERT EstadoSolicitud OFF 
GO

CREATE TABLE SolicitudDespacho (
	-- FASE 1 CLIENTE
	SolicitudDespachoId	INT PRIMARY KEY IDENTITY (10101, 1),
	TipoSolicitudId INT NOT NULL FOREIGN KEY REFERENCES TipoSolicitud (TipoSolicitudId),
	EstadoSolicitudId INT NOT NULL FOREIGN KEY REFERENCES EstadoSolicitud (EstadoSolicitudId),
	FechaSolicitud DATETIME NOT NULL,
	FechaRecepcion DATETIME NOT NULL,
	BodegaOrigen VARCHAR(10) NOT NULL,
	NumeroCliente VARCHAR(50) NOT NULL,
	NombreCliente VARCHAR(255) NOT NULL,
	DireccionCliente VARCHAR(255) NOT NULL,
	ComunaClienteId INT NOT NULL FOREIGN KEY REFERENCES Comuna (ComunaId),
	NumeroTelefonoContacto VARCHAR(15) NULL,
	RutCliente VARCHAR(8) NOT NULL,
	VRutCliente CHAR(1) NOT NULL,
	Proyecto VARCHAR(100) NOT NULL,
	PrioridadId INT NOT NULL FOREIGN KEY REFERENCES Prioridad (PrioridadId),
	UnidadNegocioId INT NOT NULL FOREIGN KEY REFERENCES UnidadNegocio (UnidadNegocioId),
	GerenciaId INT NOT NULL FOREIGN KEY REFERENCES Gerencia (GerenciaId),
	ObservacionAof VARCHAR(MAX) NULL,
	-- FASE 2 PLANIFICADOR
	FechaDespacho DATETIME NULL,
	PatenteCamion VARCHAR(10) NULL FOREIGN KEY REFERENCES Camion (Patente),
	LlamadaDiaAnterior BIT NULL,
	ComentariosLlamada VARCHAR(MAX) NULL,
	-- FASE 3 CLIENTE
	NumeroDocumento	INT NULL,
	NumeroEntrega INT NULL,
	FechaEntregaDocumento DATETIME NULL,
	FechaRecepcionDocumento	DATETIME NULL,
	Folio INT NULL,
	TipoDocumentoId INT NULL FOREIGN KEY REFERENCES TipoDocumento (TipoDocumentoId),
	-- FASE 4 BODEGA
	Concrecion BIT NULL,
	NombreConcrecion VARCHAR(255) NULL,
	RUTConcrecion VARCHAR(8) NULL,
	VRUTConcrecion VARCHAR(1) NULL,
	MotivoNoConcrecion VARCHAR(MAX) NULL,
	SolicitanteId INT NOT NULL FOREIGN KEY REFERENCES Usuario(UsuarioId)
)
GO

CREATE TABLE PersonalAsignado (
	SolicitudDespachoId INT NOT NULL FOREIGN KEY REFERENCES SolicitudDespacho (SolicitudDespachoId),
	PersonalId INT NOT NULL FOREIGN KEY REFERENCES Personal (PersonalId)
)
GO

CREATE TABLE EquiposSolicitados (
	EquipoSolicitadoId INT PRIMARY KEY IDENTITY,
	NumeroPlaca VARCHAR(50) NULL,
	Marca VARCHAR(50) NOT NULL,
	Modelo VARCHAR(50) NOT NULL,
	EstadoEquipoId INT NOT NULL FOREIGN KEY REFERENCES EstadoEquipo(EstadoEquipoId),
	SolicitudDespachoId INT NOT NULL FOREIGN KEY REFERENCES SolicitudDespacho(SolicitudDespachoId)
)
GO

CREATE TABLE EquiposRetirados (
	EquipoRetiradoId INT PRIMARY KEY IDENTITY,
	NumeroPlaca VARCHAR(50) NOT NULL,
	SolicitudDespachoId INT NOT NULL FOREIGN KEY REFERENCES SolicitudDespacho(SolicitudDespachoId)
)
GO

-- Se genera una carga masiva por cada archivo cargado
CREATE TABLE CargaMasiva (
	CargaMasivaId INT PRIMARY KEY IDENTITY(1001, 1),
	UsuarioId INT NOT NULL FOREIGN KEY REFERENCES Usuario (UsuarioId),
	FechaHora DATETIME NOT NULL,
	Archivo VARCHAR(255) NOT NULL
)
GO

-- Se genera un detalle de carga masiva por cada cliente en el archivo
CREATE TABLE CargaMasivaDetalle (
	CargaMasivaDetalleId INT PRIMARY KEY IDENTITY(1, 1),
	CargaMasivaId INT NOT NULL FOREIGN KEY REFERENCES CargaMasiva (CargaMasivaId),
	NumeroSolicitud INT NOT NULL,
	TipoSolicitud VARCHAR(100) NOT NULL, -- Enlaza por texto a tabla TipoSolicitud
	FechaSolicitud VARCHAR(20) NOT NULL,
	FechaRecepcion VARCHAR(20) NOT NULL,
	NumeroCliente VARCHAR(20) NOT NULL,
	NombreCliente VARCHAR(100) NOT NULL,
	DireccionCalleCliente VARCHAR(255) NOT NULL,
	DireccionNumeroCliente VARCHAR(255) NOT NULL,
	Region VARCHAR(100) NOT NULL, -- Enlaza por texto a tabla Region
	Comuna VARCHAR(100) NOT NULL, -- Enlaza por texto a tabla Comuna
	TelefonoContacto VARCHAR(15) NOT NULL,
	TelefonoContacto2 VARCHAR(15) NOT NULL,
	Rut VARCHAR(12) NOT NULL,
	UnidadNegocio VARCHAR(100) NOT NULL, -- Enlaza por texto a tabla UnidadNegocio
	Gerencia VARCHAR(100) NOT NULL, -- Enlaza por texto a tabla Gerencia
	ObservacionAof VARCHAR(500) NOT NULL,
	Prioridad VARCHAR(100) NOT NULL -- Enlaza por texto a tabla Prioridad
)
GO

-- Se crea un detalle de producto por cada numero de placa distinto en el archivo
CREATE TABLE CargaMasivaDetalleProducto (
	CargaMasivaDetalleId INT NOT NULL FOREIGN KEY REFERENCES CargaMasivaDetalle (CargaMasivaDetalleId),
	NumeroPlaca VARCHAR(20) NOT NULL, -- Enlaza por numero a tabla de Existencias
	UNIQUE (CargaMasivaDetalleId, NumeroPlaca)
)
GO

CREATE VIEW Existencia AS 
SELECT * FROM MiLogistic.dbo.Existencia
GO

CREATE TABLE BinToEstadoEquipo (
	Bintoestadoequipoid INT PRIMARY KEY IDENTITY(1, 1),
	Estadoequipoid INT NOT NULL,
	Bin VARCHAR(100) NOT NULL
)
GO