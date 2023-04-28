﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Proje_Hastane
{
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }
        public string tcnumara;

        sqlbaglantisi bgl=new sqlbaglantisi();
        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = tcnumara;
            //AD SOYAD
            SqlCommand komut= new SqlCommand("Select SekreterAdSoyad from Tbl_Sekreter where SekreterTC=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", LblTC.Text);
            SqlDataReader dr=komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();



            //Branşları Datagride Aktarma
            DataTable dt = new DataTable();
            SqlDataAdapter da= new SqlDataAdapter("Select * From Tbl_Branslar",bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //Doktorları Listeye Aktarma
            DataTable dt3 = new DataTable();
            SqlDataAdapter da3 = new SqlDataAdapter("Select (DoktorAd +' '+ DoktorSoyad) as 'Doktorlar',DoktorBrans From Tbl_Doktorlar",bgl.baglanti());
            da3.Fill(dt3);
            dataGridView2.DataSource = dt3;


            //Branşı Combobox a Aktarma
            SqlCommand komut2 = new SqlCommand("Select BransAd From Tbl_Branslar",bgl.baglanti());
            SqlDataReader dr2=komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add (dr2[0]);
            }
            bgl.baglanti().Close();
            

        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutkaydet = new SqlCommand("insert into Tbl_Randevular (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values (@r1,@r2,@r3,@r4)",bgl.baglanti());
            komutkaydet.Parameters.AddWithValue("@r1",MskTarih.Text);
            komutkaydet.Parameters.AddWithValue("@r2",MskSaat.Text);
            komutkaydet.Parameters.AddWithValue("@r3",CmbBrans.Text);
            komutkaydet.Parameters.AddWithValue("@r4",CmbDoktor.Text);
            komutkaydet.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Oluşturuldu");

        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Items.Clear();

            SqlCommand komut = new SqlCommand("Select DoktorAd,DoktorSoyad From Tbl_Doktorlar where DoktorBrans=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", CmbBrans.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbDoktor.Items.Add(dr[0]+" " + dr[1]);
            }
            bgl.baglanti().Close();

        }

        private void BtnDuyuruOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Duyurular (Duyuru) values (@d1)",bgl.baglanti());//Burada Tbl_Duyurulara Ekle Duyuru=@d1 den gelen değeri 
            komut.Parameters.AddWithValue("@d1",RchDuyuru.Text);//RchDuyurudaki Yazıyı @d1 e aktar Yani Adımlar Şöyle (Tbl_Duyurular=Duyuru-=-@d1-=-RchDuyurudaki Yazı)
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu");
            
        }

        private void BtnDoktorPanel_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli drp=new FrmDoktorPaneli();
            drp.Show();

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {

        }

        private void BtnBransPanel_Click(object sender, EventArgs e)
        {
            FrmBrans fr=new FrmBrans();
            fr.Show();
        }

        private void BtnListe_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi fr=new FrmRandevuListesi();
            fr.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmDuyurular fr=new FrmDuyurular();
            fr.Show();
        }
    }
}
