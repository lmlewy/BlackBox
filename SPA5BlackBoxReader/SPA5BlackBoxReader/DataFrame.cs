using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Resources;

namespace SPA5BlackBoxReader
{
    class DataFrame
    {
        CultureInfo cultureInfo = null;

        private int blkLenght;
        private int lxNumb;
        private int blkType;
        private DateTime timeStamp;
        private int timeStampTick;

        private List<string> listOfMessages = new List<string>();
        private int CRC32;

        public DataFrame(CultureInfo c)
        {
        cultureInfo = c;
        blkLenght = 0;
        lxNumb = 0;
        blkType = 0;
        timeStamp = new DateTime(2000, 1, 1, 0, 0, 0);
        //timeStamp
        timeStampTick = 0;
        }

        public void DecodeDataFrame(byte[] frame)
        {
            blkLenght = (frame[0] << 8) + frame[1];
            lxNumb = (frame[2] << 8) + frame[3];
            blkType = frame[4];

            timeStamp = new DateTime(((frame[8] << 8) + frame[9]), frame[10], frame[11], frame[12], frame[13], frame[14]);
            timeStampTick = frame[15];

            CRC32 = (frame[blkLenght - 4]<<24) + (frame[blkLenght - 3]<<16) + (frame[blkLenght - 2]<<8) + frame[blkLenght - 1];

            if (checkCRC(CRC32) == true)
            {
                if (blkType == 1)
                {
                    int numberOfFrames = (blkLenght - 16) / 4;
                    byte[] byteMessage = new byte[8];

                    for (int i = 0; i < numberOfFrames; i++)
                    {
                        for (int b = 0; b < 8; b++)
                        {
                            byteMessage[b] = frame[16+i+b];
                        }

                        Message mess = new Message(cultureInfo);
                        mess.DecodeMessage(byteMessage);
                        List<string> l = new List<string>();
                        l = mess.DecodedMessage;

                        listOfMessages.Add(timeStamp.ToString() + ":");
                        foreach(string s in l)
                        {
                            listOfMessages.Add(s);
                        }

                    }

                }
            }

        }

        private bool checkCRC(int crc)
        {
            bool OK = true;

            return OK;
        }

        public List<string> decodedFrame()
        {
            return listOfMessages;

            
        }




    }
}
