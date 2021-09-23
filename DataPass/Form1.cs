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

namespace DataPass
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-OC5036T\MSSQLSERVER1;Initial Catalog=test;Integrated Security=True");

        void list()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from TblData", sqlConnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string name = TxtName.Text;
            byte[] namearray = ASCIIEncoding.ASCII.GetBytes(name);
            string namepassdata = Convert.ToBase64String(namearray);

            string surname = TxtSurname.Text;
            byte[] surnamearray = ASCIIEncoding.ASCII.GetBytes(surname);
            string surnamepassdata = Convert.ToBase64String(surnamearray);

            string mail = TxtMail.Text;
            byte[] mailarray = ASCIIEncoding.ASCII.GetBytes(mail);
            string mailpassdata = Convert.ToBase64String(mailarray);

            string userpass = TxtPass.Text;
            byte[] userpassarray = ASCIIEncoding.ASCII.GetBytes(userpass);
            string userpasspassdata = Convert.ToBase64String(userpassarray);

            string AccountNo = TxtAccountNo.Text;
            byte[] AccountNoarray = ASCIIEncoding.ASCII.GetBytes(AccountNo);
            string AccountNopassdata = Convert.ToBase64String(AccountNoarray);

            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("insert into TblData (Name,Surname,Mail,Pass,AccountNo) values (@p1,@p2,@p3,@p4,@p5)", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@p1", namepassdata);
            sqlCommand.Parameters.AddWithValue("@p2", surnamepassdata);
            sqlCommand.Parameters.AddWithValue("@p3", mailpassdata);
            sqlCommand.Parameters.AddWithValue("@p4", userpasspassdata);
            sqlCommand.Parameters.AddWithValue("@p5", AccountNopassdata);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            MessageBox.Show("kayıt başarılı");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //list();

            dataGridView1.DataSource = null;
            dataGridView1.ColumnCount = 6;
            dataGridView1.Columns[0].Name = "ID";
            dataGridView1.Columns[1].Name = "ADI";
            dataGridView1.Columns[2].Name = "SOYADI";
            dataGridView1.Columns[3].Name = "MAIL";
            dataGridView1.Columns[4].Name = "SIFRE";
            dataGridView1.Columns[5].Name = "HESAPNO";

            sqlConnection.Open();
            SqlCommand komut = new SqlCommand("select*from TblData", sqlConnection);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                byte[] adcozum = Convert.FromBase64String(dr[1].ToString());
                string ad = ASCIIEncoding.ASCII.GetString(adcozum);
                byte[] soyadcozum = Convert.FromBase64String(dr[2].ToString());
                string soyad = ASCIIEncoding.ASCII.GetString(soyadcozum);
                byte[] mailcozum = Convert.FromBase64String(dr[3].ToString());
                string mail = ASCIIEncoding.ASCII.GetString(mailcozum);
                byte[] sifrecozum = Convert.FromBase64String(dr[4].ToString());
                string sifre = ASCIIEncoding.ASCII.GetString(sifrecozum);
                byte[] hesapnocozum = Convert.FromBase64String(dr[5].ToString());
                string hesapno = ASCIIEncoding.ASCII.GetString(hesapnocozum);

                string[] veriler = { dr[0].ToString(), ad, soyad, mail, sifre, hesapno };
                dataGridView1.Rows.Add(veriler);
            }
        }

            private void button2_Click(object sender, EventArgs e)
            {

            }

            private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
            {
                string namesolution = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                byte[] namesolutionarray = Convert.FromBase64String(namesolution);
                string namedata = ASCIIEncoding.ASCII.GetString(namesolutionarray);
                TxtName.Text = namedata;

                string surname = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                byte[] surnamearray = Convert.FromBase64String(surname);
                string surnamedata = ASCIIEncoding.ASCII.GetString(surnamearray);
                TxtSurname.Text = surnamedata;

                string mail = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                byte[] mailarray = Convert.FromBase64String(mail);
                string maildata = ASCIIEncoding.ASCII.GetString(mailarray);
                TxtMail.Text = maildata;

                string pass = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                byte[] passarray = Convert.FromBase64String(pass);
                string passdata = ASCIIEncoding.ASCII.GetString(passarray);
                TxtPass.Text = passdata;

                string accountno = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                byte[] accountnoarray = Convert.FromBase64String(accountno);
                string accountdata = ASCIIEncoding.ASCII.GetString(accountnoarray);
                TxtAccountNo.Text = accountdata;
            }
        }
    } 
