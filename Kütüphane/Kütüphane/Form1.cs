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
using System.Threading;

namespace Kütüphane
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

		}
		SqlConnection conn = new SqlConnection("Data Source=DESKTOP-RN1V6Q9;Initial Catalog=Kutuphane;Integrated Security=True");

		private void Form1_Load(object sender, EventArgs e)
		{
			Sifirla();
			LblHataBildiri.Visible = false;
		}
		public void Sifirla()
		{
			TxtKullaniciAdi.Clear();
			TxtSifre.Clear();
			checkBox1.Checked = false;
			TxtSifre.PasswordChar = '*';
		}
		public void LblHata()
		{
			LblHataBildiri.Visible = true;
			LblHataBildiri.ForeColor = Color.Red;
			Thread.Sleep(5000);
			Sifirla();
		}
		public void LblOnay()
		{
			LblHataBildiri.Visible = true;
			LblHataBildiri.ForeColor = Color.Green;
			Thread.Sleep(2000);
			Sifirla();
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox1.Checked == true)
			{
				TxtSifre.PasswordChar = '\0';
			}
			else
			{
				TxtSifre.PasswordChar = '*';
			}
		}

		private void BtnGirisYap_Click(object sender, EventArgs e)
		{
			string kullaniciadi = TxtKullaniciAdi.Text.ToString();
			string sifre = TxtSifre.Text.ToString();
			SqlCommand command = new SqlCommand("SELECT AdminKullaniciAdi,AdminSifre From Admin",conn);
			conn.Open();
			SqlDataReader reader = command.ExecuteReader();
			string gelenkullaniciadi = "";
			string gelensifre = "";
			while (reader.Read())
			{
				gelenkullaniciadi = reader["AdminKullaniciAdi"].ToString();
				gelensifre = reader["AdminSifre"].ToString();
			}
			reader.Close();
			conn.Close();
			if (kullaniciadi == gelenkullaniciadi && sifre == gelensifre)
			{
				LblHataBildiri.Text = "Başarılı Bir Şekilde Giriş Yapıldı !";
				LblOnay();
				this.Hide();
				AdminPanelAcilisSayfasi adminpanel = new AdminPanelAcilisSayfasi();
				adminpanel.ShowDialog();
				

			}
			else if (kullaniciadi != gelenkullaniciadi && sifre == gelensifre)
			{
				LblHataBildiri.Text = "Kullanıcı Adı Yanlış Tekrar Deneyiniz !";
				LblHata();
			}
			else if (kullaniciadi == gelenkullaniciadi && sifre != gelensifre)
			{
				LblHataBildiri.Text = "Şifre Yanlış Tekrar Deneyiniz !";
				LblHata();
			}
			else if (kullaniciadi!=gelenkullaniciadi && sifre!=gelensifre)
			{
				LblHataBildiri.Text = "Kullanıcı Adı ve Şifre Yanlış Tekrar Deneyiniz !";
				LblHata();
			}
			else if (kullaniciadi != null && sifre == null)
			{
				LblHataBildiri.Text = "Şifre Alanını Boş Bıraktınız Lütfen Tekrar Deneyiniz !";
				LblHata();
			}
			else if (kullaniciadi==null &&sifre!=null)
			{
				LblHataBildiri.Text = "Kullanıcı Adı Kısmını Boş Bıraktınız Lütfen Tekrar Deneyiniz !";
				LblHata();
			}
			else if (kullaniciadi==null && sifre==null)
			{
				LblHataBildiri.Text = "Kullanıcı Adı ve Şifre Boş Doldurup Tekrar Deneyiniz !";
				LblHata();
			}
		}
	}
}
