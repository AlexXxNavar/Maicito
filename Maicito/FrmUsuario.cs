using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Maicito
{
    public partial class FrmUsuario : Form
    {
        string NIT = "";
        int empleado = 0;
        public FrmUsuario()
        {
            InitializeComponent();
        }

        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            groupBox2.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;

        }

        void limpiar()
        {
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            comboBox1.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count - 1)
            {


                textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox6.Text = NIT;
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                label9.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                button3.Enabled = true;
                button2.Enabled = false;
                button1.Enabled = true;
                groupBox2.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection conex;
            SqlCommand comando;
            SqlDataAdapter adaptador;
            DataTable datos = new DataTable();
            string sql, cadena;
            cadena = "Server=LAPTOP-KVL566MP; Database=Maicito; User Id=sa; Password=database;";
            using (conex = new SqlConnection(cadena))
            {
                conex.Open();
                sql = "SELECT Usuario.*, Empleados.CodigoEmpleado, Empleados.Nombre, Empleados.Apellidos FROM Usuario INNER JOIN Empleados ON Usuario.IDEmpleado = Empleados.IDEmpleado;";
                comando = new SqlCommand(sql, conex);
                adaptador = new SqlDataAdapter(comando);
                adaptador.Fill(datos);
                dataGridView1.DataSource = datos;
                dataGridView1.Refresh();
                dataGridView1.AutoResizeColumns();
                groupBox2.Enabled = false;
            } // fin del using
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conex;
            SqlCommand comando;
            SqlParameter param;
            string sql, cadena, respuesta;
            cadena = "Server=LAPTOP-KVL566MP; Database=Maicito; User Id=sa; Password=database;";
            try
            {
                using (conex = new SqlConnection(cadena))
                {
                    conex.Open();
                    sql = "Update Empleados set CodigoEmpleado=@cui, Telefono=@dir,Nombre=@nom, Apellidos=@ape Where IdEmpleado=" + int.Parse(label9.Text);
                    comando = new SqlCommand(sql, conex);
                    param = new SqlParameter("@nom", textBox5.Text);
                    comando.Parameters.Add(param);
                    param = new SqlParameter("@ape", textBox7.Text);
                    comando.Parameters.Add(param);
                    param = new SqlParameter("@dir", comboBox1.Text);
                    comando.Parameters.Add(param);
                    param = new SqlParameter("@cui", textBox6.Text);
                    comando.Parameters.Add(param);
                    comando.ExecuteNonQuery();
                    respuesta = "Se ha editado el registro";
                    limpiar();
                    groupBox2.Enabled = false;
                }
            }
            catch (Exception error)
            {
                respuesta = "Error: " + error.Message;
            }
            MessageBox.Show(respuesta);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conex;
            SqlCommand comando;
            SqlParameter param;
            string sql, cadena, respuesta;
            cadena = "Server=LAPTOP-KVL566MP; Database=Maicito; User Id=sa; Password=database;";
            try
            {
                using (conex = new SqlConnection(cadena))
                {
                    conex.Open();
                    sql = "Insert Into Empleados (CodigoEmpleado, Telefono ,Nombre, Apellidos, Disponible) Values(@cui, @dir,@nom, @ape,@disp)";
                    comando = new SqlCommand(sql, conex);
                    param = new SqlParameter("@nom", textBox5.Text);
                    comando.Parameters.Add(param);
                    param = new SqlParameter("@ape", textBox7.Text);
                    comando.Parameters.Add(param);
                    param = new SqlParameter("@dir", comboBox1.Text);
                    comando.Parameters.Add(param);
                    param = new SqlParameter("@cui", textBox6.Text);
                    comando.Parameters.Add(param);
                    param = new SqlParameter("@disp", 1);
                    comando.Parameters.Add(param);
                    comando.ExecuteNonQuery();
                    respuesta = "Se ha creado un nuevo registro";
                    MessageBox.Show(respuesta, "Nuevo registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    button1.Enabled = true;
                    limpiar();
                    button2.Enabled = false;
                    groupBox2.Enabled = false;
                }
            }
            catch (Exception error)
            {
                respuesta = "Error: " + error.Message;

                MessageBox.Show(respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox2.Enabled = true;
            button1.Enabled = false;
            button2.Enabled = true;
            limpiar();
        }
    }
}
