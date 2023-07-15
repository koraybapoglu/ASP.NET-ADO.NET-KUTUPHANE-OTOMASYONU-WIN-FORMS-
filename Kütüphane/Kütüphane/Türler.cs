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
	public partial class Türler : Form
	{
		public Türler()
		{
			InitializeComponent();
		}
		SqlConnection conn = new SqlConnection("Data Source=DESKTOP-RN1V6Q9;Initial Catalog=Kutuphane;Integrated Security=True");
		public void acilirsayfa()
		{
			CmbTürlerSil.Text = "";
			CmbYeniTürlerAdi.Text = "";
			CmbTürlerSil.Items.Clear();
			CmbYeniTürlerAdi.Items.Clear();
			TxtTürlerAdi.Text = "";
			TxtYeniTürlerAdi.Text = "";
			SqlCommand yayinevigetir = new SqlCommand("SELECT TurAdi From Turler;", conn);
			conn.Close();
			conn.Open();
			SqlDataReader yayinevioku = yayinevigetir.ExecuteReader();
			while (yayinevioku.Read())
			{
				CmbTürlerSil.Items.Add(yayinevioku.GetString(0));
				CmbYeniTürlerAdi.Items.Add(yayinevioku.GetString(0));
			}
			yayinevioku.Close();
			conn.Close();
			dataGridView1.DataSource = null;
			SqlCommand datagridviewdoldur = new SqlCommand("SELECT * FROM Turler", conn);
			conn.Open();
			SqlDataReader verilerioku = datagridviewdoldur.ExecuteReader();
			DataTable dt = new DataTable();
			dt.Load(verilerioku);
			verilerioku.Close();
			conn.Close();
			dataGridView1.DataSource = dt;
			dataGridView1.Columns[0].Width = 650;
			dataGridView1.Columns[1].Width = 650;
		}

		private void Türler_Load(object sender, EventArgs e)
		{
			acilirsayfa();
		}

		private void BtnTürlerEkle_Click(object sender, EventArgs e)
		{
			SqlCommand TurIdOku = new SqlCommand("SELECT TurID FROM Turler",conn);
			int turid = 0;
			conn.Open();
			SqlDataReader TurIdOku2 = TurIdOku.ExecuteReader();
			while (TurIdOku2.Read())
			{
				turid = TurIdOku2.GetInt32(0);
			}
			TurIdOku2.Close();
			conn.Close();
			SqlCommand TurEkle = new SqlCommand("INSERT INTO Turler (TurID,TurAdi) VALUES ((@prm1),(@prm2))", conn);
			TurEkle.Parameters.AddWithValue("@prm1", turid+1);
			TurEkle.Parameters.AddWithValue("@prm2", TxtTürlerAdi.Text.ToString());
			conn.Open();
			TurEkle.ExecuteNonQuery();
			conn.Close();
			MessageBox.Show("Tür Başarıyla Eklendi !");
			acilirsayfa();
		}

		private void BtnTürlerSil_Click(object sender, EventArgs e)
		{
			SqlCommand yayinevisil = new SqlCommand("DELETE FROM Turler WHERE TurAdi =(@prm1);", conn);
			yayinevisil.Parameters.AddWithValue("@prm1", CmbTürlerSil.SelectedItem.ToString());
			conn.Open();
			yayinevisil.ExecuteNonQuery();
			MessageBox.Show("Tür Başarıyla Silindi !");
			acilirsayfa();
		}

		private void BtnTürlerGüncelle_Click(object sender, EventArgs e)
		{
			SqlCommand yayinevigüncelle = new SqlCommand("UPDATE Turler SET TurAdi = @prm1 WHERE TurAdi=@prm2;", conn);
			yayinevigüncelle.Parameters.AddWithValue("@prm1", TxtYeniTürlerAdi.Text.ToString());
			yayinevigüncelle.Parameters.AddWithValue("@prm2", CmbYeniTürlerAdi.SelectedItem.ToString());
			conn.Open();
			yayinevigüncelle.ExecuteNonQuery();
			MessageBox.Show(CmbYeniTürlerAdi.SelectedItem.ToString() + " " + "Adlı Tür Başarılı bir şekilde " + TxtYeniTürlerAdi.Text + " " + "olarak başarılı bir şekilde oluşturuldu !");
			conn.Close();
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
			YayinevleriAdminForm yayinevleriadmfrm = new YayinevleriAdminForm();
			yayinevleriadmfrm.ShowDialog();
			this.Hide();
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void button7_Click(object sender, EventArgs e)
		{
			Yazarlar yzrl = new Yazarlar();
			yzrl.ShowDialog();
			this.Hide();
		}

		private void BtnCevirmenlerFrm_Click(object sender, EventArgs e)
		{
			Çevirmenler Cevirmenler = new Çevirmenler();
			Cevirmenler.ShowDialog();
			this.Hide();
		}
	}
}
