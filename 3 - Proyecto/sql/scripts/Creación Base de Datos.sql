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

CREATE TABLE Cliente(
	ClienteId INT PRIMARY KEY IDENTITY(2001, 1),
	Nombre VARCHAR(255) NOT NULL,
	RUT VARCHAR(12) NOT NULL
)

CREATE TABLE Perfil(
	PerfilId INT PRIMARY KEY IDENTITY,
	Descripcion VARCHAR(50) NOT NULL
)

SET IDENTITY_INSERT Perfil ON

INSERT INTO Perfil (PerfilId, Descripcion) VALUES (1, 'SuperUsuario') 
INSERT INTO Perfil (PerfilId, Descripcion) VALUES (2, 'Administrador') 
INSERT INTO Perfil (PerfilId, Descripcion) VALUES (3, 'Planificador') 
INSERT INTO Perfil (PerfilId, Descripcion) VALUES (4, 'Cliente') 
INSERT INTO Perfil (PerfilId, Descripcion) VALUES (5, 'Bodega') 

SET IDENTITY_INSERT Perfil OFF

CREATE TABLE Usuario(
	UsuarioId INT PRIMARY KEY IDENTITY(1001, 1),
	PerfilId INT NOT NULL FOREIGN KEY REFERENCES Perfil (PerfilId),
	Username VARCHAR(15) NOT NULL UNIQUE,
	Password VARCHAR(15) NOT NULL,
	Nombres VARCHAR(255) NOT NULL,
	ApellidoPaterno VARCHAR(255) NULL,
	ApellidoMaterno VARCHAR(50) NULL,
	Email VARCHAR(255) NOT NULL,
	ClienteId INT NULL FOREIGN KEY REFERENCES Cliente (ClienteId)
)

INSERT INTO Usuario (PerfilId, Username, Password, Nombres, ApellidoPaterno, ApellidoMaterno, Email, ClienteId) 
VALUES (1, 'dperdigon', '123', 'David', 'Perdigón', 'García', 'davidperdigon.g@gmail.com', NULL)
INSERT INTO Usuario (PerfilId, Username, Password, Nombres, ApellidoPaterno, ApellidoMaterno, Email, ClienteId)  
VALUES (1, 'fbalmaceda', '123', 'Fancisco', 'Balmaceda', 'Neira', 'francisco.vivacuba@gmail.com', NULL)
INSERT INTO Usuario (PerfilId, Username, Password, Nombres, ApellidoPaterno, ApellidoMaterno, Email, ClienteId)  
VALUES (1, 'administrador', '123', 'Usuario', 'Administrador', NULL, 'pruebas@prueba.cl', NULL)
INSERT INTO Usuario (PerfilId, Username, Password, Nombres, ApellidoPaterno, ApellidoMaterno, Email, ClienteId)  
VALUES (1, 'planificador', '123', 'Usuario', 'Planificador', NULL, 'pruebas@prueba.cl', NULL)
INSERT INTO Usuario (PerfilId, Username, Password, Nombres, ApellidoPaterno, ApellidoMaterno, Email, ClienteId)  
VALUES (1, 'cliente', '123', 'Usuario', 'Cliente', NULL, 'pruebas@prueba.cl', NULL)
INSERT INTO Usuario (PerfilId, Username, Password, Nombres, ApellidoPaterno, ApellidoMaterno, Email, ClienteId)  
VALUES (1, 'bodega', '123', 'Usuario', 'Bodega', NULL, 'pruebas@prueba.cl', NULL)



