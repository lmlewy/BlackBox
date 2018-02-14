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
        private BackgroundWorker decodeMessagesBackgroundWorker = null;

        List<string> itemsList = new List<string>();    //to jest szybkie

        byte[] ramka = new byte[500];
        List<byte[]> ramkaList = new List<byte[]>();

        String sConStr;

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
                int l;
                if ( ( l = fileBytes.Length) > 5)
                {
                    //Parallel.For(3, fileBytes.Length, currentByte =>
                    for (int currentByte = 0; currentByte < fileBytes.Length; currentByte++)
                    {
                        if ((fileBytes[currentByte] == 0xff) && (fileBytes[currentByte + 1] == 0xff) &&
                            (fileBytes[currentByte + 2] == 0xff) && (fileBytes[currentByte + 3] == 0xff))
                        {
                            int frameLenght = (fileBytes[currentByte + 4] << 8) + fileBytes[currentByte + 5];
                            //byte[] ramka = new byte[500];
                            if ((fileBytes[currentByte + 8]) == 1)
                            {
                                for (int n = 0; n < frameLenght; n++)
                                {
                                    ramka[n] = fileBytes[currentByte + 4 + n];
                                }
                                ramkaList.Add(ramka);
                            }
                        }
                    }
                    //});

                }
                else
                {

                }

                foreach (byte[] r in ramkaList)
                {


                }





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
                    textBoxZdekodowane.Clear();

                    sConStr = openFileDialog1.FileName;

                    byte[] tablica = File.ReadAllBytes(@sConStr);

                    if (null == decodeMessagesBackgroundWorker)
                    {
                        decodeMessagesBackgroundWorker = new BackgroundWorker();
                        decodeMessagesBackgroundWorker.DoWork += new DoWorkEventHandler(decodeMessagesBackgroundWorker_DoWork);
                        decodeMessagesBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(decodeMessagesBackgroundWorker_RunWorkerCompleted);
                        decodeMessagesBackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(decodeMessagesBackgroundWorker_ProgressChanged);
                        decodeMessagesBackgroundWorker.WorkerReportsProgress = true;
                        decodeMessagesBackgroundWorker.WorkerSupportsCancellation = true;
                    }
                    decodeMessagesBackgroundWorker.RunWorkerAsync();               




                }

            }


        }


        //private string BinaryStringToHexString(string binary)
        //{
        //    StringBuilder result = new StringBuilder(binary.Length / 8 + 1);

        //    // TODO: check all 1's or 0's... Will throw otherwise

        //    int mod4Len = binary.Length % 8;
        //    if (mod4Len != 0)
        //    {
        //        // pad to length multiple of 8
        //        binary = binary.PadLeft(((binary.Length / 8) + 1) * 8, '0');
        //    }

        //    for (int i = 0; i < binary.Length; i += 8)
        //    {
        //        string eightBits = binary.Substring(i, 8);
        //        result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
        //    }

        //    return result.ToString();
        //}



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



        private void AppendTextBoxBinRew(string sText)
        {
            richTextBoxBin.Text = richTextBoxBin.Text.Insert(0,"\r\n" + sText);//działa z BackgroundWorkerem
        }

        private void AppendTextBoxBin(string sText)
        {
            richTextBoxBin.Text = richTextBoxBin.Text + sText + "\n";
        }


        ////BackgroundWorker
        void binHexBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //var fs = new FileStream(@sConStr, FileMode.Open);
            //var len = (int)fs.Length;
            //var bits = new byte[len];
            //fs.Read(bits, 0, len);
            //string textLine = "";
            //int progress = 0;

            //for (int ix = 0; ix < len; ix += 16)
            //{
            //    var cnt = Math.Min(16, len - ix);
            //    var line = new byte[cnt];
            //    Array.Copy(bits, ix, line, 0, cnt);

            //    textLine += ix.ToString();
            //    textLine += "   ";
            //    textLine += BitConverter.ToString(line);
            //    textLine += "   ";

            //    ///for (int jx = 0; jx < cnt; ++jx)
            //    //    if (line[jx] < 0x20 || line[jx] > 0x7f) line[jx] = (byte)'.';
            //    //////Console.WriteLine(Encoding.ASCII.GetString(line));
            //    //textBoxBin.AppendText(Environment.NewLine);
            //    textLine += Environment.NewLine;



            //    progress = Convert.ToInt32(((ix * 1.0)/len)*100.0);
            //    Thread.Sleep(50);
            //    binHexBackgroundWorker.ReportProgress(progress, textLine);
            //    textLine = "";
            //}

            //fs.Close();

            int numberOfElements = itemsList.Count();
            int progress = 0, n = 0;

            //foreach (string linia in itemsList)
            for (int i = itemsList.Count - 1; i >= 0; i--)
            {
                
                //int arrayLenght = mojaTab.Length;

                    if (binHexBackgroundWorker.CancellationPending)
                        {
                            e.Cancel = true;
                            break;
                        }

                    Thread.Sleep(20);
                    n++;
                    progress = Convert.ToInt32(((n * 1.0) / numberOfElements) * 100.0);
                    binHexBackgroundWorker.ReportProgress(progress, itemsList[i]);
                    

            }






        }

        void binHexBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            AppendTextBoxBin(e.UserState.ToString());
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






        void decodeMessagesBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string textLine = "";
            int progress = 0;


            if (decodeMessagesBackgroundWorker.CancellationPending)
            {
                e.Cancel = true;
                //break; to musi byc w pętli
            }


            Thread.Sleep(50);
            decodeMessagesBackgroundWorker.ReportProgress(progress, textLine);
        }


        void decodeMessagesBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            AppendTextBoxBin(e.UserState.ToString() + "\n");
            toolStripProgressBar.Value = e.ProgressPercentage;
        }

        void decodeMessagesBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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
