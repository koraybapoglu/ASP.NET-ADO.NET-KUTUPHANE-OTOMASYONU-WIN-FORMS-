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
	public partial class Çevirmenler : Form
	{
		public Çevirmenler()
		{
			InitializeComponent();
		}
		SqlConnection conn = new SqlConnection("Data Source=DESKTOP-RN1V6Q9;Initial Catalog=Kutuphane;Integrated Security=True");
		public void acilirsayfa()
		{
			CmbCevirmenGüncelleSec.Text = "";
			CmbCevirmenSil.Text = "";
			CmbCevirmenGüncelleSec.Items.Clear();
			CmbCevirmenSil.Items.Clear();
			foreach (Control control in this.Controls)
			{
				if (control is TextBox textBox)
				{
					textBox.Clear();
				}
			}
			SqlCommand CevirmenGetir = new SqlCommand("SELECT CevirmenAdi,CevirmenSoyadi From Cevirmenler;", conn);
			conn.Close();
			conn.Open();
			SqlDataReader CevirmenOku = CevirmenGetir.ExecuteReader();
			while (CevirmenOku.Read())
			{
				CmbCevirmenGüncelleSec.Items.Add(CevirmenOku.GetString(0) + CevirmenOku.GetString(1));
				CmbCevirmenSil.Items.Add(CevirmenOku.GetString(0) + CevirmenOku.GetString(1));
			}
			CevirmenOku.Close();
			conn.Close();
			dataGridView1.DataSource = null;
			SqlCommand datagridviewdoldur = new SqlCommand("SELECT * FROM Cevirmenler", conn);
			conn.Open();
			SqlDataReader verilerioku = datagridviewdoldur.ExecuteReader();
			DataTable dt = new DataTable();
			dt.Load(verilerioku);
			verilerioku.Close();
			conn.Close();
			dataGridView1.DataSource = dt;
			dataGridView1.Columns[0].Width = 313;
			dataGridView1.Columns[1].Width = 314;
			dataGridView1.Columns[2].Width = 313;
			dataGridView1.Columns[3].Width = 314;
		}

		private void BtnKitapTürlerFrm_Click(object sender, EventArgs e)
		{
			Türler trl = new Türler();
			trl.ShowDialog();
			this.Hide();
		}

		private void BtnYayinEvleriFrm_Click(object sender, EventArgs e)
		{
			YayinevleriAdminForm yayinevlerifrm = new YayinevleriAdminForm();
			yayinevlerifrm.ShowDialog();
			this.Hide();
		}

		private void BtnYazarlarFrm_Click(object sender, EventArgs e)
		{
			Yazarlar yzrl = new Yazarlar();
			yzrl.ShowDialog();
			this.Hide();
		}

		private void button8_Click(object sender, EventArgs e)
		{

		}

		private void BtnCevirmenEkle_Click(object sender, EventArgs e)
		{
			int CevirmenID = 0;
			SqlCommand CevirmenIDGetir = new SqlCommand("SELECT MAX(CevirmenID) FROM Cevirmenler", conn);
			conn.Open();
			SqlDataReader CevirmenIDOku = CevirmenIDGetir.ExecuteReader();
			while (CevirmenIDOku.Read())
			{
				CevirmenID = CevirmenIDOku.GetInt32(0);
			}
			CevirmenIDOku.Close();
			conn.Close();
			SqlCommand CevirmenEkle = new SqlCommand("INSERT INTO Cevirmenler (CevirmenID,CevirmenAdi,CevirmenSoyadi,CevirdigiDil) VALUES (@prm1,@prm2,@prm3,@prm4)", conn);
			CevirmenEkle.Parameters.AddWithValue("@prm1", CevirmenID + 1);
			CevirmenEkle.Parameters.AddWithValue("@prm2", TxtCevirmenAdiEkle.Text.ToString());
			CevirmenEkle.Parameters.AddWithValue("@prm3", TxtCevirmenSoyadiEkle.Text.ToString());
			CevirmenEkle.Parameters.AddWithValue("@prm4", TxtCevirdigiDilEkle.Text.ToString());
			conn.Open();
			CevirmenEkle.ExecuteNonQuery();
			MessageBox.Show("Çevirmen Başarılı Bir Şekilde Eklendi !");
			conn.Close();
			acilirsayfa();
		}

		private void Çevirmenler_Load(object sender, EventArgs e)
		{
			acilirsayfa();
		}

		private void BtnCevirmenSil_Click(object sender, EventArgs e)
		{
			SqlCommand CevirmenSil = new SqlCommand("DELETE FROM Cevirmenler WHERE CONCAT(CevirmenAdi,CevirmenSoyadi) = (@prm1);", conn);
			CevirmenSil.Parameters.AddWithValue("@prm1", CmbCevirmenSil.SelectedItem.ToString());
			conn.Open();
			CevirmenSil.ExecuteNonQuery();
			MessageBox.Show(CmbCevirmenSil.SelectedItem.ToString() + " " + "Adlı Çevirmen Başarıyla Silindi ! ");
			conn.Close();
			acilirsayfa();
		}

		private void BtnCevirmenGüncelle_Click(object sender, EventArgs e)
		{
			SqlCommand CevirmenGüncelle = new SqlCommand("UPDATE Cevirmenler SET CevirmenAdi = @prm1 ,CevirmenSoyadi = @prm2,CevirdigiDil = @prm3 WHERE CONCAT(CevirmenAdi,CevirmenSoyadi) = @prm4;", conn);
			CevirmenGüncelle.Parameters.AddWithValue("@prm1", TxtYeniCevirmenAdiGüncelle.Text.ToString());
			CevirmenGüncelle.Parameters.AddWithValue("@prm2", TxtYeniCevirmenSoyadiGüncelle.Text.ToString());
			CevirmenGüncelle.Parameters.AddWithValue("@prm3", TxtYeniCevrilenDilGüncelle.Text.ToString());
			CevirmenGüncelle.Parameters.AddWithValue("@prm4", CmbCevirmenGüncelleSec.SelectedItem.ToString());
			conn.Open();
			CevirmenGüncelle.ExecuteNonQuery();
			MessageBox.Show(CmbCevirmenGüncelleSec.SelectedItem.ToString() + " " + "Adlı Çevirmen Başarıyla Güncellendi !");
			conn.Close();
			acilirsayfa();
		}

		private void BtnCevirmenEkle_Click_1(object sender, EventArgs e)
		{
			int CevirmenID = 0;
			SqlCommand CevirmenIDGetir = new SqlCommand("SELECT MAX(CevirmenID) FROM Cevirmenler", conn);
			conn.Open();
			SqlDataReader CevirmenIDOku = CevirmenIDGetir.ExecuteReader();
			while (CevirmenIDOku.Read())
			{
				CevirmenID = CevirmenIDOku.GetInt32(0);
			}
			CevirmenIDOku.Close();
			conn.Close();
			SqlCommand CevirmenEkle = new SqlCommand("INSERT INTO Cevirmenler (CevirmenID,CevirmenAdi,CevirmenSoyadi,CevirdigiDil) VALUES (@prm1,@prm2,@prm3,@prm4)", conn);
			CevirmenEkle.Parameters.AddWithValue("@prm1", CevirmenID + 1);
			CevirmenEkle.Parameters.AddWithValue("@prm2", TxtCevirmenAdiEkle.Text.ToString());
			CevirmenEkle.Parameters.AddWithValue("@prm3", TxtCevirmenSoyadiEkle.Text.ToString());
			CevirmenEkle.Parameters.AddWithValue("@prm4", TxtCevirdigiDilEkle.Text.ToString());
			conn.Open();
			CevirmenEkle.ExecuteNonQuery();
			MessageBox.Show("Çevirmen Başarılı Bir Şekilde Eklendi !");
			conn.Close();
			acilirsayfa();
		}

		private void BtnCevirmenSil_Click_1(object sender, EventArgs e)
		{
			SqlCommand CevirmenSil = new SqlCommand("DELETE FROM Cevirmenler WHERE CONCAT(CevirmenAdi,CevirmenSoyadi) = (@prm1);", conn);
			CevirmenSil.Parameters.AddWithValue("@prm1", CmbCevirmenSil.SelectedItem.ToString());
			conn.Open();
			CevirmenSil.ExecuteNonQuery();
			MessageBox.Show(CmbCevirmenSil.SelectedItem.ToString() + " " + "Adlı Çevirmen Başarıyla Silindi ! ");
			conn.Close();
			acilirsayfa();
		}

		private void BtnCevirmenGüncelle_Click_1(object sender, EventArgs e)
		{
			SqlCommand CevirmenGüncelle = new SqlCommand("UPDATE Cevirmenler SET CevirmenAdi = @prm1 ,CevirmenSoyadi = @prm2,CevirdigiDil = @prm3 WHERE CONCAT(CevirmenAdi,CevirmenSoyadi) = @prm4;", conn);
			CevirmenGüncelle.Parameters.AddWithValue("@prm1", TxtYeniCevirmenAdiGüncelle.Text.ToString());
			CevirmenGüncelle.Parameters.AddWithValue("@prm2", TxtYeniCevirmenSoyadiGüncelle.Text.ToString());
			CevirmenGüncelle.Parameters.AddWithValue("@prm3", TxtYeniCevrilenDilGüncelle.Text.ToString());
			CevirmenGüncelle.Parameters.AddWithValue("@prm4", CmbCevirmenGüncelleSec.SelectedItem.ToString());
			conn.Open();
			CevirmenGüncelle.ExecuteNonQuery();
			MessageBox.Show(CmbCevirmenGüncelleSec.SelectedItem.ToString() + " " + "Adlı Çevirmen Başarıyla Güncellendi !");
			conn.Close();
			acilirsayfa();
		}
	}
}
