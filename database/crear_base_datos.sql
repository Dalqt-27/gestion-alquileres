use [master]
go

CREATE DATABASE [202601_5494_DB_ALQUILERES]
GO

USE [202601_5494_DB_ALQUILERES]
GO

CREATE TABLE Cliente (
    IdCliente   INT IDENTITY(1,1) PRIMARY KEY,
    RazonSocial VARCHAR(120) NOT NULL,
    RUC         VARCHAR(11)  NOT NULL UNIQUE,
    Telefono    VARCHAR(15)  NULL,
    Eliminado   BIT NOT NULL DEFAULT 0
);


CREATE TABLE Equipo (
    IdEquipo     INT IDENTITY(1,1) PRIMARY KEY,
    Nombre       VARCHAR(100) NOT NULL,
    CostoDiario  DECIMAL(10,2) NOT NULL,
    Disponible   BIT NOT NULL DEFAULT 1,
    Eliminado    BIT NOT NULL DEFAULT 0
);

CREATE TABLE Alquiler (
    IdAlquiler       INT IDENTITY(1,1) PRIMARY KEY,
    IdCliente        INT NOT NULL FOREIGN KEY REFERENCES Cliente(IdCliente),
    IdEquipo         INT NOT NULL FOREIGN KEY REFERENCES Equipo(IdEquipo),
    FechaAlquiler    DATETIME NOT NULL DEFAULT GETDATE(),
    FechaDevolucion  DATETIME NULL,
    CostoTotal       DECIMAL(10,2) NULL,
    Eliminado        BIT NOT NULL DEFAULT 0
);


INSERT INTO Cliente (RazonSocial, RUC, Telefono, Eliminado) VALUES
('Estudio Fotográfico Luz Andina SAC', '20512345671', '014567890', 0),
('Producciones Digitales Perú EIRL', '20512345672', '014567891', 0),
('Eventos Corporativos Sol SAC', '20512345673', '014567892', 0),
('Video Producciones Norte SAC', '20512345674', '014567893', 0),
('Consultora Multimedia Andes SAC', '20512345675', '014567894', 1); -- eliminado

INSERT INTO Equipo (Nombre, CostoDiario, Disponible, Eliminado) VALUES
('Cámara Canon EOS R6', 150.00, 1, 0),
('Laptop Dell XPS 15', 90.00, 1, 0),
('Proyector Epson X400', 60.00, 1, 0),
('Trípode profesional Manfrotto', 25.00, 1, 0),
('Micrófono inalámbrico Rode', 40.00, 1, 0),
('Dron DJI Mini 3', 180.00, 1, 1); -- eliminado
