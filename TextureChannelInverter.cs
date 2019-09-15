using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace Editor.TextureManipulationUtilities
{
    public class TextureChannelInverter : EditorWindow
    {
        private Texture2D textureMap;

        private Vector2 scrollPos;
        private static EditorWindow window;
        private bool flipRed;
        private bool flipGreen;
        private bool flipBlue;
        private bool flipAlpha;
        private bool saveToDifferentTexture;

        private enum Channels
        {
            Red,
            Green,
            Blue,
            Alpha
        }

        [MenuItem("Tools/NormalMapInverter")]
        public static void ShowWindow()
        {
            window = GetWindow(typeof(TextureChannelInverter), false);
        }

        private void OnInspectorUpdate()
        {
            if (!window)
            {
                window = GetWindow(typeof(TextureChannelInverter), false);
            }
        }

        private void OnGUI()
        {
            if (window)
            {
                GUILayout.BeginArea(new Rect(0, 0, window.position.size.x, window.position.size.y));
                GUILayout.BeginVertical();
                scrollPos = GUILayout.BeginScrollView(scrollPos, false, true, GUILayout.ExpandHeight(true));
            }

            var wrap = new GUIStyle()
            {
                wordWrap = true,
                alignment = TextAnchor.MiddleCenter
            };

            GUILayout.Space(20f);
            GUILayout.BeginVertical(EditorStyles.helpBox);
            textureMap = (Texture2D) EditorGUILayout.ObjectField("Normal Map", textureMap, typeof(Texture2D), false);

            flipRed = GUILayout.Toggle(false, "Flip red channel");

            flipGreen = GUILayout.Toggle(true, "Flip green channel (use this for normal maps)");

            flipBlue = GUILayout.Toggle(false, "Flip blue channel");

            flipAlpha = GUILayout.Toggle(false, "Flip alpha channel");

            if (GUILayout.Button("Flip normal height"))
            {
                if (flipRed)
                {
                    FlipChannel(Channels.Red);
                }

                if (flipGreen)
                {
                    FlipChannel(Channels.Green);
                }

                if (flipBlue)
                {
                    FlipChannel(Channels.Blue);
                }

                if (flipAlpha)
                {
                    FlipChannel(Channels.Alpha);
                }
            }

            saveToDifferentTexture =
                GUILayout.Toggle(false, "Check to save to a new texture. Leave unchecked to overwrite.");

            GUILayout.Space(10f);
            GUILayout.EndVertical();
            GUILayout.Space(10f);
            if (!window) return;
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            GUILayout.EndArea();

        }

        private void FlipChannel(Channels _channel)
        {
            var tempTexture = new Texture2D(textureMap.width, textureMap.height, textureMap.graphicsFormat, TextureCreationFlags.None);


        }
    }
}
