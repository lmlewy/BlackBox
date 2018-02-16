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
        private int alertNumber;
        private string alertName;
        private string alertStatus;
        private string alertCategory;
        private string alertGroup;

        public int AlertNumber
        {
            get { return alertNumber; }
        }

        public string AlertName
        {
            get { return alertName; }
        }

        public string AlertStatus
        {
            get { return alertStatus; }
        }

        public List<string> DecodedMessage
        {
            get { return decodedMessage;  }
        }

        ResourceManager resmgr = new ResourceManager("SPA5BlackBoxReader.Lang", typeof(Message).Assembly);
        CultureInfo internalCI = null;

        public Message(CultureInfo ci)
        {
            internalCI = ci;

            alertNumber = 0;
            alertName = "";
            alertStatus = "";
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
                    DecodeAlert(message);
                    break;

                case Constants.MESSTYPE_EVENT:
                    DecodeEvent(message);
                    break;

                case Constants.MESSTYPE_ELS95:

                    break;

                case Constants.MESSTYPE_MODE:
                    DecodeMode(message);
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
            decodedMessage.Add("System location");

        }

        private void DecodeAlert(byte[] alert)
        {
            //List<string> decodedAlarm = new List<string>();

            alertNumber = (alert[1] << 8) + alert[2];

            alertName = resmgr.GetString("alert" + alertNumber.ToString(), internalCI);
            if (alertName == null) alertName = "Not Recognized event";

            if (alert[3] == 0x01) alertStatus = resmgr.GetString("alertNotActive", internalCI);
            else if (alert[3] == 0x02) alertStatus = resmgr.GetString("alertActive" , internalCI);
            else alertStatus = resmgr.GetString("alertNotRecognized" , internalCI);
           
            if (alert[4] == 0x01) alertCategory = resmgr.GetString("alertFirstCategory" , internalCI);
            else if (alert[4] == 0x02) alertCategory = resmgr.GetString("alertSecondCategory" , internalCI);
            else alertCategory = resmgr.GetString("alertCategoryNotRecognized" , internalCI);

            alertGroup = alert[5].ToString();

            decodedMessage.Add("Alert number " + alertNumber.ToString());
            decodedMessage.Add(alertName);
            decodedMessage.Add(alertStatus);
            decodedMessage.Add(alertCategory);

            //return decodedAlarm;
        }

        private void DecodeEvent(byte[] decEvent)
        {
            alertNumber = (decEvent[1] << 8) + decEvent[2];

            alertName = resmgr.GetString("event" + alertNumber.ToString(), internalCI);
            if (alertName == null) alertName = "Not Recognized event";

            if (decEvent[3] == 0x01) alertStatus = resmgr.GetString("eventNotActive", internalCI);
            else if (decEvent[3] == 0x02) alertStatus = resmgr.GetString("eventActive" , internalCI);
            else alertStatus = resmgr.GetString("eventNotRecognized" , internalCI);

            if (decEvent[4] == 0x01) alertCategory = resmgr.GetString("eventFirstCategory" , internalCI);
            else if (decEvent[4] == 0x02) alertCategory = resmgr.GetString("eventSecondCategory" , internalCI);
            else alertCategory = resmgr.GetString("eventCategoryNotRecognized" , internalCI);

            alertGroup = decEvent[5].ToString();

            decodedMessage.Add("Event number " + alertNumber.ToString());
            decodedMessage.Add(alertName);
            decodedMessage.Add(alertStatus);
            decodedMessage.Add(alertCategory);

        }

        private void DecodeELS95(byte[] ELS95diag)
        {
            decodedMessage.Add("ELS-95 diagnostics");

        }

        private void DecodeMode(byte[] SPA5mode)
        {
            decodedMessage.Add("System mode");

        }

        private void DecodeFileDesc()
        {
            decodedMessage.Add("File descryptor");

        }

        private void DecodeEHE2(byte[] EHE2diag)
        {
            decodedMessage.Add("EHE-2 diagnostics");

        }



    }
}
