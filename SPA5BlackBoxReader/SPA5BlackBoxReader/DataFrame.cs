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


    }
}
