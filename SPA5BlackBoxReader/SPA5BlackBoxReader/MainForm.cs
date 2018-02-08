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
using System.IO;

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

                    String sConStr;
                    sConStr = openFileDialog1.FileName;

                    var fs = new FileStream(@sConStr, FileMode.Open);
                    var len = (int)fs.Length;
                    var bits = new byte[len];
                    fs.Read(bits, 0, len);

                    for (int ix = 0; ix < len; ix += 16)
                    {
                        var cnt = Math.Min(16, len - ix);
                        var line = new byte[cnt];
                        Array.Copy(bits, ix, line, 0, cnt);

                        //Console.Write("{0:X6}  ", ix);
                        textBoxBin.AppendText(ix.ToString());
                        //Console.Write(BitConverter.ToString(line));
                        textBoxBin.AppendText("  ");
                        textBoxBin.AppendText(BitConverter.ToString(line));
                        //Console.Write("  ");
                        textBoxBin.AppendText("  ");

                        for (int jx = 0; jx < cnt; ++jx)
                            if (line[jx] < 0x20 || line[jx] > 0x7f) line[jx] = (byte)'.';
                        //Console.WriteLine(Encoding.ASCII.GetString(line));
                        textBoxBin.AppendText(Environment.NewLine);
                    }

                    fs.Close();
                }
                else if (tabControl.SelectedTab == tabControl.TabPages["tabPageDecEvent"])
                {
                    textBoxZdekodowane.Clear();

                    String sConStr;
                    sConStr = openFileDialog1.FileName;

                    byte[] tablica = File.ReadAllBytes(@sConStr);

                    bool nowaRamka = false;
                    byte[,] ramka = new byte[200, 8];
                    int x = 0, y = 0;

                    for (int wiersz = 0; wiersz < ramka.GetLength(0) - 1; wiersz++)
                        for (int element = 0; element < ramka.GetLength(1) - 1; element++)
                            ramka[wiersz, element] = 0;

                    for (int i = 3; i < tablica.GetLength(0); i++)
                    {

                        if (((tablica[i - 3] == 0xff) && (tablica[i - 2] == 0xff) && (tablica[i - 1] == 0xff) && (tablica[i] == 0xff)))
                        //|| tablica[i])
                        {
                            textBoxZdekodowane.AppendText(Environment.NewLine);
                            textBoxZdekodowane.AppendText("#######################");
                            textBoxZdekodowane.AppendText(Environment.NewLine);

                            //nowaRamka = true;

                            textBoxZdekodowane.AppendText("calkowita liczba bajtów w bloku: ");
                            textBoxZdekodowane.AppendText(((ramka[0, 0] << 8) + ramka[0, 1]).ToString());
                            textBoxZdekodowane.AppendText(Environment.NewLine);

                            textBoxZdekodowane.AppendText("Numer przeajazdu: ");
                            textBoxZdekodowane.AppendText(((ramka[0, 2] << 7) + (ramka[0, 3] >> 1)).ToString());
                            textBoxZdekodowane.AppendText(Environment.NewLine);

                            textBoxZdekodowane.AppendText("Kanał: ");
                            if ((ramka[0, 3] & 0x01) == 0x00) textBoxZdekodowane.AppendText("A");
                            else textBoxZdekodowane.AppendText("B");
                            textBoxZdekodowane.AppendText(Environment.NewLine);

                            textBoxZdekodowane.AppendText("Typ bloku: ");
                            if (ramka[0, 4] == 0x01) textBoxZdekodowane.AppendText("blok danych");
                            else if (ramka[0, 4] == 0x02) textBoxZdekodowane.AppendText("blok pliku");
                            else textBoxZdekodowane.AppendText("nierozpoznany");
                            textBoxZdekodowane.AppendText(Environment.NewLine);

                            textBoxZdekodowane.AppendText("Time Stamp: ");
                            int temp = (ramka[1, 0] << 8) + ramka[1, 1];
                            textBoxZdekodowane.AppendText(((ramka[1, 0] << 8) + ramka[1, 1]).ToString() + "-"); //rok
                            textBoxZdekodowane.AppendText(ramka[1, 2].ToString() + "-"); //mies
                            textBoxZdekodowane.AppendText(ramka[1, 3].ToString() + ", "); //dzien
                            textBoxZdekodowane.AppendText(ramka[1, 4].ToString() + ":"); //h
                            textBoxZdekodowane.AppendText(ramka[1, 5].ToString() + ":"); //min
                            textBoxZdekodowane.AppendText(ramka[1, 6].ToString() + ", "); //s
                            textBoxZdekodowane.AppendText(ramka[1, 7].ToString() + "tick, ");
                            textBoxZdekodowane.AppendText(Environment.NewLine);


                            temp = (((ramka[0, 0] << 8) + ramka[0, 1]) - 4);
                            int iloscWiadomosci = ((((ramka[0, 0] << 8) + ramka[0, 1]) - 4) / 8);


                            for (int nrWiadomosci = 2; nrWiadomosci <= iloscWiadomosci - 1; nrWiadomosci++)
                            {
                                int typWiadomosci = ramka[nrWiadomosci, 0];

                                textBoxZdekodowane.AppendText("Typ wiadomości: ");
                                switch (typWiadomosci)
                                {
                                    case 0x01:
                                        textBoxZdekodowane.AppendText("lokalizacja przejazdu: ");
                                        textBoxZdekodowane.AppendText(Convert.ToString(ramka[nrWiadomosci, 1])
                                                                    + Convert.ToString(ramka[nrWiadomosci, 2])
                                                                    + Convert.ToString(ramka[nrWiadomosci, 3])
                                                                    + Convert.ToString(ramka[nrWiadomosci, 4])
                                                                    + Convert.ToString(ramka[nrWiadomosci, 5])
                                                                    + Convert.ToString(ramka[nrWiadomosci, 6])
                                                                    + Convert.ToString(ramka[nrWiadomosci, 7]));
                                        textBoxZdekodowane.AppendText(Environment.NewLine);
                                        break;
                                    case 0x02:
                                        textBoxZdekodowane.AppendText("alarm \n");
                                        //textBoxZdekodowane.AppendText(Environment.NewLine);
                                        textBoxZdekodowane.AppendText("numer alarmu: " + ((ramka[nrWiadomosci, 1] << 8) + ramka[nrWiadomosci, 2]).ToString() + "\n");
                                        //textBoxZdekodowane.AppendText(Environment.NewLine);
                                        textBoxZdekodowane.AppendText("status alarmu: ");
                                        if (ramka[nrWiadomosci, 3] == 0x01) textBoxZdekodowane.AppendText("nie aktywny");
                                        else if (ramka[nrWiadomosci, 3] == 0x02) textBoxZdekodowane.AppendText("aktywny");
                                        else textBoxZdekodowane.AppendText("nieznany, wartosc = " + ramka[nrWiadomosci, 3].ToString());
                                        textBoxZdekodowane.AppendText(Environment.NewLine);
                                        textBoxZdekodowane.AppendText("kategoria alarmu: ");
                                        if (ramka[nrWiadomosci, 4] == 0x01) textBoxZdekodowane.AppendText("pierwsza");
                                        else if (ramka[nrWiadomosci, 4] == 0x02) textBoxZdekodowane.AppendText("druga");
                                        else textBoxZdekodowane.AppendText("nieznany " + ramka[nrWiadomosci, 4].ToString());
                                        textBoxZdekodowane.AppendText(Environment.NewLine);
                                        textBoxZdekodowane.AppendText("grupa alarmu: " + ramka[nrWiadomosci, 5].ToString());
                                        textBoxZdekodowane.AppendText(Environment.NewLine);

                                        break;
                                    case 0x03:
                                        textBoxZdekodowane.AppendText("zdarzenie \n");
                                        //textBoxZdekodowane.AppendText(Environment.NewLine);
                                        textBoxZdekodowane.AppendText("numer zdarzenia: " + ((ramka[nrWiadomosci, 1] << 8) + ramka[nrWiadomosci, 2]).ToString() + "\n");
                                        //textBoxZdekodowane.AppendText(Environment.NewLine);
                                        textBoxZdekodowane.AppendText("status zdarzenia: ");
                                        if (ramka[nrWiadomosci, 3] == 0x01) textBoxZdekodowane.AppendText("nie aktywny");
                                        else if (ramka[nrWiadomosci, 3] == 0x02) textBoxZdekodowane.AppendText("aktywny");
                                        else textBoxZdekodowane.AppendText("nieznany" + ramka[nrWiadomosci, 3].ToString());
                                        textBoxZdekodowane.AppendText(Environment.NewLine);
                                        textBoxZdekodowane.AppendText("grupa azdarzenia: " + ramka[nrWiadomosci, 5].ToString());
                                        textBoxZdekodowane.AppendText(Environment.NewLine);

                                        break;
                                    case 0x04:
                                        textBoxZdekodowane.AppendText("Diagnostyka czujników ELS-95 \n");






                                        textBoxZdekodowane.AppendText(Environment.NewLine);
                                        break;
                                    case 0x05:
                                        textBoxZdekodowane.AppendText("Tryb pracy systemu SPA-5 \n");
                                        textBoxZdekodowane.AppendText("Tryb pracy: ");
                                        if (ramka[nrWiadomosci, 1] == 0x01) textBoxZdekodowane.AppendText("START");
                                        else if (ramka[nrWiadomosci, 1] == 0x02) textBoxZdekodowane.AppendText("DIAG");
                                        else if (ramka[nrWiadomosci, 1] == 0x08) textBoxZdekodowane.AppendText("ACTIVE");
                                        else textBoxZdekodowane.AppendText("nieznany" + ramka[nrWiadomosci, 3].ToString());
                                        textBoxZdekodowane.AppendText(Environment.NewLine);
                                        textBoxZdekodowane.AppendText("Numer modulu ktory wymusił przejscie do DIAG: ");
                                        textBoxZdekodowane.AppendText(ramka[nrWiadomosci, 2].ToString());
                                        textBoxZdekodowane.AppendText(Environment.NewLine);
                                        textBoxZdekodowane.AppendText("Numer bledu w module: ");
                                        textBoxZdekodowane.AppendText(ramka[nrWiadomosci, 3].ToString());
                                        textBoxZdekodowane.AppendText(Environment.NewLine);
                                        textBoxZdekodowane.AppendText("Numer modulu ktory wymusił przejscie do DIAG: ");
                                        textBoxZdekodowane.AppendText(ramka[nrWiadomosci, 4].ToString());
                                        textBoxZdekodowane.AppendText(Environment.NewLine);
                                        textBoxZdekodowane.AppendText("Numer bledu w module: ");
                                        textBoxZdekodowane.AppendText(ramka[nrWiadomosci, 5].ToString());
                                        textBoxZdekodowane.AppendText(Environment.NewLine);
                                        textBoxZdekodowane.AppendText("Numer modulu ktory wymusił przejscie do DIAG: ");
                                        textBoxZdekodowane.AppendText(ramka[nrWiadomosci, 6].ToString());
                                        textBoxZdekodowane.AppendText(Environment.NewLine);
                                        textBoxZdekodowane.AppendText("Numer bledu w module: ");
                                        textBoxZdekodowane.AppendText(ramka[nrWiadomosci, 7].ToString());
                                        textBoxZdekodowane.AppendText(Environment.NewLine);
                                        break;
                                    case 0x06:
                                        textBoxZdekodowane.AppendText("deskryptor pliku \n");



                                        textBoxZdekodowane.AppendText(Environment.NewLine);
                                        break;
                                    case 0x07:
                                        textBoxZdekodowane.AppendText("diagnostyka EHE-2w01 \n");



                                        textBoxZdekodowane.AppendText(Environment.NewLine);
                                        break;

                                    default:
                                        textBoxZdekodowane.AppendText("Nierozpoznany typ ramki!!! ");
                                        textBoxZdekodowane.AppendText(Environment.NewLine);
                                        break;


                                }




                            }


                            x = 0;
                            y = 0;

                            for (int wiersz = 0; wiersz < ramka.GetLength(0) - 1; wiersz++)
                                for (int element = 0; element < ramka.GetLength(1) - 1; element++)
                                    ramka[wiersz, element] = 0;

                        }
                        else
                        {
                            ramka[x, y] = tablica[i];
                            y++;
                            if (y >= 8)
                            {
                                y = 0;
                                x++;
                            }
                        }


                    }




                }

            }


        }

        private void labelCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }












    }
}
