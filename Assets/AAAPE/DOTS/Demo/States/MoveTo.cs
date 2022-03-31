using Unity.Entities;
using Unity.Mathematics;

namespace AAAPE.DOTS.Demo
{

    [GenerateAuthoringComponent]
    public struct MoveTo : IComponentData
    {
        public float3 Position;

        public float Speed;

        public bool Reached(float3 dest) {
           return math.all(this.Position == dest);
        }   
    }
}
