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
        public static string HoraEntrada;
        public static int idAcceso;
        public static List<string> nombre = new List<string>();
        public static List<string> apellidoP = new List<string>();
        public static List<string> apellidoM = new List<string>();
    }
}
