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
	public partial class Adresler : Form
	{
		public Adresler()
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
		public void AdresGetir() 
		{
			SqlCommand AdresGetir = new SqlCommand("SELECT AdresAciklama FROM Adresler;", conn);
			conn.Open();
			SqlDataReader AdresOku = AdresGetir.ExecuteReader();
			while (AdresOku.Read())
			{
				CmbAdresSil.Items.Add(AdresOku.GetString(0));
			}
			conn.Close();
			AdresOku.Close();
		}
		public void acilirsayfa()
		{
			ClearComboBoxItems(this);
			ClearComboBoxes();
			foreach (Control control in this.Controls)
			{
				if (control is TextBox textBox)
				{
					textBox.Clear();
				}
			}
			SqlCommand UyeGetir = new SqlCommand("SELECT CONCAT(UyeAdi,UyeSoyadi) From Uyeler", conn);
			conn.Open();
			SqlDataReader UyeOku = UyeGetir.ExecuteReader();
			while (UyeOku.Read())
			{
				CmbAdresEkleÜye.Items.Add(UyeOku.GetString(0));
				CmbAdresGüncelleÜye.Items.Add(UyeOku.GetString(0));
			}
			conn.Close();
			UyeOku.Close();
			dataGridView1.DataSource = null;
			SqlCommand datagridviewdoldur = new SqlCommand("SELECT U.UyeAdi, U.UyeSoyadi, U.UyeTelefon, A.AdresAciklama, A.Ulke, A.Sehir, A.Ilce, A.PostaKodu FROM Uyeler U LEFT JOIN Adresler A ON U.UyeID = A.UyeID;", conn);
			conn.Open();
			SqlDataReader verilerioku = datagridviewdoldur.ExecuteReader();
			DataTable dt = new DataTable();
			dt.Load(verilerioku);
			verilerioku.Close();
			conn.Close();
			AdresGetir();
			dataGridView1.DataSource = dt;
			dataGridView1.Columns[0].Width = 156;
			dataGridView1.Columns[1].Width = 156;
			dataGridView1.Columns[2].Width = 156;
			dataGridView1.Columns[3].Width = 156;
			dataGridView1.Columns[4].Width = 156;
			dataGridView1.Columns[5].Width = 156;
			dataGridView1.Columns[6].Width = 156;
			dataGridView1.Columns[7].Width = 156;
		}

		private void Adresler_Load(object sender, EventArgs e)
		{
			acilirsayfa();
		}
		private void BtnCevirmenEkle_Click(object sender, EventArgs e)
		{
			int adresid = 0;
			SqlCommand AdresIDGetir = new SqlCommand("SELECT MAX(AdresID) FROM Adresler", conn);
			conn.Open();
			SqlDataReader AdresIDOku = AdresIDGetir.ExecuteReader();
			while (AdresIDOku.Read())
			{
				if (AdresIDOku.IsDBNull(0))
				{
					adresid = 0;
				}
				else
				{
					adresid = AdresIDOku.GetInt32(0);
				}
			}
			conn.Close();
			AdresIDOku.Close();
			int uyeid = 0;
			SqlCommand UyeIDGetir = new SqlCommand("SELECT UyeID From Uyeler WHERE CONCAT(UyeAdi,UyeSoyadi)=@prm1", conn);
			UyeIDGetir.Parameters.AddWithValue("@prm1", CmbAdresEkleÜye.SelectedItem.ToString());
			conn.Open();
			SqlDataReader UyeIDOku= UyeIDGetir.ExecuteReader();
			while (UyeIDOku.Read())
			{
				uyeid=UyeIDOku.GetInt32(0);
			}
			conn.Close();
			UyeIDOku.Close();
			SqlCommand AdresEkle = new SqlCommand("INSERT INTO Adresler (AdresID,UyeID, AdresAciklama, Ulke, Sehir, Ilce, PostaKodu) VALUES (@prm1,@prm2,@prm3,@prm4, @prm5,@prm6,@prm7 );", conn);
			AdresEkle.Parameters.AddWithValue("@prm1", adresid + 1);
			AdresEkle.Parameters.AddWithValue("@prm2", uyeid);
			AdresEkle.Parameters.AddWithValue("@prm3", RchÜyeAdresTxt.Text.ToString());
			AdresEkle.Parameters.AddWithValue("@prm4", TxtAdresEkleUlke.Text.ToString());
			AdresEkle.Parameters.AddWithValue("@prm5", TxtAdresEkleSehir.Text.ToString());
			AdresEkle.Parameters.AddWithValue("@prm6", TxtAdresEkleİlçe.Text.ToString());
			AdresEkle.Parameters.AddWithValue("@prm7", TxtAdresEklePostaKodu.Text.ToString());
			conn.Open();
			AdresEkle.ExecuteNonQuery();
			conn.Close();
			MessageBox.Show(CmbAdresEkleÜye.SelectedItem.ToString()+" "+"Adlı Üyeye Adres Başarıyla Eklendi !");
			acilirsayfa();
		}

		private void BtnAdresGüncelle_Click(object sender, EventArgs e)
		{
			int uyeid = 0;
			SqlCommand UyeIDGetir = new SqlCommand("SELECT UyeID From Uyeler WHERE CONCAT(UyeAdi,UyeSoyadi)=@prm1", conn);
			UyeIDGetir.Parameters.AddWithValue("@prm1", CmbAdresGüncelleÜye.SelectedItem.ToString());
			conn.Open();
			SqlDataReader UyeIDOku = UyeIDGetir.ExecuteReader();
			while (UyeIDOku.Read())
			{
				uyeid = UyeIDOku.GetInt32(0);
			}
			conn.Close();
			UyeIDOku.Close();
			int adresid = 0;
			SqlCommand AdresIDGetir = new SqlCommand("SELECT AdresID FROM Adresler WHERE UyeID = @prm1;", conn);
			AdresIDGetir.Parameters.AddWithValue("@prm1", uyeid);
			conn.Open();
			SqlDataReader AdresIDOku = AdresIDGetir.ExecuteReader();
			while (AdresIDOku.Read())
			{
				adresid=AdresIDOku.GetInt32(0);
			}
			conn.Close();
			AdresIDOku.Close();
			SqlCommand AdresGüncelle = new SqlCommand("UPDATE Adresler SET AdresAciklama = @prm1, Ulke = @prm2, Sehir = @prm3, Ilce = @prm4, PostaKodu = @prm5 WHERE AdresID = @prm6;", conn);
			AdresGüncelle.Parameters.AddWithValue("@prm1", RchGüncelleAdres.Text.ToString());
			AdresGüncelle.Parameters.AddWithValue("@prm2", TxtAdresGüncelleÜlke.Text.ToString());
			AdresGüncelle.Parameters.AddWithValue("@prm3", TxtAdresGüncelleSehir.Text.ToString());
			AdresGüncelle.Parameters.AddWithValue("@prm4", TxtAdresGüncelleİlce.Text.ToString());
			AdresGüncelle.Parameters.AddWithValue("@prm5", TxtAdresGüncellePostaKodu.Text.ToString());
			AdresGüncelle.Parameters.AddWithValue("@prm6", adresid);
			conn.Open();
			AdresGüncelle.ExecuteNonQuery();
			conn.Close();
			MessageBox.Show(CmbAdresGüncelleÜye.SelectedItem.ToString()+" "+"Adlı Üyenin Adresi Başarıyla Güncellendi !");
			acilirsayfa();
		}

		private void BtnAdresSil_Click(object sender, EventArgs e)
		{
			int adresid = 0;
			SqlCommand AdresIDGetir=new SqlCommand("SELECT AdresID FROM Adresler WHERE AdresAciklama =@prm1;",conn);
			AdresIDGetir.Parameters.AddWithValue("@prm1", CmbAdresSil.SelectedItem.ToString());
			conn.Open();
			SqlDataReader AdresOku = AdresIDGetir.ExecuteReader();
			while (AdresOku.Read())
			{
				adresid = AdresOku.GetInt32(0);
			}
			conn.Close();
			AdresOku.Close();
			SqlCommand AdresSil = new SqlCommand("DELETE FROM Adresler WHERE AdresID = @prm1;", conn);
			AdresSil.Parameters.AddWithValue("@prm1", adresid);
			conn.Open();
			AdresSil.ExecuteNonQuery();
			conn.Close();
			MessageBox.Show("Adres Başarıyla Silindi !");
			acilirsayfa();



		}

		private void BtnKitapTürlerFrm_Click(object sender, EventArgs e)
		{
			Türler trl = new Türler();
			trl.ShowDialog();
			this.Hide();
		}

		private void BtnYayinEvleriFrm_Click(object sender, EventArgs e)
		{
			YayinevleriAdminForm yayinevlerfrm = new YayinevleriAdminForm();
			yayinevlerfrm.ShowDialog();
			this.Hide();
		}

		private void BtnYazarlarFrm_Click(object sender, EventArgs e)
		{
			Yazarlar yzrl = new Yazarlar();
			yzrl.ShowDialog();
			this.Hide();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			Uyeler uylr = new Uyeler();
			uylr.ShowDialog();
			this.Hide();
		}

		private void BtnCevirmenlerFrm_Click(object sender, EventArgs e)
		{
			Çevirmenler cevirmenlr = new Çevirmenler();
			cevirmenlr.ShowDialog();
			this.Hide();
		}

		private void BtnKitaplarFrm_Click(object sender, EventArgs e)
		{
			Kitaplar kitplr = new Kitaplar();
			kitplr.ShowDialog();
			this.Hide();
		}

		private void BtnAdreslerFrm_Click(object sender, EventArgs e)
		{
			Adresler adrsl = new Adresler();
			adrsl.ShowDialog();
			this.Hide();
		}
	}
}
