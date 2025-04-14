using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryEspecheConexionBD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            clsConexionBD objConectarBD = new clsConexionBD();

            objConectarBD.ConectarBD();
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            clsConexionBD objConexion = new clsConexionBD();

            
            objConexion.ConectarBD();

            // Obtenemos los datos
            DataTable tabla = objConexion.ObtenerContactos();
        }
    }
}

