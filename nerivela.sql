DROP DATABASE NeriVela;

CREATE DATABASE NeriVela;
use NeriVela;
/*select * from personal;*/

CREATE TABLE personal
(
	idUsuario int(11) NOT NULL auto_increment,
    nombre varchar(20) NOT NULL,
    ApellidoP varchar(20) NOT NULL,
    ApellidoM varchar(20) NOT NULL,
    calle varchar(50) NOT NULL,
    colonia varchar(50) NOT NULL,
    numExt varchar(5) NOT NULL,
    cp varchar(5) NOT NULL,
    telefono varchar(10),
    email varchar(50) NOT NULL,
    profesion varchar(50) NOT NULL,
    cargo varchar(20) NOT NULL,
    usuario varchar(20) unique,
    password varchar(20),
    PRIMARY KEY (idUsuario)
) ENGINE=INNODB;


CREATE TABLE bitacora
(
	idAcceso int not null auto_increment,
	Usuario varchar(20),
    Nombre varchar(20),
    Apellido varchar(20),
    Fecha varchar(20),
	HoraEntrada varchar(20),
    HoraSalida varchar(20),
    PRIMARY KEY (idAcceso)
) ENGINE=INNODB;

CREATE TABLE Padres
(
	idPadres int(11) NOT NULL auto_increment,
    nombre varchar(20) NOT NULL,
    ApellidoP varchar(20) NOT NULL,
    ApellidoM varchar(20) NOT NULL,
    lugTrabajo varchar(30) NOT NULL,
    Profesion varchar(30),
    telefono varchar(10),
    Celular varchar(10),
     Calle varchar(50) NOT NULL,
    Colonia varchar(50) NOT NULL,
    NumExt varchar(5),
    cp varchar(5) NOT NULL,
    PRIMARY KEY (idPadres)
) ENGINE=INNODB;

CREATE TABLE Maestros
(
	idMaestros int(11) NOT NULL auto_increment,
    nombre varchar(20) NOT NULL,
    ApellidoP varchar(20) NOT NULL,
    ApellidoM varchar(20) NOT NULL,
    calle varchar(50) NOT NULL,
    colonia varchar(50) NOT NULL,
    numExt varchar(5) ,
    cp varchar(5) NOT NULL,
    telefono varchar(10),
    Celular varchar(10),
    Email varchar(50) NOT NULL,
    gradoEncargado int(1) NOT NULL,
    Profesion varchar(50) NOT NULL,
    FechNac date NOT NULL,
    RFC varchar(18) unique NOT NULL,
    PRIMARY KEY (idMaestros)
) ENGINE=INNODB;

CREATE TABLE Grado
(
	idGrado int(11) NOT NULL auto_increment,
    grado varchar(1) NOT NULL,
    grupo varchar(1) NOT NULL,
    idMaestros int(11) NOT NULL,
    PRIMARY KEY (idGrado),
    CONSTRAINT fk_idMaestros FOREIGN KEY (idMaestros)
	REFERENCES Maestros(idMaestros)
) ENGINE=INNODB;

CREATE TABLE Alumno
(
	idAlumno int(11) NOT NULL auto_increment,
    nombre varchar(20) NOT NULL,
    ApellidoP varchar(20) NOT NULL,
    ApellidoM varchar(20) NOT NULL,
    calle varchar(50) NOT NULL,
    colonia varchar(50) NOT NULL,
    numExt varchar(5),
    cp varchar(5) NOT NULL,
    telEmer varchar(10) NOT NULL,
    Genero varchar(9) NOT NULL,
    lugNac varchar(30) NOT NULL,
    FechNac date NOT NULL,
    Alergias varchar(250),
    CURP varchar(20) unique NOT NULL,
    idPadres int(11) NOT NULL,
    idGrado int(11) NOT NULL,
    PRIMARY KEY (idAlumno),
    CONSTRAINT idPadres FOREIGN KEY (idPadres)
	REFERENCES Padres(idPadres),
    CONSTRAINT idGrado FOREIGN KEY (idGrado)
	REFERENCES Grado(idGrado)
) ENGINE=INNODB;

