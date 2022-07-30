CREATE TABLE Usuario(
	idUsuario		int PRIMARY KEY NOT NULL,
	userName		nvarchar(32) NOT NULL,
	userPass		nvarchar(32) NOT NULL,
	nombre			nvarchar(32) NOT NULL,
	apellido		nvarchar(32) NOT NULL,
	correo			nvarchar(64) NOT NULL,
	telefono		nvarchar(10) NOT NULL,
	genero			int NOT NULL,
	fechaIngreso	datetime NOT NULL,
)
DROP TABLE Usuario
IDENTITY(1,1) 
ALTER TABLE	Usuario (
	fechaIngreso	datetime NOT NULL,
)

INSERT INTO Usuario ( userName, userPass, nombre, apellido, correo, telefono, genero, fechaIngreso) VALUES ( 'jquixchan', '123abc', 'Jose', 'Quixchan', 'jq@gmail.com', '44334343', '1', 29-20-2022);