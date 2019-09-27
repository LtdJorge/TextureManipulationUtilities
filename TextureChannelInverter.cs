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
        private bool flipGreen = true;
        private bool flipBlue;
        private bool flipAlpha;
        private bool saveToDifferentTexture;
        private bool textureHasAlpha;

        private enum Channels
        {
            Red,
            Green,
            Blue,
            Alpha
        }

        private int channelToFlip = 1;

        [MenuItem("Tools/TextureChannelInverter")]
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

            /*
            flipRed = GUILayout.Toggle(flipRed, "Flip red channel");

            flipGreen = GUILayout.Toggle(flipGreen, "Flip green channel (use this for normal maps)");

            flipBlue = GUILayout.Toggle(flipBlue, "Flip blue channel");

            flipAlpha = GUILayout.Toggle(flipAlpha, "Flip alpha channel");

            */
            string[] texts = { "Flip red channel", "Flip green channel (use this for normal maps)", "Flip blue channel", "Flip alpha channel" };

            channelToFlip = GUILayout.SelectionGrid(channelToFlip, texts, 1);

            GUILayout.Space(20f);

            if (GUILayout.Button("Flip selected channel"))
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

            int x = textureMap.width;
            int y = textureMap.height;
            for(int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if(textureMap.format == TextureFormat.DXT5)
                    tempTexture.SetPixel(i, j, new Color(textureMap.GetPixel(i, j).r, textureMap.GetPixel(i, j).g, textureMap.GetPixel(i, j).b));
                }
            }
            if(_channel == Channels.Green)
            {
                for (int i = 0; i < x; i++)
                {
                    for (int j = 0; j < y; j++)
                    {
                        tempTexture.SetPixel(i, j, new Color(textureMap.GetPixel(i, j).r, textureMap.GetPixel(i, j).g, textureMap.GetPixel(i, j).b));
                    }
                }
            }
        }
    }
}
