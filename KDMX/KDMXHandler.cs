using UnityEngine;
using KSP.IO;

namespace KDMX
{
    class KDMXHandler
    {
        public static void handleHook(string hookname)
        {
            if (!KDMX.DMXEventBusy)
            {
                KDMX.DMXEventBusy = true;
                KDMX.outputConsole(hookname);
            }
        }
        public static void setEventDone(){
            {
    }
}
