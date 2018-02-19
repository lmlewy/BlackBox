﻿using System;
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
        

        public ListOfFrames(CultureInfo ci)
        {
            cultureInfo = ci;
        }


        public List<string> DecodeFileAsList( byte[] binFile)
        {
            List<String> DecodedFrameList = new List<String>();

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
                    //df.DecodeDataFrame(b);
                    List<string> l = df.DecodeDataFrameToList(b);
                    foreach (string s in l)
                        DecodedFrameList.Add(s);
                }

            }

             return DecodedFrameList;
        }

        public string[][] DecodeFileAsTable(byte[] binFile)
        {
            string[][] table = new string[10000][];
            int numberOfFrames = 0;

            //table[0] = new string[5];
            //table[0][0] = "aaa";
            //table[0][1] = "bbb";
            //table[0][2] = "ccc";
            //table[0][3] = "ddd";
            //table[0][4] = "eee";

            for (int i = binFile.Length - (1 + 5); i > 0; i--)
            {
                if ((binFile[i] == 0xff) && (binFile[i + 1] == 0xff) && (binFile[i + 2] == 0xff) && (binFile[i + 3] == 0xff) && (binFile[i + 4] != 0xff))
                {
                    int frameLenght = (binFile[i + 4] << 8) + binFile[i + 5];
                    byte[] b = new byte[frameLenght];
                    for (int j = 0; j < frameLenght; j++)
                    {
                        b[j] = binFile[i + 4 + j];
                    }
                    DataFrame df = new DataFrame(cultureInfo);
                    string[][] table2 = new string[10][];
                    table2 = df.DecodeDataFrameToTable(b);
                    
                    //foreach (string s in l)
                    //{
                    //}

                    for (int j = 0; j < 4; j++)
                    {
                        //table[numberOfFrames][j] = t[j];
                    }




                }

            }


            return table;
        }



    }
}
