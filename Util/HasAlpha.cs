﻿using UnityEngine;

namespace Editor.TextureManipulationUtilities.Util
{
    public class HasAlpha
    {
        public static bool hasAlpha(Texture2D texture)
        {
            if(
                texture.format == TextureFormat.DXT5 
                || texture.format == TextureFormat.BC7
                || texture.format == TextureFormat.DXT5Crunched
                )
            {
                return true;
            }
            else if (texture.format == TextureFormat.DXT1)
            {
                return false;
            }
            else
            {
                Debug.Log("Texture format not recognised for texture " + texture.name);
            }
            return false;
        }
    }
}