using UnityEngine;

namespace TextureManipulationUtilities.Util
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

            if (texture.format == TextureFormat.DXT1)
            {
                return false;
            }

            Debug.Log("Texture format not recognized for texture " + texture.name);
            return false;
        }
    }
}