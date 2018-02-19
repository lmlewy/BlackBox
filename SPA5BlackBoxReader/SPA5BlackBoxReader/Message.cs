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

        ResourceManager resmgr = new ResourceManager("SPA5BlackBoxReader.Lang", typeof(Message).Assembly);
        CultureInfo internalCI = null;

        public Message(CultureInfo ci)
        {
            internalCI = ci;

            alertNumber = 0;
            alertName = "";
            alertStatus = "";
        }


        public List<string> DecodeMessageToList(byte[] message)
        {
            List<string> decodedMessage = new List<string>();
            decodedMessage.Clear();
            switch (message[0])
            {
                case Constants.MESSTYPE_LOCATION:
                    decodedMessage.Add(DecodeLocation(message));
                    break;

                case Constants.MESSTYPE_ALERT:
                    decodedMessage = DecodeAlert(message).ToList();
                    break;

                case Constants.MESSTYPE_EVENT:
                    decodedMessage = DecodeEvent(message).ToList();
                    break;

                case Constants.MESSTYPE_ELS95:

                    break;

                case Constants.MESSTYPE_MODE:
                    decodedMessage.Add(DecodeMode(message));
                    break;

                case Constants.MESSTYPE_FILEDESC:

                    break;

                case Constants.MESSTYPE_EHE2:

                    break;

                default:

                    break;
            }

            return decodedMessage;
        }

        public string[] DecodeMessageToTable(byte[] message)
        {
            int i = 0;
            string[] decodedMessage = new string[4];
            switch (message[0])
            {
                case Constants.MESSTYPE_LOCATION:
                    decodedMessage[0] = DecodeLocation(message);
                    break;

                case Constants.MESSTYPE_ALERT:
                    foreach (string s in DecodeAlert(message))
                        decodedMessage[i++] = s;
                    break;

                case Constants.MESSTYPE_EVENT:
                    foreach (string s in DecodeEvent(message))
                        decodedMessage[i++] = s;
                    break;

                case Constants.MESSTYPE_ELS95:

                    break;

                case Constants.MESSTYPE_MODE:
                    decodedMessage[0] = DecodeMode(message);
                    break;

                case Constants.MESSTYPE_FILEDESC:

                    break;

                case Constants.MESSTYPE_EHE2:

                    break;

                default:

                    break;
            }

            return decodedMessage;
        }




        private string DecodeLocation(byte[] location)
        {

            return "System location";
        }

        private List<string> DecodeAlert(byte[] alert)
        {
            List<string> decodedAlarm = new List<string>();

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

            decodedAlarm.Add("Alert number " + alertNumber.ToString());
            decodedAlarm.Add(alertName);
            decodedAlarm.Add(alertStatus);
            decodedAlarm.Add(alertCategory);

            return decodedAlarm;
        }

        private List<string> DecodeEvent(byte[] decEvent)
        {
            List<string> decodedEvent = new List<string>();

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

            decodedEvent.Add("Event number " + alertNumber.ToString());
            decodedEvent.Add(alertName);
            decodedEvent.Add(alertStatus);
            decodedEvent.Add(alertCategory);

            return decodedEvent;
        }

        private string DecodeELS95(byte[] ELS95diag)
        {

            return "ELS-95 diagnostics";
        }

        private string DecodeMode(byte[] SPA5mode)
        {

            return "System mode";
        }

        private string DecodeFileDesc()
        {

            return "File descryptor";
        }

        private string DecodeEHE2(byte[] EHE2diag)
        {

            return "EHE-2 diagnostics";
        }



    }
}
