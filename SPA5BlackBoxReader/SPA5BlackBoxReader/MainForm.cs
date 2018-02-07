using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;
using System.Globalization;

namespace SPA5BlackBoxReader
{
    public partial class MainForm : Form
    {
        CultureInfo ci = null;
        ResourceManager resmgr = new ResourceManager("SPA5BlackBoxReader.Lang", typeof(MainForm).Assembly);

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ci = new CultureInfo("en-GB");
            updateLabels();
        }

        private void updateLabels()
        {
            this.labelFileToolStripMenuItem.Text = resmgr.GetString("labelFile", ci);
            this.labelReadToolStripMenuItem.Text = resmgr.GetString("labelRead", ci);
            this.labelChngLangToolStripMenuItem.Text = resmgr.GetString("labelChngLang", ci);
            this.labelCloseToolStripMenuItem.Text = resmgr.GetString("labelClose", ci);

            this.labelInfoToolStripMenuItem.Text = resmgr.GetString("labelInfo", ci);
            this.labelAboutProgToolStripMenuItem.Text = resmgr.GetString("labelAboutProg", ci);

            this.tabPageBin.Text = resmgr.GetString("labelBin", ci);
            this.tabPageDecEvent.Text = resmgr.GetString("labelDecEvent", ci);

        }

        private void polskiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ci = new CultureInfo("pl-PL");
            updateLabels();
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ci = new CultureInfo("en-GB");
            updateLabels();
        }


        private void labelReadToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void labelCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }










    }
}
