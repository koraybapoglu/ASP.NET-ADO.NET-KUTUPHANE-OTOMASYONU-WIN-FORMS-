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
	public partial class Uyeler : Form
	{
		public Uyeler()
		{
			InitializeComponent();
		}
		SqlConnection conn = new SqlConnection("Data Source=DESKTOP-RN1V6Q9;Initial Catalog=Kutuphane;Integrated Security=True");
		public void ClearComboBoxItems(Control control)
		{
			foreach (Control childControl in control.Controls)
			{
				if (childControl is ComboBox comboBox)
				{
					comboBox.Items.Clear();
				}
				else if (childControl.HasChildren)
				{
					ClearComboBoxItems(childControl); // Konteyner kontrol içerisindeki alt öğeleri kontrol etmek için tekrar çağırılır
				}
			}
		}
		public void ClearComboBoxes()
		{
			foreach (Control control in this.Controls)
			{
				if (control is ComboBox comboBox)
				{
					comboBox.Text = string.Empty; // veya comboBox.Text = "";
				}
			}
		}
		public void acilirsayfa()
		{
			conn.Close();
			ClearComboBoxItems(this);
			ClearComboBoxes();
			foreach (Control control in this.Controls)
			{
				if (control is TextBox textBox)
				{
					textBox.Clear();
				}
			}
			SqlCommand UyeGetir = new SqlCommand("SELECT CONCAT(UyeAdi,UyeSoyadi) FROM Uyeler;", conn);

			conn.Open();
			SqlDataReader UyeOku = UyeGetir.ExecuteReader();
			while (UyeOku.Read())
			{
				CmbÜyeGüncelleSec.Items.Add(UyeOku.GetString(0));
				CmbUyeSil.Items.Add(UyeOku.GetString(0));
			}
			conn.Close();
			dataGridView1.DataSource = null;
			SqlCommand datagridviewdoldur = new SqlCommand("SELECT UyeID,UyeAdi,UyeSoyadi,UyeTelefon From Uyeler", conn);
			conn.Open();
			SqlDataReader verilerioku = datagridviewdoldur.ExecuteReader();
			DataTable dt = new DataTable();
			dt.Load(verilerioku);
			verilerioku.Close();
			conn.Close();
			dataGridView1.DataSource = dt;
			dataGridView1.Columns[0].Width = 313;
			dataGridView1.Columns[1].Width = 313;
			dataGridView1.Columns[2].Width = 313;
			dataGridView1.Columns[3].Width = 313;
		}

		private void BtnÜyeEkle_Click(object sender, EventArgs e)
		{
			int uyeid = 0;
			SqlCommand UyeIDGetir = new SqlCommand("SELECT MAX(UyeID) From Uyeler;", conn);
			conn.Open();
			SqlDataReader UyeIDOKU = UyeIDGetir.ExecuteReader();
			while (UyeIDOKU.Read())
			{
				if (UyeIDOKU.IsDBNull(0))
				{
					uyeid = 0;
				}
				else
				{
					uyeid = UyeIDOKU.GetInt32(0);
				}
			}
			conn.Close();
			UyeIDOKU.Close();
			SqlCommand ÜyeEkle = new SqlCommand("INSERT INTO Uyeler (UyeID,UyeAdi,UyeSoyadi,UyeTelefon) VALUES (@prm1,@prm2,@prm3,@prm4);", conn);
			ÜyeEkle.Parameters.AddWithValue("@prm1", uyeid + 1);
			ÜyeEkle.Parameters.AddWithValue("@prm2", TxtÜyeAdiEkle.Text.ToString());
			ÜyeEkle.Parameters.AddWithValue("@prm3", TxtÜyeSoyadiEkle.Text.ToString());
			ÜyeEkle.Parameters.AddWithValue("@prm4", MskTxtTelefonEkle.Text.ToString());
			conn.Open();
			ÜyeEkle.ExecuteNonQuery();
			MessageBox.Show(TxtÜyeAdiEkle.Text.ToString()+" "+TxtÜyeSoyadiEkle.Text.ToString()+" "+"Adlı Üye Başarıyla Eklendi !");
			conn.Close();
			acilirsayfa();

		}

		private void BtnUyeSil_Click(object sender, EventArgs e)
		{
			int uyeid = 0;
			SqlCommand UyeIDGetir = new SqlCommand("SELECT UyeID FROM Uyeler WHERE CONCAT(UyeAdi,UyeSoyadi) = @prm1;", conn);
			UyeIDGetir.Parameters.AddWithValue("@prm1", CmbUyeSil.SelectedItem.ToString());
			conn.Open();
			SqlDataReader UyeIDOku = UyeIDGetir.ExecuteReader();
			while (UyeIDOku.Read())
			{
				uyeid = UyeIDOku.GetInt32(0);
			}
			conn.Close();
			UyeIDOku.Close();
			SqlCommand UyeSil = new SqlCommand("DELETE FROM Uyeler WHERE UyeID = @prm1;", conn);
			UyeSil.Parameters.AddWithValue("@prm1", uyeid);
			conn.Open();
			UyeSil.ExecuteNonQuery();
			conn.Close();
			MessageBox.Show(CmbUyeSil.SelectedItem.ToString() + " " + "Adlı Üye Başarıyla Silindi !");
			acilirsayfa();
		}

		private void BtnCevirmenGüncelle_Click(object sender, EventArgs e)
		{
			int uyeid = 0;
			SqlCommand UyeIDGetir = new SqlCommand("SELECT UyeID FROM Uyeler WHERE CONCAT(UyeAdi,UyeSoyadi) = @prm1;", conn);
			UyeIDGetir.Parameters.AddWithValue("@prm1", CmbÜyeGüncelleSec.SelectedItem.ToString());
			conn.Open();
			SqlDataReader UyeIDOku = UyeIDGetir.ExecuteReader();
			while (UyeIDOku.Read())
			{
				uyeid = UyeIDOku.GetInt32(0);
			}
			conn.Close();
			UyeIDOku.Close();
			SqlCommand UyeGuncelle = new SqlCommand("UPDATE Uyeler SET UyeAdi = @prm1, UyeSoyadi = @prm2, UyeTelefon = @prm3 WHERE UyeID = @prm4;",conn);
			UyeGuncelle.Parameters.AddWithValue("@prm1",TxtÜyeAdiGüncelle.Text.ToString());
			UyeGuncelle.Parameters.AddWithValue("@prm2", TxtÜyeSoyadiGüncelle.Text.ToString());
			UyeGuncelle.Parameters.AddWithValue("@prm3", MskÜyeTelGüncelle.Text.ToString());
			UyeGuncelle.Parameters.AddWithValue("@prm4", uyeid);
			conn.Open();
			UyeGuncelle.ExecuteNonQuery();
			conn.Close();
			MessageBox.Show(CmbÜyeGüncelleSec.SelectedItem.ToString() + " " + "Adlı Üye Başarıyla Güncellendi !");
			acilirsayfa();
		}

		private void BtnKitapTürleriFrm_Click(object sender, EventArgs e)
		{
			Türler trl = new Türler();
			trl.ShowDialog();
			this.Hide();
		}

		private void Uyeler_Load(object sender, EventArgs e)
		{
			acilirsayfa();
		}

		private void BtnYayinevleriFrm_Click(object sender, EventArgs e)
		{
			YayinevleriAdminForm yayinevleri = new YayinevleriAdminForm();
			yayinevleri.ShowDialog();
			this.Hide();
		}

		private void BtnYazarlarFrm_Click(object sender, EventArgs e)
		{
			Yazarlar yzrl = new Yazarlar();
			yzrl.ShowDialog();
			this.Hide();
		}

		private void BtnUyelerFrm_Click(object sender, EventArgs e)
		{

		}

		private void BtnCevirmenlerFrm_Click(object sender, EventArgs e)
		{
			Çevirmenler cevirmenler = new Çevirmenler();
			cevirmenler.ShowDialog();
			this.Hide();
		}

		private void BtnKitaplarFrm_Click(object sender, EventArgs e)
		{
			Kitaplar kitaplar=new Kitaplar();
			kitaplar.ShowDialog();
			this.Hide();
		}
	}
}
