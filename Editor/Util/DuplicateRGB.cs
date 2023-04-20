using UnityEngine;

namespace TextureManipulationUtilities.Editor.Util
{
    public class DuplicateRGB
    {
        public static Texture2D duplicateRGB(Texture2D source)
        {
            var renderTex = RenderTexture.GetTemporary(source.width, source.height, 0, RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.sRGB);
            Graphics.Blit(source, renderTex);

            RenderTexture.active = renderTex;

            var readableTex = new Texture2D(source.width, source.height);
            readableTex.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
            readableTex.Apply();

            RenderTexture.ReleaseTemporary(renderTex);
            return readableTex;
        }
    }
}

