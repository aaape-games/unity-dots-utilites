using Unity.Entities;
using Unity.Mathematics;

namespace AAAPE.DOTS.Demo
{
    [GenerateAuthoringComponent]
    public struct MoveTo : IComponentData
    {
        public float3 Position;

        public float Speed;

        public bool Reached(float3 dest)
        {
            return math.all(this.Position == dest);
        }

        public float3 Destination(float3 current)
        {
            return this.Position - current;
        }

        public float3 Destination2D(float3 current)
        {
            return this.Position - new float3
            {
                x = current.x,
                y = this.Position.y,
                z = current.z
            };
        }
    }
}