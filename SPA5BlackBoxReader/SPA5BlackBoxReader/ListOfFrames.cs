using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Resources;

namespace SPA5BlackBoxReader
{
    class ListOfFrames
    {
        CultureInfo cultureInfo = null;

        //private List<DataFrame> FrameList = new List<DataFrame>();
        private List<String> DecodedFrameList = new List<String>();

        public ListOfFrames(CultureInfo ci)
        {
            cultureInfo = ci;

        }


        public List<string> DecodeFile( byte[] binFile)
        {
            for (int i = binFile.Length - (1+5); i > 0; i--)
            {
                if ((binFile[i] == 0xff) && (binFile[i + 1] == 0xff) && (binFile[i + 2] == 0xff) && (binFile[i + 3] == 0xff) && (binFile[i + 4] != 0xff))
                {
                    int frameLenght = (binFile[i + 4] << 8) + binFile[i + 5];
                    byte[] b = new byte[frameLenght];
                    for (int j = 0; j < frameLenght; j++ )
                    {
                        b[j] = binFile[i + 4 + j];
                    }
                    DataFrame df = new DataFrame(cultureInfo);
                    df.DecodeDataFrame(b);
                    List<string> l = df.decodedFrame();
                    foreach (string s in l)
                        DecodedFrameList.Add(s);
                }

            }

             return DecodedFrameList;
        }



    }
}
