using UnityEngine;

namespace KDMX
{
    [KSPAddon(KSPAddon.Startup.SpaceCentre, false)]
    public class KDMXSpacecentre : MonoBehaviour
    {

        private static Rect windowPosition = new Rect(0, 0, 320, 240);
        private static GUIStyle windowStyle = null;
        private static bool buttonState = false;

        public void Awake()
        {
            RenderingManager.AddToPostDrawQueue(0, OnDraw);
        }
        public void Start()
        {
            print("[KDMX] Entering Space Centre!");
            windowStyle = new GUIStyle(HighLogic.Skin.window);
        }

        private void OnDraw()
        {
            windowPosition = GUI.Window(1234, windowPosition, OnWindow, "KDMX Control", windowStyle);
        }

        private void OnWindow(int windowID)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("ABC-");
            GUILayout.Label("123");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            buttonState = GUILayout.Toggle(buttonState, "Button State: " + buttonState);
            GUILayout.EndHorizontal();

            GUI.DragWindow();
        }
    }
}
