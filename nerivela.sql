-- phpMyAdmin SQL Dump
-- version 4.1.14
-- http://www.phpmyadmin.net
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 09-04-2019 a las 10:42:57
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
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=92 ;

--
-- Volcado de datos para la tabla `alumno`
--

INSERT INTO `alumno` (`idAlumno`, `nombre`, `ApellidoP`, `ApellidoM`, `calle`, `colonia`, `numExt`, `cp`, `telEmer`, `Genero`, `lugNac`, `FechNac`, `Alergias`, `CURP`, `idPadres`, `idGrado`) VALUES
(1, ' JESUS LEONARDO ', 'AREVALO', 'CARDENAS', 'CAMPECHE', 'Vista Alegre', '34', '39855', '7441212567', 'Masculino', 'Acapulco de Juarez', '2010-04-29', 'Ninguna', 'AECJ090429HCHRRS01', 1, 4),
(2, ' PERLA CLARISSA', 'ARIAS', ' HERNANDEZ', 'MAZATLAN', 'Nueva Linea', '670', '39809', '7445690991', 'Femenino', 'Acapulco', '2010-11-01', 'Ninguna', 'AIHP091101MCHRRR03', 2, 4),
(3, 'CARLOS ', 'BARRERA ', 'VILLANUEVA', 'VENEZUELA', 'TUXTEPEC', '69', '39766', '7440201345', 'Masculino', 'Acapulco de Juarez', '2010-06-14', 'Pelicilina', 'BAVC090614HCHRLR04', 3, 4),
(4, ' JUAN MANUEL', 'CARRASCO ', 'MARTINEZ', 'JALISCO', 'CENTRO', '241', '39271', '7447658439', 'Masculino', 'CHILPANCINGO', '2010-04-21', 'NINGUNA', 'CAMJ090421HCHRRN05', 4, 4),
(5, 'RUBEN ANTONIO', 'CEBALLOS ', 'MARTINEZ', 'VILLA HERMOSA', 'LAS PALMAS', '90', '39872', '7442895372', 'Masculino', 'ACAPULCO DE JUAREZ', '2010-07-11', 'NINGUNA', 'CEMR090711HCHBRB00', 5, 4),
(6, 'EDDY FABIAN', 'CERA ', 'JUAREZ', 'POLACO', 'LOMAS DEL PEDREGAL ', '74', '39638', '7447814547', 'Masculino', 'ACAPULCO DE JUAREZ', '2010-03-15', 'CAMARONES', 'CEJE090315HCHRRD06', 6, 4),
(7, ' MARIA CRISTINA', ' CORRAL', 'BALDERRAMA', 'XINAMECA', 'LA PIRAGUA ', '1010', '39451', '7448912365', 'Femenino', 'CUIDAD DE MEXICO', '2010-11-27', 'NINGUNA', 'COBC091127MCHRLR08', 7, 4),
(8, 'ANA SOLEDAD', 'CORREA ', 'CONTRERAS', 'TEXCOCO', 'CENTRO', '310', '39850', '7448721691', 'Femenino', 'CUIDAD DE MEXICO', '2010-10-07', 'NINGUNA', 'COCA091007MDGRNN02', 8, 4),
(9, 'SERGIO ALEJANDRO', ' DELGADO ', 'MORLET', 'CRUZ GRANDE', 'LOS ANGELES ', '1447', '39854', '7449652131', 'Masculino', 'ACAPULCO DE JUAREZ', '2010-10-08', 'NINGUNA', 'DEMS091008HCHLXR04', 9, 4),
(10, 'JULIO CESAR', 'ENRIQUEZ ', 'GAYTAN', 'CALLE 5', 'CENTRO', '545', '39874', '7448526984', 'Masculino', 'AGUASCALIENTES', '2010-08-06', 'NINGUNA', 'EIGJ090806HCHNYL07', 10, 4),
(11, 'JORGE JOAN', 'GARCIA ', 'MIMBELA', 'NUEVA VISTA', 'CENTRO', '14', '39154', '7449652314', 'Masculino', 'ACAPULCO DE JUAREZ', '2010-03-14', 'NINGUNA', 'GAMJ090314HCHRMR05', 11, 4),
(12, ' PERLA LILIANA', 'GONZALEZ ', 'LINARES', 'SANTA CRUZ', 'CENTRO', '145', '39147', '7449215503', 'Femenino', 'ACAPULCO DE JUAREZ', '2010-07-29', 'NINGUNA', 'GOLP090729MCHNNR03', 12, 4),
(13, 'DANIEL', 'GONZALEZ', ' VILLA', 'ROMA', 'COSTA GRANDE', '367', '39900', '7446321057', 'Masculino', 'ACAPULCO DE JUAREZ', '2010-06-10', 'PARACETAMOL', 'GOVD090610HCHNLN02', 13, 4),
(14, ' HECTOR JESUS', 'GRADO', 'ARMENDARIZ', 'NORIEGA', 'BELLAVISTA', '90', '39781', '5587452152', 'Masculino', 'PUEBLA', '2010-09-26', 'NINGUNA', 'GAAH090926HCHRRC04', 14, 4),
(15, 'CONCEPCION', 'VAZQUEZ ', 'LOPEZ', 'VELADEZ', 'CENTRO', '124', '36952', '7441602539', 'Femenino', 'ACAPULCO DE JUAREZ', '2010-12-08', 'NINGUNA', 'VALC091208MGRZPN02', 15, 4),
(16, 'LUIS EDUARDO', 'HERRERA', 'VILLARREAL ', 'POLAR', 'ROMA', '87', '39880', '7445602948', 'Masculino', 'ACAPULCO DE JUAREZ', '2011-02-24', 'NINGUNA', 'HEVL080224HCHRLS09', 16, 3),
(17, 'ERICK FRANCISCO ', 'HOLGUIN ', 'AGUIRRE', 'CALLE 3', 'TEMASCAL', '1093', '39084', '7449652145', 'Masculino', 'ACAPULCO', '2011-02-18', 'NINGUNA', 'HOAE110218HCHLGR02', 17, 3),
(18, 'JOSE', 'JALIFE ', 'AGUIRRE ', 'ZAPOPAN', 'OLIVA', '523', '39874', '5596876520', 'Masculino', 'ACAPULCO DE JUAREZ', '2011-10-04', 'NINGUNA', 'JAAJ111004HCLLGS07', 18, 3),
(19, 'MARIA DE LOS ANGELES', 'OLIVARES ', 'AYALA', 'CALLE 6', 'CENTRO', '53', '39874', '7442589514', 'Femenino', 'ACAPULCO DE JUAREZ', '2011-09-07', 'NINGUNA', 'OIAA110907MCHLYN00', 19, 3),
(20, 'GUILLERMO', 'OLIVAS ', 'TENA', 'LA CIMA', 'TUXTEPEC', '11', '39111', '5578301012', 'Masculino', 'MORELOS', '2011-11-12', 'NINGUNA', 'OITG111112HCHLNL26', 20, 3),
(21, 'AARON', ' OLIVAS ', 'VEGA', 'JACARANDAS', 'LOS ANGELES', '143', '39010', '7448731698', 'Masculino', 'ACAPULCO DE JUAREZ', '2011-02-10', 'NINGUNA', 'OIVA110210HCHLGR05', 21, 3),
(22, 'ENRIQUE', 'PAYAN', ' VILLANUEVA ', 'EL ROBLE', 'VELADEZ', '02', '39001', '7447040621', 'Masculino', 'ACAPULCO DE JUAREZ', '2011-07-31', 'NINGUNA', 'PAVE110731HCHYLN05', 22, 3),
(23, 'JOSE RUBEN', 'RAMIREZ ', 'RODRIGUEZ', 'RIOS', 'POLAR', '22', '39040', '7443635102', 'Masculino', 'ACAPULCO DE JUAREZ', '2011-06-21', 'NINGUNA', 'RARR110621HCHMDB00', 23, 3),
(24, 'ADRIAN', 'SANTIESTEBAN ', 'ONTIVEROS', 'LOMA ALTA', 'ALEGRIA', '034', '39843', '7441234569', 'Masculino', 'ACAPULCO', '2011-11-21', 'NINGUNA', 'SAOA111121HCHNND03', 24, 3),
(25, 'JORGE', 'VILLARREAL ', 'CHACON', 'PEDREGAL', 'GALLARDO', '73', '39600', '5574896581', 'Masculino', 'CHILPANCINGO', '2011-05-13', 'NINGUNA', 'VICJ110513HCHLHR04', 25, 3),
(26, 'CARLA CELIA', 'CORONA', 'RAMIREZ', 'GRIJALBA', 'LUNA NUEVA', '21', '39209', '7446532011', 'Femenino', 'ACAPULCO DE JUAREZ', '2011-07-11', 'NINGUNA', 'CORC110711MMCRMR05', 26, 3),
(27, 'TANIA', 'CORTES', 'SEGURA', 'CAMPECHE', 'FABRICA', '38', '39481', '7441239871', 'Femenino', 'CULIACAN', '2011-03-14', 'NINGUNA', 'COST110314MDFRGN04', 27, 3),
(28, 'DANIELA EMILIANA', 'DARCIA', 'MARTINEZ', 'VALLE DEL SOL', 'SONORA', '623', '39844', '7444700212', 'Femenino', 'ACAPULCO DE JUAREZ', '2011-07-21', 'NINGUNA', 'GAMD110721MDFRRN09', 28, 3),
(29, 'ADRIANA', 'DIAZ', 'CONTRERAS', 'LUZ', 'JAGUAR', '278', '39146', '7559686214', 'Femenino', 'CANCUN', '2011-09-23', 'POLVO', 'DICA110923MDFZND01', 29, 3),
(30, 'HILDA', 'DIAZ ', 'RAMIREZ', 'EMILIANO ZAPATA', 'ZACATECAS', '2902', '39520', '5589367101', 'Femenino', 'ACAPULCO DE JUAREZ', '2011-05-07', 'NINGUNA', 'DIRH110507MDFZML00', 30, 3),
(31, 'JUDITH ANGELICA', 'DOMINGUEZ', 'ALONSO', 'SINALOA', 'SAN JUAN', '400', '39875', '7448956221', 'Femenino', 'ACAPULCO DE JUAREZ', '2012-03-10', 'NINGUNA', 'DOAJ120310MDFMLD00', 31, 2),
(32, 'ARTURO', 'BACALLAO', 'SANTIAGO', 'OAXACA', 'POLAR', '64', '39409', '7441019102', 'Masculino', 'ACAPULCO DE JUAREZ', '2012-11-13', 'NINGUNA', 'BASA121113HDFCNR05', 32, 2),
(33, 'DELIA', 'BARCENAS ', 'OLVERA ', 'JUAREZ', 'VISTA ALEGRE', '01', '39056', '5567392833', 'Femenino', 'PUEBLA', '2012-08-22', 'PESCADO', 'BAOD120822MDFRLL00', 33, 2),
(34, 'ROSA MARIA', 'CAMPUZANO', 'LUNA', 'PEDREGAL', 'LOMAS', '26', '39057', '7448596526', 'Femenino', 'ACAPULCO DE JUAREZ', '2012-05-29', 'NINGUNA', 'CALR120529MQTMNS05', 34, 2),
(35, 'GABRIELA YRAIS', 'CASANOVA', 'FUENTES', 'BRISAS DEL MAR', 'CAMPECHE', '35', '39050', '7448789210', 'Femenino', 'ACAPULCO DE JUAREZ', '2012-06-05', 'NINGUNA', 'CAFG120605MDFSNB01', 35, 2),
(36, 'NOEMI', 'ESCALONA', 'MONTES DE OCA', 'CALLE 1', 'CUAUHTEMOC', '99', '39102', '7448792102', 'Femenino', 'GUADALAJARA', '2012-01-26', 'NINGUNA', 'EAMN120126MDFSNM03', 36, 2),
(37, 'ESMERALDA', 'FERRER ', 'JUAREZ', 'SINFONIA', 'ARROYO SECO', '670', '39850', '7448210392', 'Femenino', 'ACAPULCO DE JUAREZ', '2012-01-12', 'NINGUNA', 'FEJE120112MMCRRS02', 37, 2),
(38, 'OSVALDO', 'FLORES', 'CRUZ', 'FUGAZ', 'CASATILLO', '80', '39874', '5531193732', 'Masculino', 'SAN LUIS POTOSI', '2012-05-03', 'NINGUNA', 'FOCO120503HDFLRS03', 38, 2),
(39, 'YAZMIN', 'FLORES', 'JUAREZ', 'LIMA', 'BRISAS DEL MAR', '453', '39875', '7448736552', 'Femenino', 'ACAPULCO DE JUAREZ', '2012-11-10', 'NINGUNA', 'FOJY121110MDFLRZO1', 39, 2),
(40, 'MARIA DEL CONSUELO', 'GALINDO ', 'GONZALEZ', 'PERA', 'MIRAMAR', '865', '39087', '7444112411', 'Femenino', 'ACAPULCO DE JUAREZ', '2012-02-24', 'PESCADO', 'GAGC120224MDFLNN01', 40, 2),
(41, 'JONATHAN ISSAC', 'LOPEZ', 'CANO', 'ALIANZA', 'VERACUZ', '560', '39044', '5572562044', 'Masculino', 'CUIDAD DE MEXICO', '2012-09-21', 'NINGUNA', 'LOCJ120921HCHPNN09', 41, 2),
(42, 'GUILLERMO', 'OLIVAS ', 'TENA', 'TORRES', 'BUROCRATA', '673', '39086', '7446521035', 'Masculino', 'ACAPULCO DE JUAREZ', '2012-11-12', 'CAMARON', 'OITG121112HCHLNL26', 42, 2),
(43, 'ADRIAN', 'SANTIESTEBAN', 'ONTIVEROS', 'BARRAN', 'LOMA', '564', '39034', '7445149622', 'Masculino', 'ACAPULCO DE JUAREZ', '2019-03-23', 'POLVO', 'SAOA791121HCHNND03', 43, 2),
(44, 'JORGE', 'VILLARREAL ', 'CHACON ', 'LIMA', 'CAUDILLO', '823', '39505', '7444968523', 'Masculino', 'ACAPULCO DE JUAREZ', '2012-05-13', 'CHOCOLATE', 'VICJ120513HCHLHR04', 44, 2),
(45, 'FRANCISCO ALFREDO', 'VIZCARRA', 'ROBLES', 'CHARCO', 'LAS BRISAS', '309', '39887', '7448951241', 'Masculino', 'CUIDAD DE MEXICO', '2012-06-29', 'NINGUNA', 'VIRF120629HSLZBR06', 45, 2),
(46, 'MARICELA', 'GONZALEZ ', 'ALVAREZ ', 'JACARANDA', 'COHETEROS', '455', '39869', '7448987545', 'Femenino', 'ACAPULCO DE JUAREZ', '2013-05-11', 'NINGUNA', 'GOAM130511MDFNLR07', 46, 1),
(47, 'ARTURO ALEJANDRO', 'GIL', 'MIRANDA', 'CALLE 4', 'COSTA AZUL', '087', '39601', '4778895148', 'Masculino', 'PUEBLA', '2013-08-03', 'NINGUNA', 'GIMA130803HDFLRR09', 47, 1),
(48, 'DIANA PATRICIA', 'HERNANDEZ', 'AMARO ', 'CRIL', 'BRAVA', '985', '39711', '5587445681', 'Femenino', 'CUIDAD DE MEXICO', '2013-03-17', 'NINGUNA', 'HEAD130317MDFRMN01', 48, 1),
(49, 'DIANA KARINA', 'HERNANDEZ', 'MORALES ', 'NUEVA VISTA', 'LEON', '476', '39402', '7449585135', 'Femenino', 'MORELOS', '2013-12-12', 'NINGUNA', 'HEMD131212MDFRRN06', 49, 1),
(50, 'JULIO CESAR', 'HERNANDEZ', 'PENA', 'GOMEZ', 'GUZMAN', '111', '39111', '7449895325', 'Masculino', 'ACAPULCO DE JUAREZ', '2013-06-09', 'NINGUNA', 'HEPJ130609HGRRXL08', 50, 1),
(51, 'NANCY', 'ISIDRO', 'GUTIERREZ ', 'FIGURA', 'DEL VALLE', '92', '39871', '7448845105', 'Femenino', 'ACAPULCO DE JUAREZ', '2013-03-24', 'NINGUNA', 'IIGN130324MMCSTN05', 51, 1),
(52, 'GUADALUPE', 'JAIME', 'GONZALEZ', 'MENA', 'LEON', '854', '39548', '7448510250', 'Femenino', 'ACAPULCO DE JUAREZ', '2013-07-02', 'NINGUNA', 'JAGG130702MDFMND01', 52, 1),
(53, 'PAMELA SHARON', 'JUAREZ ', 'SOTO', 'FABRICA', 'VISTA ALEGRE', '342', '39564', '7443625547', 'Femenino', 'ACAPULCO DE JUAREZ', '2013-08-20', 'PULPO', 'JUSP130820MDFRTM02', 53, 1),
(54, 'MONICA CRISTINA', 'KAPLAN', 'CASTELLANOS', 'SANTA CRUZ', 'NOVEDADES', '32', '39658', '7449685550', 'Femenino', 'ACAPULCO DE JUAREZ', '2013-10-27', 'NINGUNA', 'KACM131027MDFPSN07', 54, 1),
(55, 'BELEM', 'LAGUNA', 'HERNANDEZ', 'VELA', 'LIMA', '432', '39754', '7448273014', 'Femenino', 'ACAPULCO DE JUAREZ', '2013-05-10', 'NINGUNA', 'LAHB130510MDFGRL00', 55, 1),
(56, 'FLAVIO', 'LEON ', 'CONDE ', 'RIVERA', 'DEL VALLE', '231', '39584', '7448952515', 'Masculino', 'ACAPULCO DE JUAREZ', '2013-05-07', 'SALMON', 'LECF130507HTLNNL02', 56, 1),
(57, 'KARLA LISSET', 'LOPEZ ', 'ESTRADA', 'GRIL', 'TECONCHE', '54', '39000', '7443256102', 'Femenino', 'VERACRUZ', '2013-03-30', 'NINGUNA', 'LOEK130330MDFPSR07', 57, 1),
(58, 'MARCO ANTONIO', 'LUCAS ', 'GONZALEZ ', 'LA LAJA', 'MATA', '51', '39555', '4473258552', 'Masculino', 'IGUALA', '2013-12-08', 'NINGUNA', 'LUGM131208HMCCNR09', 58, 1),
(59, 'ALFONSO', 'MONDRAGON ', 'ALMERIA ', 'VILLA HERMOSA', 'MOCTEZUMA', '453', '39871', '7449825202', 'Masculino', 'ACAPULCO DE JUAREZ', '2013-08-05', 'NINGUNA', 'MOAA130805HDFNLL04', 59, 1),
(60, 'JULIO CESAR', 'PEREZ', 'BOJAS', 'SONORA', 'COLOSO', '2155', '39502', '7449185211', 'Masculino', 'ACAPULCO DE JUAREZ', '2013-05-13', 'PELICILINA', 'PEBJ130513HDFRRL07', 60, 1),
(61, 'LETICIA', 'MUNOZ ', 'CARRANZA ', 'SINALOA', 'MIRADOR', '1123', '39611', '7449685212', 'Femenino', 'ACAPULCO DE JUAREZ', '2008-02-25', 'NINGUNA', 'MUCL080225MMCXRT05', 61, 6),
(62, 'LAURA', 'NAVARRETE', 'DIAZ ', 'GALVANEZ', 'EL TANQUE', '65', '39685', '7449520202', 'Femenino', 'ACAPULCO DE JUAREZ', '2008-11-02', 'NINGUNA', 'NADL081102MDFVZR01', 62, 6),
(63, 'JESSICA', 'ARELLANO ', 'OCHOA', 'ERA', 'EL PORVENIR', '56', '39650', '7443625114', 'Femenino', 'ACAPULCO DE JUAREZ', '2008-07-24', 'NINGUNA', 'AEOJ080724MDFRCS08', 63, 6),
(64, 'VICTOR KRISTIAN', 'BENITEZ ', 'ALARCON', 'SOR JUANA', 'VAZQUEZ', '685', '39474', '5574179653', 'Masculino', 'CUIDAD DE MEXICO', '2008-03-26', 'NINGUNA', 'BEAV080326HDFNLC01', 64, 6),
(65, 'KATIA BERENICE', 'DIAZ', 'RODRIGUEZ ', 'PASCUARO', 'MAZATLAN', '341', '39020', '7446986321', 'Femenino', 'ACAPULCO DE JUAREZ', '2008-12-02', 'NINGUNA', 'DIRK081202MDFZDT09', 65, 6),
(66, 'LILIANA', 'DIAZ DE LEON ', 'CRUZ ', 'DIAZ', 'LAS GRANJAS', '45', '39078', '7448135321', 'Femenino', 'ACAPULCO DE JUAREZ', '2008-12-31', 'PARACETAMOL', 'DICL081231MDFZRL08', 66, 6),
(67, 'IVONE MARITZA', 'ZEPEDA', 'FERNANDEZ ', 'CALLE 2', 'VICTORIA', '21', '39665', '4773896885', 'Femenino', 'LEON', '2008-05-03', 'POLVO', 'ZEFI080503MBCPRV07', 67, 6),
(68, 'CESAR', 'VENTURA ', 'MARTINEZ ', 'LOS TIGRES', 'FLAMINGOS', '2577', '39325', '7445634564', 'Masculino', 'MORELOS', '2008-04-16', 'NINGUNA', 'VEMC080416HMCNRS05', 68, 6),
(69, 'ANA KAREN', 'MARTINEZ ', 'ARREOLA ', 'SALOMON', 'VICTORIA', '78', '39887', '7440253452', 'Femenino', 'IGUALA', '2008-03-02', 'NINGUNA', 'MAAA080302MDFRRN03', 69, 6),
(70, 'FRANCISCO JAVIER', 'REYES', 'CARRILLO ', 'HIELO', 'SANTA ELENA', '234', '39545', '7440287824', 'Masculino', 'ACAPULCO DE JUAREZ', '2019-03-23', 'NINGUNA', 'RECF080526HMCYRT00', 70, 6),
(71, 'OMAR', 'BARAJAS ', 'CORTES ', 'SOLAR', 'DIAZ', '15', '39772', '7442356564', 'Masculino', 'MORELOS', '2008-11-26', 'NINGUNA', 'BACO081126HGRRRM02', 71, 6),
(72, 'ELENA ANAI', 'ROA', 'SANCHEZ ', 'PEDREGOSO', 'HORNOS', '574', '39544', '7443686832', 'Femenino', 'ACAPULCO DE JUAREZ', '2008-06-15', 'NINGUNA', 'ROSE080615MMSXNL00', 72, 6),
(73, 'JESUS ALBERTO', 'RUANO ', 'VELAZQUEZ', 'PIEDRA', 'HUERTA', '423', '39245', '7445465561', 'Masculino', 'CUIDAD DE MEXICO', '2008-04-20', 'NINGUNA', 'RUVJ080420HDFNLS06', 73, 6),
(74, 'LUIS MANUEL', 'HERNANDEZ ', 'PEREZ', 'LOMA', 'HOSPITAL', '524', '39545', '7442534448', 'Masculino', 'ACAPULCO DE JUAREZ', '2008-01-18', 'PELICILINA', 'HEPL080118HGRRRS08', 74, 6),
(75, 'JESUS', 'ADAME', 'GODINEZ ', 'VISTA LIBRE', 'VENEZUELA', '49', '39787', '7448958611', 'Masculino', 'ACAPULCO DE JUAREZ', '2008-12-24', 'POLVO', 'AAGJ081224HGRDDS04', 75, 6),
(76, 'CLAUDIA LIZ', 'RUIZ ', 'FRAGA ', 'WEN', 'JACARANDAS', '56', '39652', '7449565396', 'Femenino', 'ACAPULCO DE JUAREZ', '2009-03-08', 'NINGUNA', 'RUFC090308MDFZRL09', 76, 5),
(77, 'VALERIA MAGALY', 'SANCHEZ ', 'AVILA ', 'URBANA', 'ORTIZ', '762', '39787', '7445652535', 'Femenino', 'ACAPULCO DE JUAREZ', '2009-07-15', 'NINGUNA', 'SAAV090715MDFNVL06', 77, 5),
(78, 'SILVIA', 'SOLANO ', 'OLIVARES ', 'ROMAN', 'PEDREGOSO', '463', '39006', '7446596568', 'Femenino', 'ACAPULCO DE JUAREZ', '2009-06-02', 'NINGUNA', 'SOOS090602MDFLLL00', 78, 5),
(79, 'NAYELI', 'PAZ ', 'NAVA ', 'TERRAZAS', 'ROMA', '0982', '39021', '7449853263', 'Femenino', 'CUIDAD DE MEXICO', '2009-12-30', 'NINGUNA', 'PANN091230MDFZVY00', 79, 5),
(80, 'HILDA REBECA', 'PIGEONUTT ', 'DIAZ ', 'PARRA', 'LUNA DEL MAR', '39', '39775', '7446886552', 'Femenino', 'OAXACA', '2009-11-16', 'NINGUNA', 'PIDH091116MDFGZL02', 80, 5),
(81, 'EDNA LAURA', 'PORTILLO', 'GONZALEZ ', 'ALANZA', 'PALOMARES', '26', '39784', '7446589653', 'Femenino', 'MORELIA', '2009-08-03', 'NINGUNA', 'POGE090803MPLRND00', 81, 5),
(82, 'XOCHITL ANAID', 'RAMIREZ', 'MEJIA ', 'FERNANDEZ', 'MATAMOROS', '80', '39587', '7445868435', 'Femenino', 'ACAPULCO DE JUAREZ', '2009-08-07', 'NINGUNA', 'RAMX090807MDFMJC02', 82, 5),
(83, 'MONSERRAT', 'MORALES ', 'LEON ', 'SINALOA', 'LOMA ALTA', '50', '39995', '7446924484', 'Femenino', 'ACAPULCO DE JUAREZ', '2009-10-12', 'NINGUNA', 'MOLM091012MDFRNN09', 83, 5),
(84, 'JESUS', 'UGALDE', 'RIVERA ', 'MAR DEL CARIBER', 'SALAMANCA', '48', '39741', '5589787640', 'Masculino', 'CIUDAD DE MEXICO', '2009-03-17', 'POLVO', 'UARJ090317HMCGVS07', 84, 5),
(85, 'LUIS ARMANDO', 'SEVILLA ', 'NAVA ', 'PERA', 'NUEVA ZONA', '360', '39101', '7448545640', 'Masculino', 'ACAPULCO DE JUAREZ', '2009-05-22', 'NINGUNA', 'SENL090522HGRVVS02', 85, 5),
(86, 'GUILLERMO', 'RIVERA ', 'BARRERA ', 'ZONA', 'LA PAROTA', '549', '39505', '7449829685', 'Masculino', 'IGUALA', '2009-12-14', 'PELICILINA', 'RIBG091214HGRVRL10', 86, 5),
(87, 'MIGUEL ANGEL', 'CABRERA', 'ADAME ', 'DAMIAN', 'VISTA ALEGRE', '60', '39874', '7446465454', 'Masculino', 'ACAPULCO DE JUAREZ', '2009-11-17', 'PARACETAMOL', 'CAAM091117HGRBDG13', 87, 5),
(88, 'MILTON EMANUEL', ' ARROYO', 'AGUILAR ', 'SONORA', 'LA POZA', '492', '39084', '7449355799', 'Masculino', 'ACAPULCO DE JUAREZ', '2009-06-20', 'NINGUNA', 'AOAM090620HGRRGL04', 88, 5),
(89, 'LUIS RANGEL', ' DE LA O', 'ARROYO', 'LOS ALTOS', 'LOS TIGRES', '231', '39284', '7446298168', 'Masculino', 'ACAPULCO DE JUAREZ', '2009-05-11', 'NINGUNA', 'OXAL090511HGRXRS05', 89, 5),
(90, 'RODRIGO', 'DONJUAN ', 'MORA ', 'MATA', 'LA MIRA', '693', '39657', '7446915935', 'Masculino', 'VERACRUZ', '2009-11-10', 'NINGUNA', 'DOMR091104HGRNRD06', 90, 5),
(91, 'MARTHA', 'TERAN', 'LARA', '52', 'CARABALI', '52', '39560', '7444383583', 'Femenino', 'ACAPULCO DE JUAREZ', '2012-08-03', 'PELICILINA', 'TEML030813MGRRRR09', 91, 1);

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
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=191 ;

