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
    public partial class HizmetlerForm : Form
    {
        DatabaseConnection db = new DatabaseConnection();


        public HizmetlerForm()
        {
            InitializeComponent();
            dgvHizmetler.SelectionChanged += dgvHizmetler_SelectionChanged;
            HizmetleriListele();
        }
        private void HizmetleriListele()
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT HizmetID, HizmetAdi, Ucret, Sure, Aciklama FROM Hizmetler",
                db.GetConnection()
            );

            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvHizmetler.DataSource = dt;
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand(
                "INSERT INTO Hizmetler(HizmetAdi,Ucret,Sure,Aciklama) VALUES(@HizmetAdi,@Ucret,@Sure,@Aciklama)",
                db.GetConnection()
            );

            komut.Parameters.AddWithValue("@HizmetAdi", txtHizmetAdi.Text);
            komut.Parameters.AddWithValue("@Ucret", txtUcret.Text);
            komut.Parameters.AddWithValue("@Sure", txtSure.Text);
            komut.Parameters.AddWithValue("@Aciklama", txtAciklama.Text);

            db.GetConnection().Open();
            komut.ExecuteNonQuery();
            db.GetConnection().Close();

            MessageBox.Show("Hizmet başarıyla kaydedildi.");
            HizmetleriListele();
        }

        private void dgvHizmetler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtHizmetAdi.Text = dgvHizmetler.Rows[e.RowIndex].Cells["HizmetAdi"].Value.ToString();
                txtUcret.Text = dgvHizmetler.Rows[e.RowIndex].Cells["Ucret"].Value.ToString();
                txtSure.Text = dgvHizmetler.Rows[e.RowIndex].Cells["Sure"].Value.ToString();
                txtAciklama.Text = dgvHizmetler.Rows[e.RowIndex].Cells["Aciklama"].Value.ToString();
            }
        }
    
private void dgvHizmetler_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvHizmetler.CurrentRow != null)
            {
                txtHizmetAdi.Text = dgvHizmetler.CurrentRow.Cells["HizmetAdi"].Value.ToString();
                txtUcret.Text = dgvHizmetler.CurrentRow.Cells["Ucret"].Value.ToString();
                txtSure.Text = dgvHizmetler.CurrentRow.Cells["Sure"].Value.ToString();
                txtAciklama.Text = dgvHizmetler.CurrentRow.Cells["Aciklama"].Value.ToString();
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand(
    "UPDATE Hizmetler SET HizmetAdi=@HizmetAdi, Ucret=@Ucret, Sure=@Sure, Aciklama=@Aciklama WHERE HizmetID=@HizmetID",
    db.GetConnection()
);

            komut.Parameters.AddWithValue("@HizmetAdi", txtHizmetAdi.Text);
            komut.Parameters.AddWithValue("@Ucret", txtUcret.Text);
            komut.Parameters.AddWithValue("@Sure", txtSure.Text);
            komut.Parameters.AddWithValue("@Aciklama", txtAciklama.Text);
            komut.Parameters.AddWithValue("@HizmetID", dgvHizmetler.CurrentRow.Cells["HizmetID"].Value);

            db.GetConnection().Open();
            komut.ExecuteNonQuery();
            db.GetConnection().Close();

            MessageBox.Show("Hizmet başarıyla güncellendi.");
            HizmetleriListele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand(
    "DELETE FROM Hizmetler WHERE HizmetID=@HizmetID",
    db.GetConnection()
);

            komut.Parameters.AddWithValue("@HizmetID", dgvHizmetler.CurrentRow.Cells["HizmetID"].Value);

            db.GetConnection().Open();
            komut.ExecuteNonQuery();
            db.GetConnection().Close();

            MessageBox.Show("Hizmet başarıyla silindi.");
            HizmetleriListele();
        }
    }
}
