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
        clsConexionBD conexion = new clsConexionBD();
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conexion.ConectarBD();
            MostrarProductos();
            cmbCategoria.Items.Add("Tecnología");
            cmbCategoria.Items.Add("Hogar");
            cmbCategoria.Items.Add("Ropa");
            cmbCategoria.SelectedIndex = 0; // Selecciona la primera por defecto

            this.BackColor = Color.WhiteSmoke;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Font = new Font("Segoe UI", 10, FontStyle.Regular);



            dgvDatos.ReadOnly = true;
            dgvDatos.ReadOnly = true;
            dgvDatos.ClearSelection(); // Quita selección inicial
            dgvDatos.DefaultCellStyle.SelectionBackColor = dgvDatos.DefaultCellStyle.BackColor;
            dgvDatos.DefaultCellStyle.SelectionForeColor = dgvDatos.DefaultCellStyle.ForeColor;
            dgvDatos.AllowUserToAddRows = false;
            dgvDatos.AllowUserToDeleteRows = false;

            // Estilo general
            // Estilo general
            dgvDatos.EnableHeadersVisualStyles = false;
            dgvDatos.BorderStyle = BorderStyle.None;
            dgvDatos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvDatos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            // Colores de cabecera
            dgvDatos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 122, 204); // Azul moderno
            dgvDatos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDatos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Estilo de celdas
            dgvDatos.DefaultCellStyle.BackColor = Color.White;
            dgvDatos.DefaultCellStyle.ForeColor = Color.Black;
            dgvDatos.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dgvDatos.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgvDatos.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Alternancia de colores en filas
            dgvDatos.RowsDefaultCellStyle.BackColor = Color.White;
            dgvDatos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240); // Gris claro

            // Ajustes de comportamiento
            dgvDatos.RowHeadersVisible = false;
            dgvDatos.AllowUserToResizeRows = false;

            dgvDatos.BorderStyle = BorderStyle.None;
            dgvDatos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvDatos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            // Colores de cabecera
            dgvDatos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 122, 204); // Azul moderno
            dgvDatos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDatos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Estilo de celdas
            dgvDatos.DefaultCellStyle.BackColor = Color.White;
            dgvDatos.DefaultCellStyle.ForeColor = Color.Black;
            dgvDatos.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            dgvDatos.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgvDatos.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Alternancia de colores en filas
            dgvDatos.RowsDefaultCellStyle.BackColor = Color.White;
            dgvDatos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240); // Gris claro

            // Ajustes de comportamiento
            dgvDatos.RowHeadersVisible = false;
            dgvDatos.AllowUserToResizeRows = false;
            dgvDatos.AllowUserToResizeColumns = false;

        }
        private void MostrarProductos()
        {
            string query = "SELECT * FROM Productos";

            using (SqlConnection conexion = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Comercio;Trusted_Connection=True;"))
            {
                try
                {
                    SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    dgvDatos.DataSource = tabla;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar productos: " + ex.Message);
                }
            }
        }
        

        public void MoverDatos()
        {
            // Obtener valores
            string nombre = txtNombre.Text;
            string descripcion = txtDescripcion.Text;
            decimal precio;
            int stock = (int)numStock.Value;
            int categoriaId = cmbCategoria.SelectedIndex + 1; // Tecnología = 1, Hogar = 2, Ropa = 3

            if (!decimal.TryParse(txtPrecio.Text, out precio))
            {
                MessageBox.Show("Precio inválido. Ingrese un número válido.");
                return;
            }

            string query = "INSERT INTO Productos (Nombre, Descripcion, Precio, Stock, CategoriaId) " +
                           "VALUES (@Nombre, @Descripcion, @Precio, @Stock, @CategoriaId)";

            using (SqlConnection conexion = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Comercio;Trusted_Connection=True;"))
            {
                try
                {
                    conexion.Open();
                    SqlCommand comando = new SqlCommand(query, conexion);
                    comando.Parameters.AddWithValue("@Nombre", nombre);
                    comando.Parameters.AddWithValue("@Descripcion", descripcion);
                    comando.Parameters.AddWithValue("@Precio", precio);
                    comando.Parameters.AddWithValue("@Stock", stock);
                    comando.Parameters.AddWithValue("@CategoriaId", categoriaId);

                    comando.ExecuteNonQuery();
                    MessageBox.Show("Producto agregado correctamente.");
                    MostrarProductos(); // Actualizar el DataGridView
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar producto: " + ex.Message);
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            MoverDatos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int codigo;
            if (!int.TryParse(txtID.Text, out codigo))
            {
                MessageBox.Show("Ingrese un código válido.");
                return;
            }

            string query = "DELETE FROM Productos WHERE Codigo = @Codigo";

            using (SqlConnection conexion = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Comercio;Trusted_Connection=True;"))
            {
                try
                {
                    conexion.Open();
                    SqlCommand comando = new SqlCommand(query, conexion);
                    comando.Parameters.AddWithValue("@Codigo", codigo);

                    int filasAfectadas = comando.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Producto eliminado correctamente.");
                        MostrarProductos(); // Actualiza la grilla
                    }
                    else
                    {
                        MessageBox.Show("No se encontró un producto con ese código.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el producto: " + ex.Message);
                }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            string codigo = txtID.Text.Trim(); // ahora es string

            if (string.IsNullOrEmpty(codigo))
            {
                MessageBox.Show("Ingrese un código válido.");
                return;
            }

            string nombre = txtNombre.Text;
            string descripcion = txtDescripcion.Text;
            decimal precio;
            int stock = (int)numStock.Value;
            int categoriaId = cmbCategoria.SelectedIndex + 1;

            if (!decimal.TryParse(txtPrecio.Text, out precio))
            {
                MessageBox.Show("Precio inválido. Ingrese un número válido.");
                return;
            }

            string query = "UPDATE Productos SET Nombre = @Nombre, Descripcion = @Descripcion, Precio = @Precio, Stock = @Stock, CategoriaId = @CategoriaId " +
                           "WHERE Codigo = @Codigo";

            using (SqlConnection conexion = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Comercio;Trusted_Connection=True;"))
            {
                try
                {
                    conexion.Open();
                    SqlCommand comando = new SqlCommand(query, conexion);
                    comando.Parameters.AddWithValue("@Nombre", nombre);
                    comando.Parameters.AddWithValue("@Descripcion", descripcion);
                    comando.Parameters.AddWithValue("@Precio", precio);
                    comando.Parameters.AddWithValue("@Stock", stock);
                    comando.Parameters.AddWithValue("@CategoriaId", categoriaId);
                    comando.Parameters.AddWithValue("@Codigo", codigo); // ahora es string

                    int filasAfectadas = comando.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Producto modificado correctamente.");
                        MostrarProductos();
                    }
                    else
                    {
                        MessageBox.Show("No se encontró un producto con ese código.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al modificar el producto: " + ex.Message);
                }
            }
        }
        private void BuscarProductoPorCodigo()
        {
            int codigo;
            if (!int.TryParse(txtID.Text, out codigo))
            {
                MessageBox.Show("Ingrese un código válido.");
                return;
            }

            string query = "SELECT * FROM Productos WHERE Codigo = @Codigo";

            using (SqlConnection conexion = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=Comercio;Trusted_Connection=True;"))
            {
                try
                {
                    SqlDataAdapter adaptador = new SqlDataAdapter(query, conexion);
                    adaptador.SelectCommand.Parameters.AddWithValue("@Codigo", codigo);

                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);

                    if (tabla.Rows.Count > 0)
                    {
                        dgvDatos.DataSource = tabla;
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ningún producto con ese código.");
                        dgvDatos.DataSource = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar producto: " + ex.Message);
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarProductoPorCodigo();
        }

        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            MostrarProductos();
        }
    }
}

