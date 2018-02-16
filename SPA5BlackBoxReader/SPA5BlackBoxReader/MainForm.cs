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

        String sConStr;

        private BackgroundWorker binHexBackgroundWorker = null;
        private BackgroundWorker decodeMessagesBackgroundWorker = null;

        List<string> itemsList = new List<string>();    //to jest szybkie
        List<string> decodedFramesList = new List<string>();

        //byte[] ramka = new byte[500];
        //List<byte[]> ramkaList = new List<byte[]>();


        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ci = new CultureInfo("en-GB");
            updateLabels();

            richTextBoxBin.Multiline = true;
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

                sConStr = openFileDialog1.FileName;
                byte[] fileBytes = File.ReadAllBytes(@sConStr);

                ListOfFrames decodedList = new ListOfFrames(ci);
                decodedFramesList = decodedList.DecodeFile(fileBytes);





                if (tabControl.SelectedTab == tabControl.TabPages["tabPageBin"])
                {
                    richTextBoxBin.Clear();

                    ////To działa - jest średnio szybkie
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


                    //to jest szybkie ale wykorzystuje listę
                    string bufor = "";
                    int hexNumber = 0, lineNumber = 0;

                    itemsList.Clear();
                    foreach (byte b in fileBytes)
                    {
                        if (hexNumber == 0) bufor += lineNumber.ToString() + ": ";
                        bufor += " ";
                        if (b < 16) bufor += "0";
                        bufor += b.ToString("x").ToUpperInvariant();
                        hexNumber++;
                        if (hexNumber > 16)
                        {
                            itemsList.Add(bufor);
                            bufor = "";
                            hexNumber = 0;
                            lineNumber += 16;
                        }
                    }

                    string calosc = "";
                    ////for (int i = itemsList.Count - 1; i >= 0; i--)
                    for (int i = 0; i < itemsList.Count; i++)
                    {
                        calosc = calosc + itemsList[i] + '\n';
                    }
                    richTextBoxBin.Text = calosc;
                    // koniec fragmentu z listą

                }
                else if (tabControl.SelectedTab == tabControl.TabPages["tabPageDecEvent"])
                {
                    richTextBoxZdekodowane.Clear();


                    string calosc = "";
                    ////for (int i = itemsList.Count - 1; i >= 0; i--)
                    for (int i = 0; i < decodedFramesList.Count; i++)
                    {
                        calosc = calosc + decodedFramesList[i] + '\n';
                    }
                    richTextBoxZdekodowane.Text = calosc;

                    //if (null == decodeMessagesBackgroundWorker)
                    //{
                        //decodeMessagesBackgroundWorker = new BackgroundWorker();
                        //decodeMessagesBackgroundWorker.DoWork += new DoWorkEventHandler(decodeMessagesBackgroundWorker_DoWork);
                        //decodeMessagesBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(decodeMessagesBackgroundWorker_RunWorkerCompleted);
                        //decodeMessagesBackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(decodeMessagesBackgroundWorker_ProgressChanged);
                        //decodeMessagesBackgroundWorker.WorkerReportsProgress = true;
                        //decodeMessagesBackgroundWorker.WorkerSupportsCancellation = true;
                    //}
                    //decodeMessagesBackgroundWorker.RunWorkerAsync();               




                }

            }


        }


        private void labelStopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((null != binHexBackgroundWorker) && binHexBackgroundWorker.IsBusy)
            {
                binHexBackgroundWorker.CancelAsync();
            }

            if ((null != decodeMessagesBackgroundWorker) && decodeMessagesBackgroundWorker.IsBusy)
            {
                decodeMessagesBackgroundWorker.CancelAsync();
            }
        }


        private void labelCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
