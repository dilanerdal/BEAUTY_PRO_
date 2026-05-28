using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BEAUTY_PRO_
{
    public partial class AnaPanel : Form
    {
        public AnaPanel()
        {
            InitializeComponent();
            AnaPanelTasarim();
            DashboardVerileriGetir();
        }

        private void DashboardVerileriGetir()
        {
            lblAktifMusteri.Text = "256";
            lblBugunkuRandevu.Text = "12";
            lblAylikKazanc.Text = "₺28.450";
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
    }
}
