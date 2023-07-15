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

namespace Kütüphane
{
	public partial class Yazarlar : Form
	{
		public Yazarlar()
		{
			InitializeComponent();
		}
		SqlConnection conn = new SqlConnection("Data Source=DESKTOP-RN1V6Q9;Initial Catalog=Kutuphane;Integrated Security=True");
		public void acilirsayfa()
		{
			CmbYazarGüncelle.Text = "";
			CmbYazarSil.Text = "";
			CmbYazarGüncelle.Items.Clear();
			CmbYazarSil.Items.Clear();
			TxtYazarAdiEkle.Text = "";
			TxtYazarSoyadiEkle.Text = "";
			SqlCommand yazargetir = new SqlCommand("SELECT YazarAdi,YazarSoyadi From Yazarlar;", conn);
			conn.Close();
			conn.Open();
			SqlDataReader yazaroku = yazargetir.ExecuteReader();
			while (yazaroku.Read())
			{
				CmbYazarGüncelle.Items.Add(yazaroku.GetString(0) + yazaroku.GetString(1));
				CmbYazarSil.Items.Add(yazaroku.GetString(0) + yazaroku.GetString(1));
			}
			yazaroku.Close();
			conn.Close();
			dataGridView1.DataSource = null;
			SqlCommand datagridviewdoldur = new SqlCommand("SELECT * FROM Yazarlar", conn);
			conn.Open();
			SqlDataReader verilerioku = datagridviewdoldur.ExecuteReader();
			DataTable dt = new DataTable();
			dt.Load(verilerioku);
			verilerioku.Close();
			conn.Close();
			dataGridView1.DataSource = dt;
			dataGridView1.Columns[0].Width = 465;
			dataGridView1.Columns[1].Width = 465;
			dataGridView1.Columns[2].Width = 465;
		}

		private void Yazarlar_Load(object sender, EventArgs e)
		{
			acilirsayfa();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Türler trl = new Türler();
			trl.ShowDialog();
			this.Hide();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			YayinevleriAdminForm yayinevleriAdminForm = new YayinevleriAdminForm();
			yayinevleriAdminForm.ShowDialog();
			this.Hide();
		}

		private void label6_Click(object sender, EventArgs e)
		{

		}

		private void BtnYazarEkle_Click(object sender, EventArgs e)
		{
			string yazaradi = TxtYazarAdiEkle.Text;
			string yazarsoyadi = TxtYazarSoyadiEkle.Text;
			SqlCommand yazaridögren = new SqlCommand("SELECT MAX(YazarID) From Yazarlar;", conn);
			conn.Open();
			SqlDataReader yazaridoku = yazaridögren.ExecuteReader();
			int yazarid = 0;
			while (yazaridoku.Read())
			{
				yazarid = yazaridoku.GetInt32(0);
			}
			yazaridoku.Close();
			conn.Close();
			SqlCommand yazarekle = new SqlCommand("INSERT INTO Yazarlar (YazarID,YazarAdi,YazarSoyadi) VALUES (@prm1,@prm2,@prm3)", conn);
			yazarekle.Parameters.AddWithValue("@prm1", yazarid + 1);
			yazarekle.Parameters.AddWithValue("@prm2", TxtYazarAdiEkle.Text.ToString());
			yazarekle.Parameters.AddWithValue("@prm3", TxtYazarSoyadiEkle.Text.ToString());
			conn.Open();
			yazarekle.ExecuteNonQuery();
			MessageBox.Show("Yazar Başarıyla Eklendi !");
			yazarid = 0;
			acilirsayfa();
		}

		private void BtnYazarSil_Click(object sender, EventArgs e)
		{
			string yazaradisoyadi = CmbYazarSil.Text.ToString();
			SqlCommand yazarsil = new SqlCommand("DELETE FROM Yazarlar WHERE CONCAT(YazarAdi,YazarSoyadi) = (@prm1);", conn);
			yazarsil.Parameters.AddWithValue("@prm1", yazaradisoyadi.ToString());
			conn.Open();
			yazarsil.ExecuteNonQuery();
			conn.Close();
			MessageBox.Show("Yazar Başarıyla Silindi !");
			acilirsayfa();
		}

		private void BtnYazarGüncelle_Click(object sender, EventArgs e)
		{
			SqlCommand yazargüncelle = new SqlCommand("UPDATE Yazarlar SET YazarAdi = (@prm1),YazarSoyadi=(@prm2)WHERE CONCAT(YazarAdi,YazarSoyadi) = (@prm3);", conn);
			yazargüncelle.Parameters.AddWithValue("@prm1", TxtYeniYazarAdi.Text);
			yazargüncelle.Parameters.AddWithValue("@prm2", TxtYeniYazarSoyadi.Text);
			yazargüncelle.Parameters.AddWithValue("@prm3", CmbYazarGüncelle.Text.ToString());
			conn.Open();
			yazargüncelle.ExecuteNonQuery();
			conn.Close();
			acilirsayfa();
		}
	}
}