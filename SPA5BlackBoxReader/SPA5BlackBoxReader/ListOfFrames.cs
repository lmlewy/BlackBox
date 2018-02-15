using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPA5BlackBoxReader
{
    class ListOfFrames
    {
        private List<DataFrame> FrameList = new List<DataFrame>();
        private List<String> DecodedFrameList = new List<String>();

        public void ReadFile( byte[] binFile)
        {
            DecodedFrameList.Add("aaa");






        }

        public List<String> Ramki()
        {
            return DecodedFrameList;
        }


    }
}
