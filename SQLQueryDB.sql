CREATE TABLE Usuario(
	idUsuario		int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	userName		nvarchar(32) NOT NULL,
	userPass		nvarchar(32) NOT NULL,
	nombre			nvarchar(32) NOT NULL,
	apellido		nvarchar(32) NOT NULL,
	correo			nvarchar(64) NOT NULL,
	telefono		nvarchar(10) NOT NULL,
	genero			int NOT NULL,
	fechaIngreso	datetime2 DEFAULT(getdate()) NOT NULL,
)
DROP TABLE Usuario
 
ALTER TABLE	Usuario(
	fechaIngreso	datetime NOT NULL,
)

INSERT INTO Usuario ( userName, userPass, nombre, apellido, correo, telefono, genero) VALUES ( 'jquixchan', '123abc', 'Jose', 'Quixchan', 'jq@gmail.com', '44334343', '1');
INSERT INTO Usuario ( userName, userPass, nombre, apellido, correo, telefono, genero) VALUES ( 'lquixchan', '123abc', 'Luis', 'Quixchan', 'lq@gmail.com', '44334343', '1');
INSERT INTO Usuario ( userName, userPass, nombre, apellido, correo, telefono, genero) VALUES ( 'juanChu', '123abc', 'Juan Carlos', 'Chupapija', 'JC@gmail.com', '44334343', '1');

CREATE TABLE Usuario(
	idUsuario		int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	userName		nvarchar(32) NOT NULL,
	userPassHash	varbinary NOT NULL,
	userPassSalt	varbinary NOT NULL,
	nombre			nvarchar(32) NOT NULL,
	apellido		nvarchar(32) NOT NULL,
	correo			nvarchar(64) NOT NULL,
	telefono		nvarchar(10) NOT NULL,
	genero			int NOT NULL,
	fechaIngreso	datetime DEFAULT(getdate()) NOT NULL,
)
DROP TABLE Cliente

INSERT INTO Usuario ( userName, userPassHash, userPassSalt, nombre, apellido, correo, telefono, genero) VALUES ( 'jquixchan', '0x5468697320697320612074657374', '0x5468697320697320612074657374', 'Jose', 'Quixchan', 'jq@gmail.com', '44334343', '1');
INSERT INTO Usuario ( userName, userPassHash, userPassSalt, nombre, apellido, correo, telefono, genero) VALUES ( 'lquixchan', '0x5468697320697320612074657374', '0x5468697320697320612074657374', 'Luis', 'Quixchan', 'lq@gmail.com', '44334343', '1');
INSERT INTO Usuario ( userName, userPassHash, userPassSalt, nombre, apellido, correo, telefono, genero) VALUES ( 'juanChu', '0x5468697320697320612074657374', '0x5468697320697320612074657374', 'Juan Carlos', 'Chupapija', 'JC@gmail.com', '44334343', '1');

CREATE TABLE Cliente(
	idCliente		int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	nombre			nvarchar(32) NOT NULL,
	apellido		nvarchar(32) NOT NULL,
	correo			nvarchar(64) NOT NULL,
	telefono		nvarchar(10) NOT NULL,
	genero			int NOT NULL,
	fechaIngreso	datetime DEFAULT(getdate()) NOT NULL,
)

INSERT INTO Usuario ( userName, userPassHash, userPassSalt, nombre, apellido, correo, telefono, genero) VALUES ( 'juanChu', '0x5468697320697320612074657374', '0x5468697320697320612074657374', 'Juan Carlos', 'Chupapija', 'JC@gmail.com', '44334343', '1');

CREATE TABLE Producto(
	idProducto		int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	nombre			nvarchar(32) NOT NULL,
	descripcion		nvarchar(32) NOT NULL,
	descuento		int NOT NULL,
	fechaIngreso	datetime DEFAULT(getdate()) NOT NULL,
)

CREATE TABLE Menu(
	idMenu			int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	nombre			nvarchar(32) NOT NULL,
	descripcion		nvarchar(32) NOT NULL,
	fechaIngreso	datetime DEFAULT(getdate()) NOT NULL,
)



