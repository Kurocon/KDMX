using UnityEngine;
using KSP.IO;
using System.Xml;

namespace KDMX
{
    [KSPAddon(KSPAddon.Startup.MainMenu, false)]
    public class KDMX : MonoBehaviour
    {
        private static Rect windowPosition = new Rect(0, 0, 320, 240);
        private static GUIStyle windowStyle = null;
        private static bool buttonState = false;

        public void Awake() {
            print("[KDMX] We have awoken from our endless slumber!");
            RenderingManager.AddToPostDrawQueue(0, OnDraw);
        }
        public void Start()
        {
            print("[KDMX] And now we are starting to take over the entire Kerbalverse!");
            print("[KDMX] Loading Preferences!!");
            LoadPrefs();
            print("[KDMX] Preferences Loaded!");
            windowStyle = new GUIStyle(HighLogic.Skin.window);
        }

        private void OnDraw()
        {
            windowPosition = GUI.Window(1234, windowPosition, OnWindow, "KDMX Control", windowStyle);
        }

        private void OnWindow(int windowID){
            GUILayout.BeginHorizontal();
            GUILayout.Label("ABC-");
            GUILayout.Label("123");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            buttonState = GUILayout.Toggle(buttonState, "Button State: " + buttonState);
            GUILayout.EndHorizontal();           
            
            GUI.DragWindow();
        }

        public static void LoadPrefs()
        {
            //config = PluginConfiguration.CreateForType<KDMX>(null);
            //config.load();
            //testConfig = config.GetValue<object>("testConfig", testConfig);
            //saveConfigSettings();

            //Create the XmlDocument.
            XmlDocument doc = new XmlDocument();
            doc.Load("GameData\\Kurocon\\Plugins\\PluginData\\DMXConfig.xml");

            //Display all events.
            XmlNodeList elemList = doc.GetElementsByTagName("event");
            for (int i = 0; i < elemList.Count; i++)
            {
                print(elemList[i].Value);
            }
        }
    }
}