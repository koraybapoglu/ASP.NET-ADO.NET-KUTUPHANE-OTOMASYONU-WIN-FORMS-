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

namespace Kütüphane
{
	public partial class YayinevleriAdminForm : Form
	{
		public YayinevleriAdminForm()
		{
			InitializeComponent();

		}
		public void acilirsayfa()
		{
			CmbYayinEviGüncelle.Text = "";
			CmbYayinEviSil.Text = "";
			CmbYayinEviGüncelle.Items.Clear();
			CmbYayinEviSil.Items.Clear();
			TxtYayinEviAdi.Text = "";
			TxtYeniYayinEviAdi.Text = "";
			SqlCommand yayinevigetir = new SqlCommand("SELECT YayineviAdi From Yayinevleri;", conn);
			conn.Open();
			SqlDataReader yayinevioku = yayinevigetir.ExecuteReader();
			while (yayinevioku.Read())
			{
				CmbYayinEviGüncelle.Items.Add(yayinevioku.GetString(0));
				CmbYayinEviSil.Items.Add(yayinevioku.GetString(0));
			}
			yayinevioku.Close();
			conn.Close();
			dataGridView1.DataSource = null;
			SqlCommand datagridviewdoldur = new SqlCommand("SELECT * FROM Yayinevleri",conn);
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
		SqlConnection conn = new SqlConnection("Data Source=DESKTOP-RN1V6Q9;Initial Catalog=Kutuphane;Integrated Security=True");

		private void BtnYayinEviEkle_Click(object sender, EventArgs e)
		{
			SqlCommand YayinEviEkle = new SqlCommand("INSERT INTO Yayinevleri VALUES (@prm1)", conn);
			YayinEviEkle.Parameters.AddWithValue("@prm1", TxtYayinEviAdi.Text.ToString());
			conn.Open();
			YayinEviEkle.ExecuteNonQuery();
			conn.Close();
			MessageBox.Show("YayınEvi Başarıyla Eklendi !");
			acilirsayfa();
		}
		private void YayinevleriAdminForm_Load(object sender, EventArgs e)
		{
			acilirsayfa();
		}
		private void button2_Click(object sender, EventArgs e)
		{
			this.Hide();
			YayinevleriAdminForm yayinevleriadminfrm = new YayinevleriAdminForm();
			yayinevleriadminfrm.ShowDialog();	
		}

		private void groupBox1_Enter(object sender, EventArgs e)
		{

		}

		private void BtnYayinEviSil_Click(object sender, EventArgs e)
		{
			SqlCommand yayinevisil=new SqlCommand("DELETE FROM Yayinevleri WHERE YayinEviAdi =(@prm1);", conn);
			yayinevisil.Parameters.AddWithValue("@prm1", CmbYayinEviSil.SelectedItem.ToString());
			conn.Open();
			yayinevisil.ExecuteNonQuery();
			MessageBox.Show("YayınEvi Başarıyla Silindi !");
			acilirsayfa();
		}

		private void BtnYayinEviGüncelle_Click(object sender, EventArgs e)
		{
			SqlCommand yayinevigüncelle = new SqlCommand("UPDATE YayinEvleri SET YayinEviadi = @prm1 WHERE YayinEviAdi=@prm2;",conn);
			yayinevigüncelle.Parameters.AddWithValue("@prm1", TxtYeniYayinEviAdi.Text.ToString());
			yayinevigüncelle.Parameters.AddWithValue("@prm2", CmbYayinEviGüncelle.SelectedItem.ToString());
			conn.Open();
			yayinevigüncelle.ExecuteNonQuery();
			MessageBox.Show(CmbYayinEviGüncelle.SelectedItem.ToString()+" "+"Adlı YayınEvi Başarılı bir şekilde "+TxtYeniYayinEviAdi.Text+" "+"olarak başarılı bir şekilde oluşturuldu !");
			conn.Close();
			acilirsayfa();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Türler trl = new Türler();
			trl.ShowDialog();
			this.Hide();
		}
	}
}
