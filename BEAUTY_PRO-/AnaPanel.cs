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

namespace BEAUTY_PRO_
{
    public partial class AnaPanel : Form
   
    {
        DatabaseConnection db = new DatabaseConnection();
        public AnaPanel()
        {
            InitializeComponent();
            AnaPanelTasarim();
            DashboardVerileriGetir();
            SonRandevulariGetir();
        }



        private void DashboardVerileriGetir()
        {
            {
                db.GetConnection().Open();

                SqlCommand komutMusteri = new SqlCommand(
                    "SELECT COUNT(*) FROM Musteriler",
                    db.GetConnection()
                );
                lblAktifMusteri.Text = komutMusteri.ExecuteScalar().ToString();

                SqlCommand komutRandevu = new SqlCommand(
                    "SELECT COUNT(*) FROM Randevular WHERE Tarih = CAST(GETDATE() AS DATE)",
                    db.GetConnection()
                );
                lblBugunkuRandevu.Text = komutRandevu.ExecuteScalar().ToString();

                SqlCommand komutKazanc = new SqlCommand(
                    "SELECT ISNULL(SUM(Tutar), 0) FROM Odemeler WHERE MONTH(OdemeTarihi)=MONTH(GETDATE()) AND YEAR(OdemeTarihi)=YEAR(GETDATE())",
                    db.GetConnection()
                );
                lblAylikKazanc.Text = "₺" + komutKazanc.ExecuteScalar().ToString();

                db.GetConnection().Close();
            }
        }
        private void SonRandevulariGetir()
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT TOP 5 m.AdSoyad AS Musteri, h.HizmetAdi AS Hizmet, r.Tarih, r.Saat FROM Randevular r INNER JOIN Musteriler m ON r.MusteriID = m.MusteriID INNER JOIN Hizmetler h ON r.HizmetID = h.HizmetID ORDER BY r.RandevuID DESC",
                db.GetConnection()
            );

            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvSonRandevular.DataSource = dt;
        }

        private void AnaPanelTasarim()
        {
            Panel panelMenu = new Panel();
            panelMenu.Name = "panelMenu";
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Width = 220;
            panelMenu.BackColor = Color.FromArgb(20, 20, 40);
            panelMenu.Dock = DockStyle.Left;

            this.Controls.Add(panelMenu);
        }
        private void AnaPanel_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnMusteriler_Click(object sender, EventArgs e)
        {
            MusterilerForm frm = new MusterilerForm();
            frm.ShowDialog();
        }

        private void btnRandevular_Click(object sender, EventArgs e)
        {
            RandevuForm frm = new RandevuForm();
            frm.ShowDialog();
        }

        private void btnOdemeler_Click(object sender, EventArgs e)
        {
            OdemeForm frm = new OdemeForm();
            frm.ShowDialog();
        }

        private void btnHizmetler_Click(object sender, EventArgs e)
        {
            HizmetlerForm frm = new HizmetlerForm();
            frm.ShowDialog();
        }

        private void btnPersoneller_Click(object sender, EventArgs e)
        {
            PersonelForm frm = new PersonelForm();
            frm.ShowDialog();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panelIcerik_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTarih.Text = DateTime.Now.ToShortDateString();
            lblSaat.Text = DateTime.Now.ToLongTimeString();
        }
    }
}
