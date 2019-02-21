﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Control_Escolar
{
    class conexion
    {
        public void Consulta(string conexion, string consulta)
        {

            MySqlConnection conn;
            MySqlCommand com;



            try
            {
                conn = new MySqlConnection(conexion);
                conn.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;

            }



            try
            {
                com = new MySqlCommand(consulta, conn);

                com.ExecuteNonQuery();
                MessageBox.Show("Se han Ingresado los datos");
                return;



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

                return;
            }
            finally
            {


                conn.Close();

            }





        }


        public int Consul(string conexion, string consulta)
        {

            MySqlConnection conn;
            MySqlCommand com;



            try
            {
                conn = new MySqlConnection(conexion);
                conn.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;

            }



            try
            {
                com = new MySqlCommand(consulta, conn);

                MySqlDataReader myReader = com.ExecuteReader();
                myReader.Read();
                int resultado = Convert.ToInt32(myReader["COUNT(*)"]);
                return resultado;



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

                return 0;
            }
            finally
            {


                conn.Close();

            }





        }
    }
}
