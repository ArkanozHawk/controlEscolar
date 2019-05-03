using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Animations;
using MaterialSkin.Controls;
using MySql.Data.MySqlClient;

namespace Control_Escolar
{
    public partial class login : MaterialForm
    {
        public static void ThreadProc()

        {
            Application.Run(new registro());
        }

        public static void ThreadPrincipal()

        {
            Application.Run(new principal());
        }

        public login()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Red900, Primary.Red700, Primary.Red900, Accent.Red700, TextShade.WHITE);
        }

        conexion obj = new conexion();

        string nombre, ApellidoP, ApellidoM, calle, colonia, numExt, cp, telefono, email, profesion, cargo, usuario, password, Fecha, HoraEntrada, ApellidoPU, NombreU, ApellidoMU;

        private void txtUsuario_Click(object sender, EventArgs e)
        {

        }

        private void login_Load(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn;
                MySqlCommand com;


                string conexion = "server=localhost;uid=root;database=nerivela";
                conn = new MySqlConnection(conexion);
                conn.Open();
            }
            catch
            {
                MySqlConnection conn;
                MySqlCommand com;
                MySqlConnection conDatabase = new MySqlConnection("Data Source=localhost;" + "UserId=root;");
                MySqlCommand cmdDatabase = new MySqlCommand("CREATE DATABASE nerivela;", conDatabase);
                conDatabase.Open();
                cmdDatabase.ExecuteNonQuery();
                conDatabase.Close();
                string conexion = "server=localhost;uid=root;database=nerivela";
                string query = "CREATE TABLE IF NOT EXISTS `alumno` ( idAlumno int(11) NOT NULL AUTO_INCREMENT, nombre varchar(20) NOT NULL, ApellidoP varchar(20) NOT NULL, ApellidoM varchar(20) NOT NULL, calle varchar(50) NOT NULL, colonia varchar(50) NOT NULL, numExt varchar(5) DEFAULT NULL, cp varchar(5) NOT NULL, telEmer varchar(10) NOT NULL, Genero varchar(9) NOT NULL, lugNac varchar(30) NOT NULL, FechNac date NOT NULL, Alergias varchar(250) DEFAULT NULL, CURP varchar(20) NOT NULL, idPadres int(11) NOT NULL, idGrado int(11) NOT NULL, PRIMARY KEY(idAlumno), UNIQUE KEY CURP (CURP), KEY idPadres (idPadres), KEY idGrado (idGrado)) ENGINE = InnoDB  DEFAULT CHARSET = latin1 AUTO_INCREMENT = 92;" +
                    "CREATE TABLE IF NOT EXISTS `bitacora` (`idAcceso` int(11) NOT NULL AUTO_INCREMENT, `Usuario` varchar(20) DEFAULT NULL,`Nombre` varchar(20) DEFAULT NULL,`Apellido` varchar(20) DEFAULT NULL,`Fecha` varchar(20) DEFAULT NULL,`HoraEntrada` varchar(20) DEFAULT NULL,`HoraSalida` varchar(20) DEFAULT NULL,PRIMARY KEY(`idAcceso`)) ENGINE = InnoDB  DEFAULT CHARSET = latin1 AUTO_INCREMENT = 191; " +
                    "CREATE TABLE IF NOT EXISTS `calificaciones` (`idCalificaciones` int(11) NOT NULL AUTO_INCREMENT,`CalificacionMen` decimal(3, 1) NOT NULL,`idAlumno` int(11) NOT NULL,`Mes` varchar(20) NOT NULL,`idMaterias` int(11) NOT NULL,PRIMARY KEY(`idCalificaciones`),KEY `fk_idAlumno` (`idAlumno`),KEY `fk_idMaterias` (`idMaterias`)) ENGINE = InnoDB  DEFAULT CHARSET = latin1 AUTO_INCREMENT = 324; " +
                    "CREATE TABLE IF NOT EXISTS `grado` (`idGrado` int(11) NOT NULL AUTO_INCREMENT,`grado` varchar(1) NOT NULL,`grupo` varchar(1) NOT NULL,`idMaestros` int(11) NOT NULL,PRIMARY KEY(`idGrado`),KEY `fk_idMaestros` (`idMaestros`)) ENGINE = InnoDB  DEFAULT CHARSET = latin1 AUTO_INCREMENT = 7; " +
                    "INSERT INTO `grado` (`idGrado`, `grado`, `grupo`, `idMaestros`) VALUES(1, '1', 'A', 1),(2, '2', 'A', 2),(3, '3', 'A', 3),(4, '4', 'A', 4),(5, '5', 'A', 5),(6, '6', 'A', 6); " +
                    "CREATE TABLE IF NOT EXISTS `maestros` (`idMaestros` int(11) NOT NULL AUTO_INCREMENT,`nombre` varchar(20) NOT NULL,`ApellidoP` varchar(20) NOT NULL,`ApellidoM` varchar(20) NOT NULL,`calle` varchar(50) NOT NULL,`colonia` varchar(50) NOT NULL,`numExt` varchar(5) DEFAULT NULL,`cp` varchar(5) NOT NULL,`telefono` varchar(10) DEFAULT NULL,`Celular` varchar(10) DEFAULT NULL,`Email` varchar(50) NOT NULL,`gradoEncargado` int(1) NOT NULL,`Profesion` varchar(50) NOT NULL,`FechNac` date NOT NULL,`RFC` varchar(18) NOT NULL,PRIMARY KEY(`idMaestros`),UNIQUE KEY `RFC` (`RFC`)) ENGINE = InnoDB  DEFAULT CHARSET = latin1 AUTO_INCREMENT = 7; " +
                    "INSERT INTO `maestros` (`idMaestros`, `nombre`, `ApellidoP`, `ApellidoM`, `calle`, `colonia`, `numExt`, `cp`, `telefono`, `Celular`, `Email`, `gradoEncargado`, `Profesion`, `FechNac`, `RFC`) VALUES(1, 'José', 'Jacinto', 'Cuevas', 'Morteros', 'Marroquin', '70', '39640', '1587496', '744610125', 'jjCuevas@hotmail.com', 1, 'Lic. en Educación Primaria', '1972-07-30', 'JJCM720730TE'),(2, 'Lucia', 'Figueroa', 'Pineda', 'SONORA', 'PROGRESO', '76', '39350', '1900893', '744763789', 'luciFigP43@outlock.com', 2, 'Lic. en Educación Primaria', '1967-03-25', 'LFPU670325'),(3, 'Rafael', 'Ramírez', 'Castañeda', 'AQUILES SERDÁN', 'SANTA DOROTE', '49', '39970', '1479632', '744827307', 'RafaCast100@hotmail.com', 3, 'Lic. en Educación Primaria', '1977-04-11', 'RRFP770411QO'),(4, 'Ana Lilia', 'Morales', 'Simon', 'AV. 20 DE NOVIEMBRE', 'CENTRO', '1024', '39300', '9856731', '744637219', 'Ana_lilia@gmail.com', 4, 'Lic. en Educación Primaria', '1971-01-03', 'ALMS710103BG'),(5, 'Alberto Daniel', 'Fuentes', 'Dávila', 'IGNACIO MATIAS', 'MA. LUISA', '6', '39122', '1157496', '744732275', 'Fuentes-Albert@gmail.com', 5, 'Lic. en Educación Primaria', '1974-11-09', 'ADFD741109QW'),(6, 'Fabiola', 'Torres', 'Quintero', 'AV INDEPENDENCIA', 'CENTRO', '545', '39450', '1140833', '744793122', 'fabi_tq90@live.com.mx', 6, 'Lic. en Educación Primaria', '1970-09-18', 'FTQH700918MB'); " +
                    "CREATE TABLE IF NOT EXISTS `materias` (`idMaterias` int(11) NOT NULL AUTO_INCREMENT,`nombre` varchar(50) NOT NULL,`idGrado` int(11) NOT NULL,PRIMARY KEY(`idMaterias`),KEY `fk_idGrado` (`idGrado`)) ENGINE = InnoDB  DEFAULT CHARSET = latin1 AUTO_INCREMENT = 60; " +
                    "INSERT INTO `materias` (`idMaterias`, `nombre`, `idGrado`) VALUES(1, 'Español', 1),(2, 'Español', 2),(3, 'Español', 3),(4, 'Español', 4),(5, 'Español', 5),(6, 'Español', 6),(7, 'Matematicas', 1),(8, 'Matematicas', 2),(9, 'Matematicas', 3),(10, 'Matematicas', 4),(11, 'Matematicas', 5),(12, 'Matematicas', 6),(13, 'Ingles', 1),(14, 'Ingles', 2),(15, 'Ingles', 3),(16, 'Ingles', 4),(17, 'Ingles', 5),(18, 'Ingles', 6),(19, 'Conocimiento del medio', 1),(20, 'Conocimiento del medio', 2),(21, 'Ciencias Naturales', 3),(22, 'Ciencias Naturales', 4),(23, 'Ciencias Naturales', 5),(24, 'Ciencias Naturales', 6),(25, 'La entidad donde vivo', 3),(26, 'Formación Cívica y Ética', 3),(27, 'Formación Cívica y Ética', 4),(28, 'Formación Cívica y Ética', 5),(29, 'Formación Cívica y Ética', 6),(30, 'Geografía', 4),(31, 'Geografía', 5),(32, 'Geografía', 6),(33, 'Historia', 4),(34, 'Historia', 5),(35, 'Historia', 6),(36, 'Artes', 1),(37, 'Artes', 2),(38, 'Artes', 3),(39, 'Artes', 4),(40, 'Artes', 5),(41, 'Artes', 6),(42, 'Educación Socioemocional', 1),(43, 'Educación Socioemocional', 2),(44, 'Educación Socioemocional', 3),(45, 'Educación Socioemocional', 4),(46, 'Educación Socioemocional', 5),(47, 'Educación Socioemocional', 6),(48, 'Educación Física', 1),(49, 'Educación Física', 2),(50, 'Educación Física', 3),(51, 'Educación Física', 4),(52, 'Educación Física', 5),(53, 'Educación Física', 6),(54, 'Inasistencia', 1),(55, 'Inasistencia', 2),(56, 'Inasistencia', 3),(57, 'Inasistencia', 4),(58, 'Inasistencia', 5),(59, 'Inasistencia', 6); " +
                    "CREATE TABLE IF NOT EXISTS `padres` (`idPadres` int(11) NOT NULL AUTO_INCREMENT,`nombre` varchar(20) NOT NULL,`ApellidoP` varchar(20) NOT NULL,`ApellidoM` varchar(20) NOT NULL,`lugTrabajo` varchar(30) NOT NULL,`Profesion` varchar(30) DEFAULT NULL,`telefono` varchar(10) DEFAULT NULL,`Celular` varchar(10) DEFAULT NULL,`Calle` varchar(50) NOT NULL,`Colonia` varchar(50) NOT NULL,`NumExt` varchar(5) DEFAULT NULL,`cp` varchar(5) NOT NULL,PRIMARY KEY(`idPadres`)) ENGINE = InnoDB  DEFAULT CHARSET = latin1 AUTO_INCREMENT = 92; " +
                    "CREATE TABLE IF NOT EXISTS `personal` (`idUsuario` int(11) NOT NULL AUTO_INCREMENT,`nombre` varchar(20) NOT NULL,`ApellidoP` varchar(20) NOT NULL,`ApellidoM` varchar(20) NOT NULL,`calle` varchar(50) NOT NULL,`colonia` varchar(50) NOT NULL,`numExt` varchar(5) NOT NULL,`cp` varchar(5) NOT NULL,`telefono` varchar(10) DEFAULT NULL,`email` varchar(50) NOT NULL,`profesion` varchar(50) NOT NULL,`cargo` varchar(20) NOT NULL,`usuario` varchar(20) DEFAULT NULL,`password` varchar(20) DEFAULT NULL,PRIMARY KEY(`idUsuario`),UNIQUE KEY `usuario` (`usuario`)) ENGINE = InnoDB  DEFAULT CHARSET = latin1 AUTO_INCREMENT = 2; " +
                    "ALTER TABLE `alumno`ADD CONSTRAINT `idGrado` FOREIGN KEY(`idGrado`) REFERENCES `grado` (`idGrado`),ADD CONSTRAINT `idPadres` FOREIGN KEY(`idPadres`) REFERENCES `padres` (`idPadres`); " +
                    "ALTER TABLE `grado`ADD CONSTRAINT `fk_idMaestros` FOREIGN KEY(`idMaestros`) REFERENCES `maestros` (`idMaestros`); " +
                    "ALTER TABLE `materias`ADD CONSTRAINT `fk_idGrado` FOREIGN KEY(`idGrado`) REFERENCES `grado` (`idGrado`) ON DELETE NO ACTION ON UPDATE NO ACTION; ";
                obj.insBitacora(conexion, query);

            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void Titulo_Click(object sender, EventArgs e)
        {

        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            System.Threading.Thread registrando = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc));

            registrando.Start();
            this.Close();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            MySqlConnection conn;
            MySqlCommand com;

            usuario = txtUsuario.Text;
            password = txtContra.Text;


            //string conexion = "server=localhost;uid=root;pwd=digi3.0;database=nerivela";
            string conexion = "server=localhost;uid=root;database=nerivela";
            string query = "SELECT COUNT(*) FROM personal where usuario = '" + usuario + "' and password = '" + password + "';";
            string query1 = "SELECT * FROM personal where usuario = '" + usuario + "' and password = '" + password + "';";
            int resultado = obj.Consul(conexion, query);
            if (resultado == 1)
            {
                conn = new MySqlConnection(conexion);
                conn.Open();

                com = new MySqlCommand(query1, conn);

                MySqlDataReader myreader = com.ExecuteReader();

                myreader.Read();


                NombreU = Convert.ToString(myreader["Nombre"]);
                ApellidoPU = Convert.ToString(myreader["ApellidoP"]);


                sesion.Usuario = usuario;
                sesion.Password = password;
                Fecha = DateTime.Now.ToString("dd/MM/yyyy");


                HoraEntrada = DateTime.Now.ToString("hh:mm:ss");


                string inserta_bitacora = "INSERT INTO bitacora (Usuario,Nombre,Apellido,Fecha,HoraEntrada) " + "values('" + usuario + "','" + NombreU + "','" + ApellidoPU + "','" + Fecha + "','" + HoraEntrada + "');";
                obj.insBitacora(conexion, inserta_bitacora);
                string posicion = "SELECT idAcceso FROM bitacora ORDER by idAcceso DESC limit 1;";
                int posi = obj.Acceso(conexion, posicion);
                sesion.idAcceso = posi;
                sesion.HoraEntrada = HoraEntrada;
                System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadPrincipal));
                pantalla.Start();
                CheckForIllegalCrossThreadCalls = false;
                this.Close();
            }
            else
            {
                MessageBox.Show("Datos Invalidos, prueba de nuevo");
            }

        }
    }
}
