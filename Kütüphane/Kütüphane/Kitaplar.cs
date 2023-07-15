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
	public partial class Kitaplar : Form
	{
		public Kitaplar()
		{
			InitializeComponent();
		}
		SqlConnection conn = new SqlConnection("Data Source=DESKTOP-RN1V6Q9;Initial Catalog=Kutuphane;Integrated Security=True");

		private void BtnKitapTürleriFrm_Click(object sender, EventArgs e)
		{
			Türler trl = new Türler();
			trl.ShowDialog();
			this.Hide();
		}
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
			ClearComboBoxItems(this);
			ClearComboBoxes();
			foreach (Control control in this.Controls)
			{
				if (control is TextBox textBox)
				{
					textBox.Clear();
				}
			}
			SqlCommand KitapGetir = new SqlCommand("SELECT KitapAdi From Kitaplar;", conn);
			SqlCommand YazarGetir = new SqlCommand("SELECT YazarAdi From Yazarlar;", conn);
			SqlCommand TurGetir = new SqlCommand("SELECT TurAdi From Turler", conn);
			SqlCommand YayineviGetir = new SqlCommand("SELECT YayinEviAdi From YayinEvleri", conn);
			SqlCommand CevirmenGetir = new SqlCommand("SELECT CevirmenAdi From Cevirmenler", conn);
			conn.Close();
			conn.Open();
			SqlDataReader KitapOku = KitapGetir.ExecuteReader();
			while (KitapOku.Read())
			{
				CmbKitapGüncelleSec.Items.Add(KitapOku.GetString(0));
				CmbKitapSil.Items.Add(KitapOku.GetString(0));
			}
			KitapOku.Close();
			conn.Close();
			conn.Open();
			SqlDataReader YazarOku = YazarGetir.ExecuteReader();
			while (YazarOku.Read())
			{
				CmbKitapEkleYazar.Items.Add(YazarOku.GetString(0));
				CmbKitapGüncelleYazar.Items.Add(YazarOku.GetString(0));
			}
			YazarOku.Close();
			conn.Close();
			conn.Open();
			SqlDataReader TurOku = TurGetir.ExecuteReader();
			while (TurOku.Read())
			{
				CmbKitapEkleTür.Items.Add((TurOku.GetString(0)));
				CmbKitapGüncelleTürSec.Items.Add(TurOku.GetString(0));
			}
			TurOku.Close();
			conn.Close();
			conn.Open();
			SqlDataReader YayinEviOku = YayineviGetir.ExecuteReader();
			while (YayinEviOku.Read())
			{
				CmbKitapEkleYayinevi.Items.Add(YayinEviOku.GetString(0));
				CmbKitapGüncelleYayinEvi.Items.Add(YayinEviOku.GetString(0));
			}
			YayinEviOku.Close();
			conn.Close();
			conn.Open();
			SqlDataReader CevirmenOku = CevirmenGetir.ExecuteReader();
			while (CevirmenOku.Read())
			{
				CmbKitapEkleCevirmen.Items.Add(CevirmenOku.GetString(0));
				CmbKitapGüncelleCevirmen.Items.Add(CevirmenOku.GetString(0));
			}
			CevirmenOku.Close();
			conn.Close();
			dataGridView1.DataSource = null;
			SqlCommand datagridviewdoldur = new SqlCommand("SELECT k.KitapID, k.KitapAdi, yz.YazarAdi, tr.TurAdi, ye.YayinEviAdi, cv.CevirmenAdi FROM Kitaplar k JOIN Yazarlar yz ON k.YazarID = yz.YazarID JOIN Turler tr ON k.TurID = tr.TurID JOIN Yayinevleri ye ON k.YayinEviID = ye.YayinEviID JOIN Cevirmenler cv ON k.CevirmenID = cv.CevirmenID;", conn);
			conn.Open();
			SqlDataReader verilerioku = datagridviewdoldur.ExecuteReader();
			DataTable dt = new DataTable();
			dt.Load(verilerioku);
			verilerioku.Close();
			conn.Close();
			dataGridView1.DataSource = dt;
			dataGridView1.Columns[0].Width = 210;
			dataGridView1.Columns[1].Width = 210;
			dataGridView1.Columns[2].Width = 210;
			dataGridView1.Columns[3].Width = 210;
			dataGridView1.Columns[4].Width = 210;
		}

		private void BtnYayinevleriFrm_Click(object sender, EventArgs e)
		{
			YayinevleriAdminForm yayinevleriadminfrm = new YayinevleriAdminForm();
			yayinevleriadminfrm.ShowDialog();
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
		}

		private void button1_Click(object sender, EventArgs e)
		{
			int yazarid = 0;
			SqlCommand YazarIDGetir = new SqlCommand("SELECT YazarID From Yazarlar WHERE YazarAdi=@prm1;", conn);
			YazarIDGetir.Parameters.AddWithValue("@prm1", CmbKitapEkleYazar.SelectedItem.ToString());
			conn.Open();
			SqlDataReader YazarIDOku = YazarIDGetir.ExecuteReader();
			while (YazarIDOku.Read())
			{
				yazarid = YazarIDOku.GetInt32(0);
			}
			conn.Close();
			YazarIDOku.Close();
			int turid = 0;
			SqlCommand TurIDGetir = new SqlCommand("SELECT TurID FROM Turler WHERE TurAdi=@prm1", conn);
			TurIDGetir.Parameters.AddWithValue("@prm1", CmbKitapEkleTür.SelectedItem.ToString());
			conn.Open();
			SqlDataReader TurIDOku = TurIDGetir.ExecuteReader();
			while (TurIDOku.Read())
			{
				turid = TurIDOku.GetInt32(0);
			}
			conn.Close();
			TurIDOku.Close();
			int yayineviid = 0;
			SqlCommand YayinEvleriIDGetir = new SqlCommand("SELECT YayinEviID FROM YayinEvleri WHERE YayinEviAdi=@prm1", conn);
			YayinEvleriIDGetir.Parameters.AddWithValue("@prm1", CmbKitapEkleYayinevi.SelectedItem.ToString());
			conn.Open();
			SqlDataReader YayinEvleriIDOku = YayinEvleriIDGetir.ExecuteReader();
			while (YayinEvleriIDOku.Read())
			{
				yayineviid = YayinEvleriIDOku.GetInt32(0);
			}
			conn.Close();
			YayinEvleriIDOku.Close();
			int cevirmenid = 0;
			SqlCommand CevirmenIDGetir = new SqlCommand("SELECT CevirmenID FROM Cevirmenler WHERE CevirmenAdi=@prm1", conn);
			CevirmenIDGetir.Parameters.AddWithValue("@prm1", CmbKitapEkleCevirmen.SelectedItem.ToString());
			conn.Open();
			SqlDataReader CevirmenIDOku = CevirmenIDGetir.ExecuteReader();
			while (CevirmenIDOku.Read())
			{
				cevirmenid = CevirmenIDOku.GetInt32(0);
			}
			conn.Close();
			CevirmenIDOku.Close();
			int kitapid = 0;
			SqlCommand KitapIDGetir = new SqlCommand("SELECT MAX(KitapID) FROM Kitaplar", conn);
			conn.Open();
			SqlDataReader KitapIDOku = KitapIDGetir.ExecuteReader();
			while (KitapIDOku.Read())
			{
				if (KitapIDOku.IsDBNull(0))
				{
					kitapid = 0;
				}
				else
				{
					kitapid = KitapIDOku.GetInt32(0);
				}
			}
			conn.Close();
			KitapIDOku.Close();
			SqlCommand KitapEkle = new SqlCommand("INSERT INTO Kitaplar (KitapID, KitapAdi, YazarID, TurID, YayinEviID, CevirmenID) VALUES (@prm1,@prm2,@prm3,@prm4,@prm5,@prm6);", conn);
			KitapEkle.Parameters.AddWithValue("@prm1", kitapid + 1);
			KitapEkle.Parameters.AddWithValue("@prm2", TxtKitapAdiEkle.Text.ToString());
			KitapEkle.Parameters.AddWithValue("@prm3", yazarid.ToString());
			KitapEkle.Parameters.AddWithValue("@prm4", turid.ToString());
			KitapEkle.Parameters.AddWithValue("@prm5", yayineviid.ToString());
			KitapEkle.Parameters.AddWithValue("@prm6", cevirmenid.ToString());
			conn.Open();
			KitapEkle.ExecuteNonQuery();
			conn.Close();
			MessageBox.Show(TxtKitapAdiEkle.Text.ToString() + " " + "Adlı Kitap Başarıyla Eklendi !");
			acilirsayfa();
		}

		private void BtnKitapSil_Click(object sender, EventArgs e)
		{
			SqlCommand KitapSil = new SqlCommand("DELETE FROM Kitaplar WHERE KitapAdi =@prm1;", conn);
			KitapSil.Parameters.AddWithValue("@prm1", CmbKitapSil.SelectedItem.ToString());
			conn.Open();
			KitapSil.ExecuteNonQuery();
			MessageBox.Show(CmbKitapSil.SelectedItem.ToString() + " " + "Adlı Kitap Başarıyla Silindi !");
			conn.Close();
			acilirsayfa();
		}

		private void BtnKitapGüncelle_Click(object sender, EventArgs e)
		{
			int kitapid = 0;
			int yazarid = 0;
			int turid = 0;
			int yayineviid = 0;
			int cevirmenid = 0;
			SqlCommand KitapIDGetir = new SqlCommand("SELECT KitapID FROM Kitaplar WHERE KitapAdi=@prm1;", conn);
			KitapIDGetir.Parameters.AddWithValue("@prm1", CmbKitapGüncelleSec.SelectedItem.ToString());
			conn.Open();
			SqlDataReader KitapIDOku = KitapIDGetir.ExecuteReader();
			while (KitapIDOku.Read())
			{
				kitapid = KitapIDOku.GetInt32(0);
			}
			KitapIDOku.Close();
			conn.Close();
			SqlCommand YazarIDGetir = new SqlCommand("SELECT YazarID FROM Yazarlar WHERE YazarAdi=@prm1", conn);
			YazarIDGetir.Parameters.AddWithValue("@prm1", CmbKitapGüncelleYazar.SelectedItem.ToString());
			conn.Open();
			SqlDataReader YazarIDOku = YazarIDGetir.ExecuteReader();
			while (YazarIDOku.Read())
			{
				yazarid = YazarIDOku.GetInt32(0);
			}
			YazarIDOku.Close();
			conn.Close();
			SqlCommand TurIDGetir = new SqlCommand("SELECT TurID FROM Turler WHERE TurAdi=@prm1", conn);
			TurIDGetir.Parameters.AddWithValue("@prm1", CmbKitapGüncelleTürSec.SelectedItem.ToString());
			conn.Open();
			SqlDataReader TurIDOku = TurIDGetir.ExecuteReader();
			while (TurIDOku.Read())
			{
				turid = TurIDOku.GetInt32(0);
			}
			TurIDOku.Close();
			conn.Close();
			SqlCommand YayınEviIDGetir = new SqlCommand("SELECT YayinEviID FROM YayinEvleri WHERE YayinEviAdi=@prm1", conn);
			YayınEviIDGetir.Parameters.AddWithValue("@prm1", CmbKitapGüncelleYayinEvi.SelectedItem.ToString());
			conn.Open();
			SqlDataReader YayınEviIDOku = YayınEviIDGetir.ExecuteReader();
			while (YayınEviIDOku.Read())
			{
				yayineviid = YayınEviIDOku.GetInt32(0);
			}
			YayınEviIDOku.Close();
			conn.Close();
			SqlCommand CevirmenIDGetir = new SqlCommand("SELECT CevirmenID FROM Cevirmenler WHERE CevirmenAdi=@prm1", conn);
			CevirmenIDGetir.Parameters.AddWithValue("@prm1", CmbKitapGüncelleCevirmen.SelectedItem.ToString());
			conn.Open();
			SqlDataReader CevirmenIDOku = CevirmenIDGetir.ExecuteReader();
			while (CevirmenIDOku.Read())
			{
				cevirmenid = CevirmenIDOku.GetInt32(0);
			}
			conn.Close();
			CevirmenIDOku.Close();
			SqlCommand KitapGüncelle = new SqlCommand("UPDATE Kitaplar SET KitapAdi =@prm1, YazarID =@prm2, TurID = @prm3,   YayineviID =@prm4,CevirmenID =@prm5 WHERE KitapID = @prm6;", conn);
			KitapGüncelle.Parameters.AddWithValue("@prm1", TxtKitapGüncelleKitapAdi.Text.ToString());
			KitapGüncelle.Parameters.AddWithValue("@prm2", yazarid);
			KitapGüncelle.Parameters.AddWithValue("@prm3", turid);
			KitapGüncelle.Parameters.AddWithValue("@prm4", yayineviid);
			KitapGüncelle.Parameters.AddWithValue("@prm5", cevirmenid);
			KitapGüncelle.Parameters.AddWithValue("@prm6", kitapid);
			conn.Open();
			KitapGüncelle.ExecuteNonQuery();
			conn.Close();
			MessageBox.Show(CmbKitapGüncelleSec.SelectedItem.ToString() + " " + "Adlı Kitap Başarıyla" + " " + TxtKitapGüncelleKitapAdi.Text.ToString() + "Adı İle Güncellendi !");
			acilirsayfa();
		}

		private void Kitaplar_Load(object sender, EventArgs e)
		{
			acilirsayfa();
		}
	}
}
