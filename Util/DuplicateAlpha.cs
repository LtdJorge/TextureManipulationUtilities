using UnityEngine;

namespace Editor.TextureManipulationUtilities.Util
{
    public class DuplicateAlpha
    {
        public static Texture2D duplicateAlpha(Texture2D source)
        {
            //RenderTexture renderTex = RenderTexture.GetTemporary(source.width, source.height, 0, RenderTextureFormat.RFloat, RenderTextureReadWrite.Default);
            //Graphics.Blit(source, renderTex);

            //RenderTexture previous = RenderTexture.active;
            //RenderTexture.active = renderTex;

            Texture2D readableTex = new Texture2D(source.width, source.height);
            readableTex.SetPixels(source.GetPixels());
            //readableTex.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
            //readableTex.Apply();

            //RenderTexture.ReleaseTemporary(renderTex);
            return readableTex;
        }
    }
}

