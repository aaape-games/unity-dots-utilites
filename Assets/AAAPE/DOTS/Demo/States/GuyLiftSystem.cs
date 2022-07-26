using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

namespace AAAPE.DOTS.Demo
{
    // this is only ran when the NoGravityFlag exists somewhere
    [WithGameFlag(typeof(NoGravityFlag))]
    public partial class GuyLiftSystem : EcsSystem
    {
        protected override void OnUpdate()
        {
            float DeltaTime = Time.DeltaTime;
            Entities
                .WithAll<Guy>()
                .ForEach((ref Translation translation) => { translation.Value += new float3(0, DeltaTime, 0); })
                .ScheduleParallel();
        }
    }
}