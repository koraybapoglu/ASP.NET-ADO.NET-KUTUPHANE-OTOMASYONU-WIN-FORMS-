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
	public partial class AdminPanelAcilisSayfasi : Form
	{
		public AdminPanelAcilisSayfasi()
		{
			InitializeComponent();
		}

		private void AdminPanelAcilisSayfasi_Load(object sender, EventArgs e)
		{
			SqlConnection conn = new SqlConnection("Data Source=DESKTOP-RN1V6Q9;Initial Catalog=Kutuphane;Integrated Security=True");
			SqlCommand kacüyevar = new SqlCommand("SELECT COUNT(*) AS VeriSayisi FROM Uyeler",conn);
			conn.Open();
			SqlDataReader kacüyevaroku = kacüyevar.ExecuteReader();
			while (kacüyevaroku.Read())
			{
				LblAktifÜyeSayisi.Text = kacüyevaroku[0].ToString();
			}
			conn.Close();
			kacüyevaroku.Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.Hide();
			YayinevleriAdminForm yayinevleriAdminForm = new YayinevleriAdminForm();
			yayinevleriAdminForm.ShowDialog();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Türler trl = new Türler();
			trl.ShowDialog();
			this.Hide();
		}

		private void BtnYazarlarFrm_Click(object sender, EventArgs e)
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

		private void BtnKitaplarFrm_Click(object sender, EventArgs e)
		{
			Kitaplar ktplr = new Kitaplar();
			ktplr.ShowDialog();
			this.Hide();
		}

		private void BtnUyelerFrm_Click(object sender, EventArgs e)
		{
			Uyeler uylr = new Uyeler();
			uylr.ShowDialog();
			this.Hide();
		}

		private void BtnAdreslerFrm_Click(object sender, EventArgs e)
		{
			Adresler adrsl = new Adresler();
			adrsl.ShowDialog();
			this.Hide();
		}

		private void BtnKitapİslemleriFrm_Click(object sender, EventArgs e)
		{
			KitapIslemleri kitapislmlr=new KitapIslemleri();
			kitapislmlr.ShowDialog();
			this.Hide();
		}
	}
}