--
-- Volcado de datos para la tabla `bitacora`
--

INSERT INTO `bitacora` (`idAcceso`, `Usuario`, `Nombre`, `Apellido`, `Fecha`, `HoraEntrada`, `HoraSalida`) VALUES
(1, 'lizethepetete', 'MArtha', 'Teran', '12/03/2019', '09:50:16', NULL),
(2, 'lizethepetete', 'MArtha', 'Teran', '12/03/2019', '09:56:46', NULL),
(3, 'lizethepetete', 'MArtha', 'Teran', '12/03/2019', '10:04:21', '12/03/2019 10:05:11 '),
(4, 'lizethepetete', 'MArtha', 'Teran', '14/03/2019', '07:23:51', NULL),
(5, 'lizethepetete', 'MArtha', 'Teran', '14/03/2019', '09:18:41', '14/03/2019 09:25:15 '),
(6, 'lizethepetete', 'MArtha', 'Teran', '14/03/2019', '09:35:09', '14/03/2019 09:37:21 '),
(7, 'lizethepetete', 'MArtha', 'Teran', '14/03/2019', '09:43:09', '14/03/2019 09:43:15 '),
(8, 'lizethepetete', 'MArtha', 'Teran', '21/03/2019', '09:53:44', '22/03/2019 04:56:25 '),
(9, 'lizethepetete', 'MArtha', 'Teran', '22/03/2019', '07:17:10', NULL),
(10, 'lizethepetete', 'MArtha', 'Teran', '22/03/2019', '07:19:46', '22/03/2019 09:00:23 '),
(11, 'lizethepetete', 'MArtha', 'Teran', '22/03/2019', '06:53:33', NULL),
(12, 'lizethepetete', 'MArtha', 'Teran', '23/03/2019', '04:19:45', '23/03/2019 07:31:53 '),
(13, 'lizethepetete', 'MArtha', 'Teran', '23/03/2019', '07:58:38', '24/03/2019 01:39:11 '),
(14, 'lizethepetete', 'Martha Lizeth', 'Teran', '31/03/2019', '04:38:39', '31/03/2019 04:45:24 '),
(15, 'lizethepetete', 'Martha Lizeth', 'Teran', '31/03/2019', '04:50:09', '31/03/2019 04:55:50 '),
(16, 'lizethepetete', 'Martha Lizeth', 'Teran', '31/03/2019', '05:01:48', '31/03/2019 05:02:36 '),
(17, 'lizethepetete', 'Martha Lizeth', 'Teran', '31/03/2019', '05:04:09', NULL),
(18, 'lizethepetete', 'Martha Lizeth', 'Teran', '31/03/2019', '05:06:45', NULL),
(19, 'lizethepetete', 'Martha Lizeth', 'Teran', '31/03/2019', '05:09:41', NULL),
(20, 'lizethepetete', 'Martha Lizeth', 'Teran', '31/03/2019', '05:24:35', NULL),
(21, 'lizethepetete', 'Martha Lizeth', 'Teran', '31/03/2019', '05:31:07', '31/03/2019 05:35:31 '),
(22, 'lizethepetete', 'Martha Lizeth', 'Teran', '31/03/2019', '05:37:42', '31/03/2019 05:44:54 '),
(23, 'lizethepetete', 'Martha Lizeth', 'Teran', '31/03/2019', '06:00:13', '31/03/2019 06:06:18 '),
(24, 'lizethepetete', 'Martha Lizeth', 'Teran', '31/03/2019', '06:08:19', '31/03/2019 06:09:40 '),
(25, 'lizethepetete', 'Martha Lizeth', 'Teran', '31/03/2019', '06:49:46', NULL),
(26, 'lizethepetete', 'Martha Lizeth', 'Teran', '31/03/2019', '06:52:32', NULL),
(27, 'lizethepetete', 'Martha Lizeth', 'Teran', '31/03/2019', '06:55:38', NULL),
(28, 'lizethepetete', 'Martha Lizeth', 'Teran', '31/03/2019', '07:00:39', NULL),
(29, 'lizethepetete', 'Martha Lizeth', 'Teran', '31/03/2019', '07:01:32', NULL),
(30, 'lizethepetete', 'Martha Lizeth', 'Teran', '31/03/2019', '07:08:31', NULL),
(31, 'lizethepetete', 'Martha Lizeth', 'Teran', '31/03/2019', '07:11:34', NULL),
(32, 'lizethepetete', 'Martha Lizeth', 'Teran', '31/03/2019', '07:12:15', NULL),
(33, 'lizethepetete', 'Martha Lizeth', 'Teran', '31/03/2019', '09:49:55', NULL),
(34, 'lizethepetete', 'Martha Lizeth', 'Teran', '31/03/2019', '11:07:06', '31/03/2019 11:08:18 '),
(35, 'lizethepetete', 'Martha Lizeth', 'Teran', '31/03/2019', '11:10:18', '31/03/2019 11:10:41 '),
(36, 'lizethepetete', 'Martha Lizeth', 'Teran', '02/04/2019', '12:08:54', NULL),
(37, 'lizethepetete', 'Martha Lizeth', 'Teran', '02/04/2019', '07:46:17', NULL),
(38, 'lizethepetete', 'Martha Lizeth', 'Teran', '02/04/2019', '07:50:18', '02/04/2019 07:52:01 '),
(39, 'lizethepetete', 'Martha Lizeth', 'Teran', '02/04/2019', '07:54:01', '02/04/2019 07:54:26 '),
(40, 'lizethepetete', 'Martha Lizeth', 'Teran', '02/04/2019', '07:59:31', NULL),
(41, 'lizethepetete', 'Martha Lizeth', 'Teran', '02/04/2019', '08:00:03', NULL),
(42, 'lizethepetete', 'Martha Lizeth', 'Teran', '02/04/2019', '09:20:17', '02/04/2019 09:21:34 '),
(43, 'lizethepetete', 'Martha Lizeth', 'Teran', '02/04/2019', '09:24:07', '02/04/2019 09:24:48 '),
(44, 'lizethepetete', 'Martha Lizeth', 'Teran', '02/04/2019', '09:25:22', '02/04/2019 09:25:35 '),
(45, 'lizethepetete', 'Martha Lizeth', 'Teran', '02/04/2019', '09:28:12', '02/04/2019 09:28:24 '),
(46, 'lizethepetete', 'Martha Lizeth', 'Teran', '02/04/2019', '09:29:42', '02/04/2019 09:30:04 '),
(47, 'lizethepetete', 'Martha Lizeth', 'Teran', '02/04/2019', '09:31:22', '02/04/2019 09:31:39 '),
(48, 'lizethepetete', 'Martha Lizeth', 'Teran', '02/04/2019', '09:33:23', '02/04/2019 09:33:38 '),
(49, 'lizethepetete', 'Martha Lizeth', 'Teran', '02/04/2019', '09:34:30', '02/04/2019 09:34:52 '),
(50, 'lizethepetete', 'Martha Lizeth', 'Teran', '02/04/2019', '09:35:43', '02/04/2019 09:36:28 '),
(51, 'lizethepetete', 'Martha Lizeth', 'Teran', '02/04/2019', '09:38:08', '02/04/2019 09:38:28 '),
(52, 'lizethepetete', 'Martha Lizeth', 'Teran', '03/04/2019', '06:00:38', '03/04/2019 06:03:55 '),
(53, 'lizethepetete', 'Martha Lizeth', 'Teran', '03/04/2019', '06:32:21', '03/04/2019 06:34:28 '),
(54, 'lizethepetete', 'Martha Lizeth', 'Teran', '03/04/2019', '07:39:45', '03/04/2019 07:51:39 '),
(55, 'lizethepetete', 'Martha Lizeth', 'Teran', '03/04/2019', '08:23:17', '03/04/2019 08:23:43 '),
(56, 'lizethepetete', 'Martha Lizeth', 'Teran', '03/04/2019', '08:25:06', '03/04/2019 08:25:30 '),
(57, 'lizethepetete', 'Martha Lizeth', 'Teran', '03/04/2019', '08:27:26', '03/04/2019 08:27:53 '),
(58, 'lizethepetete', 'Martha Lizeth', 'Teran', '03/04/2019', '08:54:59', '03/04/2019 08:55:45 '),
(59, 'lizethepetete', 'Martha Lizeth', 'Teran', '03/04/2019', '09:00:03', NULL),
(60, 'lizethepetete', 'Martha Lizeth', 'Teran', '03/04/2019', '09:04:04', NULL),
(61, 'lizethepetete', 'Martha Lizeth', 'Teran', '03/04/2019', '09:05:01', NULL),
(62, 'lizethepetete', 'Martha Lizeth', 'Teran', '03/04/2019', '09:05:47', NULL),
(63, 'lizethepetete', 'Martha Lizeth', 'Teran', '03/04/2019', '09:07:36', '03/04/2019 09:12:15 '),
(64, 'lizethepetete', 'Martha Lizeth', 'Teran', '03/04/2019', '09:35:04', '03/04/2019 09:35:23 '),
(65, 'lizethepetete', 'Martha Lizeth', 'Teran', '03/04/2019', '09:48:34', NULL),
(66, 'lizethepetete', 'Martha Lizeth', 'Teran', '03/04/2019', '09:59:32', NULL),
(67, 'lizethepetete', 'Martha Lizeth', 'Teran', '03/04/2019', '10:03:52', NULL),
(68, 'lizethepetete', 'Martha Lizeth', 'Teran', '03/04/2019', '10:05:42', '03/04/2019 10:06:20 '),
(69, 'lizethepetete', 'Martha Lizeth', 'Teran', '03/04/2019', '10:10:10', '03/04/2019 10:11:53 '),
(70, 'lizethepetete', 'Martha Lizeth', 'Teran', '03/04/2019', '10:25:09', '03/04/2019 10:25:38 '),
(71, 'lizethepetete', 'Martha Lizeth', 'Teran', '03/04/2019', '11:23:06', '03/04/2019 11:23:46 '),
(72, 'lizethepetete', 'Martha Lizeth', 'Teran', '03/04/2019', '11:24:54', '03/04/2019 11:25:06 '),
(73, 'lizethepetete', 'Martha Lizeth', 'Teran', '04/04/2019', '01:03:30', NULL),
(74, 'lizethepetete', 'Martha Lizeth', 'Teran', '04/04/2019', '01:08:24', '04/04/2019 01:08:38 '),
(75, 'lizethepetete', 'Martha Lizeth', 'Teran', '04/04/2019', '06:27:42', '04/04/2019 06:29:05 '),
(76, 'lizethepetete', 'Martha Lizeth', 'Teran', '04/04/2019', '07:16:16', '04/04/2019 07:17:33 '),
(77, 'lizethepetete', 'Martha Lizeth', 'Teran', '04/04/2019', '07:27:36', '04/04/2019 07:27:48 '),
(78, 'lizethepetete', 'Martha Lizeth', 'Teran', '04/04/2019', '07:40:25', '04/04/2019 07:41:55 '),
(79, 'lizethepetete', 'Martha Lizeth', 'Teran', '04/04/2019', '07:43:16', '04/04/2019 07:43:30 '),
(80, 'lizethepetete', 'Martha Lizeth', 'Teran', '04/04/2019', '07:44:19', '04/04/2019 07:44:29 '),
(81, 'lizethepetete', 'Martha Lizeth', 'Teran', '04/04/2019', '05:22:19', '04/04/2019 05:23:55 '),
(82, 'lizethepetete', 'Martha Lizeth', 'Teran', '04/04/2019', '05:26:05', NULL),
(83, 'lizethepetete', 'Martha Lizeth', 'Teran', '04/04/2019', '05:26:47', '04/04/2019 05:27:32 '),
(84, 'lizethepetete', 'Martha Lizeth', 'Teran', '04/04/2019', '06:04:42', '04/04/2019 06:05:33 '),
(85, 'lizethepetete', 'Martha Lizeth', 'Teran', '04/04/2019', '06:07:53', '04/04/2019 06:09:43 '),
(86, 'lizethepetete', 'Martha Lizeth', 'Teran', '04/04/2019', '07:42:25', NULL),
(87, 'lizethepetete', 'Martha Lizeth', 'Teran', '04/04/2019', '09:08:03', NULL),
(88, 'lizethepetete', 'Martha Lizeth', 'Teran', '04/04/2019', '09:10:00', NULL),
(89, 'lizethepetete', 'Martha Lizeth', 'Teran', '04/04/2019', '10:51:21', NULL),
(90, 'lizethepetete', 'Martha Lizeth', 'Teran', '04/04/2019', '10:52:10', NULL),
(91, 'lizethepetete', 'Martha Lizeth', 'Teran', '04/04/2019', '10:53:40', '04/04/2019 10:53:51 '),
(92, 'lizethepetete', 'Martha Lizeth', 'Teran', '04/04/2019', '10:58:19', '04/04/2019 10:58:28 '),
(93, 'lizethepetete', 'Martha Lizeth', 'Teran', '05/04/2019', '04:38:31', NULL),
(94, 'lizethepetete', 'Martha Lizeth', 'Teran', '05/04/2019', '04:48:21', NULL),
(95, 'lizethepetete', 'Martha Lizeth', 'Teran', '05/04/2019', '04:49:14', NULL),
(96, 'lizethepetete', 'Martha Lizeth', 'Teran', '05/04/2019', '04:51:28', '05/04/2019 04:53:58 '),
(97, 'lizethepetete', 'Martha Lizeth', 'Teran', '05/04/2019', '04:54:53', '05/04/2019 04:55:01 '),
(98, 'lizethepetete', 'Martha Lizeth', 'Teran', '05/04/2019', '04:55:13', '05/04/2019 04:55:19 '),
(99, 'lizethepetete', 'Martha Lizeth', 'Teran', '05/04/2019', '05:02:41', '05/04/2019 05:02:55 '),
(100, 'lizethepetete', 'Martha Lizeth', 'Teran', '05/04/2019', '05:04:19', '05/04/2019 05:05:23 '),
(101, 'lizethepetete', 'Martha Lizeth', 'Teran', '05/04/2019', '06:23:03', '05/04/2019 06:24:36 '),
(102, 'lizethepetete', 'Martha Lizeth', 'Teran', '05/04/2019', '06:44:53', '05/04/2019 06:45:46 '),
(103, 'lizethepetete', 'Martha Lizeth', 'Teran', '05/04/2019', '06:56:32', '05/04/2019 06:57:23 '),
(104, 'lizethepetete', 'Martha Lizeth', 'Teran', '05/04/2019', '07:00:09', '05/04/2019 07:03:23 '),
(105, 'lizethepetete', 'Martha Lizeth', 'Teran', '05/04/2019', '07:27:19', NULL),
(106, 'lizethepetete', 'Martha Lizeth', 'Teran', '05/04/2019', '07:28:23', '05/04/2019 07:31:44 '),
(107, 'lizethepetete', 'Martha Lizeth', 'Teran', '05/04/2019', '07:33:42', '05/04/2019 07:39:59 '),
(108, 'lizethepetete', 'Martha Lizeth', 'Teran', '05/04/2019', '11:55:38', NULL),
(109, 'lizethepetete', 'Martha Lizeth', 'Teran', '06/04/2019', '12:59:19', '06/04/2019 01:00:01 '),
(110, 'lizethepetete', 'Martha Lizeth', 'Teran', '06/04/2019', '01:21:01', NULL),
(111, 'lizethepetete', 'Martha Lizeth', 'Teran', '06/04/2019', '06:30:01', NULL),
(112, 'lizethepetete', 'Martha Lizeth', 'Teran', '06/04/2019', '06:37:36', NULL),
(113, 'lizethepetete', 'Martha Lizeth', 'Teran', '06/04/2019', '06:39:40', NULL),
(114, 'lizethepetete', 'Martha Lizeth', 'Teran', '06/04/2019', '07:07:38', '06/04/2019 07:12:30 '),
(115, 'lizethepetete', 'Martha Lizeth', 'Teran', '06/04/2019', '07:35:43', '06/04/2019 07:37:08 '),
(116, 'lizethepetete', 'Martha Lizeth', 'Teran', '06/04/2019', '07:41:54', '06/04/2019 07:42:06 '),
(117, 'lizethepetete', 'Martha Lizeth', 'Teran', '06/04/2019', '08:41:44', '06/04/2019 08:42:13 '),
(118, 'lizethepetete', 'Martha Lizeth', 'Teran', '07/04/2019', '01:25:58', NULL),
(119, 'lizethepetete', 'Martha Lizeth', 'Teran', '07/04/2019', '01:28:27', NULL),
(120, 'lizethepetete', 'Martha Lizeth', 'Teran', '07/04/2019', '01:38:01', NULL),
(121, 'lizethepetete', 'Martha Lizeth', 'Teran', '07/04/2019', '01:40:18', '07/04/2019 01:41:57 '),
(122, 'lizethepetete', 'Martha Lizeth', 'Teran', '07/04/2019', '02:49:52', NULL),
(123, 'lizethepetete', 'Martha Lizeth', 'Teran', '07/04/2019', '03:59:19', NULL),
(124, 'lizethepetete', 'Martha Lizeth', 'Teran', '07/04/2019', '04:01:09', NULL),
(125, 'lizethepetete', 'Martha Lizeth', 'Teran', '07/04/2019', '04:03:01', NULL),
(126, 'lizethepetete', 'Martha Lizeth', 'Teran', '07/04/2019', '04:03:59', NULL),
(127, 'lizethepetete', 'Martha Lizeth', 'Teran', '07/04/2019', '04:22:45', NULL),
(128, 'lizethepetete', 'Martha Lizeth', 'Teran', '07/04/2019', '05:27:12', '07/04/2019 05:27:25 '),
(129, 'lizethepetete', 'Martha Lizeth', 'Teran', '07/04/2019', '05:50:43', '07/04/2019 05:52:41 '),
(130, 'lizethepetete', 'Martha Lizeth', 'Teran', '07/04/2019', '05:54:49', '07/04/2019 05:56:45 '),
(131, 'lizethepetete', 'Martha Lizeth', 'Teran', '07/04/2019', '05:56:55', NULL),
(132, 'lizethepetete', 'Martha Lizeth', 'Teran', '07/04/2019', '06:53:13', '07/04/2019 06:58:26 '),
(133, 'lizethepetete', 'Martha Lizeth', 'Teran', '07/04/2019', '08:03:46', NULL),
(134, 'lizethepetete', 'Martha Lizeth', 'Teran', '07/04/2019', '08:04:34', NULL),
(135, 'lizethepetete', 'Martha Lizeth', 'Teran', '07/04/2019', '08:43:49', NULL),
(136, 'lizethepetete', 'Martha Lizeth', 'Teran', '07/04/2019', '08:45:27', '07/04/2019 08:46:20 '),
(137, 'lizethepetete', 'Martha Lizeth', 'Teran', '07/04/2019', '08:47:49', '07/04/2019 08:48:52 '),
(138, 'lizethepetete', 'Martha Lizeth', 'Teran', '07/04/2019', '10:21:04', '07/04/2019 10:22:13 '),
(139, 'lizethepetete', 'Martha Lizeth', 'Teran', '07/04/2019', '10:23:28', '07/04/2019 10:23:38 '),
(140, 'lizethepetete', 'Martha Lizeth', 'Teran', '07/04/2019', '10:38:59', NULL),
(141, 'lizethepetete', 'Martha Lizeth', 'Teran', '07/04/2019', '10:42:07', NULL),
(142, 'lizethepetete', 'Martha Lizeth', 'Teran', '07/04/2019', '11:11:34', NULL),
(143, 'LIZETHEPETETE', 'Martha Lizeth', 'Teran', '07/04/2019', '11:18:01', NULL),
(144, 'lizethepetete', 'Martha Lizeth', 'Teran', '07/04/2019', '11:20:01', '07/04/2019 11:22:37 '),
(145, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '12:05:31', '08/04/2019 12:06:19 '),
(146, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '12:08:16', '08/04/2019 12:08:38 '),
(147, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '12:57:47', NULL),
(148, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '12:59:57', NULL),
(149, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '01:08:09', NULL),
(150, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '01:09:01', NULL),
(151, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '01:13:30', NULL),
(152, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '01:22:12', NULL),
(153, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '01:24:12', '08/04/2019 01:25:52 '),
(154, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '01:36:30', '08/04/2019 01:38:25 '),
(155, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '01:59:07', '08/04/2019 01:59:37 '),
(156, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '02:05:37', '08/04/2019 02:06:30 '),
(157, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '02:09:36', '08/04/2019 02:10:23 '),
(158, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '02:11:47', '08/04/2019 02:12:46 '),
(159, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '02:35:49', NULL),
(160, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '02:38:48', NULL),
(161, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '02:41:02', NULL),
(162, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '02:43:36', '08/04/2019 02:44:28 '),
(163, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '02:53:32', NULL),
(164, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '02:54:00', '08/04/2019 02:54:47 '),
(165, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '03:03:40', '08/04/2019 03:04:46 '),
(166, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '03:06:57', NULL),
(167, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '03:17:30', '08/04/2019 03:17:40 '),
(168, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '03:05:53', '08/04/2019 03:06:12 '),
(169, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '03:06:46', NULL),
(170, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '03:07:59', NULL),
(171, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '03:18:38', '08/04/2019 03:19:11 '),
(172, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '03:20:44', '08/04/2019 03:21:36 '),
(173, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '03:22:53', '08/04/2019 03:23:56 '),
(174, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '03:25:16', '08/04/2019 03:41:57 '),
(175, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '05:26:00', '08/04/2019 05:28:20 '),
(176, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '05:38:02', NULL),
(177, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '07:31:01', NULL),
(178, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '07:52:28', '08/04/2019 07:54:18 '),
(179, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '08:12:06', '08/04/2019 08:13:53 '),
(180, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '09:14:01', '08/04/2019 09:14:26 '),
(181, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '09:29:03', '08/04/2019 09:29:14 '),
(182, 'lizethepetete', NULL, NULL, NULL, '08/04/2019 09:37:14 ', '08/04/2019 09:37:26 '),
(183, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '09:39:21', '08/04/2019 09:39:38 '),
(184, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '10:09:00', NULL),
(185, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '10:15:10', NULL),
(186, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '10:25:55', '08/04/2019 10:26:17 '),
(187, 'lizethepetete', 'Martha Lizeth', 'Teran', '08/04/2019', '10:29:53', '08/04/2019 10:30:16 '),
(188, 'lizethepetete', 'Martha Lizeth', 'Teran', '09/04/2019', '03:04:05', '09/04/2019 03:06:57 '),
(189, 'lizethepetete', 'Martha Lizeth', 'Teran', '09/04/2019', '03:07:58', '09/04/2019 03:09:22 '),
(190, 'lizethepetete', 'Martha Lizeth', 'Teran', '09/04/2019', '03:13:42', '09/04/2019 03:14:46 ');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `calificaciones`
--

