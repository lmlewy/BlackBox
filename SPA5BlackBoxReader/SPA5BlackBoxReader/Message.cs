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
        ResourceManager resmgr = new ResourceManager("SPA5BlackBoxReader.Lang", typeof(Message).Assembly);
        CultureInfo internalCI = null;

        public Message(CultureInfo ci)
        {
            internalCI = ci;
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
                    decodedMessage.Add("Message type Error");
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
            int alertNumber = 0;
            string alertName = "";
            string alertStatus = "";
            string alertCategory = "";
            string alertGroup = "";

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
            decodedAlarm.Add(alertGroup);

            return decodedAlarm;
        }

        private List<string> DecodeEvent(byte[] decEvent)
        {
            List<string> decodedEvent = new List<string>();
            int eventNumber = 0;
            string eventName = "";
            string eventStatus = "";
            //string eventCategory = "";
            string eventGroup = "";

            eventNumber = (decEvent[1] << 8) + decEvent[2];

            eventName = resmgr.GetString("event" + eventNumber.ToString(), internalCI);
            if (eventName == null) eventName = "Not Recognized event";

            if (decEvent[3] == 0x01) eventStatus = resmgr.GetString("eventNotActive", internalCI);
            else if (decEvent[3] == 0x02) eventStatus = resmgr.GetString("eventActive", internalCI);
            else eventStatus = resmgr.GetString("eventNotRecognized", internalCI);

            //if (decEvent[4] == 0x01) eventCategory = resmgr.GetString("eventFirstCategory", internalCI);
            //else if (decEvent[4] == 0x02) eventCategory = resmgr.GetString("eventSecondCategory", internalCI);
            //else eventCategory = resmgr.GetString("eventCategoryNotRecognized", internalCI);

            eventGroup = decEvent[5].ToString();

            decodedEvent.Add("Event number " + eventNumber.ToString());
            decodedEvent.Add(eventName);
            decodedEvent.Add(eventStatus);
            decodedEvent.Add("");
            decodedEvent.Add(eventGroup);

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
