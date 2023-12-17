using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace TextureManipulationUtilities.Util
{
#if HAS_BURST
    [BurstCompile]
#endif
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
