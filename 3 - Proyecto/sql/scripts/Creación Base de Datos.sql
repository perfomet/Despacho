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
	Nombre VARCHAR(255) NOT NULL,
	RUT VARCHAR(11) NOT NULL,
	VRUT VARCHAR(1)
)

SET IDENTITY_INSERT Cliente ON

INSERT INTO Cliente (ClienteId, Nombre, RUT, VRUT) VALUES (1, 'Cliente de pruebas', '11111111','1')

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
VALUES (1, 'fbalmaseda', '123', 'Fancisco', 'Balmaseda', 'Neyra', 'francisco.vivacuba@gmail.com', NULL)
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

CREATE TABLE Region (
	RegionId INT IDENTITY(1,1) PRIMARY KEY,
	Region VARCHAR(100) NULL,
)

INSERT INTO Region (Region) VALUES ('TARAPACÁ')
INSERT INTO Region (Region) VALUES ('ANTOFAGASTA')
INSERT INTO Region (Region) VALUES ('ATACAMA')
INSERT INTO Region (Region) VALUES ('COQUIMBO')
INSERT INTO Region (Region) VALUES ('REGIÓN DE VALPARAÍSO')
INSERT INTO Region (Region) VALUES ('REGIÓN DEL LIBERTADOR BERNARDO O´HIGGINS')
INSERT INTO Region (Region) VALUES ('REGIÓN DEL MAULE')
INSERT INTO Region (Region) VALUES ('REGIÓN DEL BIOBIO')
INSERT INTO Region (Region) VALUES ('REGIÓN DE LA ARAUCANÍA')
INSERT INTO Region (Region) VALUES ('REGIÓN DE LOS LAGOS')
INSERT INTO Region (Region) VALUES ('REGIÓN DE AISÉN DEL GENERAL CARLOS IBAÑEZ DEL CAMPO')
INSERT INTO Region (Region) VALUES ('REGIÓN DE MAGALLANES Y DE LA ANTÁRTICA CHILENA')
INSERT INTO Region (Region) VALUES ('REGIÓN METROPOLITANA')
INSERT INTO Region (Region) VALUES ('REGIÓN DE LOS RÍOS')
INSERT INTO Region (Region) VALUES ('ARICA Y PARINACOTA')
INSERT INTO Region (Region) VALUES ('ÑUBLE')

CREATE TABLE Provincia (
	ProvinciaId INT IDENTITY(1,1) PRIMARY KEY,
	RegionId INT NOT NULL FOREIGN KEY REFERENCES Region (RegionId),
	Orden INT NOT NULL,
	Provincia VARCHAR(50) NOT NULL,
)

INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (1, 1,'IQUIQUE')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (1, 2,'TAMARUGAL')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (2, 1,'ANTOFAGASTA')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (2, 2,'EL LOA')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (2, 3,'TOCOPILLA')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (3, 1,'COPIAPÓ')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (3, 2,'CHAÑARAL')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (3, 3,'HUASCO')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (4, 4,'ELQUI')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (4, 5,'CHOAPA')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (4, 6,'LIMARÍ')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (5, 1,'VALPARAÍSO')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (5, 2,'ISLA DE PASCUA')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (5, 3,'LOS ANDES')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (5, 4,'PETORCA')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (5, 5,'QUILLOTA')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (5, 6,'SAN ANTONIO')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (5, 7,'SAN FELIPE DE ACONCAGUA')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (5, 8,'MARGA MARGA')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (6, 1,'CACHAPOAL')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (6, 2,'CARDENAL CARO')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (6, 3,'COLCHAGUA')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (7, 1,'TALCA')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (7, 2,'CAUQUENES')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (7, 3,'CURICÓ')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (7, 4,'LINARES')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (8, 1,'CONCEPCIÓN')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (8, 2,'ARAUCO')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (8, 3,'BIOBÍO')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (9, 1,'CAUTÍN')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (9, 2,'MALLECO')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (10, 1,'LLANQUIHUE')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (10, 2,'CHILOÉ')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (10, 3,'OSORNO')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (10, 4,'PALENA')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (11, 1,'COIHAIQUE')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (11, 2,'AISÉN')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (11, 3,'CAPITÁN PRAT')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (11, 4,'GENERAL CARRERA')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (12, 1,'MAGALLANES')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (12, 2,'ANTÁRTICA CHILENA')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (12, 3,'TIERRA DEL FUEGO')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (12, 4,'ÚLTIMA ESPERANZA')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (13, 1,'SANTIAGO')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (13, 2,'CORDILLERA')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (13, 3,'CHACABUCO')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (13, 4,'MAIPO')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (13, 5,'MELIPILLA')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (13, 6,'TALAGANTE')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (14, 1,'VALDIVIA')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (14, 2,'RANCO')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (15, 1,'ARICA')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (15, 2,'PARINACOTA')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (16, 1,'DIGUILLÓN')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (16, 2,'ITATA')
INSERT INTO Provincia (RegionId, Orden, Provincia) VALUES (16, 3,'PUNILLA')

