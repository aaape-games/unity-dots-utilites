using Unity.Entities;
using Unity.Mathematics;

namespace AAAPE.DOTS.StateDemo
{

    [GenerateAuthoringComponent]
    public struct MoveTo : IComponentData
    {
        public float3 Position;

        public float Speed;
    }
}
