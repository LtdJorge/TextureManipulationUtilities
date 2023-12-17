using System.IO;
using TextureManipulationUtilities.Util;
using UnityEditor;
using UnityEngine;

namespace TextureManipulationUtilities
{
    public class TextureChannelInverter : EditorWindow
    {
        private Texture2D textureMap;

        private Vector2 scrollPos;
        private static EditorWindow window;
        private bool flipRed;
#pragma warning disable CS0414 // Field is assigned but its value is never used
        private bool _flipGreen = true;
#pragma warning restore CS0414 // Field is assigned but its value is never used
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
            textureMap = (Texture2D) EditorGUILayout.ObjectField("Texture", textureMap, typeof(Texture2D), false);

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
                FlipChannel((Channels)channelToFlip);
                /*
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
                }*/
            }

            saveToDifferentTexture =
                GUILayout.Toggle(saveToDifferentTexture, "Check to save to a new texture. Leave unchecked to overwrite.");

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
            //TODO: Funciona pero solo para texturas con alpha, hay que hacerlo para las otras
            var tempTexture = new Texture2D(textureMap.width, textureMap.height, TextureFormat.RGBAFloat, true);
            var dupTexture = DuplicateTexture.duplicateTexture(textureMap);
            var x = textureMap.width;
            var y = textureMap.height;

            var hasAlpha = HasAlpha.hasAlpha(textureMap);
            
            //Invert green of texture with alpha
            if (hasAlpha && _channel == Channels.Green)
            {
                for(var i = 0; i < x; i++)
                {
                    for (var j = 0; j < y; j++)
                    {
                        tempTexture.SetPixel(i, j, new Color(dupTexture.GetPixel(i, j).r, 1-dupTexture.GetPixel(i, j).g, dupTexture.GetPixel(i, j).b, dupTexture.GetPixel(i,j).a));

                    }
                }
            }
            //Invert alpha
            else if (hasAlpha && _channel == Channels.Alpha)
            {
                for(var i = 0; i < x; i++)
                {
                    for (var j = 0; j < y; j++)
                    {
                        tempTexture.SetPixel(i, j, new Color(dupTexture.GetPixel(i, j).r, dupTexture.GetPixel(i, j).g, dupTexture.GetPixel(i, j).b, 1-dupTexture.GetPixel(i,j).a));

                    }
                }
            }
            //Invert red of image with alpha
            else if (hasAlpha && _channel == Channels.Red)
            {
                for(var i = 0; i < x; i++)
                {
                    for (var j = 0; j < y; j++)
                    {
                        tempTexture.SetPixel(i, j, new Color(1-dupTexture.GetPixel(i, j).r, dupTexture.GetPixel(i, j).g, dupTexture.GetPixel(i, j).b, dupTexture.GetPixel(i,j).a));

                    }
                }
            }
            //Invert blue of image with alpha
            else if (hasAlpha && _channel == Channels.Blue)
            {
                for(var i = 0; i < x; i++)
                {
                    for (var j = 0; j < y; j++)
                    {
                        tempTexture.SetPixel(i, j, new Color(dupTexture.GetPixel(i, j).r, dupTexture.GetPixel(i, j).g, 1-dupTexture.GetPixel(i, j).b, dupTexture.GetPixel(i,j).a));
                    }
                }
            }

            tempTexture.Apply();
            string path;
            if (saveToDifferentTexture)
            {
                path = EditorUtility.SaveFilePanelInProject("Save texture to directory", textureMap.name+"Inverted", "png", "Saved");    
            }
            else
            {
                path = EditorUtility.SaveFilePanelInProject("Save texture to directory", textureMap.name, "png", "Saved");    
            }

            var textureData = tempTexture.EncodeToPNG();
            if (path.Length == 0) return;
            if (textureData != null)
            {
                File.WriteAllBytes(path, textureData);
            }

            AssetDatabase.Refresh();

            Debug.Log("Saved");

            /*
            if(_channel == Channels.Green)
            {
                for (var i = 0; i < x; i++)
                {
                    for (var j = 0; j < y; j++)
                    {
                        tempTexture.SetPixel(i, j, new Color(textureMap.GetPixel(i, j).r, 1-textureMap.GetPixel(i, j).g, textureMap.GetPixel(i, j).b));
                    }
                }
            }
            */
        }
    }
}
