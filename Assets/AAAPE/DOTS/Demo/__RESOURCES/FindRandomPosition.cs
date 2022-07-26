using Unity.Entities;
using Unity.Mathematics;

namespace AAAPE.DOTS.Demo
{
    [GenerateAuthoringComponent]
    public struct FindRandomPosition : IComponentData
    {
        public float3 Min;

        public float3 Max;
    }
}