USE [TP6_Movil_Prueba]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--Tabla Productos
CREATE TABLE Productos (
IdProductos int IDENTITY (1,1) PRIMARY KEY,
Nombre varchar (300) NOT NULL,
Descripcion varchar (300) NOT NULL,
Precio decimal (18,2) NOT NULL,
Stock int NULL,
Imagen varchar (400) NOT NULL
)
GO


---Tabla Usuarios
CREATE TABLE Usuarios (
	IdUsuario int IDENTITY (1,1) PRIMARY KEY,
	Nombre varchar(100) NOT NULL,
	Email varchar(400) NOT NULL,
	Imagen varchar(200) NOT NULL,
	Contrasenia varchar(400) NULL,
	CategoriaPreferida varchar(200) NOT NULL,
	IdRol int NOT NULL,
	Activo bit NOT NULL
) 
GO

-- Tabla Carrito
CREATE TABLE Carrito (
    IdCarrito int IDENTITY (1,1) PRIMARY KEY NOT NULL,
    IdProductos int NOT NULL,  -- Columna para referenciar a Productos
    IdUsuario int NOT NULL,    -- Columna para referenciar a Usuarios
    PrecioTotalCarrito decimal (18,2) NOT NULL,
    FechaCreacion datetime NOT NULL,
    Descripcion varchar(100) NOT NULL,

    -- REFERENCIA A LAS TABLAS USUARIOS Y PRODUCTOS
    FOREIGN KEY (IdProductos) REFERENCES Productos(IdProductos),
    FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario)
)
GO

--Detalle Carrito
CREATE TABLE DetalleCarrito(
IdDetalleCarrito int IDENTITY (1,1) PRIMARY KEY NOT NULL,
PrecioTotalDetalleCarrito decimal (18,2) NOT NULL,
IdCarrito int NOT NULL,
FechaFactura datetime,
DetalleFactura varchar(800) NOT NULL,
FechaCreacionFactura datetime NOT NULL,
FOREIGN KEY (IdCarrito) REFERENCES Carrito (IdCarrito)
)
GO






-------------------------------------------------------------------------------------------------------------------------------------------------


INSERT INTO Productos (Nombre, Descripcion, Precio, Stock, Imagen)
VALUES ('El Codigo Da Vinci - Dan Brown', 'Un thriller que sigue a Robert Langdon, un simbologista que se ve involucrado en un asesinato en el Museo del Louvre. A medida que investiga, descubre una conspiración que se remonta a los primeros días del cristianismo.',12500,200, 'C:\Users\Joaquin\Desktop\TP 6 - Programación Móvil\codigodavinci.jpeg');

INSERT INTO Productos (Nombre, Descripcion, Precio, Stock, Imagen)
VALUES ('La chica del tren - Paula Hawkins','La historia se centra en Rachel, una mujer que se obsesiona con la vida de una pareja que observa desde el tren. Cuando la mujer desaparece, Rachel se convierte en una testigo clave, pero su propia vida está llena de secretos oscuros.',10000,300,'C:\Users\Joaquin\Desktop\TP 6 - Programación Móvil\lachicadeltren.jpeg');

INSERT INTO Productos (Nombre, Descripcion, Precio, Stock, Imagen)
VALUES('Perdida - Gillian Flynn','En el día de su quinto aniversario, Nick Dunne reporta la desaparición de su esposa, Amy. A medida que la investigación avanza, se revelan verdades inquietantes sobre su matrimonio y los oscuros secretos de ambos.',9800,12,'C:\Users\Joaquin\Desktop\TP 6 - Programación Móvil\perdida.jpeg');

INSERT INTO Productos(Nombre, Descripcion, Precio, Stock, Imagen)
VALUES('El silencio de los inocentes - Thomas Harris','La agente del FBI Clarice Starling busca la ayuda del encarcelado asesino en serie Hannibal Lecter para capturar a otro asesino. La historia explora la psicología del crimen y la manipulación.',11200,10,'C:\Users\Joaquin\Desktop\TP 6 - Programación Móvil\elsilenciodelosinocentes.jpeg');

INSERT INTO Productos (Nombre, Descripcion, Precio, Stock, Imagen)
VALUES('Los pilares de la tierra - Ken Follet','Ambientada en la Inglaterra medieval, la novela narra la historia de la construcción de una catedral en un pueblo y las luchas de poder entre nobles, religiosos y ciudadanos. Un drama lleno de intrigas y personajes complejos.',15000,50,'C:\Users\Joaquin\Desktop\TP 6 - Programación Móvil\lospilaresdelatierra.jpeg');

INSERT INTO Productos (Nombre, Descripcion, Precio, Stock, Imagen)
VALUES('El Psicoanalista - John Katzenbach','El Dr. Frederick Starks recibe un mensaje amenazante de un misterioso atacante que le da un mes para descubrir su identidad. A medida que intenta salvar su vida, se adentra en un juego psicológico peligroso.',9200,50,'C:\Users\Joaquin\Desktop\TP 6 - Programación Móvil\elpsicoanalista.jpeg');

INSERT INTO Productos (Nombre, Descripcion, Precio, Stock, Imagen)
VALUES('La sombra del viento - Carlos Ruiz Zafon','Daniel, un joven en la Barcelona de la posguerra, descubre un libro en un misterioso cementerio de libros olvidados. A medida que investiga la vida del autor, se enfrenta a un oscuro secreto que amenaza su vida.',11500,30,'C:\Users\Joaquin\Desktop\TP 6 - Programación Móvil\lasombradelviento.jpeg');

