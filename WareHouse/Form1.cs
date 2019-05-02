using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WareHouse
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection;

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Бауыржан\Мой документы\Visual Studio 2015\Projects\WareHouse\WareHouse\CityDB.mdf;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [City]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(sqlReader["Id"] + "\t " + Convert.ToString(sqlReader["City"]) + "\t " + sqlReader["PhoneNumber"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
            //    sqlConnection.Close();
            Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("INSERT INTO [City] (City, PhoneNumber)VALUES(@City, @PhoneNumber)", sqlConnection);

            command.Parameters.AddWithValue("City", textBox1.Text);
            command.Parameters.AddWithValue("PhoneNumber", textBox4.Text);

            await command.ExecuteNonQueryAsync();
        }

        private async void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [City]", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(sqlReader["Id"] + "\t " + Convert.ToString(sqlReader["City"]) + sqlReader["PhoneNumber"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text) &&
               !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) &&
               !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text))
            {
                SqlCommand command = new SqlCommand("UPDATE [City] SET [City]=@City, [PhoneNumber]=@PhoneNumber WHERE [Id]=@Id", sqlConnection);

                command.Parameters.AddWithValue("Id", textBox6.Text);
                command.Parameters.AddWithValue("City", textBox2.Text);
                command.Parameters.AddWithValue("PhoneNumber", textBox5.Text);

                await command.ExecuteNonQueryAsync();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text))
            {
                SqlCommand command = new SqlCommand("DELETE FROM [City] WHERE [Id] = @Id", sqlConnection);

                command.Parameters.AddWithValue("Id", textBox3.Text);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
