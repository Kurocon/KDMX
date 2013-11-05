using UnityEngine;
using KSP.IO;
using System.Xml;

namespace KDMX
{
    [KSPAddon(KSPAddon.Startup.EveryScene, false)]
    public class KDMX : MonoBehaviour
    {
        private static Rect windowPosition = new Rect(0, 0, 320, 240);
        private static GUIStyle windowStyle = null;
        private static float slider1 = 0;
        private static float slider2 = 0;
        private static float slider3 = 0;
        private static Rect position1 = new Rect(624, 686, 100, 100);
        private static Rect position2 = new Rect(724, 686, 100, 100);
        private static Rect position3 = new Rect(824, 686, 100, 100);
        private static Rect position4 = new Rect(624, 586, 300, 100);
        private static Color red = new Color(255, 0, 0);
        private static Color green = new Color(0, 255, 0);
        private static Color blue = new Color(0, 0, 255);
        public static float DMXRed = 0;
        public static float DMXGreen = 0;
        public static float DMXBlue = 0;
        public static bool DMXEventBusy = false;

        public void Awake() {
            print("[KDMX] We have awoken from our endless slumber!");
            RenderingManager.AddToPostDrawQueue(0, OnDraw);
        }
        public void Start()
        {
            print("[KDMX] And now we are starting to take over the entire Kerbalverse!");
            string currentLevel = Application.loadedLevelName;
            print("[KDMX] Current level is " + currentLevel);

            if (currentLevel != "loadingBuffer")
            {
                print("[KDMX] We are not loading!");
                print("[KDMX] Loading Preferences!!");
                LoadPrefs();
                print("[KDMX] Preferences Loaded!");

                print("[KDMX] Adding hooks to GameEvents");
                GameEvents.onCollision.Add(this.onCollisionHook);
                print("[KDMX] Hook added: onCollision");
                GameEvents.onCrash.Add(this.onCrashHook);
                print("[KDMX] Hook added: onCrash");
                GameEvents.onCrashSplashdown.Add(this.onCrashSplashdownHook);
                print("[KDMX] Hook added: onCrashSplashdown");
                GameEvents.onCrewBoardVessel.Add(this.onCrewBoardVesselHook);
                print("[KDMX] Hook added: onCrewBoardVessel");
                GameEvents.onCrewKilled.Add(this.onCrewKilledHook);
                print("[KDMX] Hook added: onCrewKilled");
                GameEvents.onCrewOnEva.Add(this.onCrewOnEvaHook);
                print("[KDMX] Hook added: onCrewEva");
                GameEvents.onDominantBodyChange.Add(this.onDominantBodyChangeHook);
                print("[KDMX] Hook added: onDominantBodyChange");
                GameEvents.onGamePause.Add(this.onGamePauseHook);
                print("[KDMX] Hook added: onGamePause");
                GameEvents.onGameUnpause.Add(this.onGameUnpauseHook);
                print("[KDMX] Hook added: onGameUnpause");
                GameEvents.onJointBreak.Add(this.onJointBreakHook);
                print("[KDMX] Hook added: onJointBreak");
                GameEvents.onLaunch.Add(this.onLaunchHook);
                print("[KDMX] Hook added: onLaunch");
                GameEvents.onOverheat.Add(this.onOverheatHook);
                print("[KDMX] Hook added: onOverheat");
                GameEvents.onPartCouple.Add(this.onPartCoupleHook);
                print("[KDMX] Hook added: onPartCouple");
                GameEvents.onPartExplode.Add(this.onPartExplodeHook);
                print("[KDMX] Hook added: onPartExplode");
                GameEvents.onPartUndock.Add(this.onPartUndockHook);
                print("[KDMX] Hook added: onPartUndock");
                GameEvents.onSameVesselDock.Add(this.onSameVesselDockHook);
                print("[KDMX] Hook added: onSameVesselDock");
                GameEvents.onSameVesselUndock.Add(this.onSameVesselUndockHook);
                print("[KDMX] Hook added: onSameVesselUndock");
                GameEvents.onSplashDamage.Add(this.onSplashDamageHook);
                print("[KDMX] Hook added: onSplashDamage");
                GameEvents.onStageActivate.Add(this.onStageActivateHook);
                print("[KDMX] Hook added: onStageActivate");
                GameEvents.onStageSeparation.Add(this.onStageSeparationHook);
                print("[KDMX] Hook added: onStageSeperation");
                GameEvents.onTimeWarpRateChanged.Add(this.onTimeWarpRateChangedHook);
                print("[KDMX] Hook added: onTimeWarpRateChanged");
                GameEvents.onUndock.Add(this.onUndockHook);
                print("[KDMX] Hook added: onUndock");
                GameEvents.onVesselOrbitClosed.Add(this.onVesselOrbitClosedHook);
                print("[KDMX] Hook added: onVesselOrbitClosed");
                GameEvents.onVesselOrbitEscaped.Add(this.onVesselOrbitEscapedHook);
                print("[KDMX] Hook added: onVesselOrbitEscaped");
                print("[KDMX] Hooks added");

                KDMXHandler.startDMX();
                print("[KDMX] DMX Handler initialized");
            }
            else
            {
                print("[KDMX] The game is still loading, do nothing...");
            }

            windowStyle = new GUIStyle(HighLogic.Skin.window);
        }

