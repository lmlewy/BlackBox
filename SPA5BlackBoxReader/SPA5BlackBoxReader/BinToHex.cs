using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;
using System.Globalization;
using System.IO;

namespace SPA5BlackBoxReader
{
    class BinToHex
    {
        public BinToHex()
        {

        }

        public void readFile(String sConStr)
        {




            var fs = new FileStream(@sConStr, FileMode.Open);
            var len = (int)fs.Length;
            var bits = new byte[len];
            fs.Read(bits, 0, len);

            for (int ix = 0; ix < len; ix += 16)
            {
                var cnt = Math.Min(16, len - ix);
                var line = new byte[cnt];
                Array.Copy(bits, ix, line, 0, cnt);

                //////Console.Write("{0:X6}  ", ix);
                //textBoxBin.AppendText(ix.ToString());
                //////Console.Write(BitConverter.ToString(line));
                //textBoxBin.AppendText("  ");
                //textBoxBin.AppendText(BitConverter.ToString(line));
                //////Console.Write("  ");
                //textBoxBin.AppendText("  ");

                //for (int jx = 0; jx < cnt; ++jx)
                //    if (line[jx] < 0x20 || line[jx] > 0x7f) line[jx] = (byte)'.';
                //////Console.WriteLine(Encoding.ASCII.GetString(line));
                //textBoxBin.AppendText(Environment.NewLine);
            }

            fs.Close();





        }







    }
}
