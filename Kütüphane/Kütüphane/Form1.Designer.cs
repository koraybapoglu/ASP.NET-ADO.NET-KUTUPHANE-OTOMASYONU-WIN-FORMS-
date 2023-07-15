namespace Kütüphane
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.BtnGirisYap = new System.Windows.Forms.Button();
			this.TxtKullaniciAdi = new System.Windows.Forms.TextBox();
			this.TxtSifre = new System.Windows.Forms.TextBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.LblHataBildiri = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// BtnGirisYap
			// 
			this.BtnGirisYap.Location = new System.Drawing.Point(143, 278);
			this.BtnGirisYap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.BtnGirisYap.Name = "BtnGirisYap";
			this.BtnGirisYap.Size = new System.Drawing.Size(288, 81);
			this.BtnGirisYap.TabIndex = 0;
			this.BtnGirisYap.Text = "GirişYap";
			this.BtnGirisYap.UseVisualStyleBackColor = true;
			this.BtnGirisYap.Click += new System.EventHandler(this.BtnGirisYap_Click);
			// 
			// TxtKullaniciAdi
			// 
			this.TxtKullaniciAdi.Location = new System.Drawing.Point(147, 89);
			this.TxtKullaniciAdi.Name = "TxtKullaniciAdi";
			this.TxtKullaniciAdi.Size = new System.Drawing.Size(288, 34);
			this.TxtKullaniciAdi.TabIndex = 1;
			// 
			// TxtSifre
			// 
			this.TxtSifre.Location = new System.Drawing.Point(147, 167);
			this.TxtSifre.Name = "TxtSifre";
			this.TxtSifre.Size = new System.Drawing.Size(288, 34);
			this.TxtSifre.TabIndex = 2;
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(143, 224);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(150, 32);
			this.checkBox1.TabIndex = 3;
			this.checkBox1.Text = "ŞifreyiGöster";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 95);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 28);
			this.label1.TabIndex = 4;
			this.label1.Text = "Kullanıcı Adı:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(79, 173);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 28);
			this.label2.TabIndex = 5;
			this.label2.Text = "Şifre:";
			// 
			// LblHataBildiri
			// 
			this.LblHataBildiri.AutoSize = true;
			this.LblHataBildiri.Location = new System.Drawing.Point(261, 390);
			this.LblHataBildiri.Name = "LblHataBildiri";
			this.LblHataBildiri.Size = new System.Drawing.Size(32, 28);
			this.LblHataBildiri.TabIndex = 6;
			this.LblHataBildiri.Text = "....";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Brown;
			this.ClientSize = new System.Drawing.Size(593, 491);
			this.Controls.Add(this.LblHataBildiri);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.TxtSifre);
			this.Controls.Add(this.TxtKullaniciAdi);
			this.Controls.Add(this.BtnGirisYap);
			this.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "Form1";
			this.Text = "Admin";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button BtnGirisYap;
		private System.Windows.Forms.TextBox TxtKullaniciAdi;
		private System.Windows.Forms.TextBox TxtSifre;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label LblHataBildiri;
	}
}

