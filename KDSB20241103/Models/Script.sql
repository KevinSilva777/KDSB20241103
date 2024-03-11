 --Crear la base de datos
CREATE DATABASE KDSB20241103DB;
GO

-- Usar la base de datos recién creada
USE KDSB20241103DB;
GO

-- Crear la tabla Cliente
CREATE TABLE Cliente (
    IdCliente INT PRIMARY KEY IDENTITY(1,1),
    NombreCliente VARCHAR(100) NOT NULL,
    TotalVenta DECIMAL(10,2) NOT NULL
);
GO

-- Crear la tabla TelefonoCliente (Detalle)
CREATE TABLE TelefonoCliente (
    IdTelefono INT PRIMARY KEY IDENTITY(1,1),
    IdCliente INT NOT NULL,
    NumeroTelefono VARCHAR(20) NOT NULL,
    FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente) ON DELETE CASCADE
);
GO
