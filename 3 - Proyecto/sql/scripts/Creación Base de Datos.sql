/********************************************************
* SCRIPT DE CREACI�N DE LA BASE DE DATOS ****************
* CUALQUIER CAMBIO DE ESTRUCTURA DEBE SER AGREGADO AQU� *
* CREADO POR: David Perdig�n ****************************
* FECHA Y HORA: 07/01/2020 23:00 ************************
********************************************************/

IF EXISTS(SELECT 1 FROM sys.databases WHERE name LIKE 'Despacho') BEGIN
	DROP DATABASE Despacho
END

CREATE DATABASE Despacho

