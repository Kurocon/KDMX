using UnityEngine;
using KSP.IO;
using System.Xml;

namespace KDMX
{
    class KDMXHandler
    {
        public static bool debugMode = true;
        public static string eventName;
        public static int numSteps = 0;
        public static XmlDocument dmxPreferences;
        public static XmlNodeList dmxEventList;
        public static XmlNodeList dmxTimeblockList;
        public static string[] configuredEvents;
        public static string[] begin;
        public static string[] end;
        public static string[] change;
        public static string[] wait;

        public static void handleHook(string hookname)
        {
            if (!KDMX.DMXEventBusy)
            {
                KDMX.DMXEventBusy = true;
                KDMX.outputConsole("Event! " + hookname);
                getEventInfo(hookname);
                setEventDone();
            }
        }

        public static void setEventDone()
        {
            KDMX.DMXEventBusy = false;
        }

        public static void startDMX()
        {
            OpenDMX.start();
            LoadPrefs();
        }
        public static void setDMX(int channel, byte value)
        {
            OpenDMX.setDmxValue(channel, value);
        }

        public static void LoadPrefs()
        {

            //Create the XmlDocument.
            dmxPreferences = new XmlDocument();
            dmxPreferences.Load("GameData\\Kurocon\\Plugins\\PluginData\\DMXConfig.xml");

            //Display all events.
            dmxEventList = dmxPreferences.SelectNodes("/event");
        }

        public static void getEventInfo(string eventName)
        {
            for (int i = 0; i < dmxEventList.Count; i++)
            {
                string eventType = dmxEventList[i].Attributes["type"].InnerText;
                string eventContinuous = dmxEventList[i].Attributes["continuous"].InnerText;
                if (eventName == eventType)
                {
                    KDMX.outputConsole("Found an event with that name in the configuration!");
                    KDMX.outputConsole("Event name: " + eventType);
                    KDMX.outputConsole("Continuous: " + eventContinuous);
                    dmxTimeblockList = dmxEventList[i].SelectNodes("/timeblock");
                    for (int j = 0; j < dmxEventList[i].Count; j++)
                    {

                    }
                }
            }
        }
        
    }
}