CREATE TABLE IF NOT EXISTS `calificaciones` (
  `idCalificaciones` int(11) NOT NULL AUTO_INCREMENT,
  `CalificacionMen` decimal(3,1) NOT NULL,
  `idAlumno` int(11) NOT NULL,
  `Mes` varchar(20) NOT NULL,
  `idMaterias` int(11) NOT NULL,
  PRIMARY KEY (`idCalificaciones`),
  KEY `fk_idAlumno` (`idAlumno`),
  KEY `fk_idMaterias` (`idMaterias`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=324 ;

--
-- Volcado de datos para la tabla `calificaciones`
--

INSERT INTO `calificaciones` (`idCalificaciones`, `CalificacionMen`, `idAlumno`, `Mes`, `idMaterias`) VALUES
(144, '10.0', 6, 'Septiembre', 4),
(145, '10.0', 6, 'Septiembre', 33),
(146, '8.0', 6, 'Septiembre', 27),
(147, '8.5', 6, 'Septiembre', 39),
(148, '9.0', 6, 'Septiembre', 45),
(149, '10.0', 6, 'Septiembre', 51),
(150, '10.0', 6, 'Septiembre', 10),
(151, '7.5', 6, 'Septiembre', 16),
(152, '10.0', 6, 'Septiembre', 22),
(153, '10.0', 6, 'Septiembre', 30),
(154, '0.0', 6, 'Septiembre', 57),
(155, '10.0', 6, 'Octubre', 4),
(156, '8.5', 6, 'Octubre', 33),
(157, '9.5', 6, 'Octubre', 27),
(158, '9.0', 6, 'Octubre', 39),
(159, '9.5', 6, 'Octubre', 45),
(160, '10.0', 6, 'Octubre', 51),
(161, '9.0', 6, 'Octubre', 10),
(162, '8.0', 6, 'Octubre', 16),
(163, '7.5', 6, 'Octubre', 22),
(164, '5.5', 6, 'Octubre', 30),
(165, '2.0', 6, 'Octubre', 57),
(166, '10.0', 5, 'Septiembre', 4),
(167, '6.0', 5, 'Septiembre', 33),
(168, '9.0', 5, 'Septiembre', 27),
(169, '8.5', 5, 'Septiembre', 39),
(170, '8.0', 5, 'Septiembre', 45),
(171, '10.0', 5, 'Septiembre', 51),
(172, '10.0', 5, 'Septiembre', 10),
(173, '10.0', 5, 'Septiembre', 16),
(174, '10.0', 5, 'Septiembre', 22),
(175, '9.5', 5, 'Septiembre', 30),
(176, '1.0', 5, 'Septiembre', 57),
(177, '10.0', 6, 'Noviembre', 4),
(178, '9.0', 6, 'Noviembre', 10),
(179, '8.0', 6, 'Noviembre', 16),
(180, '8.0', 6, 'Noviembre', 22),
(181, '5.5', 6, 'Noviembre', 30),
(182, '7.5', 6, 'Noviembre', 33),
(183, '9.5', 6, 'Noviembre', 27),
(184, '9.0', 6, 'Noviembre', 39),
(185, '8.5', 6, 'Noviembre', 51),
(186, '7.5', 6, 'Noviembre', 45),
(187, '3.0', 6, 'Noviembre', 57),
(188, '8.5', 6, 'Diciembre', 4),
(189, '9.5', 6, 'Diciembre', 10),
(190, '9.0', 6, 'Diciembre', 16),
(191, '7.5', 6, 'Diciembre', 22),
(192, '6.0', 6, 'Diciembre', 30),
(193, '5.5', 6, 'Diciembre', 33),
(194, '5.0', 6, 'Diciembre', 27),
(195, '9.5', 6, 'Diciembre', 39),
(196, '9.5', 6, 'Diciembre', 51),
(197, '10.0', 6, 'Diciembre', 45),
(198, '4.0', 6, 'Diciembre', 57),
(199, '8.5', 6, 'Enero', 4),
(200, '7.5', 6, 'Enero', 10),
(201, '10.0', 6, 'Enero', 16),
(202, '7.5', 6, 'Enero', 22),
(203, '6.5', 6, 'Enero', 30),
(204, '5.0', 6, 'Enero', 33),
(205, '10.0', 6, 'Enero', 27),
(206, '9.0', 6, 'Enero', 39),
(207, '9.0', 6, 'Enero', 51),
(208, '8.5', 6, 'Enero', 45),
(209, '0.0', 6, 'Enero', 57),
(210, '10.0', 6, 'Febrero', 4),
(211, '7.0', 6, 'Febrero', 10),
(212, '8.5', 6, 'Febrero', 16),
(213, '7.5', 6, 'Febrero', 22),
(214, '7.0', 6, 'Febrero', 30),
(215, '5.5', 6, 'Febrero', 33),
(216, '10.0', 6, 'Febrero', 27),
(217, '10.0', 6, 'Febrero', 39),
(218, '8.5', 6, 'Febrero', 51),
(219, '9.0', 6, 'Febrero', 45),
(220, '1.0', 6, 'Febrero', 57),
(221, '8.5', 6, 'Marzo', 4),
(222, '8.5', 6, 'Marzo', 10),
(223, '7.0', 6, 'Marzo', 16),
(224, '8.5', 6, 'Marzo', 22),
(225, '7.5', 6, 'Marzo', 30),
(226, '6.5', 6, 'Marzo', 33),
(227, '10.0', 6, 'Marzo', 27),
(228, '9.5', 6, 'Marzo', 39),
(229, '8.0', 6, 'Marzo', 51),
(230, '9.0', 6, 'Marzo', 45),
(231, '1.0', 6, 'Marzo', 57),
(232, '9.5', 6, 'Abril', 4),
(233, '9.0', 6, 'Abril', 10),
(234, '9.0', 6, 'Abril', 16),
(235, '9.0', 6, 'Abril', 22),
(236, '8.5', 6, 'Abril', 30),
(237, '10.0', 6, 'Abril', 33),
(238, '10.0', 6, 'Abril', 27),
(239, '9.5', 6, 'Abril', 39),
(240, '8.0', 6, 'Abril', 51),
(241, '8.5', 6, 'Abril', 45),
(242, '2.0', 6, 'Abril', 57),
(243, '10.0', 6, 'Mayo', 4),
(244, '9.5', 6, 'Mayo', 10),
(245, '10.0', 6, 'Mayo', 16),
(246, '8.5', 6, 'Mayo', 22),
(247, '8.0', 6, 'Mayo', 30),
(248, '7.5', 6, 'Mayo', 33),
(249, '10.0', 6, 'Mayo', 27),
(250, '9.5', 6, 'Mayo', 39),
(251, '9.5', 6, 'Mayo', 51),
(252, '9.0', 6, 'Mayo', 45),
(253, '0.0', 6, 'Mayo', 57),
(254, '10.0', 6, 'Junio', 4),
(255, '9.5', 6, 'Junio', 10),
(256, '9.0', 6, 'Junio', 16),
(257, '9.0', 6, 'Junio', 22),
(258, '8.0', 6, 'Junio', 30),
(259, '7.5', 6, 'Junio', 33),
(260, '10.0', 6, 'Junio', 27),
(261, '8.5', 6, 'Junio', 39),
(262, '9.5', 6, 'Junio', 51),
(263, '8.0', 6, 'Junio', 45),
(264, '2.0', 6, 'Junio', 57),
(269, '10.0', 6, 'Diagnostico', 4),
(270, '9.5', 6, 'Diagnostico', 10),
(271, '9.0', 6, 'Diagnostico', 16),
(272, '8.0', 6, 'Diagnostico', 22),
(273, '8.0', 6, 'Diagnostico', 30),
(274, '5.5', 6, 'Diagnostico', 33),
(275, '9.5', 6, 'Diagnostico', 27),
(276, '7.0', 6, 'Diagnostico', 39),
(277, '7.0', 6, 'Diagnostico', 51),
(278, '9.0', 6, 'Diagnostico', 45),
(279, '0.0', 6, 'Diagnostico', 57),
(280, '8.5', 2, 'Octubre', 4),
(281, '8.5', 2, 'Octubre', 10),
(282, '7.5', 2, 'Octubre', 16),
(283, '6.5', 2, 'Octubre', 22),
(284, '5.5', 2, 'Octubre', 30),
(285, '9.0', 2, 'Octubre', 33),
(286, '9.0', 2, 'Octubre', 27),
(287, '8.5', 2, 'Octubre', 39),
(288, '8.5', 2, 'Octubre', 51),
(289, '8.5', 2, 'Octubre', 45),
(290, '1.0', 2, 'Octubre', 57),
(291, '8.6', 2, 'Septiembre', 4),
(292, '8.2', 2, 'Septiembre', 10),
(293, '8.0', 2, 'Septiembre', 16),
(294, '8.5', 2, 'Septiembre', 22),
(295, '5.0', 2, 'Septiembre', 30),
(296, '7.0', 2, 'Septiembre', 33),
(297, '10.0', 2, 'Septiembre', 27),
(298, '10.0', 2, 'Septiembre', 39),
(299, '8.0', 2, 'Septiembre', 51),
(300, '9.5', 2, 'Septiembre', 45),
(301, '0.0', 2, 'Septiembre', 57),
(302, '8.0', 2, 'Noviembre', 4),
(303, '8.5', 2, 'Noviembre', 10),
(304, '7.5', 2, 'Noviembre', 16),
(305, '7.0', 2, 'Noviembre', 22),
(306, '5.5', 2, 'Noviembre', 30),
(307, '8.5', 2, 'Noviembre', 33),
(308, '9.0', 2, 'Noviembre', 27),
(309, '8.5', 2, 'Noviembre', 39),
(310, '8.5', 2, 'Noviembre', 51),
(311, '8.0', 2, 'Noviembre', 45),
(312, '1.0', 2, 'Noviembre', 57),
(313, '9.0', 2, 'Diciembre', 4),
(314, '9.5', 2, 'Diciembre', 10),
(315, '9.0', 2, 'Diciembre', 16),
(316, '9.0', 2, 'Diciembre', 22),
(317, '5.5', 2, 'Diciembre', 30),
(318, '6.5', 2, 'Diciembre', 33),
(319, '10.0', 2, 'Diciembre', 27),
(320, '9.0', 2, 'Diciembre', 39),
(321, '9.5', 2, 'Diciembre', 51),
(322, '9.5', 2, 'Diciembre', 45),
(323, '0.0', 2, 'Diciembre', 57);

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

INSERT INTO `grado` (`idGrado`, `grado`, `grupo`, `idMaestros`) VALUES
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

INSERT INTO `maestros` (`idMaestros`, `nombre`, `ApellidoP`, `ApellidoM`, `calle`, `colonia`, `numExt`, `cp`, `telefono`, `Celular`, `Email`, `gradoEncargado`, `Profesion`, `FechNac`, `RFC`) VALUES
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
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=60 ;

--
-- Volcado de datos para la tabla `materias`
--

INSERT INTO `materias` (`idMaterias`, `nombre`, `idGrado`) VALUES
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
(20, 'Conocimiento del medio', 2),
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
(53, 'Educación Física', 6),
(54, 'Inasistencia', 1),
(55, 'Inasistencia', 2),
(56, 'Inasistencia', 3),
(57, 'Inasistencia', 4),
(58, 'Inasistencia', 5),
(59, 'Inasistencia', 6);

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
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=92 ;

--
-- Volcado de datos para la tabla `padres`
--

INSERT INTO `padres` (`idPadres`, `nombre`, `ApellidoP`, `ApellidoM`, `lugTrabajo`, `Profesion`, `telefono`, `Celular`, `Calle`, `Colonia`, `NumExt`, `cp`) VALUES
(1, ' NATIVIDAD', 'FUENTES', ' ALMAZAN', 'Hogar', 'Ama de casa', '7441212567', '7441900877', 'Venezuela', 'Vista Alegre', '34', '39855'),
(2, ' NOE', 'FLORES ', 'LEYVA', 'Homex', 'Lic Arquitectura', '7448596112', '7449638874', 'Guadalupe', 'Nueva Linea', '670', '39809'),
(3, 'JUAN', 'MATEO ', 'BENITEZ', 'Palacio de Justicia', 'Lic Derecho', '7444412035', '7449987562', 'AGUSTIN LARA', 'TUXTEPEC', '69', '39766'),
(4, 'JOSEFINA ', 'ENRIQUEZ ', 'PEREZ', 'NEGOCIO PROPIO', 'COMERCIANTE', '7448779652', '7445697123', 'INDEPENDENCIA', 'CENTRO', '779', '39867'),
(5, 'VICTORIA EUGENIA ', 'CUEVAS ', 'JIMENEZ', 'NEGOCIO PROPIO', 'VENDEDOR', '7448592145', '7448522157', '20 DE NOVIEMBRE', 'LAS PLAYAS', '1024', '39800'),
(6, 'MARIO ALBERTO', 'GALARCE ', 'NAVA', 'ESCUELA EMILIANO ZAPATA', 'PROFESOR DE INGLES', '7441005789', '7449685270', 'ROMA', 'PEDREGOSO', '09', '39080'),
(7, 'ISIDRO ', 'BRAVO ', 'UBIETA', 'PIZZAS REAL', 'REPARTIDOR', '7448264150', '7440398157', 'ZARAGOZA', 'LA PIRAGUA', '1010', '39451'),
(8, 'IVONNE ', 'JUAREZ ', 'GAVITO', 'HOTEL TORTUGA', 'RESEPCIONISTA', '7441235478', '7443216548', 'MATAMOROS', 'CENTRO', '310', '39123'),
(9, 'HECTOR IGNACIO ', 'GOMEZ ', 'FUENTES', 'AYUNTAMIENTO', 'POLICIA', '7441185214', '7440365782', 'BENITO JUAREZ', 'LOS ANGELES ', '1447', '39542'),
(10, 'MA DEL REFUGIO ', 'ARREOLA FEREGRINO', 'FEREGRINO', 'HOTEL ISLA BONITA', 'GERENTE GENERAL', '7449872131', '7440354857', '5 DE MAYO', 'CENTRO', '545', '39891'),
(11, 'JUAN CARLOS', 'OLIVO ', 'FERNANDEZ', 'IBM CORPORATION', 'ING ELECTROMECANICO', '7441852321', '7442184780', 'MORELOS', 'SINALOA', '251', '78961'),
(12, 'SERGIO ', 'GOMEZ ', 'SOLIS', 'CONDOMINIO LAS BRISAS', 'JEFE DE MANTENIMIENTO', '7441020304', '7446523010', 'ALDAMA', 'CENTRO', '147', '39562'),
(13, 'JOSE', 'PEREZ', 'GONZALEZ', 'CASUNI', 'ING DE CAMPO', '7446482138', '7449856125', 'LIBERTAD', 'COSTA GRANDE', '415', '39658'),
(14, 'MARIA ISABEL', 'AGUILAR ', 'AGUILAR ', 'HOTEL EMPORIO', 'SUPERVISORA', '7448526892', '5541102042', 'SALAMANCA', 'BELLAVISTA', '90', '39652'),
(15, 'OSCAR', ' ARREOLA ', 'PEREGRINA', 'RESTAURANTE EL RECUERDO', 'MESERO', '7442513698', '7442031958', 'CALLE 5', 'CENTRO', '45', '39820'),
(16, 'ENRIQUE ', 'RODRIGUEZ ', 'CABRERA', 'HOTEL EL PRESIDENTE', 'RECEPCIONISTA', '7441528470', '7443216548', 'TABASCO', 'NUEVA VISTA', '981', '39721'),
(17, 'ROSA ', 'VERGARA ', 'CASTILLO', 'CONDOMINIO EL OASIS', 'LIC EN INFORMATICA', '7444187520', '5598722455', 'ALEMAN', 'GALLARDO', '09', '39827'),
(18, 'JORGE ', 'MERCADO ', 'PITOL', 'SEGURO IMSS', 'DOCTOR', '7446985524', '5589746251', 'JALISCO', 'LAS ANCLAS', '092', '39483'),
(19, 'VERONICA ', 'DOMINGUEZ ', 'GALAN', 'RESTAURANTE LOS TARASCOS', 'MESERA', '7448965214', '5593210205', 'XOCHIMILCO', 'WEST', '09', '39874'),
(20, 'ADELA ', 'MAGRO ', 'MENDOZA', 'AYUNTAMIENTO', 'SECRETARIA', '7449652133', '7449856210', 'GUERRERO', 'NATIVIDAD', '13', '39856'),
(21, 'ARTURO', 'BRAVO ', 'ARELO', 'AYUNTAMIENTO', 'GERENTE ADMINISTRATIVO', '7449687512', '7449856212', 'BENITO JUAREZ', 'CAMACHO', '1294', '39778'),
(22, 'MARIA ANA ', 'GARCIA', 'MORALES', 'LIVERPOOL', 'COSTURERA', '7448547968', '5589624120', 'GALVANEZ', 'JUAREZ', '03', '39003'),
(23, 'JAZIEL HUMBERTO', 'OCHOA ', 'SALDANA', 'TALLER RODOLFO', 'MECANICO', '7441658941', '7449356200', 'CALLE 3', 'LIMA', '71', '39101'),
(24, 'SANTIAGO ', 'ENRIQUEZ ', 'RAMIREZ', 'ESCUELA BENITO JUAREZ', 'DIRECTOR', '7443541009', '7449478665', 'PEDREGOSO', 'PALOMARES', '91', '39884'),
(25, 'SOFIA ', ' PEREZ', 'FLORES', 'COLEGIO MAC GREGOR', 'SECRETARIA', '7447847847', '5531316467', 'OCAMPO', 'CENTRO', '34', '39504'),
(26, 'GABRIELA', 'ABRAJAN', 'CORDERO', 'SORIANA', 'CAJERA', '7449685123', '7444485217', 'SALAMANCA', 'CARABALI', '43', '39139'),
(27, 'JUAN RAMON ', 'AGUAYO ', 'REVELES', 'INFORNAVIT', 'ASESOR DE VENTAS', '7449832562', '7444471023', 'SAN LUIS', 'COTA', '309', '39174'),
(28, ' BERTHA ', 'AGUILAR ', 'MACIAS', 'RESTAURANTE EL AMIGO MIGUEL', 'COSINERA', '7443256108', '7444587858', 'SONORA', 'COTA', '643', '39455'),
(29, 'GREGORIO', 'AGUILER', 'REYES', 'FORD MEXICO', 'GERENTE DE VENTAS', '7442030156', '7440158422', 'CALLE 8', 'SEN', '384', '39845'),
(30, 'SUSANA', 'ALEMAN', 'COTA', 'CHEDRAWI', 'CAJERA', '7448956258', '5510300269', 'TABASCO', 'SANTA CRUZ', '8534', '39873'),
(31, 'FERNANDO', 'GAMEZ', 'QUEZADA', 'PEMEX', 'LIC EN CONTABILIDAD', '7444851152', '7440036402', 'MAZATLAN', 'SAN JUAN', '400', '39854'),
(32, 'ANDREA', 'CASTRO', 'JIMENEZ', 'COLEGIO LA SALLE', 'PSICOLOGA', '7448521456', '7442301581', 'TABASCO', 'SAN JUAN', '64', '39645'),
(33, 'MARTHA', 'CORDOVA', 'ROJAS', 'ESTETICA ROSITA', 'ESTILISTA', '7448965238', '7444896521', 'SANJUAN', 'PROGRESO', '97', '39203'),
(34, 'JOSE', 'CISNEROS', 'ELIZALDE', 'MADERAS CAMPECHE', 'CARPINTERO', '7442036505', '7444015585', 'CALLE 2', '2 SOLES', '4049', '39888'),
(35, 'KAREN', 'OLIVA', 'PEDREGOSO', 'HOGAR', 'AMA DE CASA', '7445621144', '5565891447', 'TAMAULIPAS', 'ICACOS', '309', '39500'),
(36, 'GUSTAVO', 'ELIAS', 'DOMINGUEZ', 'CFE COMISION DE LUZ', 'CONTADOR', '7448957892', '7443201574', 'ALTAMIRA', 'AGUIRRE', '70', '39613'),
(37, 'ALFREDO', 'VALLE', 'LOPEZ', 'NEGOCIO PROPIO', 'COMERCIANTE', '7448956125', '7440587012', 'BONFIL ', 'ARROYO SECO', '670', '39850'),
(38, 'GABRIELA', 'FLORES', 'CRUZ', 'SEGURO SOCIAL', 'DOCTORA', '7448892164', '7442115461', 'CALLE 3', 'BALCONES', '45', '39876'),
(39, 'CARLOS', 'ESTRADA', 'MORALES', 'ISSTE', 'ENFERMERO', '7448547552', '7449632854', 'LAS PLAYAS', 'ALTA VISTA', '3409', '39778'),
(40, 'SOL', 'AGUIRRE', 'LORENZO', 'HOTEL LAS PLAYAS', 'SUPERVISORA', '7448922155', '7446321002', 'NORTE', 'MIRAMAR', '342', '39058'),
(41, 'JULIO', 'FERNANDEZ', 'PEREZ', 'COLEGIO LA SALLE', 'MAESTRO DE INGLES', '5587415694', '5502358054', 'MAZATLAN', 'VERACRUZ', '540', '39044'),
(42, 'MONICA', 'LARA', 'PEDREGOSO', 'NEGOCIO PROPIO', 'COMERCIANTE', '7440321056', '7444720365', 'BUENAVISTA', 'BUROCRATA', '673', '39086'),
(43, 'CESAR', 'CORREA', 'DELGADO', 'PIZZAS DOMINOS', 'REPARTIDOR', '7448596412', '7441475662', 'POLACO', 'LOMAS', '564', '39320'),
(44, 'PAULINA', 'TORRES', 'SABALA', 'SALON BRILLAMAR', 'ESTILISTA', '7442589654', '7442103257', 'CALLE 9', 'CAUDILLO', '823', '39505'),
(45, 'MARIELA', 'SOSA', 'GARCIA', 'HOTEL EMPORIO', 'RECAMARISTA', '5587495822', '7449856221', 'CERESO', 'LAS BRISAS', '309', '39884'),
(46, 'ALIN', 'GOMEZ', 'FRANCO', 'RESTAURANTE TOKS', 'MESERA', '5587459627', '5587102345', 'CHINAMECA', 'COHOTEROS', '452', '39845'),
(47, 'VERONICA', 'DEL SUR', 'ALTAMIRANO', 'KINDER AMERICA', 'SECRETARIA', '7449885485', '7448482055', 'NOGUEDA', 'COSTA AZUL', '085', '39658'),
(48, 'HERIBERTO', 'HERNANDEZ', 'GOMEZ', 'RESTAURANTE ITTALIANIS', 'MESERO', '5589892278', '5145215402', 'CONDESA', 'BRAVA', '987', '39784'),
(49, 'NAYELI', 'GUTIERREZ', 'GUZMAN', 'GALERIAS DIANA', 'ADMINISTRADORA', '7449658812', '7443200516', 'SOL', 'PRIVADA', '3423', '39870'),
(50, 'PATRICIA', 'LORENZO', 'VELA', 'PIZZAS HOT', 'MESERA', '7447440012', '7444403251', 'NIEVES', 'GRIJALBA', '341', '39233'),
(51, 'BERENICE', 'ALCANTAR', 'AGUILAR', 'AURRERA CENTRO', 'CAJERA', '7448972013', '7446352114', 'CORAL', 'DEL VALLE', '23', '39541'),
(52, 'ALMA', 'GUZMAN', 'LOPEZ', 'NEGOCIO PROPIO', 'COMERCIANTE', '7443020011', '7444520203', 'PASCUARO', 'LEON', '342', '39514'),
(53, 'MARCO', 'HERNANDEZ', 'GARNICA', 'CENTRAL DE AUTOBUSES', 'CHOFER', '7448852174', '7449858200', 'CORAL', 'LEON', '32', '39852'),
(54, 'RICARDO ARTURO', 'ALFARO', 'PERALTA', 'PROPIO', 'TAXISTA', '7443233054', '7444121560', 'SOL', 'POLAR', '342', '39645'),
(55, 'ANA', 'MONTIEL', 'MORALES', 'HOGAR', 'AMA DE CASA', '7443021052', '7442321051', 'GIRO', 'LIMA', '342', '39200'),
(56, 'SUSANA', 'GALINDO', 'HANK', 'COLEGIO ZUMARRAGA', 'MAESTRA', '7449566548', '7443202501', 'DEL VALLE', 'ROSALINDA', '341', '39651'),
(57, 'RAFAEL', 'ALVAREZ', 'PADILLA', 'PROPIO', 'TAXISTA', '7449558214', '7442551660', 'RASTRO', 'TECONCHE', '76', '39688'),
(58, 'CLAUDIA', 'BAUTISTA', 'MARTINEZ', 'SORIANA', 'CAJERA', '7448221255', '7448222124', 'NOGUEDA', 'MATA', '67', '39584'),
(59, 'REBECA', 'DIAZ', 'PINEDA', 'HOTEL LAS PLAYAS', 'RECEPCIONISTA', '7440321022', '7440205200', 'EMILIO', 'MOCTEZUMA', '094', '39622'),
(60, 'MARIA ', 'AVALOS', 'PEDRAZA', 'FAMACIA SIMILARES', 'CAJERA', '7448920220', '7443696301', 'CAPIRE', 'COLOSO', '5285', '39685'),
(61, 'SERGIO', 'ALANIS', 'PRADO', 'COCACOLA', 'GERENTE', '7446358200', '7444365215', 'PALMAR', 'MIRADOR', '369', '39652'),
(62, 'VICTOR', 'VEGA', 'PACHECO', 'HOTEL EL CANO', 'CONTADOR', '7448547412', '7449378854', 'GOLMAR', 'EL TANQUE', '912', '39655'),
(63, 'ALMA', 'ROBLES', 'GALINDO', 'ESCUELA EMILIANO ZAPATA', 'SECRETARIA', '7449543687', '7441425254', 'GARITA', 'JUAREZ', '4625', '39256'),
(64, 'KAREN', 'ALVAREZ', 'PEREZ', 'LOMAS DEL MAR', 'ARQUITECTA', '7448956351', '7443689585', 'FUENTES', 'VAZQUEZ', '58', '39871'),
(65, 'SILVESTRE', 'ALFARO', 'PANIAGUA', 'TALLER MIGUEL', 'PLOMERO', '7442585254', '7442383125', 'FARALLON', 'JUAREZ', '389', '39457'),
(66, 'DANIEL', 'ARREOLA', 'ARANA', 'RESTAURANTE EL PESCADOR', 'COCINERO', '7446859686', '7443686956', 'EMPERADOR', 'LAS GRANJAS', '45', '39078'),
(67, 'ESMERALDA', 'AMEZCUA', 'AMARO', 'RESTAURANTE WOOLWORD', 'COCINERA', '4773874364', '4773686546', 'LAS BRIZAS', 'VICTORIA', '52', '39745'),
(68, 'MIGUEL ANGEL', 'BARRAGAN', 'HERNANDEZ', 'TV AZTECA', 'PERIODISTA', '7442547542', '7445457854', 'LAS GRANJAS', 'FLAMINGOS', '2587', '39455'),
(69, 'DARIO', 'BALTAZAR', 'JAIMES', 'TALLER EL MEJOR', 'MECANICO', '7444254363', '7442575854', 'FRONTERA', 'VICTORIA', '45', '39424'),
(70, 'RAMSES', 'JUSTO', 'JIMENEZ', 'SECRETARIA PUBLICA', 'DIRECTOR', '7445482541', '7445454568', 'MARQUEZ', 'SANTA ELENA', '534', '39857'),
(71, 'JOSE LUIS', 'JUAREZ', 'VALLES', 'CORDINACION NACIONAL', 'SECRETARIO', '7441024501', '7442015425', 'ICACOS', 'SALMON', '44', '39516'),
(72, 'ARTURO', 'LOPEZ', 'VALENCIA', 'FARMACIAS SIMILARES', 'DORTOR', '7442115487', '7442542523', 'BENITO JUAREZ', 'HORNOS', '9454', '39512'),
(73, 'LUIS', 'CRUZ', 'BETANCOURT', 'CAMPECHE', 'CARPINTERO', '7441535611', '7446569619', 'GLORIA', 'BERNAL', '461', '39454'),
(74, 'JUDITH', 'CRUZ', 'CAMARGO', 'SEGURO SOCIAL', 'ENFERMERA', '7441245721', '7442458523', 'Diaz Ordaz', 'HOSPITAL', '312', '39876'),
(75, 'JOYCE', 'ROJANO', 'CRPRIANO', 'RESTAURANTE VIPS', 'MESERA', '7445365656', '7445353565', 'SALAMANCA', 'VENEZUELA', '58', '39454'),
(76, 'FRANCISCO', 'RIVERA', 'ROMA', 'ESCUELA EMILIANO ZAPATA', 'MAESTRO', '7445236856', '7442349552', 'INDUSTRIAL ', 'JACARANDAS', '31', '39784'),
(77, 'JOSEFINA', 'TOVAL', 'SOTO', 'AYUNTAMIENTO', 'LIC EN INFORMATICA', '7441235412', '7442534864', 'TAMAULIPAS', 'ORTIZ', '340', '39625'),
(78, 'ROSA', 'SANCHEZ', 'ALVAREZ', 'HOGAR', 'AMA DE CASA', '7445685553', '7442544655', 'MATAMOROS', 'SOLAR', '845', '39222'),
(79, 'ELIZABETH', 'BUENDIA', 'POLANCO', 'COMISION DE LUZ', 'ING EN ELECTRONICA', '7445454347', '7448693300', 'PASCUARO', 'CORTANA', '6569', '39554'),
(80, 'LAURA', 'GARCIA', 'QUIROZ', 'HOTEL TORTUGA', 'RESEPCIONISTA', '7445651231', '7444968695', 'CERBANTES', 'LUNA DEL MAR', '554', '39243'),
(81, 'ELENA', 'SANDOVAL', 'MEDINA', 'JUSGADOS', 'ABOGADA', '7446569554', '7445655520', 'CASTILLO', 'SANTA CRUZ', '429', '39421'),
(82, 'JULIANA', 'MILLAN', 'SALAS', 'TV AZTECA', 'REPORTERA', '7445863563', '7445435411', 'REYES', 'MATAMOROS', '80', '39411'),
(83, 'PABLO', 'QUIROZ', 'PORTILLO', 'CARNICERIA EL BUEN DIENTE', 'CARNICERO', '7446956241', '7444689655', 'PIEDRA', 'LOMA ALTA', '50', '39995'),
(84, 'IGNACIO', 'GARCIA', 'DELGADO', 'AYUNTAMIENTO', 'POLICIA', '5598797894', '7446441478', 'TERRANOVA', 'ADAME', '322', '39874'),
(85, 'PEDRO', 'PERALTA', 'VILLA', 'APASCO', 'CONTADOR', '7446846942', '7443569435', 'MAZATLAN', 'NUEVA ZONA', '221', '39101'),
(86, 'CARLOS', 'GUZMAN', 'LEONES', 'CONDOMINIO SOLAR', 'JARDINERO', '7446549536', '7446959320', 'PERA', 'LA PAROTA', '3423', '39781'),
(87, 'ARTURO', 'POLACO', 'ROMAN', 'SALON BELLA Y LISTA', 'ESTILISTA', '7446456450', '7440320632', 'VILLAS', 'VISTA ALEGRE', '452', '39250'),
(88, 'ROMAN', 'FUENTES', 'GRIJALBA', 'GYM ROMAN', 'INSTRUCTOR', '7442128542', '7448534334', 'PROVIDENCIA', 'LA POZA', '456', '39065'),
(89, 'GUSTAVO', 'RIVERA', 'LARA', 'NEGOCIO PROPIO', 'ING EN ELECTRONICA', '7442138587', '7442010120', 'CAMPECHE', 'LOS TIGRES', '4265', '39658'),
(90, 'ARANZA', 'ALMAZAN', 'GALAN', 'ISSTE', 'ENFERMERA', '7449820023', '7444969030', 'LIBERTAD', 'LA MIRA', '39', '39744'),
(91, 'MONICA', 'LARA ', 'CHIQUITO', 'NEGOCIO PROPIO', 'COMERCIANTE', '7441212091', '7441900783', 'CAMPECHE', 'CARABALI', '52', '39560');

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
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- Volcado de datos para la tabla `personal`
--

INSERT INTO `personal` (`idUsuario`, `nombre`, `ApellidoP`, `ApellidoM`, `calle`, `colonia`, `numExt`, `cp`, `telefono`, `email`, `profesion`, `cargo`, `usuario`, `password`) VALUES
(1, 'Martha Lizeth', 'Teran', 'Lara', 'qweqweqwe', 'asdasdasdsad', '21', '12345', '1234567890', 'sdfdfsdfsdf', 'sdfsdfsdfsdf', 'Director(a)', 'lizethepetete', '3tortugas');

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `alumno`
--
ALTER TABLE `alumno`
  ADD CONSTRAINT `idGrado` FOREIGN KEY (`idGrado`) REFERENCES `grado` (`idGrado`),
  ADD CONSTRAINT `idPadres` FOREIGN KEY (`idPadres`) REFERENCES `padres` (`idPadres`);

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
