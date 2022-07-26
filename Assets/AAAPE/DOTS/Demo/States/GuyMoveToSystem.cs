using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
using Unity.Entities.CodeGeneratedJobForEach;
using Writer = Unity.Entities.EntityCommandBuffer.ParallelWriter;
using Unity.Collections;


namespace AAAPE.DOTS.Demo
{
    public partial class GuyMoveToSystem : EcsSystem
    {
        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;


            EndSimulationScheduler scheduler = new EndSimulationScheduler(Dependency);
            EntityCommandBuffer.ParallelWriter writer = scheduler.ScheduleParallel();

            Dependency = Entities
                .WithAll<Guy>()
                // here you can set to only run action on components with state x
                .WithSharedComponentFilter(new GuyState { Value = GuyStates.WALKING })
                .ForEach((int entityInQueryIndex, Entity entity, ref Translation translation, ref Rotation rotation,
                    in MoveTo moveTo) =>
                {
                    float3 toDestination = moveTo.Destination2D(translation.Value);
                    if (moveTo.Reached(translation.Value))
                    {
                        writer.AddSharedComponent<GuyState>(entityInQueryIndex, entity, new GuyState(GuyStates.IDLE));
                        return;
                    }


                    float3 delta = math.normalize(toDestination) * deltaTime * moveTo.Speed;
                    translation.Value = math.length(delta) < math.length(toDestination)
                        ? translation.Value + delta
                        : moveTo.Position;
                    rotation.Value = quaternion.LookRotation(toDestination, new float3(0, 1, 0));
                })
                .ScheduleParallel(Dependency)
                .WithScheduler(scheduler);
        }
    }
}