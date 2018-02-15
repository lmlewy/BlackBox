using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Resources;

namespace SPA5BlackBoxReader
{
    class Message
    {
        private List<string> decodedMessage = new List<string>();
        ResourceManager resmgr = new ResourceManager("SPA5BlackBoxReader.Lang", typeof(MainForm).Assembly);
        CultureInfo internalCI = null;

        Message(CultureInfo ci)
        {
            internalCI = ci;
        }


        public void DecodeMessage(byte[] message)
        {
            decodedMessage.Clear();
            switch (message[0])
            {
                case Constants.MESSTYPE_LOCATION:
                    DecodeLocation(message);
                    break;

                case Constants.MESSTYPE_ALERT:

                    break;

                case Constants.MESSTYPE_EVENT:

                    break;

                case Constants.MESSTYPE_ELS95:

                    break;

                case Constants.MESSTYPE_MODE:

                    break;

                case Constants.MESSTYPE_FILEDESC:

                    break;

                case Constants.MESSTYPE_EHE2:

                    break;

                default:


                    break;
            }



        }

        private void DecodeLocation(byte[] location)
        {


        }

        private List<string> DecodeAlert(byte[] alert)
        {
            List<string> decodedAlarm = new List<string>();

            int alertNumber = (alert[1] << 8) + alert[2];

            string alertName = resmgr.GetString("alert" + alertNumber.ToString() , internalCI);

            string alertStatus;
            if (alert[3] == 0x01) alertStatus = resmgr.GetString("alertNotActive", internalCI);
            else if (alert[3] == 0x02) alertStatus = resmgr.GetString("alertActive" + alertNumber.ToString(), internalCI);
            else alertStatus = resmgr.GetString("alertNotRecognized" + alertNumber.ToString(), internalCI);

            string alertCategory;
            if (alert[4] == 0x01) alertCategory = resmgr.GetString("alertFirstCategory" + alertNumber.ToString(), internalCI);
            else if (alert[4] == 0x02) alertCategory = resmgr.GetString("alertSecondCategory" + alertNumber.ToString(), internalCI);
            else alertCategory = resmgr.GetString("alertCategoryNotRecognized" + alertNumber.ToString(), internalCI);

            string alertGroup = alert[5].ToString();

            decodedAlarm.Add("Alert number " + alertNumber.ToString());
            decodedAlarm.Add(alertName);
            decodedAlarm.Add(alertStatus);
            decodedAlarm.Add(alertCategory);

            return decodedAlarm;
        }

        private void DecodeEvent(byte[] decEvent)
        {


        }

        private void DecodeELS95(byte[] ELS95diag)
        {


        }

        private void DecodeMode(byte[] SPA5mode)
        {


        }

        private void DecodeFileDesc()
        {


        }

        private void DecodeEHE2(byte[] EHE2diag)
        {


        }



    }
}