INSERT INTO Productos (Nombre, Descripcion, Precio, Stock, Imagen)
VALUES('La verdad sobre el caso Harry Quebert - Joël Dicker','El escritor Marcus Goldman investiga la condena de su mentor Harry Quebert por el asesinato de una joven. A medida que desentraña la historia, descubre secretos que cambian su vida y la percepción de su mentor.',10500,300,'C:\Users\Joaquin\Desktop\TP 6 - Programación Móvil\laverdaddelcasoharryquebert.jpeg');

INSERT INTO Productos (Nombre, Descripcion, Precio, Stock, Imagen)
VALUES('Rebecca - Daphne Du Maurier','La historia sigue a una joven que se casa con el viudo Maxim de Winter, pero se siente opacada por la presencia de su primera esposa, Rebecca. La atmósfera gótica y el misterio que rodea a Rebecca la llevan a una inquietante revelación.',40,9600,'C:\Users\Joaquin\Desktop\TP 6 - Programación Móvil\rebecca.jpeg');

INSERT INTO Productos (Nombre, Descripcion, Precio, Stock, Imagen)
VALUES('Shutter Island - Dennis Lehane','En 1954, el mariscal de EE. UU. Teddy Daniels investiga la desaparición de una paciente en un hospital psiquiátrico en una isla remota. A medida que profundiza en el caso, comienza a cuestionar su propia cordura y la naturaleza de la verdad.',8900,43,'C:\Users\Joaquin\Desktop\TP 6 - Programación Móvil\shutterisland.jpeg');

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


--Agrego 5 Usuarios para probar la funcionalidad

SET IDENTITY_INSERT Usuarios OFF;

INSERT INTO Usuarios (Nombre, Email, Contrasenia, Imagen, CategoriaPreferida, IdRol, Activo)
VALUES('Jose Rodriguez','josero@gmail.com','adewqe232!','C:\Users\Joaquin\Desktop\TP 6 - Programación Móvil\usuarios.jpg','Comedia', 1, 1);

INSERT INTO Usuarios (Nombre, Email, Contrasenia, Imagen, CategoriaPreferida, IdRol, Activo)
VALUES('Cristian Perez','cristianpepe','abcd234','C:\Users\Joaquin\Desktop\TP 6 - Programación Móvil\usuarios.jpg','Suspenso y Drama', 1, 1);

INSERT INTO Usuarios (Nombre, Email, Contrasenia, Imagen, CategoriaPreferida, IdRol, Activo)
VALUES('Clemente Andrada','silbatoloco@gmail.com','Lgix123)','C:\Users\Joaquin\Desktop\TP 6 - Programación Móvil\usuarios.jpg','Suspenso y Drama', 1, 1);

INSERT INTO Usuarios (Nombre, Email, Contrasenia, Imagen, CategoriaPreferida, IdRol, Activo)
VALUES('Alejo Lopez','lolopelado@gmail.com','ÑL)=&"','C:\Users\Joaquin\Desktop\TP 6 - Programación Móvil\usuarios.jpg','Comedia', 1, 1);

INSERT INTO Usuarios (Nombre, Email, Contrasenia, Imagen, CategoriaPreferida, IdRol, Activo)
VALUES('Juan Carlos Alegri','juancaalegri@yahoo.com.ar','Pl0129(//(','C:\Users\Joaquin\Desktop\TP 6 - Programación Móvil\usuarios.jpg','Ciencia Ficcion', 1, 1);


----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
SET IDENTITY_INSERT Carrito ON;

INSERT INTO Carrito(FechaCreacion, PrecioTotalCarrito, Descripcion, IdUsuario, IdProductos)
VALUES('2024-10-05 15:30:00',12500, 'El Codigo Da Vinci - Dan Brown',1 ,1);

INSERT INTO Carrito(FechaCreacion, PrecioTotalCarrito, Descripcion, IdUsuario, IdProductos)
VALUES('2024-10-05 15:30:00',10000,'La chica del tren - Paula Hawkins',2,2);

INSERT INTO Carrito(FechaCreacion, PrecioTotalCarrito, Descripcion, IdUsuario, IdProductos)
VALUES('2024-10-08 12:20:00',9800, 'Perdida - Gillian Flynn', 3,3);

INSERT INTO Carrito(FechaCreacion, PrecioTotalCarrito, Descripcion, IdUsuario, IdProductos)
VALUES('2024-10-12 12:10:00',11200, 'El silencio de los inocentes - Thomas Harris', 4,4);

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

SET IDENTITY_INSERT DetalleCarrito ON;

INSERT INTO DetalleCarrito (PrecioTotalDetalleCarrito, IdCarrito, FechaFactura, DetalleFactura, FechaCreacionFactura)
VALUES (12500, 1, '2024-10-05 15:30:00', 'Compra de "El código Da Vinci", por el valor de $12.500', '2024-10-05 15:30:00');

INSERT INTO DetalleCarrito (PrecioTotalDetalleCarrito, IdCarrito, FechaFactura, DetalleFactura, FechaCreacionFactura)
VALUES (10000, 2, '2024-10-05 15:30:00', 'Compra de "La chica del tren", por el valor de $10.000', '2024-10-05 15:30:00');

INSERT INTO DetalleCarrito (PrecioTotalDetalleCarrito, IdCarrito, FechaFactura, DetalleFactura, FechaCreacionFactura)
VALUES (9800, 3, '2024-10-08 12:20:00', 'Compra de "Perdida", por el valor de $9.800', '2024-10-08 12:20:00');

INSERT INTO DetalleCarrito (PrecioTotalDetalleCarrito, IdCarrito, FechaFactura, DetalleFactura, FechaCreacionFactura)
VALUES (11200, 4, '2024-10-12 12:10:00', 'Compra de "El silencio de los inocentes", por el valor de $11.200', '2024-10-12 12:10:00');