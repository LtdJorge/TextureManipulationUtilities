﻿using System;
using System.IO;
using TextureManipulationUtilities.Editor.Util;
using Unity.Collections;
using Unity.Jobs;
using UnityEditor;
using UnityEngine;

namespace TextureManipulationUtilities.Editor
{
    public class AlphaBlender : EditorWindow
    {
        private Texture2D RGB;
        private Texture2D Alpha;
        private Texture2D FinalTexture;

        private Texture2D _RGB;
        private Texture2D _alpha;

        private static EditorWindow window;
        private Vector2 scrollPos;
        private Vector2Int texSize;
        private bool useJobSystem = true;

        [MenuItem("Tools/Alpha Blender")]
        public static void ShowWindow()
        {
            window = GetWindow(typeof(AlphaBlender), false);
        }

        private void OnInspectorUpdate()
        {
            if (!window)
            {
                window = GetWindow(typeof(AlphaBlender), false);
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

            GUIStyle wrap = new GUIStyle
            {
                wordWrap = true,
                alignment = TextAnchor.MiddleCenter
            };

            //RGB
            GUILayout.Space(20f);
            GUILayout.BeginVertical(EditorStyles.helpBox);
            RGB = (Texture2D) EditorGUILayout.ObjectField("RGB", RGB, typeof(Texture2D), false);
            if (RGB && texSize == Vector2Int.zero)
            {
                texSize = new Vector2Int(RGB.width, RGB.height);
            }

            GUILayout.Space(10f);
            GUILayout.EndVertical();

            //Alpha
            GUILayout.Space(10f);
            GUILayout.BeginVertical(EditorStyles.helpBox);
            Alpha = (Texture2D) EditorGUILayout.ObjectField("Alpha", Alpha, typeof(Texture2D), false);
            if (Alpha && texSize == Vector2Int.zero)
            {
                texSize = new Vector2Int(Alpha.width, Alpha.height);
            }

            GUILayout.Space(10f);
            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            GUILayout.Space(10f);
            if (GUILayout.Button("Pack and save"))
            {
                EditorUtility.DisplayProgressBar("Packing texture", "", 0f);
                PackTextures();
            }

            GUILayout.Space(10f);
            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            GUILayout.Space(10f);
            if (GUILayout.Button("Update texture size"))
            {
                texSize = new Vector2Int(RGB.width, RGB.height);
            }

            useJobSystem = GUILayout.Toggle(useJobSystem, "Use Job system");

            GUILayout.Space(10f);
            if (window)
            {
                GUILayout.EndScrollView();
                GUILayout.EndVertical();
                GUILayout.EndArea();
            }
        }

        private void PackTextures()
        {
            FinalTexture = new Texture2D(texSize.x, texSize.y, TextureFormat.RGBAFloat, true);

            _RGB = DuplicateRGB.duplicateRGB(RGB);
            _alpha = DuplicateAlpha.duplicateAlpha(Alpha);

            if (useJobSystem)
            {
                var RGBColors = new NativeArray<Color>(_RGB.GetPixels().Length, Allocator.TempJob);
                var AlphaColors = new NativeArray<Color>(_RGB.GetPixels().Length, Allocator.TempJob);
                RGBColors.CopyFrom(_RGB.GetPixels());
                AlphaColors.CopyFrom(_alpha.GetPixels());
                var tempArray = new NativeArray<Color>(RGBColors.Length, Allocator.TempJob);

                var job = new PackPixelsJob()
                {
                    RGBColors = RGBColors,
                    AlphaColors = AlphaColors,
                    OutputTexture = tempArray
                };

                var handle = job.Schedule(tempArray.Length, 16);
                handle.Complete();
                FinalTexture.SetPixels(tempArray.ToArray());
                RGBColors.Dispose();
                AlphaColors.Dispose();
                tempArray.Dispose();

            }
            else
            {
                EditorUtility.DisplayProgressBar("Packing texture", "This should be fast!", 0.0f);
                for (var x = 0; x < texSize.x; x++)
                {
                    for (var y = 0; y < texSize.y; y++)
                    {
                        var R = _RGB.GetPixel(x, y).r;
                        var G = _RGB.GetPixel(x, y).g;
                        var B = _RGB.GetPixel(x, y).b;

                        var A = _alpha.GetPixel(x, y).r;

                        FinalTexture.SetPixel(x, y, new Color(R, G, B, A));
                        //TODO: Is it possible to implement a realistic progressbar?
                        //EditorUtility.DisplayProgressBar("Packing texture", "This should be fast!", ((y-1)*texSize.x+x)/texSize.x*texSize.y);
                    }
                    EditorUtility.DisplayProgressBar("Packing texture", "This should be fast!", x/texSize.x*texSize.y);
                    Debug.Log(Convert.ToString(x / texSize.x * texSize.y));
                }
            }

            FinalTexture.Apply();
            EditorUtility.ClearProgressBar();

            var path = EditorUtility.SaveFilePanelInProject("Save texture to directory", "BaseColor", "png", "Saved");
            var pngData = FinalTexture.EncodeToPNG();
            if (path.Length != 0)
            {
                if (pngData != null)
                {
                    File.WriteAllBytes(path, pngData);
                }
            }

            AssetDatabase.Refresh();

            Debug.Log("Texture Saved to: " + path);
        }
    }
}