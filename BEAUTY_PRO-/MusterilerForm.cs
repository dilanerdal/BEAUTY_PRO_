using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BEAUTY_PRO_
{
    public partial class MusterilerForm : Form
    {
        DatabaseConnection db = new DatabaseConnection();

        public MusterilerForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand(
                "INSERT INTO Musteriler (AdSoyad, Telefon, Email, KayitTarihi, Notlar) VALUES (@AdSoyad, @Telefon, @Email, @KayitTarihi, @Notlar)",
                db.GetConnection()
            );

            komut.Parameters.AddWithValue("@AdSoyad", txtAdSoyad.Text);
            komut.Parameters.AddWithValue("@Telefon", txtTelefon.Text);
            komut.Parameters.AddWithValue("@Email", txtEmail.Text);
            komut.Parameters.AddWithValue("@KayitTarihi", DateTime.Now);
            komut.Parameters.AddWithValue("@Notlar", txtNotlar.Text);

            db.GetConnection().Open();
            komut.ExecuteNonQuery();
            db.GetConnection().Close();

            MessageBox.Show("Müşteri başarıyla kaydedildi.");
            MusterileriListele();
        }

        private void MusterilerForm_Load(object sender, EventArgs e)
        {
            MusterileriListele();
        }

        void MusterileriListele()
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT MusteriID, AdSoyad, Telefon, Email, Notlar FROM Musteriler",
                db.GetConnection()
            );

            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvMusteriler.DataSource = dt;
        }

        private void dgvMusteriler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtAdSoyad.Text = dgvMusteriler.Rows[e.RowIndex].Cells["AdSoyad"].Value.ToString();
                txtTelefon.Text = dgvMusteriler.Rows[e.RowIndex].Cells["Telefon"].Value.ToString();
                txtEmail.Text = dgvMusteriler.Rows[e.RowIndex].Cells["Email"].Value.ToString();
                txtNotlar.Text = dgvMusteriler.Rows[e.RowIndex].Cells["Notlar"].Value.ToString();
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand(
    "UPDATE Musteriler SET AdSoyad=@AdSoyad, Telefon=@Telefon, Email=@Email, Notlar=@Notlar WHERE MusteriID=@MusteriID",
    db.GetConnection()
);

            komut.Parameters.AddWithValue("@AdSoyad", txtAdSoyad.Text);
            komut.Parameters.AddWithValue("@Telefon", txtTelefon.Text);
            komut.Parameters.AddWithValue("@Email", txtEmail.Text);
            komut.Parameters.AddWithValue("@Notlar", txtNotlar.Text);
            komut.Parameters.AddWithValue("@MusteriID", dgvMusteriler.CurrentRow.Cells["MusteriID"].Value);

            db.GetConnection().Open();
            komut.ExecuteNonQuery();
            db.GetConnection().Close();

            MessageBox.Show("Müşteri başarıyla güncellendi.");
            MusterileriListele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand(
    "DELETE FROM Musteriler WHERE MusteriID=@MusteriID",
    db.GetConnection()
);

            komut.Parameters.AddWithValue("@MusteriID", dgvMusteriler.CurrentRow.Cells["MusteriID"].Value);

            db.GetConnection().Open();
            komut.ExecuteNonQuery();
            db.GetConnection().Close();

            MessageBox.Show("Müşteri silindi.");

            MusterileriListele();
        }
    }
}
