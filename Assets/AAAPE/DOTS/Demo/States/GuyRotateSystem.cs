using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

namespace AAAPE.DOTS.Demo
{
    [WithGameFlag(typeof(LevelStartedFlag))]
    public class GuyRotateSystem : EcsSystem
    {
        // this is only ran when the levelStartedFlag exists somewhere
        protected override void OnUpdate()
        {
            float DeltaTime = Time.DeltaTime;

            Entities
                .WithAll<Guy>()
                .ForEach((ref Rotation rotation, in Translation translation) =>
                {
                    rotation.Value = math.mul(rotation.Value, quaternion.RotateY(DeltaTime));
                }).ScheduleParallel();
        }
    }
}
