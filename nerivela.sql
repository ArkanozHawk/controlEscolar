CREATE DATABASE NeriVela;
use NeriVela;
/*select * from personal;*/

CREATE TABLE personal
(
	idUsuario int(11) NOT NULL auto_increment,
    nombre varchar(20),
    ApellidoP varchar(20),
    ApellidoM varchar(20),
    calle varchar(50),
    colonia varchar(50),
    numExt varchar(5),
    cp varchar(5),
    telefono varchar(10),
    email varchar(50),
    profesion varchar(50),
    cargo varchar(20),
    usuario varchar(20) unique,
    password varchar(20),
    PRIMARY KEY (idUsuario)
);


CREATE TABLE bitacora
(
	idAcceso int not null auto_increment,
	Usuario varchar(20),
    Fecha date,
	HoraEntrada text,
    HoraSalida text,
    PRIMARY KEY (idAcceso)
);

CREATE TABLE Alumno
(
	idAlumno int(11) NOT NULL auto_increment,
    nombre varchar(20),
    ApellidoP varchar(20),
    ApellidoM varchar(20),
    calle varchar(50),
    colonia varchar(50),
    numExt varchar(5),
    cp varchar(5),
    telEmer varchar(10),
    lugNac varchar(30),
    FechNac date,
    CURP varchar(20) unique,
    PRIMARY KEY (idAlumno)
    /*Forent Key(Fk_idPadres)*/
    /*Forent Key(Fk_idMaestros)*/
);

CREATE TABLE Padres
(
	idPadres int(11) NOT NULL auto_increment,
    nombre varchar(20),
    ApellidoP varchar(20),
    ApellidoM varchar(20),
    lugTrabajo varchar(30),
    Profesion varchar(30),
    telefono varchar(10),
    Celular varchar(10),
    PRIMARY KEY (idPadre)
);

CREATE TABLE Materias
(
	idMaterias int(11) NOT NULL auto_increment,
    nombre varchar(20),
    Calificacion decimal(2,2),
    PRIMARY KEY (idMaterias)
    /*Forent Key(Fk_idGrupos)*/
);

CREATE TABLE Calificaciones
(
	idCalificaciones int(11) NOT NULL auto_increment,
	CalificacionMen decimal(2,2),
    CalificacionTri decimal(2,2),
     PRIMARY KEY (idCalificaciones),
);

CREATE TABLE Grupos
(
	idGrupos int(11) NOT NULL auto_increment,
    grupo varchar(1),
    PRIMARY KEY (idGrupos)
    /*Forent Key(Fk_idMaestros)*/
    /*Forent Key(Fk_idMaterias)*/
);

CREATE TABLE Maestros
(
	idMaestros int(11) NOT NULL auto_increment,
    nombre varchar(20),
    ApellidoP varchar(20),
    ApellidoM varchar(20),
    calle varchar(50),
    colonia varchar(50),
    numExt varchar(5),
    cp varchar(5),
    telefono varchar(10),
    Celular varchar(10),
    Email varchar(30),
    gradoEncargado int(1),
    Profesion varchar(20),
    FechNac date,
    RFC varchar(18) unique,
    PRIMARY KEY (idMaestros)
);
/*SELECT * FROM nerivela.personal;
delete from nerivela.personal where idPersonal = 3;

SELECT COUNT(*) FROM nerivela.personal where usuario = 'Arkanoz' and password = 'digi3.0';
select * FROM nerivela.personal where usuario = 'Arkanoz' and password = 'digi3.0';*/