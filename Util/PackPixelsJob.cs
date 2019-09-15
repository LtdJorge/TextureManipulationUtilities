using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Editor.TextureManipulationUtilities.Util
{
    [BurstCompile]
    public struct PackPixelsJob : IJobParallelFor
    {
        [ReadOnly] public NativeArray<Color> RGBColors;
        [ReadOnly] public NativeArray<Color> AlphaColors;
        [WriteOnly] public NativeArray<Color> OutputTexture;

        public void Execute(int index)
        {
            var tempColor = new Color(
                RGBColors[index].r,
                RGBColors[index].g,
                RGBColors[index].b,
                AlphaColors[index].r
                );
            OutputTexture[index] = tempColor;
        }
    }
}
