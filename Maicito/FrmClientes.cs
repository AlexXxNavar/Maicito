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
    public partial class FrmClientes : Form
    {
        public FrmClientes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
            button1.Enabled = false;
            button2.Enabled = true;
            limpiar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conex;
            SqlCommand comando;
            SqlParameter param;
            string sql, cadena, respuesta;
            cadena = "Server=LAPTOP-KVL566MP; Database=Parqueo; User Id=sa; Password=B654681K;";
            try
            {
                using (conex = new SqlConnection(cadena))
                {
                    conex.Open();
                    sql = "Insert Into Piloto (CUIP, TipoSangre,TipoLicencia,FechaNacimiento, Direccion,Nombres, Apellidos, Plaza) Values(@cui,@tip1,@tip2,@fn, @dir,@nom, @ape,@pla)";
                    comando = new SqlCommand(sql, conex);
                    param = new SqlParameter("@nom", textBox1.Text);
                    comando.Parameters.Add(param);
                    param = new SqlParameter("@ape", textBox2.Text);
                    comando.Parameters.Add(param);
                    param = new SqlParameter("@fn", dateTimePicker1.Value);
                    comando.Parameters.Add(param);
                    param = new SqlParameter("@dir", textBox3.Text);
                    comando.Parameters.Add(param);
                    param = new SqlParameter("@cui", textBox4.Text);
                    comando.Parameters.Add(param);
                    param = new SqlParameter("@tip1", comboBox1.Text);
                    comando.Parameters.Add(param);
                    param = new SqlParameter("@tip2", comboBox2.Text);
                    comando.Parameters.Add(param);
                    param = new SqlParameter("@pla", 1);
                    comando.Parameters.Add(param);
                    comando.ExecuteNonQuery();
                    respuesta = "Se ha creado un nuevo registro";
                    MessageBox.Show(respuesta, "Nuevo registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    button1.Enabled = true;
                    limpiar();
                    button2.Enabled = false;
                    groupBox1.Enabled = false;
                }
            }
            catch (Exception error)
            {
                respuesta = "Error: " + error.Message;

                MessageBox.Show(respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conex;
            SqlCommand comando;
            SqlParameter param;
            string sql, cadena, respuesta;
            cadena = "Server=LAPTOP-KVL566MP; Database=Parqueo; User Id=sa; Password=B654681K;";
            try
            {
                using (conex = new SqlConnection(cadena))
                {
                    conex.Open();
                    sql = "Update Piloto set CUIP=@cui,TipoSangre=@tip1,TipoLicencia=@tip2,FechaNacimiento=@fn, Direccion=@dir,Nombres=@nom, Apellidos=@ape, Plaza=@pla Where IdPiloto=" + int.Parse(label9.Text);
                    comando = new SqlCommand(sql, conex);
                    param = new SqlParameter("@nom", textBox1.Text);
                    comando.Parameters.Add(param);
                    param = new SqlParameter("@ape", textBox2.Text);
                    comando.Parameters.Add(param);
                    param = new SqlParameter("@fn", dateTimePicker1.Value);
                    comando.Parameters.Add(param);
                    param = new SqlParameter("@dir", textBox3.Text);
                    comando.Parameters.Add(param);
                    param = new SqlParameter("@cui", textBox4.Text);
                    comando.Parameters.Add(param);
                    param = new SqlParameter("@tip1", comboBox1.Text);
                    comando.Parameters.Add(param);
                    param = new SqlParameter("@tip2", comboBox2.Text);
                    comando.Parameters.Add(param);
                    param = new SqlParameter("@pla", aux);
                    comando.Parameters.Add(param);
                    comando.ExecuteNonQuery();
                    respuesta = "Se ha editado el registro";
                    limpiar();
                    groupBox1.Enabled = false;
                }
            }
            catch (Exception error)
            {
                respuesta = "Error: " + error.Message;
            }
            MessageBox.Show(respuesta);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection conex;
            SqlCommand comando;
            SqlDataAdapter adaptador;
            DataTable datos = new DataTable();
            string sql, cadena;
            cadena = "Server=LAPTOP-KVL566MP; Database=Parqueo; User Id=sa; Password=B654681K;";
            using (conex = new SqlConnection(cadena))
            {
                conex.Open();
                sql = "Select * From Piloto";
                comando = new SqlCommand(sql, conex);
                adaptador = new SqlDataAdapter(comando);
                adaptador.Fill(datos);
                dataGridView1.DataSource = datos;
                dataGridView1.Refresh();
                dataGridView1.AutoResizeColumns();
                groupBox1.Enabled = false;
            } // fin del using
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count - 1)
            {
                aux = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString());

                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                label9.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                button3.Enabled = true;
                button2.Enabled = false;
                button1.Enabled = true;
                groupBox1.Enabled = true;
            }
        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;

        }

        void limpiar()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            aux = 0;
        }
    }
}
