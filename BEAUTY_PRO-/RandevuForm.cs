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

namespace BEAUTY_PRO_
{
    public partial class RandevuForm : Form
    {
        DatabaseConnection db = new DatabaseConnection();
        public RandevuForm()
        {
            InitializeComponent();
            dgvRandevular.SelectionChanged += dgvRandevular_SelectionChanged;
        }

        private void MusterileriGetir()
        {
            SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT MusteriID, AdSoyad FROM Musteriler",
                    db.GetConnection()
                );

            DataTable dt = new DataTable();
            da.Fill(dt);

            cmbMusteri.DataSource = dt;
            cmbMusteri.DisplayMember = "AdSoyad";
            cmbMusteri.ValueMember = "MusteriID";
        }
        private void HizmetleriGetir()
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT HizmetID, HizmetAdi FROM Hizmetler",
                db.GetConnection()
            );

            DataTable dt = new DataTable();
            da.Fill(dt);

            cmbHizmet.DataSource = dt;
            cmbHizmet.DisplayMember = "HizmetAdi";
            cmbHizmet.ValueMember = "HizmetID";
        }
        private void RandevuForm_Load(object sender, EventArgs e)
        {
            MusterileriGetir();
            HizmetleriGetir();
            RandevulariListele();
        }
        private void RandevulariListele()
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT RandevuID, MusteriID, HizmetID, Tarih, Saat, Durum FROM Randevular",
                db.GetConnection()
            );

            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvRandevular.DataSource = dt;
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand(
    "INSERT INTO Randevular(MusteriID, HizmetID, Tarih, Saat, Durum) VALUES(@MusteriID, @HizmetID, @Tarih, @Saat, @Durum)",
    db.GetConnection()
);

            komut.Parameters.AddWithValue("@MusteriID", cmbMusteri.SelectedValue);
            komut.Parameters.AddWithValue("@HizmetID", cmbHizmet.SelectedValue);
            komut.Parameters.AddWithValue("@Tarih", dtpTarih.Value.Date);
            komut.Parameters.AddWithValue("@Saat", txtSaat.Text);
            komut.Parameters.AddWithValue("@Durum", "Aktif");

            db.GetConnection().Open();
            komut.ExecuteNonQuery();
            db.GetConnection().Close();

            MessageBox.Show("Randevu başarıyla kaydedildi.");
        }
    
    private void dgvRandevular_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRandevular.CurrentRow != null)
            {
                cmbMusteri.SelectedValue = dgvRandevular.CurrentRow.Cells["MusteriID"].Value;
                cmbHizmet.SelectedValue = dgvRandevular.CurrentRow.Cells["HizmetID"].Value;
                dtpTarih.Value = Convert.ToDateTime(dgvRandevular.CurrentRow.Cells["Tarih"].Value);
                txtSaat.Text = dgvRandevular.CurrentRow.Cells["Saat"].Value.ToString();
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand(
    "UPDATE Randevular SET MusteriID=@MusteriID, HizmetID=@HizmetID, Tarih=@Tarih, Saat=@Saat WHERE RandevuID=@RandevuID",
    db.GetConnection()
);

            komut.Parameters.AddWithValue("@MusteriID", cmbMusteri.SelectedValue);
            komut.Parameters.AddWithValue("@HizmetID", cmbHizmet.SelectedValue);
            komut.Parameters.AddWithValue("@Tarih", dtpTarih.Value.Date);
            komut.Parameters.AddWithValue("@Saat", txtSaat.Text);
            komut.Parameters.AddWithValue("@RandevuID", dgvRandevular.CurrentRow.Cells["RandevuID"].Value);

            db.GetConnection().Open();
            komut.ExecuteNonQuery();
            db.GetConnection().Close();

            RandevulariListele();

            MessageBox.Show("Randevu güncellendi.");
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand(
    "DELETE FROM Randevular WHERE RandevuID=@RandevuID",
    db.GetConnection()
);

            komut.Parameters.AddWithValue(
                "@RandevuID",
                dgvRandevular.CurrentRow.Cells["RandevuID"].Value
            );

            db.GetConnection().Open();
            komut.ExecuteNonQuery();
            db.GetConnection().Close();

            RandevulariListele();

            MessageBox.Show("Randevu silindi.");
        }
    }
}