        private void OnDraw()
        {

            windowPosition = GUI.Window(1234, windowPosition, OnWindow, "KDMX Control", windowStyle);
        }

        private void OnGUI(){            
            // Assign slider values to DMX values
            DMXRed = slider1;
            DMXGreen = slider2;
            DMXBlue = slider3;

            DrawQuad(position1, new Color(DMXRed, 0, 0));
            DrawQuad(position2, new Color(0, DMXGreen, 0));
            DrawQuad(position3, new Color(0, 0, DMXBlue));
            DrawQuad(position4, new Color(DMXRed, DMXGreen, DMXBlue));
        }

        private void OnWindow(int windowID){
            GUILayout.BeginHorizontal();
            GUILayout.Label("Colors:");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("R: " + slider1);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            slider1 = GUILayout.HorizontalSlider(slider1, 0, 1);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("G: " + slider2);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            slider2 = GUILayout.HorizontalSlider(slider2, 0, 1);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("B: " + slider3);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            slider3 = GUILayout.HorizontalSlider(slider3, 0, 1);
            GUILayout.EndHorizontal();
            
            GUI.DragWindow();
        }

        public static void LoadPrefs()
        {

            //Create the XmlDocument.
            XmlDocument doc = new XmlDocument();
            doc.Load("GameData\\Kurocon\\Plugins\\PluginData\\DMXConfig.xml");

            //Display all events.
            XmlNodeList elemList = doc.GetElementsByTagName("event");
            for (int i = 0; i < elemList.Count; i++)
            {
                string elementType = elemList[i].Attributes["type"].InnerText;
                //print(elementType);
            }
        }

        /* Debugging code to draw squares */
        void DrawQuad(Rect position, Color color)
        {
            Texture2D texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, color);
            texture.Apply();
            GUI.skin.box.normal.background = texture;
            GUI.Box(position, GUIContent.none);
        }

        /* Debug print to console code */
        public static void outputConsole(string output){
            print("[KDMX] " + output);
        }

        /* Event handlers */
        public void onCollisionHook(EventReport data)
        {
            KDMXHandler.handleHook("onCollision");
        }
        public void onCrashHook(EventReport data)
        {
            KDMXHandler.handleHook("onCrash");
        }
        public void onCrashSplashdownHook(EventReport data)
        {
            KDMXHandler.handleHook("onCrashSplashdown");
        }
        public void onCrewBoardVesselHook(GameEvents.FromToAction<Part,Part> data)
        {
            KDMXHandler.handleHook("onCrewBoardVessel");
        }
        public void onCrewKilledHook(EventReport data)
        {
            KDMXHandler.handleHook("onCrewKilled");
        }
        public void onCrewOnEvaHook(GameEvents.FromToAction<Part, Part> data)
        {
            KDMXHandler.handleHook("onCrewOnEva");
        }
        public void onDominantBodyChangeHook(GameEvents.FromToAction<CelestialBody, CelestialBody> data)
        {
            KDMXHandler.handleHook("onDominantBodyChange");
        }
        public void onGamePauseHook()
        {
            KDMXHandler.handleHook("onGamePause");
        }
        public void onGameUnpauseHook()
        {
            KDMXHandler.handleHook("onGameUnpause");
        }
        public void onJointBreakHook(EventReport data)
        {
            KDMXHandler.handleHook("onJointBreak");
        }
        public void onLaunchHook(EventReport data)
        {
            KDMXHandler.handleHook("onLaunch");
        }
        public void onOverheatHook(EventReport data)
        {
            KDMXHandler.handleHook("onOverheat");
        }
        public void onPartCoupleHook(GameEvents.FromToAction<Part,Part> data)
        {
            KDMXHandler.handleHook("onPartCouple");
        }
        public void onPartExplodeHook(GameEvents.ExplosionReaction data)
        {
            KDMXHandler.handleHook("onPartExplode");
        }
        public void onPartUndockHook(Part data)
        {
            KDMXHandler.handleHook("onPartUndock");
        }
        public void onSameVesselDockHook(GameEvents.FromToAction<ModuleDockingNode,ModuleDockingNode> data)
        {
            KDMXHandler.handleHook("onSameVesselDock");
        }
        public void onSameVesselUndockHook(GameEvents.FromToAction<ModuleDockingNode, ModuleDockingNode> data)
        {
            KDMXHandler.handleHook("onSameVesselUndock");
        }
        public void onSplashDamageHook(EventReport data)
        {
            KDMXHandler.handleHook("onSplashDamage");
        }
        public void onStageActivateHook(int data)
        {
            KDMXHandler.handleHook("onStageActivate");
        }
        public void onStageSeparationHook(EventReport data)
        {
            KDMXHandler.handleHook("onStageSeperation");
        }
        public void onTimeWarpRateChangedHook()
        {
            KDMXHandler.handleHook("onTimeWarpRateChanged");
        }
        public void onUndockHook(EventReport data)
        {
            KDMXHandler.handleHook("onUndock");
        }
        public void onVesselOrbitClosedHook(Vessel data)
        {
            KDMXHandler.handleHook("onVesselOrbitClosed");
        }
        public void onVesselOrbitEscapedHook(Vessel data)
        {
            KDMXHandler.handleHook("onVesselOrbitEscaped");
        }
    }
}