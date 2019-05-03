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

namespace TEl_Rehber
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static string conString = "Server=.\\sqlexpress;Database=TelRehber;User Id=sa;Password=saw;";
       
        SqlConnection baglanti = new SqlConnection(conString);
        DataSet ds = new DataSet();



        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                    baglanti.Open();
          
                string kayit = "INSERT INTO Tbl_Kisiler (AD, SOYAD, CEP_TEL, IS_TEL, EMAIL, ADRES) VALUES ( @AD,@SOYAD,@CEPTEL,@ISTEL,@EMAIL,@ADRES)";
            
                SqlCommand komut = new SqlCommand(kayit, baglanti);
              
                komut.Parameters.AddWithValue("@AD", textBox1.Text);
                komut.Parameters.AddWithValue("@SOYAD", textBox2.Text);
                komut.Parameters.AddWithValue("@CEPTEL", textBox3.Text);
                komut.Parameters.AddWithValue("@ISTEL", textBox4.Text);
                komut.Parameters.AddWithValue("@EMAIL", textBox5.Text);
                komut.Parameters.AddWithValue("@ADRES", richTextBox1.Text);
                //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
                komut.ExecuteNonQuery();
                //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
         
                MessageBox.Show(" Kayıt İşlemi Gerçekleşti.","Bilgi", MessageBoxButtons.OK,MessageBoxIcon.Information);
                //
                baglanti.Close();
            }
            catch (Exception hata)
            {
                baglanti.Close();
                MessageBox.Show("İşlem Sırasında Hata Oluştu, bilgiler kaydedilemedi."+hata ,"dikkat",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'telRehberDataSet1.Tbl_Kisiler' table. You can move, or remove it, as needed.
            this.tbl_KisilerTableAdapter.Fill(this.telRehberDataSet1.Tbl_Kisiler);
            // TODO: This line of code loads data into the 'telRehberDataSet.Tbl_Kisiler' table. You can move, or remove it, as needed.
            this.tbl_KisilerTableAdapter.Fill(this.telRehberDataSet.Tbl_Kisiler);

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (ds.Tables.Count > 0)
                dataGridView1.Columns.Clear();
            ds.Tables.Clear();
            dataGridView1.Refresh();






            baglanti.Open();
            try
            {
                dataGridView1.DataSource = null; //Her click de datasource u null a eşitleyip içeriğini temizliyoruz


                string Sqlcmd = "SELECT AD 'ADI', SOYAD 'SOYADI', CEP_TEL 'CEP TELEFONU', IS_TEL 'İŞ TELEFONU', EMAIL 'MAİL ADRESİ', ADRES 'ADRES'  FROM Tbl_Kisiler";
                SqlDataAdapter da = new SqlDataAdapter(Sqlcmd, baglanti);//dataapter nesnesini oluşturup sqlCmd sorgu cümlesini ve sqlCon veritabanı bağlantımızı yazıyoruz
                //dataset nesnesini oluşturuyoruz
                da.Fill(ds, "Tbl_Kisiler");//sqlCmd sorgusundan gelen veriyi dataset nesnesine ekliyoruz. ben burada table ismi için Person dedim siz başka bir isimde verebilirsiniz

                if (ds.Tables[0].Rows.Count == 0)//Person tablosunda herhangi bir veri yoksa (boşsa) aşağıdaki blok çalışacak
                {
                   
                    return;//kayıt olmadığı için return ile bloğun dışına çıkıyoruz
                }
                else//kayıt varsa
                {
                    dataGridView1.AutoResizeColumns();
                    dataGridView1.AutoSizeColumnsMode =         DataGridViewAutoSizeColumnsMode.AllCells;
                    dataGridView1.DataSource = ds.Tables["Tbl_Kisiler"];//sqlCmd sorgusu ile çektiğimiz kayıtlar datagridview1 üzerinde gösteriliyor
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Hata : " + ex); //Veritabanına bağlantı sırasında alınan bir hata varsa burada gösteriliyor

            }
            finally //button1_Click olduğu sürece bu bloğa uğramadan uygulama sonlanmıyor
            {
                baglanti.Close(); //Açık olan Sql bağlantısı sonlandırılıyor

            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            int SecilenSatir;
            SecilenSatir = dataGridView1.CurrentRow.Index;

            textBox1.Text = dataGridView1.Rows[SecilenSatir].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[SecilenSatir].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[SecilenSatir].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[SecilenSatir].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.Rows[SecilenSatir].Cells[4].Value.ToString();
            richTextBox1.Text = dataGridView1.Rows[SecilenSatir].Cells[5].Value.ToString();
        }
       

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            


      
           
         
        }
    }
}