CREATE TABLE Comuna(
	ComunaId INT IDENTITY(1,1) PRIMARY KEY,
	RegionId INT NULL FOREIGN KEY REFERENCES Region (RegionId),
	ProvinciaId INT NULL FOREIGN KEY REFERENCES Provincia (ProvinciaId),
	Comuna VARCHAR(50) NULL,
)

INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (1, 1,'IQUIQUE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (1, 1,'ALTO HOSPICIO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (1, 2,'POZO ALMONTE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (1, 2,'CAMIÑA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (1, 2,'COLCHANE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (1, 2,'HUARA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (1, 2,'PICA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (2, 3,'ANTOFAGASTA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (2, 3,'MEJILLONES')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (2, 3,'SIERRA GORDA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (2, 3,'TALTAL')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (2, 4,'CALAMA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (2, 4,'OLLAGÜE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (2, 4,'SAN PEDRO DE ATACAMA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (2, 5,'TOCOPILLA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (2, 5,'MARÍA ELENA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (3, 6,'COPIAPÓ')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (3, 6,'CALDERA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (3, 6,'TIERRA AMARILLA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (3, 7,'CHAÑARAL')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (3, 7,'DIEGO DE ALMAGRO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (3, 8,'VALLENAR')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (3, 8,'ALTO DEL CARMEN')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (3, 8,'FREIRINA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (3, 8,'HUASCO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (4, 9,'LA SERENA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (4, 9,'COQUIMBO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (4, 9,'ANDACOLLO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (4, 9,'LA HIGUERA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (4, 9,'PAIGUANO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (4, 9,'VICUÑA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (4, 10,'ILLAPEL')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (4, 10,'CANELA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (4, 10,'LOS VILOS')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (4, 10,'SALAMANCA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (4, 11,'OVALLE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (4, 11,'COMBARBALÁ')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (4, 11,'MONTE PATRIA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (4, 11,'PUNITAQUI')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (4, 11,'RÍO HURTADO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 12,'VALPARAÍSO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 12,'CASABLANCA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 12,'CONCÓN')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 12,'JUAN FERNÁNDEZ')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 12,'PUCHUNCAVÍ')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 12,'QUINTERO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 12,'QUINTERO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 13,'ISLA DE PASCUA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 14,'LOS ANDES')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 14,'CALLE LARGA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 14,'RINCONADA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 14,'SAN ESTEBAN')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 15,'LA LIGUA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 15,'CABILDO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 15,'PAPUDO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 15,'PETORCA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 15,'ZAPALLAR')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 15,'ZAPALLAR')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 16,'QUILLOTA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 16,'CALERA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 16,'HIJUELAS')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 16,'LA CRUZ')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 16,'NOGALES')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 17,'SAN ANTONIO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 17,'ALGARROBO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 17,'CARTAGENA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 17,'EL QUISCO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 17,'EL TABO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 17,'SANTO DOMINGO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 18,'SAN FELIPE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 18,'CATEMU')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 18,'LLAILLAY')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 18,'PANQUEHUE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 18,'PUTAENDO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 18,'SANTA MARÍA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 19,'QUILPUÉ')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 19,'LIMACHE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 19,'OLMUÉ')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (5, 19,'VILLA ALEMANA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (6,20,'RANCAGUA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (6,20,'CODEGUA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (6,20,'COINCO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (6,20,'COLTAUCO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (6,20,'DOÑIHUE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (6,20,'GRANEROS')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (6,20,'LAS CABRAS')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (6,20,'MACHALÍ')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (6,20,'MALLOA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (6,20,'MOSTAZAL')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (6,20,'OLIVAR')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (6,20,'PEUMO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (6,20,'PICHIDEGUA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (6,20,'QUINTA DE TILCOCO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (6,20,'RENGO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (6,20,'REQUÍNOA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (6,20,'SAN VICENTE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (6,21,'PICHILEMU')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (6,21,'LA ESTRELLA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (6,21,'LITUECHE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (6,21,'MARCHIHUE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (6,21,'NAVIDAD')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (6,21,'PAREDONES')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (6,22,'SAN FERNANDO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (6,22,'CHÉPICA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (6,22,'CHIMBARONGO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (6,22,'LOLOL')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (6,22,'NANCAGUA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (6,22,'PALMILLA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (6,22,'PERALILLO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (6,22,'PLACILLA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (6,22,'PUMANQUE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (6,22,'SANTA CRUZ')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (7,23,'TALCA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (7,23,'CONSTITUCIÓN')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (7,23,'CUREPTO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (7,23,'EMPEDRADO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (7,23,'MAULE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (7,23,'PELARCO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (7,23,'PENCAHUE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (7,23,'RÍO CLARO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (7,23,'SAN CLEMENTE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (7,23,'SAN RAFAEL')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (7,24,'CAUQUENES')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (7,24,'CHANCO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (7,24,'PELLUHUE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (7,25,'CURICÓ')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (7,25,'HUALAÑÉ')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (7,25,'LICANTÉN')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (7,25,'MOLINA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (7,25,'RAUCO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (7,25,'ROMERAL')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (7,25,'SAGRADA FAMILIA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (7,25,'TENO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (7,25,'VICHUQUÉN')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (7,26,'LINARES')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (7,26,'COLBÚN')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (7,26,'LONGAVÍ')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (7,26,'PARRAL')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (7,26,'RETIRO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (7,26,'SAN JAVIER')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (7,26,'VILLA ALEGRE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (7,26,'YERBAS BUENAS')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (8,27,'CONCEPCIÓN')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (8,27,'CORONEL')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (8,27,'CHIGUAYANTE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (8,27,'FLORIDA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (8,27,'HUALQUI')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (8,27,'LOTA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (8,27,'PENCO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (8,27,'SAN PEDRO DE LA PAZ')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (8,27,'SANTA JUANA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (8,27,'TALCAHUANO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (8,27,'TOMÉ')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (8,27,'HUALPÉN')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (8,28,'LEBU')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (8,28,'ARAUCO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (8,28,'CAÑETE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (8,28,'CONTULMO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (8,28,'CURANILAHUE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (8,28,'LOS ALAMOS')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (8,28,'TIRÚA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (8,29,'LOS ÁNGELES')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (8,29,'ANTUCO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (8,29,'CABRERO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (8,29,'LAJA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (8,29,'MULCHÉN')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (8,29,'NACIMIENTO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (8,29,'NEGRETE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (8,29,'QUILACO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (8,29,'QUILLECO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (8,29,'SAN ROSENDO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (8,29,'SANTA BÁRBARA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (8,29,'TUCAPEL')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (8,29,'TUCAPEL')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (8,29,'ALTO BIOBÍO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (9,30,'TEMUCO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (9,30,'CARAHUE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (9,30,'CUNCO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (9,30,'CURARREHUE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (9,30,'FREIRE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (9,30,'GALVARINO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (9,30,'GORBEA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (9,30,'LAUTARO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (9,30,'LONCOCHE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (9,30,'MELIPEUCO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (9,30,'NUEVA IMPERIAL')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (9,30,'PADRE LAS CASAS')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (9,30,'PERQUENCO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (9,30,'PITRUFQUÉN')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (9,30,'PUCÓN')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (9,30,'SAAVEDRA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (9,30,'TEODORO SCHMIDT')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (9,30,'TOLTÉN')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (9,30,'VILCÚN')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (9,30,'VILLARRICA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (9,30,'CHOLCHOL')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (9,31,'ANGOL')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (9,31,'COLLIPULLI')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (9,31,'CURACAUTÍN')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (9,31,'ERCILLA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (9,31,'LONQUIMAY')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (9,31,'LOS SAUCES')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (9,31,'LUMACO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (9,31,'PURÉN')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (9,31,'RENAICO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (9,31,'TRAIGUÉN')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (9,31,'VICTORIA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (10,32,'PUERTO MONTT')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (10,32,'CALBUCO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (10,32,'COCHAMÓ')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (10,32,'FRESIA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (10,32,'FRUTILLAR')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (10,32,'LOS MUERMOS')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (10,32,'LLANQUIHUE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (10,32,'MAULLÍN')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (10,32,'PUERTO VARAS')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (10,33,'CASTRO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (10,33,'ANCUD')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (10,33,'CHONCHI')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (10,33,'CURACO DE VÉLEZ')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (10,33,'DALCAHUE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (10,33,'PUQUELDÓN')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (10,33,'QUEILÉN')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (10,33,'QUELLÓN')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (10,33,'QUEMCHI')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (10,33,'QUINCHAO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (10,34,'OSORNO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (10,34,'PUERTO OCTAY')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (10,34,'PURRANQUE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (10,34,'PUYEHUE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (10,34,'RÍO NEGRO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (10,34,'SAN JUAN DE LA COSTA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (10,34,'SAN PABLO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (10,35,'CHAITÉN')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (10,35,'FUTALEUFÚ')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (10,35,'HUALAIHUÉ')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (10,35,'PALENA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (11,36,'COIHAIQUE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (11,36,'LAGO VERDE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (11,37,'AISÉN')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (11,37,'CISNES')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (11,37,'GUAITECAS')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (11,38,'COCHRANE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (11,38,'O´HIGGINS')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (11,38,'TORTEL')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (11,39,'CHILE CHICO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (11,39,'RÍO IBAÑEZ')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (12,40,'PUNTA ARENAS')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (12,40,'LAGUNA BLANCA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (12,40,'RÍO VERDE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (12,40,'SAN GREGORIO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (12,41,'CABO DE HORNOS')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (12,42,'PORVENIR')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (12,42,'PRIMAVERA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (12,42,'TIMAUKEL')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (12,43,'NATALES')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (12,43,'TORRES DEL PAINE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,44,'SANTIAGO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,44,'CERRILLOS')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,44,'CERRO NAVIA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,44,'CONCHALÍ')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,44,'EL BOSQUE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,44,'ESTACIÓN CENTRAL')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,44,'HUECHURABA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,44,'INDEPENDENCIA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,44,'LA CISTERNA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,44,'LA FLORIDA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,44,'LA GRANJA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,44,'LA PINTANA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,44,'LA REINA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,44,'LAS CONDES')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,44,'LO BARNECHEA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,44,'LO ESPEJO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,44,'LO PRADO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,44,'MACUL')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,44,'MAIPÚ')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,44,'ÑUÑOA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,44,'PEDRO AGUIRRE CERDA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,44,'PEÑALOLÉN')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,44,'PROVIDENCIA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,44,'PUDAHUEL')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,44,'QUILICURA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,44,'QUINTA NORMAL')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,44,'RECOLETA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,44,'RENCA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,44,'SAN JOAQUÍN')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,44,'SAN MIGUEL')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,44,'SAN RAMÓN')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,44,'VITACURA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,45,'PUENTE ALTO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,45,'PIRQUE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,45,'SAN JOSÉ DE MAIPO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,46,'COLINA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,46,'LAMPA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,46,'TILTIL')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,47,'SAN BERNARDO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,47,'BUIN')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,47,'CALERA DE TANGO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,47,'PAINE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,48,'MELIPILLA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,48,'ALHUÉ')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,48,'CURACAVÍ')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,48,'MARÍA PINTO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,48,'SAN PEDRO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,49,'TALAGANTE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,49,'EL MONTE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,49,'ISLA DE MAIPO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,49,'PADRE HURTADO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (13,49,'PEÑAFLOR')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (14,50,'VALDIVIA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (14,50,'CORRAL')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (14,50,'LANCO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (14,50,'LOS LAGOS')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (14,50,'MÁFIL')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (14,50,'MARIQUINA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (14,50,'PAILLACO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (14,50,'PANGUIPULLI')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (14,51,'LA UNIÓN')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (14,51,'FUTRONO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (14,51,'LAGO RANCO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (14,51,'RÍO BUENO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (15,52,'ARICA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (15,52,'CAMARONES')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (15,53,'PUTRE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (15,53,'GENERAL LAGOS')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (16,54,'CHILLÁN')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (16,54,'BULNES')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (16,54,'CHILLÁN VIEJO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (16,54,'EL CARMEN')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (16,54,'PEMUCO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (16,54,'PINTO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (16,54,'QUILLÓN')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (16,54,'SAN IGNACIO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (16,54,'YUNGAY')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (16,55,'QUIRIHUE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (16,55,'COBQUECURA')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (16,55,'COELEMU')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (16,55,'NINHUE')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (16,55,'PORTEZUELO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (16,55,'RANQUIL')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (16,55,'TREGUACO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (16,56,'SAN CARLOS')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (16,56,'COIHUECO')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (16,56,'ÑIQUÉN')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (16,56,'SAN FABIÁN')
INSERT INTO Comuna (RegionId, ProvinciaId, Comuna) VALUES  (16,56,'SAN NICOLÁS')

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
	ComunaId INT NOT NULL FOREIGN KEY REFERENCES Comuna (ComunaId),
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
	VRUTConcrecion VARCHAR(1) NULL,
	MotivoNoConcrecion VARCHAR(MAX) NULL,
	RetiroReal INT NULL
)