CREATE TABLE Materias
(
	idMaterias int(11) NOT NULL auto_increment,
    nombre varchar(20) NOT NULL,
    PRIMARY KEY (idMaterias),
    idGrado int(11) NOT NULL,
    CONSTRAINT idGrado FOREIGN KEY (idGrado)
	REFERENCES Grado(idGrado)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
) ENGINE=INNODB;


CREATE TABLE Calificaciones
(
	idCalificaciones int(11) NOT NULL auto_increment,
	CalificacionMen decimal(2,2) NOT NULL,
    CalificacionTri decimal(2,2) NOT NULL,
     PRIMARY KEY (idCalificaciones)
) ENGINE=INNODB;

INSERT INTO Maestros VALUES (1, 'José', 'Jacinto', 'Cuevas', 'Morteros', 'Marroquin', '70', '39640', '1587496', '744610125', 'jjCuevas@hotmail.com', '1', 'Lic. en Educación Primaria', '1972-07-30', 'JJCM720730TE');
INSERT INTO Maestros VALUES (2, 'Lucia', 'Figueroa', 'Pineda', 'SONORA' , 'PROGRESO', '76', '39350', '1900893', '744763789', 'luciFigP43@outlock.com', '2', 'Lic. en Educación Primaria', '1967-03-25', 'LFPU670325');
INSERT INTO Maestros VALUES (3, 'Rafael', 'Ramírez', 'Castañeda', 'AQUILES SERDÁN', 'SANTA DOROTE', '49', '39970', '1479632', '744827307', 'RafaCast100@hotmail.com', '3', 'Lic. en Educación Primaria', '1977-04-11', 'RRFP770411QO');
INSERT INTO Maestros VALUES (4, 'Ana Lilia', 'Morales', 'Simon', 'AV. 20 DE NOVIEMBRE', 'CENTRO', '1024', '39300', '9856731', '744637219', 'Ana_lilia@gmail.com', '4', 'Lic. en Educación Primaria', '1971-01-03', 'ALMS710103BG');
INSERT INTO Maestros VALUES (5, 'Alberto Daniel', 'Fuentes', 'Dávila', 'IGNACIO MATIAS', 'MA. LUISA', '6', '39122', '1157496', '744732275', 'Fuentes-Albert@gmail.com', '5', 'Lic. en Educación Primaria', '1974-11-09', 'ADFD741109QW');
INSERT INTO Maestros VALUES (6, 'Fabiola', 'Torres', 'Quintero', 'AV INDEPENDENCIA', 'CENTRO', '545', '39450', '1140833', '744793122', 'fabi_tq90@live.com.mx', '6', 'Lic. en Educación Primaria', '1970-09-18', 'FTQH700918MB');

INSERT INTO Grado VALUES (1, '1', 'A', '1');
INSERT INTO Grado VALUES (2, '2', 'A', '2');
INSERT INTO Grado VALUES (3, '3', 'A', '3');
INSERT INTO Grado VALUES (4, '4', 'A', '4');
INSERT INTO Grado VALUES (5, '5', 'A', '5');
INSERT INTO Grado VALUES (6, '6', 'A', '6');

/*SELECT * FROM nerivela.Alumno;
SELECT * FROM nerivela.Grado;
SELECT * FROM nerivela.Maestros;

SELECT * FROM nerivela.personal;
delete from nerivela.personal where idPersonal = 3;

SELECT COUNT(*) FROM nerivela.personal where usuario = 'Arkanoz' and password = 'digi3.0';
select * FROM nerivela.personal where usuario = 'Arkanoz' and password = 'digi3.0';*/CREATE TABLE Grado (  idGrado int(11) NOT NULL auto_increment,     grado varchar(1),     grupo varchar(1),     idMaestros int not null,     idMaterias int not null,     PRIMARY KEY (idGrado),     CONSTRAINT idMaestros FOREIGN key (idMaestros) references Maestros (idMaestros),     CONSTRAINT idMaterias FOREIGN key (idMaterias) references Materias (idMaterias) )
