using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPA5BlackBoxReader
{
    class DataFrame
    {
        private int blkLenght;
        private int lxNumb;
        private int blkType;
        private DateTime timeStamp;
        private int timeStampTick;

        private List<byte[]> listOfMessages = new List<byte[]>();
        private long CRC32;

        DataFrame()
        {

        }

        public void DecodeDataFrame(byte[] frame)
        {
            switch (frame[0])
            {
                case 1:
                    Console.WriteLine("jeden");
                    break;

                case 2:
                    Console.WriteLine("dwa");
                    break;

                case 3:
                    Console.WriteLine("trzy");
                    break;

                default:
                    Console.WriteLine("Domyślna akcja - inny numer");
                    break;
            }



        }

 

    }
}
