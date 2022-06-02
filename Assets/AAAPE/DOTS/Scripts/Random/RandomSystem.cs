using Unity.Entities;
using Unity.Collections;
using Unity.Jobs.LowLevel.Unsafe;

namespace AAAPE.DOTS
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial class RandomSystem : SystemBase
    {
        public NativeArray<Unity.Mathematics.Random> Randoms { get; private set; }
        protected override void OnCreate()
        {
            Unity.Mathematics.Random[] randomArray = new Unity.Mathematics.Random[JobsUtility.MaxJobThreadCount];
            System.Random randomSeed = new System.Random();

            for (int i = 0; i < JobsUtility.MaxJobThreadCount; i++)
            {
                randomArray[i] = new Unity.Mathematics.Random((uint)randomSeed.Next());
            }

            this.Randoms = new NativeArray<Unity.Mathematics.Random>(randomArray, Allocator.Persistent);  
        }

        protected override void OnUpdate()
        {
        }

        protected override void OnDestroy()
        {
            this.Randoms.Dispose();
        }
    }
}
