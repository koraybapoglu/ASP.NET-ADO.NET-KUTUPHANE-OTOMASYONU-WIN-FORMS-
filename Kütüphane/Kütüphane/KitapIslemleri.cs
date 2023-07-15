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
	public partial class KitapIslemleri : Form
	{
		public KitapIslemleri()
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
		public void UyeGetir()
		{
			SqlCommand UyeGetir = new SqlCommand("SELECT CONCAT(UyeAdi,UyeSoyadi) From Uyeler", conn);
			conn.Open();
			SqlDataReader UyeOku = UyeGetir.ExecuteReader();
			while (UyeOku.Read())
			{
				CmbKitapİslemGüncelleÜye.Items.Add(UyeOku.GetString(0));
				CmbKitapİslemÜye.Items.Add(UyeOku.GetString(0));
			}
			conn.Close();
			UyeOku.Close();
		}
		public void KitapGetir()
		{
			SqlCommand KitapGetir = new SqlCommand("SELECT KitapAdi FROM Kitaplar", conn);
			conn.Open();
			SqlDataReader KitapOku = KitapGetir.ExecuteReader();
			while (KitapOku.Read())
			{
				CmbKitapİslemGüncelleKitap.Items.Add(KitapOku.GetString(0));
				CmbKitapİslemKitap.Items.Add(KitapOku.GetString(0));
			}
			conn.Close();
			KitapOku.Close();
		}
		public void acilirsayfa()
		{
			ClearComboBoxItems(this);
			ClearComboBoxes();
			UyeGetir();
			KitapGetir();
			foreach (Control control in this.Controls)
			{
				if (control is TextBox textBox)
				{
					textBox.Clear();
				}
			}
			SqlCommand IslemGetir = new SqlCommand("SELECT CONCAT(Uyeler.UyeAdi, Uyeler.UyeSoyadi, ' - ', Kitaplar.KitapAdi) AS BirlesikBilgi FROM Islemler INNER JOIN Uyeler ON Islemler.UyeID = Uyeler.UyeID INNER JOIN Kitaplar ON Islemler.KitapID = Kitaplar.KitapID;", conn);
			conn.Open();
			SqlDataReader IslemOku = IslemGetir.ExecuteReader();
			while (IslemOku.Read())
			{
				CmbKitapİslemSil.Items.Add(IslemOku.GetString(0));
				CmbKitapİslemGüncelleİslem.Items.Add(IslemOku.GetString(0));
			}
			conn.Close();
			IslemOku.Close();
			dataGridView1.DataSource = null;
			SqlCommand datagridviewdoldur = new SqlCommand("SELECT Islemler.IslemID, Uyeler.UyeAdi, Uyeler.UyeSoyadi, Kitaplar.KitapAdi, Islemler.AlinanTarih, Islemler.VerilenTarih, Islemler.IslemTuru FROM Islemler INNER JOIN Uyeler ON Islemler.UyeID = Uyeler.UyeID INNER JOIN Kitaplar ON Islemler.KitapID = Kitaplar.KitapID;", conn);
			conn.Open();
			SqlDataReader verilerioku = datagridviewdoldur.ExecuteReader();
			DataTable dt = new DataTable();
			dt.Load(verilerioku);
			verilerioku.Close();
			conn.Close();
			dataGridView1.DataSource = dt;
			dataGridView1.Columns[0].Width = 179;
			dataGridView1.Columns[1].Width = 179;
			dataGridView1.Columns[2].Width = 179;
			dataGridView1.Columns[3].Width = 179;
			dataGridView1.Columns[4].Width = 179;
			dataGridView1.Columns[5].Width = 179;
			dataGridView1.Columns[6].Width = 179;
		}

		private void KitapIslemleri_Load(object sender, EventArgs e)
		{
			acilirsayfa();
			DtpKitapİslemAlinanTarih.Enabled = false;
			DtpKitapİslemAlinanTarih.Value = DateTime.Now;

		}

		private void BtnKitapİslemEkle_Click(object sender, EventArgs e)
		{
			int islemid = 0;
			SqlCommand IslemIDGetir = new SqlCommand("SELECT MAX(IslemID) FROM Islemler", conn);
			conn.Open();
			SqlDataReader IslemIDOku = IslemIDGetir.ExecuteReader();
			while (IslemIDOku.Read())
			{
				if (IslemIDOku.IsDBNull(0))
				{
					islemid = 0;
				}
				else
				{
					islemid = IslemIDOku.GetInt32(0);
				}
			}
			conn.Close();
			IslemIDOku.Close();
			int uyeid = 0;
			SqlCommand UyeIDGetir = new SqlCommand("SELECT UyeID From Uyeler WHERE CONCAT(UyeAdi,UyeSoyadi)=@prm1", conn);
			UyeIDGetir.Parameters.AddWithValue("@prm1", CmbKitapİslemÜye.SelectedItem.ToString());
			conn.Open();
			SqlDataReader UyeIDOku = UyeIDGetir.ExecuteReader();
			while (UyeIDOku.Read())
			{
				uyeid = UyeIDOku.GetInt32(0);
			}
			conn.Close();
			UyeIDOku.Close();
			int kitapid = 0;
			SqlCommand KitapIDGetir = new SqlCommand("SELECT KitapID From Kitaplar WHERE KitapAdi=@prm1", conn);
			KitapIDGetir.Parameters.AddWithValue("@prm1", CmbKitapİslemKitap.SelectedItem.ToString());
			conn.Open();
			SqlDataReader KitapIDOku = KitapIDGetir.ExecuteReader();
			while (KitapIDOku.Read())
			{
				kitapid = KitapIDOku.GetInt32(0);
			}
			conn.Close();
			KitapIDOku.Close();
			SqlCommand KitapIslemEkle = new SqlCommand("INSERT INTO Islemler (IslemID,UyeID, KitapID, AlinanTarih, VerilenTarih, IslemTuru) VALUES (@prm1,@prm2,@prm3,@prm4,@prm5,@prm6);", conn);
			KitapIslemEkle.Parameters.AddWithValue("@prm1", islemid + 1);
			KitapIslemEkle.Parameters.AddWithValue("@prm2", uyeid);
			KitapIslemEkle.Parameters.AddWithValue("@prm3", kitapid);
			KitapIslemEkle.Parameters.AddWithValue("@prm4", DateTime.Now);
			KitapIslemEkle.Parameters.AddWithValue("@prm5", DBNull.Value);
			KitapIslemEkle.Parameters.AddWithValue("@prm6", "Kitap Verildi");
			conn.Open();
			KitapIslemEkle.ExecuteNonQuery();
			conn.Close();
			MessageBox.Show(CmbKitapİslemÜye.SelectedItem.ToString() + " " + "Adlı Kişiye" + " " + CmbKitapİslemKitap.SelectedItem.ToString() + " " + "Adlı Kitabın" + "İşlemi Başarıyla Gerçekleşti !");
			acilirsayfa();
		}

		private void BtnKitapİslemGüncelle_Click(object sender, EventArgs e)
		{
			string veri = CmbKitapİslemGüncelleİslem.SelectedItem.ToString();
			string[] ayirilmisVeri = veri.Split('-');
			string ÜyeAdiSoyadi = ayirilmisVeri[0].Trim();
			string KitapAdi = ayirilmisVeri[1].Trim();
			int islemid = 0;
			SqlCommand İslemIDGetir = new SqlCommand("SELECT IslemID FROM Islemler JOIN Kitaplar ON Islemler.KitapID = Kitaplar.KitapID JOIN Uyeler ON Islemler.UyeID = Uyeler.UyeID WHERE Kitaplar.KitapAdi = @prm1 AND CONCAT(Uyeler.UyeAdi,Uyeler.UyeSoyadi)=@prm2;", conn);
			İslemIDGetir.Parameters.AddWithValue("@prm1", KitapAdi);
			İslemIDGetir.Parameters.AddWithValue("@prm2", ÜyeAdiSoyadi);
			conn.Open();
			SqlDataReader IslemIDOku = İslemIDGetir.ExecuteReader();
			while (IslemIDOku.Read())
			{
				islemid = IslemIDOku.GetInt32(0);
			}
			conn.Close();
			IslemIDOku.Close();
			if (checkBox1.Checked == true)
			{
				SqlCommand KitapIslemTeslimEt = new SqlCommand("UPDATE Islemler SET VerilenTarih = @prm1, IslemTuru =@prm2 WHERE IslemID = @prm3", conn);
				KitapIslemTeslimEt.Parameters.AddWithValue("@prm1", DateTime.Now);
				KitapIslemTeslimEt.Parameters.AddWithValue("@prm2", "Kitap Teslim Alındı !");
				KitapIslemTeslimEt.Parameters.AddWithValue("@prm3", islemid);
				conn.Open();
				KitapIslemTeslimEt.ExecuteNonQuery();
				MessageBox.Show(CmbKitapİslemGüncelleİslem.SelectedItem.ToString() + " " + " Adlı İşlemin Kitabı Başarıyla Teslim Alındı !");
				conn.Close();
				acilirsayfa();
			}
			else
			{
				int uyeid = 0;
				SqlCommand UyeIDGetir = new SqlCommand("SELECT UyeID FROM Uyeler WHERE CONCAT(UyeAdi,UyeSoyadi)=@prm1;", conn);
				UyeIDGetir.Parameters.AddWithValue("@prm1", ÜyeAdiSoyadi);
				conn.Open();
				SqlDataReader UyeIDOku = UyeIDGetir.ExecuteReader();
				while (UyeIDOku.Read())
				{
					uyeid = UyeIDOku.GetInt32(0);
				}
				conn.Close();
				UyeIDOku.Close();
				int kitapid = 0;
				SqlCommand KitapIDGetir = new SqlCommand("SELECT KitapID FROM Kitaplar WHERE KitapAdi=@prm1", conn);
				KitapIDGetir.Parameters.AddWithValue("@prm1", KitapAdi);
				conn.Open();
				SqlDataReader KitapIDOku = KitapIDGetir.ExecuteReader();
				while (KitapIDOku.Read())
				{
					kitapid = KitapIDOku.GetInt32(0);
				}
				conn.Close();
				KitapIDOku.Close();
				SqlCommand KitapİslemYap = new SqlCommand("UPDATE Islemler SET UyeID=@prm1,KitapID=@prm2", conn);
				KitapİslemYap.Parameters.AddWithValue("@prm1", uyeid);
				KitapİslemYap.Parameters.AddWithValue("@prm2", kitapid);
				conn.Open();
				KitapİslemYap.ExecuteNonQuery();
				conn.Close();
				MessageBox.Show(CmbKitapİslemGüncelleİslem.SelectedItem.ToString() + " " + "Adlı İşlemin Üyesi Güncellemesi Başarıyla Gerçekleşti !");
				acilirsayfa();
			}
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox1.Checked == true)
			{
				CmbKitapİslemGüncelleÜye.Enabled = false;
				CmbKitapİslemGüncelleKitap.Enabled = false;
				dateTimePicker1.Enabled = true;
				dateTimePicker1.Value = DateTime.Now;

			}
			else
			{
				CmbKitapİslemGüncelleKitap.Enabled = true;
				CmbKitapİslemGüncelleÜye.Enabled = true;
				dateTimePicker1.Enabled = false;
			}
		}

		private void CmbKitapİslemSil_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void BtnAdresSil_Click(object sender, EventArgs e)
		{
			string veri = CmbKitapİslemSil.SelectedItem.ToString();
			string[] ayirilmisVeri = veri.Split('-');
			string ÜyeAdiSoyadi = ayirilmisVeri[0].Trim();
			string KitapAdi = ayirilmisVeri[1].Trim();
			int islemid = 0;
			SqlCommand IslemIDGetir = new SqlCommand("SELECT MAX(IslemID) FROM Islemler", conn);
			conn.Open();
			SqlDataReader IslemIDOku = IslemIDGetir.ExecuteReader();
			while (IslemIDOku.Read())
			{
				if (IslemIDOku.IsDBNull(0))
				{
					islemid = 0;
				}
				else
				{
					islemid = IslemIDOku.GetInt32(0);
				}
			}
			conn.Close();
			IslemIDOku.Close();
			SqlCommand IslemSil = new SqlCommand("DELETE FROM Islemler WHERE UyeID = (SELECT UyeID FROM Uyeler WHERE CONCAT(UyeAdi, UyeSoyadi) = @prm1) AND KitapID = (SELECT KitapID FROM Kitaplar WHERE KitapAdi = @prm2)", conn);
			IslemSil.Parameters.AddWithValue("@prm1", ÜyeAdiSoyadi);
			IslemSil.Parameters.AddWithValue("@prm2", KitapAdi);
			conn.Open();
			IslemSil.ExecuteNonQuery();
			conn.Close();
			acilirsayfa();
			MessageBox.Show("İşlem Başarıyla Silindi !");
		}
	}
}
