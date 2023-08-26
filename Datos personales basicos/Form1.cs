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

namespace Datos_personales_basicos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // Coneccion del SQL server 
        SqlConnection conexion = new SqlConnection("server=LAPTOP-SHFRSP0C; database=Registros;integrated security=true");

        //llena todos los espacios al ser seleccionado por el usuario 
        public void llenar_tabla()
        {
            string consulta = "select * from Datospersonales";
            SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
            DataTable dt = new DataTable();
            adaptador.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        //es un metodo que sirve para que cuando preciones cualquier boton se limpie los espacios donde se agregan los campos.
        public void limpliar_campos()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        // Carga los datos que contega la base de datos SQL SERVER para mostrarlos en la pantalla gris
        private void Form1_Load(object sender, EventArgs e)
        {
            string consulta = "select * from Datospersonales";
            SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
            DataTable dt = new DataTable();
            adaptador.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        // Este es el boton de Agregar (agrega el contenido que ingrese el usuario)
        private void button1_Click(object sender, EventArgs e)
        {
            conexion.Open();
            string consulta = "insert into Datospersonales values(" + textBox1.Text + ",'" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "')";
            SqlCommand comando = new SqlCommand(consulta, conexion);
            comando.ExecuteNonQuery();
            MessageBox.Show("Registro agregado");

            llenar_tabla();
            limpliar_campos();

            conexion.Close();
        }

        // Este boton es para Eliminar un renglo de la tabla que selecciones.
        private void button2_Click(object sender, EventArgs e)
        {
            conexion.Open();

            String consulta = "delete from Datospersonales where Id = " + textBox1.Text + "";
            SqlCommand comando = new SqlCommand(consulta, conexion);
            comando.ExecuteNonQuery();

            MessageBox.Show("Registro eliminado");
            llenar_tabla();
            limpliar_campos();

            conexion.Close();
        }

        // Este boton es para Modificar el contenido de un renglon de la tabla (no se puede modificar el espacio de "codigo")
        private void button3_Click(object sender, EventArgs e)
        {
            conexion.Open();

            string consulta = "update Datospersonales set Id=" + textBox1.Text + ",Nombre= '" + textBox2.Text + "' ,Apellido= '" + textBox3.Text + "' ,Edad= '" + textBox4.Text + "' ,Numero_de_Telefono= '" + textBox5.Text + "' where Id= " + textBox1.Text + " ";
            SqlCommand comando = new SqlCommand(consulta, conexion);
            int cant;
            cant = comando.ExecuteNonQuery();
            if (cant > 0)
            {
                MessageBox.Show("Registro modificado");
            }

            llenar_tabla();
            limpliar_campos();

            conexion.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                
        }

        // Limpia los campos que estes llenos para agregar nuevos.
        private void button4_Click(object sender, EventArgs e)
        {
            limpliar_campos();
        }

        // cierra la aplicacion.
        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // es para poner los valores que contega la tabla al ser seleccionada y ser modificada
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedCells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedCells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedCells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedCells[3].Value.ToString();
            textBox5.Text = dataGridView1.SelectedCells[4].Value.ToString();
        }
    }
}
