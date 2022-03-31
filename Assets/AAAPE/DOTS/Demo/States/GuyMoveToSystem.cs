using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
using Unity.Entities.CodeGeneratedJobForEach;

namespace AAAPE.DOTS.Demo
{
    public class GuyMoveToSystem : EcsSystem
    {

        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;
            Entities
                // here you can set to only run action on components with state x
                .WithSharedComponentFilter(new GuyState { Value = GuyStates.WALKING })

                .ForEach((ref Guy guy, ref Translation translation, ref Rotation rotation, in MoveTo moveTo) =>
                {
                    float3 toDestination = moveTo.Position - translation.Value;
                    if (moveTo.Reached(translation.Value))
                    {
                        guy.state = new GuyState{Value=GuyStates.IDLE};
                        return;
                    }

                    float3 delta = math.normalize(toDestination) * deltaTime * moveTo.Speed;
                    translation.Value = math.length(delta) < math.length(toDestination) ? translation.Value + delta : moveTo.Position;

                    //rotation.Value = quaternion.LookRotation(toDestination, new float3(0, 1, 0));
                }).ScheduleParallel();
        }
    }
}
