using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Control_Escolar
{
    static class sesion
    {
        public static string Usuario;
        public static string Password;
        public static string cargo;
        public static string HoraEntrada;
        public static int idAcceso;
        public static List<string> nombreU = new List<string>();
        public static List<string> apellidoP = new List<string>();
        public static List<string> apellidoM = new List<string>();
        public static string fnac;
        public static string nombre, AP, AM, Curp, calle, numero, Colonia, CP, LN,  telefono, Alergia,telefono1,alumno;
        public static int edad;
        public static string genero;

        public static string pictureb1;
        public static int Grado;


        public static string grado;
    }
}
