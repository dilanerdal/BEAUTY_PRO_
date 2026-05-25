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
    public partial class OdemeForm : Form
    {
        DatabaseConnection db = new DatabaseConnection();

        public OdemeForm()
        {
            InitializeComponent();
            dgvOdemeler.SelectionChanged += dgvOdemeler_SelectionChanged;
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
        private void OdemeleriListele()
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT OdemeID, RandevuID, Tutar, OdemeTipi, OdemeTarihi FROM Odemeler",
                db.GetConnection()
            );

            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvOdemeler.DataSource = dt;
        }
        private void OdemeForm_Load(object sender, EventArgs e)
        {
            OdemeleriListele();
            MusterileriGetir();

            cmbOdemeTipi.Items.Add("Nakit");
            cmbOdemeTipi.Items.Add("Kart");
            cmbOdemeTipi.Items.Add("Havale");


        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand(
    "INSERT INTO Odemeler(RandevuID, Tutar, OdemeTipi, OdemeTarihi) VALUES(@RandevuID, @Tutar, @OdemeTipi, @OdemeTarihi)",
    db.GetConnection()
);

            komut.Parameters.AddWithValue("@RandevuID", 3);
            komut.Parameters.AddWithValue("@Tutar", Convert.ToDecimal(txtTutar.Text));
            komut.Parameters.AddWithValue("@OdemeTipi", cmbOdemeTipi.Text);
            komut.Parameters.AddWithValue("@OdemeTarihi", dtpOdemeTarihi.Value.Date);

            db.GetConnection().Open();
            komut.ExecuteNonQuery();
            db.GetConnection().Close();

            OdemeleriListele();

            MessageBox.Show("Ödeme başarıyla kaydedildi.");
        }
        private void dgvOdemeler_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOdemeler.CurrentRow != null)
            {
                txtTutar.Text = dgvOdemeler.CurrentRow.Cells["Tutar"].Value.ToString();
                cmbOdemeTipi.Text = dgvOdemeler.CurrentRow.Cells["OdemeTipi"].Value.ToString();
                dtpOdemeTarihi.Value = Convert.ToDateTime(dgvOdemeler.CurrentRow.Cells["OdemeTarihi"].Value);
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand(
    "UPDATE Odemeler SET Tutar=@Tutar, OdemeTipi=@OdemeTipi, OdemeTarihi=@OdemeTarihi WHERE OdemeID=@OdemeID",
    db.GetConnection()
);

            komut.Parameters.AddWithValue("@Tutar", Convert.ToDecimal(txtTutar.Text));
            komut.Parameters.AddWithValue("@OdemeTipi", cmbOdemeTipi.Text);
            komut.Parameters.AddWithValue("@OdemeTarihi", dtpOdemeTarihi.Value.Date);

            komut.Parameters.AddWithValue(
                "@OdemeID",
                dgvOdemeler.CurrentRow.Cells["OdemeID"].Value
            );

            db.GetConnection().Open();
            komut.ExecuteNonQuery();
            db.GetConnection().Close();

            OdemeleriListele();

            MessageBox.Show("Ödeme güncellendi.");
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand(
    "DELETE FROM Odemeler WHERE OdemeID=@OdemeID",
    db.GetConnection()
);

            komut.Parameters.AddWithValue(
                "@OdemeID",
                dgvOdemeler.CurrentRow.Cells["OdemeID"].Value
            );

            db.GetConnection().Open();
            komut.ExecuteNonQuery();
            db.GetConnection().Close();

            OdemeleriListele();

            MessageBox.Show("Ödeme silindi.");
        }
    }
}
   
