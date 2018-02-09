using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;
using System.Globalization;
using System.IO;

namespace SPA5BlackBoxReader
{
    public partial class MainForm : Form
    {
        CultureInfo ci = null;
        ResourceManager resmgr = new ResourceManager("SPA5BlackBoxReader.Lang", typeof(MainForm).Assembly);

        private BackgroundWorker binHexBackgroundWorker = null;

        String sConStr;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ci = new CultureInfo("en-GB");
            updateLabels();

            textBoxBin.Multiline = true;
            textBoxBin.ScrollBars = ScrollBars.Vertical;
            textBoxBin.AcceptsReturn = true;
            textBoxBin.AcceptsTab = true;
            textBoxBin.WordWrap = true;

            textBoxBin.Multiline = true;
            textBoxBin.ScrollBars = ScrollBars.Vertical;
            textBoxBin.AcceptsReturn = true;
            textBoxBin.AcceptsTab = true;
            textBoxBin.WordWrap = true;

        }

        private void updateLabels()
        {
            this.labelFileToolStripMenuItem.Text = resmgr.GetString("labelFile", ci);
            this.labelReadToolStripMenuItem.Text = resmgr.GetString("labelRead", ci);
            this.labelChngLangToolStripMenuItem.Text = resmgr.GetString("labelChngLang", ci);
            this.labelStopToolStripMenuItem.Text = resmgr.GetString("labelStop", ci);
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
            var openFileDialog1 = new OpenFileDialog();
            var iResult = DialogResult;
            openFileDialog1.Title = "Podaj plik czarnej skrzynki";
            openFileDialog1.Filter = "BIN Files (*.bin) | *.bin";

            openFileDialog1.CheckFileExists = true;
            iResult = openFileDialog1.ShowDialog();

            if ((iResult != System.Windows.Forms.DialogResult.Cancel) && (openFileDialog1.FileName.Length != 0))
            {
                if (tabControl.SelectedTab == tabControl.TabPages["tabPageBin"])
                {
                    textBoxBin.Clear();

                    //String sConStr;
                    sConStr = openFileDialog1.FileName;


                    ////To działa
                    //if (null == binHexBackgroundWorker)
                    //{
                    //    binHexBackgroundWorker = new BackgroundWorker();
                    //    binHexBackgroundWorker.DoWork += new DoWorkEventHandler(binHexBackgroundWorker_DoWork);
                    //    binHexBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(binHexBackgroundWorker_RunWorkerCompleted);
                    //    binHexBackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(binHexBackgroundWorker_ProgressChanged);
                    //    binHexBackgroundWorker.WorkerReportsProgress = true;
                    //    binHexBackgroundWorker.WorkerSupportsCancellation = true;
                    //}
                    //binHexBackgroundWorker.RunWorkerAsync();


                    byte[] fileBytes = File.ReadAllBytes(@sConStr);
                    string bufor = "";
                    int hexNumber = 0;
                    foreach (byte b in fileBytes)
                    {
                        if (b < 10) bufor += " ";
                        bufor += b.ToString("x");
                        bufor += " ";
                        if (b < 10) bufor += " ";
                        hexNumber++;
                        if (hexNumber > 16)
                        {
                            bufor += Environment.NewLine;
                            hexNumber = 0;
                        }
                    }

                    textBoxBin.Text = bufor;
 




                }
                else if (tabControl.SelectedTab == tabControl.TabPages["tabPageDecEvent"])
                {
                    textBoxZdekodowane.Clear();

                    sConStr = openFileDialog1.FileName;

                    byte[] tablica = File.ReadAllBytes(@sConStr);

                                        




                }

            }


        }


        private string BinaryStringToHexString(string binary)
        {
            StringBuilder result = new StringBuilder(binary.Length / 8 + 1);

            // TODO: check all 1's or 0's... Will throw otherwise

            int mod4Len = binary.Length % 8;
            if (mod4Len != 0)
            {
                // pad to length multiple of 8
                binary = binary.PadLeft(((binary.Length / 8) + 1) * 8, '0');
            }

            for (int i = 0; i < binary.Length; i += 8)
            {
                string eightBits = binary.Substring(i, 8);
                result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }

            return result.ToString();
        }



        private void labelStopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((null != binHexBackgroundWorker) && binHexBackgroundWorker.IsBusy)
            {
                binHexBackgroundWorker.CancelAsync();
            }

        }


        private void labelCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        private void AppendTextBoxBinRew(string sText)
        {
            textBoxBin.Text = textBoxBin.Text.Insert(0,"\r\n" + sText);//działa z BackgroundWorkerem
        }

        private void AppendTextBoxBin(string sText)
        {
            textBoxBin.Text = textBoxBin.Text + sText + "\n";
        }


        ////BackgroundWorker
        void binHexBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var fs = new FileStream(@sConStr, FileMode.Open);
            var len = (int)fs.Length;
            var bits = new byte[len];
            fs.Read(bits, 0, len);
            string textLine = "";
            int progress = 0;

            for (int ix = 0; ix < len; ix += 16)
            {
                var cnt = Math.Min(16, len - ix);
                var line = new byte[cnt];
                Array.Copy(bits, ix, line, 0, cnt);

                textLine += ix.ToString();
                textLine += "   ";
                textLine += BitConverter.ToString(line);
                textLine += "   ";

                ///for (int jx = 0; jx < cnt; ++jx)
                //    if (line[jx] < 0x20 || line[jx] > 0x7f) line[jx] = (byte)'.';
                //////Console.WriteLine(Encoding.ASCII.GetString(line));
                //textBoxBin.AppendText(Environment.NewLine);
                textLine += Environment.NewLine;

                if (binHexBackgroundWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }

                progress = Convert.ToInt32(((ix * 1.0)/len)*100.0);
                Thread.Sleep(50);
                binHexBackgroundWorker.ReportProgress(progress, textLine);
                textLine = "";
            }

            fs.Close();

        }

        void binHexBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            AppendTextBoxBin(e.UserState.ToString() + "\n");
            toolStripProgressBar.Value = e.ProgressPercentage;
        }

        void binHexBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                AppendTextBoxBin("Przerwano.");
            }
            else
            {
                AppendTextBoxBin("Zakończono.");
                toolStripProgressBar.Value = 0;
            }
        }



    }
}
