using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
using Unity.Entities.CodeGeneratedJobForEach;
using Writer = Unity.Entities.EntityCommandBuffer.ParallelWriter;
using Unity.Collections;


namespace AAAPE.DOTS.Demo
{
    public class GuyMoveToSystem : EcsSystem
    {

        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;

            //without burst (be sure to really not use burst)
            //EndSimulationScheduler scheduler = new EndSimulationScheduler(Dependency);
            //ParallelStateChange<GuyState, GuyStates>.WithoutBurst writer = new ParallelStateChange<GuyState, GuyStates>.WithoutBurst(scheduler.ScheduleParallel());

            NativeQueue<StateChangeAction<GuyState, GuyStates>> queue = new NativeQueue<StateChangeAction<GuyState, GuyStates>>(Allocator.TempJob);
            NativeQueue<StateChangeAction<GuyState, GuyStates>>.ParallelWriter writer = queue.AsParallelWriter();

            Dependency = Entities
                .WithAll<Guy>()
                // here you can set to only run action on components with state x
                .WithSharedComponentFilter(new GuyState { Value = GuyStates.WALKING })

                .ForEach((Entity e, ref Translation translation, ref Rotation rotation, in MoveTo moveTo) =>
                {
                    float3 toDestination = moveTo.Destination2D(translation.Value);
                    if (moveTo.Reached(translation.Value))
                    {
                        writer.Enqueue(GuyState.Action(GuyStates.IDLE, e));
                        return;
                    }


                    float3 delta = math.normalize(toDestination) * deltaTime * moveTo.Speed;
                    translation.Value = math.length(delta) < math.length(toDestination) ? translation.Value + delta : moveTo.Position;

                    //rotation.Value = quaternion.LookRotation(toDestination, new float3(0, 1, 0));
                })
                .ScheduleParallel(Dependency)
                .WithStateChange(queue, EntityManager);
        }
    }
}
