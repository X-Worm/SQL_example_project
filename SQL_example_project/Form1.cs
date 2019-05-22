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

namespace SQL_example_project
{
    public partial class Form1 : Form
    {
        string conn_string = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=D:\SQL\SQLEXPRESS.MDF;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sQLEXPRESSDataSet.Pdf_Files' table. You can move, or remove it, as needed.
            this.pdf_FilesTableAdapter.Fill(this.sQLEXPRESSDataSet.Pdf_Files);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cmd_text;
            Form2 f2 = new Form2();
            if (f2.ShowDialog() == DialogResult.OK) {
                cmd_text = "INSERT INTO Pdf_Files VALUES (" +
                    "'" + f2.textBox1.Text + "' , '" + f2.textBox2.Text +
                    "' , " + f2.textBox3.Text + ")";

                SqlConnection sql_conn = new SqlConnection(conn_string);

                SqlCommand sql_comm = new SqlCommand(cmd_text, sql_conn);

                sql_conn.Open();
                sql_comm.ExecuteNonQuery();
                sql_conn.Close();

                this.pdf_FilesTableAdapter.Fill(this.sQLEXPRESSDataSet.Pdf_Files);
                dataGridView1.Refresh();
                dataGridView1.DataSource = this.sQLEXPRESSDataSet.Pdf_Files;
            }
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.pdf_FilesTableAdapter.FillBy(this.sQLEXPRESSDataSet.Pdf_Files);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string cmd_text;
            Form2 f2 = new Form2();
            int index;
            string Pdf_Name;

            index = dataGridView1.CurrentRow.Index;
            Pdf_Name = Convert.ToString(dataGridView1[1, index].Value);
            string PdfID = Convert.ToString(dataGridView1[0, index].Value);

            f2.textBox1.Text = Pdf_Name;
            f2.textBox2.Text = Convert.ToString(dataGridView1[2, index].Value);
            f2.textBox3.Text = Convert.ToString(dataGridView1[3, index].Value);


            if (f2.ShowDialog() == DialogResult.OK)
            {
                cmd_text = "UPDATE Pdf_Files SET [PdfName] = '" + f2.textBox1.Text + "', " +
                    "[Price] = " + f2.textBox3.Text + " , " +
                "[Tasks] = '" + f2.textBox2.Text + "' " +
           
                "WHERE PdfID = " + PdfID + "";

                SqlConnection sql_conn = new SqlConnection(conn_string);
                SqlCommand sql_comm = new SqlCommand(cmd_text, sql_conn);

                sql_conn.Open();
                sql_comm.ExecuteNonQuery();
                sql_conn.Close();

                this.pdf_FilesTableAdapter.Fill(this.sQLEXPRESSDataSet.Pdf_Files);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string cmd_text = "DELETE FROM Pdf_Files";
            int index;
            string PdfID;

            index = dataGridView1.CurrentRow.Index;
            PdfID = Convert.ToString(dataGridView1[0, index].Value);
            cmd_text = "DELETE FROM Pdf_Files WHERE PdfID = " + PdfID + "";

            SqlConnection sql_conn = new SqlConnection(conn_string);
            SqlCommand sql_comm = new SqlCommand(cmd_text, sql_conn);

            sql_conn.Open();
            sql_comm.ExecuteNonQuery();
            sql_conn.Close();

            
        }
    }
}
