using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

namespace AAAPE.DOTS.StateDemo
{

    public class GuyLiftSystem : SystemBase
    {
        private Camera cam;
        protected override void OnCreate()
        {
            // this is a gamestate
            GameState.RequireForUpdate<NoGravityFlag>(this);
            // you could also use 
            // RequireSingletonForUpdate<LevelStartedFlag>();
            // which is doing the same
        }

        // this is only ran when the levelStartedFlag exists somewhere
        protected override void OnUpdate()
        {
            float DeltaTime = Time.DeltaTime;

            Entities
                .WithAll<Guy>()
                .ForEach((ref Translation translation) =>
                {
                    translation.Value += new float3(0, DeltaTime , 0);
                }).ScheduleParallel();
        }
    }
}
