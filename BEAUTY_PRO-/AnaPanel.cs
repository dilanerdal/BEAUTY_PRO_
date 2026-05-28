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
        }
        private void AnaPanelTasarim()
        {
            Panel panelMenu = new Panel();
            panelMenu.Name = "panelMenu";
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Width = 220;
            panelMenu.BackColor = Color.FromArgb(20, 20, 40);

            this.Controls.Add(panelMenu);
        }
        private void AnaPanel_Load(object sender, EventArgs e)
        {

        }
    }
}
