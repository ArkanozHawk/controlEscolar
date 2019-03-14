-- phpMyAdmin SQL Dump
-- version 4.1.14
-- http://www.phpmyadmin.net
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 12-03-2019 a las 01:35:26
-- Versión del servidor: 5.6.17
-- Versión de PHP: 5.5.12


SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Base de datos: `nerivela`
--
CREATE DATABASE IF NOT EXISTS `nerivela` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `nerivela`;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `alumno`
--

CREATE TABLE IF NOT EXISTS `alumno` (
  `idAlumno` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(20) NOT NULL,
  `ApellidoP` varchar(20) NOT NULL,
  `ApellidoM` varchar(20) NOT NULL,
  `calle` varchar(50) NOT NULL,
  `colonia` varchar(50) NOT NULL,
  `numExt` varchar(5) DEFAULT NULL,
  `cp` varchar(5) NOT NULL,
  `telEmer` varchar(10) NOT NULL,
  `Genero` varchar(9) NOT NULL,
  `lugNac` varchar(30) NOT NULL,
  `FechNac` date NOT NULL,
  `Alergias` varchar(250) DEFAULT NULL,
  `CURP` varchar(20) NOT NULL,
  `idPadres` int(11) NOT NULL,
  `idGrado` int(11) NOT NULL,
  PRIMARY KEY (`idAlumno`),
  UNIQUE KEY `CURP` (`CURP`),
  KEY `idPadres` (`idPadres`),
  KEY `idGrado` (`idGrado`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `bitacora`
--

CREATE TABLE IF NOT EXISTS `bitacora` (
  `idAcceso` int(11) NOT NULL AUTO_INCREMENT,
  `Usuario` varchar(20) DEFAULT NULL,
  `Nombre` varchar(20) DEFAULT NULL,
  `Apellido` varchar(20) DEFAULT NULL,
  `Fecha` varchar(20) DEFAULT NULL,
  `HoraEntrada` varchar(20) DEFAULT NULL,
  `HoraSalida` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`idAcceso`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `calificaciones`
--

CREATE TABLE IF NOT EXISTS `calificaciones` (
  `idCalificaciones` int(11) NOT NULL AUTO_INCREMENT,
  `CalificacionMen` decimal(2,2) NOT NULL,
  `CalificacionTri` decimal(2,2) NOT NULL,
  `Mes` varchar(20) NOT NULL,
  PRIMARY KEY (`idCalificaciones`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `grado`
--

CREATE TABLE IF NOT EXISTS `grado` (
  `idGrado` int(11) NOT NULL AUTO_INCREMENT,
  `grado` varchar(1) NOT NULL,
  `grupo` varchar(1) NOT NULL,
  `idMaestros` int(11) NOT NULL,
  PRIMARY KEY (`idGrado`),
  KEY `fk_idMaestros` (`idMaestros`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=7 ;

--
-- Volcado de datos para la tabla `grado`
--

INSERT INTO `grado` VALUES
(1, '1', 'A', 1),
(2, '2', 'A', 2),
(3, '3', 'A', 3),
(4, '4', 'A', 4),
(5, '5', 'A', 5),
(6, '6', 'A', 6);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `maestros`
--

CREATE TABLE IF NOT EXISTS `maestros` (
  `idMaestros` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(20) NOT NULL,
  `ApellidoP` varchar(20) NOT NULL,
  `ApellidoM` varchar(20) NOT NULL,
  `calle` varchar(50) NOT NULL,
  `colonia` varchar(50) NOT NULL,
  `numExt` varchar(5) DEFAULT NULL,
  `cp` varchar(5) NOT NULL,
  `telefono` varchar(10) DEFAULT NULL,
  `Celular` varchar(10) DEFAULT NULL,
  `Email` varchar(50) NOT NULL,
  `gradoEncargado` int(1) NOT NULL,
  `Profesion` varchar(50) NOT NULL,
  `FechNac` date NOT NULL,
  `RFC` varchar(18) NOT NULL,
  PRIMARY KEY (`idMaestros`),
  UNIQUE KEY `RFC` (`RFC`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=7 ;

--
-- Volcado de datos para la tabla `maestros`
--

INSERT INTO `maestros` VALUES
(1, 'José', 'Jacinto', 'Cuevas', 'Morteros', 'Marroquin', '70', '39640', '1587496', '744610125', 'jjCuevas@hotmail.com', 1, 'Lic. en Educación Primaria', '1972-07-30', 'JJCM720730TE'),
(2, 'Lucia', 'Figueroa', 'Pineda', 'SONORA', 'PROGRESO', '76', '39350', '1900893', '744763789', 'luciFigP43@outlock.com', 2, 'Lic. en Educación Primaria', '1967-03-25', 'LFPU670325'),
(3, 'Rafael', 'Ramírez', 'Castañeda', 'AQUILES SERDÁN', 'SANTA DOROTE', '49', '39970', '1479632', '744827307', 'RafaCast100@hotmail.com', 3, 'Lic. en Educación Primaria', '1977-04-11', 'RRFP770411QO'),
(4, 'Ana Lilia', 'Morales', 'Simon', 'AV. 20 DE NOVIEMBRE', 'CENTRO', '1024', '39300', '9856731', '744637219', 'Ana_lilia@gmail.com', 4, 'Lic. en Educación Primaria', '1971-01-03', 'ALMS710103BG'),
(5, 'Alberto Daniel', 'Fuentes', 'Dávila', 'IGNACIO MATIAS', 'MA. LUISA', '6', '39122', '1157496', '744732275', 'Fuentes-Albert@gmail.com', 5, 'Lic. en Educación Primaria', '1974-11-09', 'ADFD741109QW'),
(6, 'Fabiola', 'Torres', 'Quintero', 'AV INDEPENDENCIA', 'CENTRO', '545', '39450', '1140833', '744793122', 'fabi_tq90@live.com.mx', 6, 'Lic. en Educación Primaria', '1970-09-18', 'FTQH700918MB');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `materias`
--

CREATE TABLE IF NOT EXISTS `materias` (
  `idMaterias` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(50) NOT NULL,
  `idGrado` int(11) NOT NULL,
  PRIMARY KEY (`idMaterias`),
  KEY `fk_idGrado` (`idGrado`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=54 ;

--
-- Volcado de datos para la tabla `materias`
--

INSERT INTO `materias` VALUES
(1, 'Español', 1),
(2, 'Español', 2),
(3, 'Español', 3),
(4, 'Español', 4),
(5, 'Español', 5),
(6, 'Español', 6),
(7, 'Matematicas', 1),
(8, 'Matematicas', 2),
(9, 'Matematicas', 3),
(10, 'Matematicas', 4),
(11, 'Matematicas', 5),
(12, 'Matematicas', 6),
(13, 'Ingles', 1),
(14, 'Ingles', 2),
(15, 'Ingles', 3),
(16, 'Ingles', 4),
(17, 'Ingles', 5),
(18, 'Ingles', 6),
(19, 'Conocimiento del medio', 1),
(20, 'Conocimiento del Medio', 2),
(21, 'Ciencias Naturales', 3),
(22, 'Ciencias Naturales', 4),
(23, 'Ciencias Naturales', 5),
(24, 'Ciencias Naturales', 6),
(25, 'La entidad donde vivo', 3),
(26, 'Formación Cívica y Ética', 3),
(27, 'Formación Cívica y Ética', 4),
(28, 'Formación Cívica y Ética', 5),
(29, 'Formación Cívica y Ética', 6),
(30, 'Geografía', 4),
(31, 'Geografía', 5),
(32, 'Geografía', 6),
(33, 'Historia', 4),
(34, 'Historia', 5),
(35, 'Historia', 6),
(36, 'Artes', 1),
(37, 'Artes', 2),
(38, 'Artes', 3),
(39, 'Artes', 4),
(40, 'Artes', 5),
(41, 'Artes', 6),
(42, 'Educación Socioemocional', 1),
(43, 'Educación Socioemocional', 2),
(44, 'Educación Socioemocional', 3),
(45, 'Educación Socioemocional', 4),
(46, 'Educación Socioemocional', 5),
(47, 'Educación Socioemocional', 6),
(48, 'Educación Física', 1),
(49, 'Educación Física', 2),
(50, 'Educación Física', 3),
(51, 'Educación Física', 4),
(52, 'Educación Física', 5),
(53, 'Educación Física', 6);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `padres`
--

CREATE TABLE IF NOT EXISTS `padres` (
  `idPadres` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(20) NOT NULL,
  `ApellidoP` varchar(20) NOT NULL,
  `ApellidoM` varchar(20) NOT NULL,
  `lugTrabajo` varchar(30) NOT NULL,
  `Profesion` varchar(30) DEFAULT NULL,
  `telefono` varchar(10) DEFAULT NULL,
  `Celular` varchar(10) DEFAULT NULL,
  `Calle` varchar(50) NOT NULL,
  `Colonia` varchar(50) NOT NULL,
  `NumExt` varchar(5) DEFAULT NULL,
  `cp` varchar(5) NOT NULL,
  PRIMARY KEY (`idPadres`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `personal`
--

CREATE TABLE IF NOT EXISTS `personal` (
  `idUsuario` int(11) NOT NULL AUTO_INCREMENT,
  `nombre` varchar(20) NOT NULL,
  `ApellidoP` varchar(20) NOT NULL,
  `ApellidoM` varchar(20) NOT NULL,
  `calle` varchar(50) NOT NULL,
  `colonia` varchar(50) NOT NULL,
  `numExt` varchar(5) NOT NULL,
  `cp` varchar(5) NOT NULL,
  `telefono` varchar(10) DEFAULT NULL,
  `email` varchar(50) NOT NULL,
  `profesion` varchar(50) NOT NULL,
  `cargo` varchar(20) NOT NULL,
  `usuario` varchar(20) DEFAULT NULL,
  `password` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`idUsuario`),
  UNIQUE KEY `usuario` (`usuario`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `alumno`
--
ALTER TABLE `alumno`
  ADD CONSTRAINT `idPadres` FOREIGN KEY (`idPadres`) REFERENCES `padres` (`idPadres`),
  ADD CONSTRAINT `idGrado` FOREIGN KEY (`idGrado`) REFERENCES `grado` (`idGrado`);

--
-- Filtros para la tabla `grado`
--
ALTER TABLE `grado`
  ADD CONSTRAINT `fk_idMaestros` FOREIGN KEY (`idMaestros`) REFERENCES `maestros` (`idMaestros`);

--
-- Filtros para la tabla `materias`
--
ALTER TABLE `materias`
  ADD CONSTRAINT `fk_idGrado` FOREIGN KEY (`idGrado`) REFERENCES `grado` (`idGrado`) ON DELETE NO ACTION ON UPDATE NO ACTION;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
