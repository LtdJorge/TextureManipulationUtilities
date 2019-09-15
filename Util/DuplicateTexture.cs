using UnityEngine;

namespace Editor.TextureManipulationUtilities.Util
{
    public class DuplicateTexture
    {
        public static Texture2D duplicateTexture(Texture2D source)
        {
            var renderTex = RenderTexture.GetTemporary(source.width, source.height, 0,
                RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.sRGB);

            Graphics.Blit(source, renderTex);
            var previous = RenderTexture.active;
            RenderTexture.active = renderTex;
            var readableText = new Texture2D(source.width, source.height);
            readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
            readableText.Apply();
            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(renderTex);
            return readableText;
        }
    }

}
