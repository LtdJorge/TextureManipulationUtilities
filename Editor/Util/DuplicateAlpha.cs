using UnityEngine;

namespace TextureManipulationUtilities.Util
{
    public class DuplicateAlpha
    {
        public static Texture2D duplicateAlpha(Texture2D source)
        {
            var readableTex = new Texture2D(source.width, source.height);
            readableTex.SetPixels(source.GetPixels());
            return readableTex;
        }
    }
}

