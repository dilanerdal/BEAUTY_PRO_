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
    public partial class PersonelForm : Form
    {
        DatabaseConnection db = new DatabaseConnection();
        public PersonelForm()
        {
            InitializeComponent();
            dgvPersoneller.SelectionChanged += dgvPersoneller_SelectionChanged;
        }
        private void PersonelleriListele()
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT PersonelID, AdSoyad, Telefon, Pozisyon, Maas FROM Personeller",
                db.GetConnection()
            );

            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvPersoneller.DataSource = dt;
        }
        private void PersonelForm_Load(object sender, EventArgs e)
        {
            PersonelleriListele();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand(
                "INSERT INTO Personeller(AdSoyad, Telefon, Pozisyon, Maas) VALUES(@AdSoyad, @Telefon, @Pozisyon, @Maas)",
                db.GetConnection()
            );

            komut.Parameters.AddWithValue("@AdSoyad", txtAdSoyad.Text);
            komut.Parameters.AddWithValue("@Telefon", txtTelefon.Text);
            komut.Parameters.AddWithValue("@Pozisyon", txtPozisyon.Text);
            komut.Parameters.AddWithValue("@Maas", txtMaas.Text);

            db.GetConnection().Open();
            komut.ExecuteNonQuery();
            db.GetConnection().Close();

            PersonelleriListele();

            MessageBox.Show("Personel başarıyla kaydedildi.");
        }
        private void dgvPersoneller_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPersoneller.CurrentRow != null)
            {
                txtAdSoyad.Text = dgvPersoneller.CurrentRow.Cells["AdSoyad"].Value.ToString();
                txtTelefon.Text = dgvPersoneller.CurrentRow.Cells["Telefon"].Value.ToString();
                txtPozisyon.Text = dgvPersoneller.CurrentRow.Cells["Pozisyon"].Value.ToString();
                txtMaas.Text = dgvPersoneller.CurrentRow.Cells["Maas"].Value.ToString();
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand(
    "UPDATE Personeller SET AdSoyad=@AdSoyad, Telefon=@Telefon, Pozisyon=@Pozisyon, Maas=@Maas WHERE PersonelID=@PersonelID",
    db.GetConnection()
);

            komut.Parameters.AddWithValue("@AdSoyad", txtAdSoyad.Text);
            komut.Parameters.AddWithValue("@Telefon", txtTelefon.Text);
            komut.Parameters.AddWithValue("@Pozisyon", txtPozisyon.Text);
            komut.Parameters.AddWithValue("@Maas", Convert.ToDecimal(txtMaas.Text));

            komut.Parameters.AddWithValue(
                "@PersonelID",
                dgvPersoneller.CurrentRow.Cells["PersonelID"].Value
            );

            db.GetConnection().Open();
            komut.ExecuteNonQuery();
            db.GetConnection().Close();

            PersonelleriListele();

            MessageBox.Show("Personel güncellendi.");
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand(
    "DELETE FROM Personeller WHERE PersonelID=@PersonelID",
    db.GetConnection()
);

            komut.Parameters.AddWithValue(
                "@PersonelID",
                dgvPersoneller.CurrentRow.Cells["PersonelID"].Value
            );

            db.GetConnection().Open();
            komut.ExecuteNonQuery();
            db.GetConnection().Close();

            PersonelleriListele();

            MessageBox.Show("Personel silindi.");
        }
    }
}
