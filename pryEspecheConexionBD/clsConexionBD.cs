using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace pryEspecheConexionBD
{
    internal class clsConexionBD
    {
        //cadena de conexion
        string cadenaConexion = "Server=localhost\\SQLEXPRESS;Database=Ventas2;Trusted_Connection=True;";


        //conector
        SqlConnection coneccionBaseDatos;

        //comando
        SqlCommand comandoBaseDatos;

        public string nombreBaseDeDatos;


        public void ConectarBD()
        {
            try
            {
                coneccionBaseDatos = new SqlConnection(cadenaConexion);

                nombreBaseDeDatos = coneccionBaseDatos.Database;

                coneccionBaseDatos.Open();

                MessageBox.Show("Conectado a " + nombreBaseDeDatos);
            }
            catch (Exception error)
            {
                MessageBox.Show("Tiene un error " + error.Message);
            }

        }
        public DataTable ObtenerContactos()
        {
            DataTable tablaContactos = new DataTable();

            try
            {
                using (coneccionBaseDatos = new SqlConnection(cadenaConexion))
                {
                    coneccionBaseDatos.Open();
                    string consulta = "SELECT * FROM dbo.Contactos";
                    SqlDataAdapter adaptador = new SqlDataAdapter(consulta, coneccionBaseDatos);
                    adaptador.Fill(tablaContactos);
                }

                MessageBox.Show("Filas obtenidas: " + tablaContactos.Rows.Count);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error al obtener datos: " + error.Message);
            }

            return tablaContactos;
        }

    }
}


