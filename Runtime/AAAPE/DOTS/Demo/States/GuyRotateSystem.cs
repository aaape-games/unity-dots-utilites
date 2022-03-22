using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

namespace AAAPE.DOTS.StateDemo
{

    public class GuyRotateSystem : SystemBase
    {
        private Camera cam;
        protected override void OnCreate()
        {
            // this is a gamestate
            GameState.RequireForUpdate<LevelStartedFlag>(this);
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
                .ForEach((ref Rotation rotation, in Translation translation) =>
                {
                    rotation.Value = math.mul(rotation.Value, quaternion.RotateY(DeltaTime));
                }).ScheduleParallel();
        }
    }
}
