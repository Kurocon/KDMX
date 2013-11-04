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
                GameEvents.onCrash.Add(this.onCrashHook);
                GameEvents.onCrashSplashdown.Add(this.onCrashSplashdownHook);
                GameEvents.onCrewBoardVessel.Add(this.onCrewBoardVesselHook);
                GameEvents.onCrewKilled.Add(this.onCrewKilledHook);
                GameEvents.onCrewOnEva.Add(this.onCrewOnEvaHook);
                GameEvents.onDominantBodyChange.Add(this.onDominantBodyChangeHook);
                GameEvents.onFlightReady.Add(this.onFlightReadyHook);
                GameEvents.onGamePause.Add(this.onGamePauseHook);
                GameEvents.onGameSceneLoadRequested.Add(this.onGameSceneLoadRequestedHook);
                GameEvents.onGameStateCreated.Add(this.onGameStateCreatedHook);
                GameEvents.onGameStateSaved.Add(this.onGameStateSavedHook);
                GameEvents.onGameUnpause.Add(this.onGameUnpauseHook);
                GameEvents.onGUIAstronautComplexDespawn.Add(this.onGUIAstronautComplexDespawnHook);
                GameEvents.onGUIAstronautComplexSpawn.Add(this.onGUIAstronautComplexSpawnHook);
                GameEvents.onGUILaunchScreenDespawn.Add(this.onGUILaunchScreenDespawnHook);
                GameEvents.onGUILaunchScreenSpawn.Add(this.onGUILaunchScreenSpawnHook);
                GameEvents.onGUIRecoveryDialogDespawn.Add(this.onGUIRecoveryDialogDespawnHook);
                GameEvents.onGUIRecoveryDialogSpawn.Add(this.onGUIRecoveryDialogSpawnHook);
                GameEvents.onGUIRnDComplexDespawn.Add(this.onGUIRnDComplexDespawnHook);
                GameEvents.onGUIRnDComplexSpawn.Add(this.onGUIRnDComplexSpawnHook);
                GameEvents.onHideUI.Add(this.onHideUIHook);
                GameEvents.onJointBreak.Add(this.onJointBreakHook);
                GameEvents.onLaunch.Add(this.onLaunchHook);
                GameEvents.onOverheat.Add(this.onOverheatHook);
                GameEvents.onPartAttach.Add(this.onPartAttachHook);
                GameEvents.onPartCouple.Add(this.onPartCoupleHook);
                GameEvents.onPartDie.Add(this.onPartDieHook);
                GameEvents.onPartExplode.Add(this.onPartExplodeHook);
                GameEvents.OnPartPurchased.Add(this.onPartPurchasedHook);
                GameEvents.onPartRemove.Add(this.onPartRemoveHook);
                GameEvents.onPartUndock.Add(this.onPartUndockHook);
                GameEvents.onPlanetariumTargetChanged.Add(this.onPlanetariumTargetChangedHook);
                GameEvents.OnProgressComplete.Add(this.onProgressCompleteHook);
                GameEvents.OnProgressReached.Add(this.onProgressReachedHook);
                GameEvents.onSameVesselDock.Add(this.onSameVesselDockHook);
                GameEvents.onSameVesselUndock.Add(this.onSameVesselUndockHook);
                GameEvents.onShowUI.Add(this.onShowUIHook);
                GameEvents.onSplashDamage.Add(this.onSplashDamageHook);
                GameEvents.onStageActivate.Add(this.onStageActivateHook);
                GameEvents.onStageSeparation.Add(this.onStageSeparationHook);
                GameEvents.OnTechnologyResearched.Add(this.onTechnologyResearchedHook);
                GameEvents.onTimeWarpRateChanged.Add(this.onTimeWarpRateChangedHook);
                GameEvents.onUndock.Add(this.onUndockHook);
                GameEvents.onVesselChange.Add(this.onVesselChangeHook);
                GameEvents.onVesselCreate.Add(this.onVesselCreateHook);
                GameEvents.onVesselDestroy.Add(this.onVesselDestroyHook);
                GameEvents.onVesselGoOffRails.Add(this.onVesselGoOffRailsHook);
                GameEvents.onVesselGoOnRails.Add(this.onVesselGoOnRailsHook);
                GameEvents.onVesselLoaded.Add(this.onVesselLoadedHook);
                GameEvents.onVesselOrbitClosed.Add(this.onVesselOrbitClosedHook);
                GameEvents.onVesselOrbitEscaped.Add(this.onVesselOrbitEscapedHook);
                GameEvents.onVesselRecovered.Add(this.onVesselRecoveredHook);
                GameEvents.OnVesselRecoveryRequested.Add(this.OnVesselRecoveryRequestedHook);
                GameEvents.onVesselRename.Add(this.onVesselRenameHook);
                GameEvents.onVesselSituationChange.Add(this.onVesselSituationChangeHook);
                GameEvents.onVesselSOIChanged.Add(this.onVesselSOIChangedHook);
                GameEvents.onVesselTerminated.Add(this.onVesselTerminatedHook);
                GameEvents.onVesselWasModified.Add(this.onVesselWasModifiedHook);
                print("[KDMX] Hooks added");
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
            print(output);
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
        public void onFlightReadyHook()
        {
            KDMXHandler.handleHook("onFlightReady");
        }
        public void onGamePauseHook()
        {
            KDMXHandler.handleHook("onGamePause");
        }
        public void onGameSceneLoadRequestedHook(GameScenes data)
        {
            KDMXHandler.handleHook("onGameSceneLoadRequested");
        }
        public void onGameStateCreatedHook(Game data)
        {
            KDMXHandler.handleHook("onGameStateCreated");
        }
        public void onGameStateSavedHook(Game data)
        {
            KDMXHandler.handleHook("onGameStateSaved");
        }
        public void onGameUnpauseHook()
        {
            KDMXHandler.handleHook("onGameUnpause");
        }
        public void onGUIAstronautComplexDespawnHook()
        {
            KDMXHandler.handleHook("onGUIAstronautComplexDespawn");
        }
        public void onGUIAstronautComplexSpawnHook()
        {
            KDMXHandler.handleHook("onGUIAstronautComplexSpawn");
        }
        public void onGUILaunchScreenDespawnHook()
        {
            KDMXHandler.handleHook("onGUILaunchScreenDespawn");
        }
        public void onGUILaunchScreenSpawnHook(GameEvents.VesselSpawnInfo data)
        {
            KDMXHandler.handleHook("onGUILaunchScreenSpawn");
        }
        public void onGUIRecoveryDialogDespawnHook(ExperimentsRecoveryDialog data)
        {
            KDMXHandler.handleHook("onGUIRecoveryDialogDespawn");
        }
        public void onGUIRecoveryDialogSpawnHook(ExperimentsRecoveryDialog data)
        {
            KDMXHandler.handleHook("onGUIRecoveryDialogSpawn");
        }
        public void onGUIRnDComplexDespawnHook()
        {
            KDMXHandler.handleHook("onGUIRnDComplexDespawn");
        }
        public void onGUIRnDComplexSpawnHook()
        {
            KDMXHandler.handleHook("onGUIRnDComplexSpawn");
        }
        public void onHideUIHook()
        {
            KDMXHandler.handleHook("onHideUI");
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
        public void onPartAttachHook(GameEvents.HostTargetAction<Part,Part> data)
        {
            KDMXHandler.handleHook("onPartAttach");
        }
        public void onPartCoupleHook(GameEvents.FromToAction<Part,Part> data)
        {
            KDMXHandler.handleHook("onPartCouple");
        }
        public void onPartDieHook(Part data)
        {
            KDMXHandler.handleHook("onPartDie");
        }
        public void onPartExplodeHook(GameEvents.ExplosionReaction data)
        {
            KDMXHandler.handleHook("onPartExplode");
        }
        public void onPartPurchasedHook(AvailablePart data)
        {
            KDMXHandler.handleHook("onPartPurchased");
        }
        public void onPartRemoveHook(GameEvents.HostTargetAction<Part, Part> data)
        {
            KDMXHandler.handleHook("onPartRemove");
        }
        public void onPartUndockHook(Part data)
        {
            KDMXHandler.handleHook("onPartUndock");
        }
        public void onPlanetariumTargetChangedHook(MapObject data)
        {
            KDMXHandler.handleHook("onPlanetariumTargetChanged");
        }
        public void onProgressCompleteHook(ProgressNode data)
        {
            KDMXHandler.handleHook("onProgressComplete");
        }
        public void onProgressReachedHook(ProgressNode data)
        {
            KDMXHandler.handleHook("onProgressReached");
        }
        public void onSameVesselDockHook(GameEvents.FromToAction<ModuleDockingNode,ModuleDockingNode> data)
        {
            KDMXHandler.handleHook("onSameVesselDock");
        }
        public void onSameVesselUndockHook(GameEvents.FromToAction<ModuleDockingNode, ModuleDockingNode> data)
        {
            KDMXHandler.handleHook("onSameVesselUndock");
        }
        public void onShowUIHook()
        {
            KDMXHandler.handleHook("onShowUI");
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
        public void onTechnologyResearchedHook(GameEvents.HostTargetAction<RDTech,RDTech.OperationResult> data)
        {
            KDMXHandler.handleHook("onTechnologyResearched");
        }
        public void onTimeWarpRateChangedHook()
        {
            KDMXHandler.handleHook("onTimeWarpRateChanged");
        }
        public void onUndockHook(EventReport data)
        {
            KDMXHandler.handleHook("onUndock");
        }
        public void onVesselChangeHook(Vessel data)
        {
            KDMXHandler.handleHook("onVesselChange");
        }
        public void onVesselCreateHook(Vessel data)
        {
            KDMXHandler.handleHook("onVesselCreate");
        }
        public void onVesselDestroyHook(Vessel data)
        {
            KDMXHandler.handleHook("onVesselDestroy");
        }
        public void onVesselGoOffRailsHook(Vessel data)
        {
            KDMXHandler.handleHook("onVesselGoOffRails");
        }
        public void onVesselGoOnRailsHook(Vessel data)
        {
            KDMXHandler.handleHook("onVesselGoOnRails");
        }
        public void onVesselLoadedHook(Vessel data)
        {
            KDMXHandler.handleHook("onVesselLoaded");
        }
        public void onVesselOrbitClosedHook(Vessel data)
        {
            KDMXHandler.handleHook("onVesselOrbitClosed");
        }
        public void onVesselOrbitEscapedHook(Vessel data)
        {
            KDMXHandler.handleHook("onVesselOrbitEscaped");
        }
        public void onVesselRecoveredHook(ProtoVessel data)
        {
            KDMXHandler.handleHook("onVesselRecovered");
        }
        public void OnVesselRecoveryRequestedHook(Vessel data)
        {
            KDMXHandler.handleHook("OnVesselRecoveryRequested");
        }
        public void onVesselRenameHook(GameEvents.FromToAction<string,string> data)
        {
            KDMXHandler.handleHook("onVesselRename");
        }
        public void onVesselSituationChangeHook(GameEvents.HostedFromToAction<Vessel,Vessel.Situations> data)
        {
            KDMXHandler.handleHook("onVesselSituationChange");
        }
        public void onVesselSOIChangedHook(GameEvents.HostedFromToAction<Vessel,CelestialBody> data)
        {
            KDMXHandler.handleHook("onVesselSOIChanged");
        }
        public void onVesselTerminatedHook(ProtoVessel data)
        {
            KDMXHandler.handleHook("onVesselTerminated");
        }
        public void onVesselWasModifiedHook(Vessel data)
        {
            KDMXHandler.handleHook("onVesselWasModified");
        }
    }
}