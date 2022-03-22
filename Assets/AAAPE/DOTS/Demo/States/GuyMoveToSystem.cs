using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

namespace AAAPE.DOTS.StateDemo {

    public class GuyMoveToSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;
            Entities
                .WithAll<Guy>()
                // here you can set to only run action on components with state x
                .WithSharedComponentFilter(new GuyState{Value = GuyStates.WALKING})
                
                .ForEach((ref Translation translation, ref Rotation rotation, in MoveTo moveTo) => {
                        float3 toDestination = moveTo.Position - translation.Value;
                        if(math.all(moveTo.Position == translation.Value)) {
                            return;
                        }

                        float3 delta = math.normalize(toDestination) * deltaTime * moveTo.Speed;
                        translation.Value = math.length(delta) < math.length(toDestination) ? translation.Value + delta : moveTo.Position;

                        rotation.Value = quaternion.LookRotation(toDestination, new float3(0, 1, 0));
                }).ScheduleParallel();
        }
    }
}
