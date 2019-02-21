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
	Usuario varchar(20),
	HoraEntrada text,
    HoraSalida text
);

/*SELECT * FROM nerivela.personal;
delete from nerivela.personal where idPersonal = 3;

SELECT COUNT(*) FROM nerivela.personal where usuario = 'Arkanoz' and password = 'digi3.0';
select * FROM nerivela.personal where usuario = 'Arkanoz' and password = 'digi3.0';*/