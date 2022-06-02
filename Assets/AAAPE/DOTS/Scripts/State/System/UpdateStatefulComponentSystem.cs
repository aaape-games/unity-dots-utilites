using Unity.Entities;
using UnityEngine;
using Unity.Collections;

namespace AAAPE.DOTS
{
    public partial class UpdateStatefulComponentSystem<TEntity, TState, TEnum> : SystemBase
        where TEntity : struct, StatefulComponent<TState, TEnum>
        where TState : struct, State<TEnum>
        where TEnum : System.Enum
    {
        protected void UpdateState(Entity e)
        {
            EntityManager.SetSharedComponentData(e, EntityManager.GetComponentData<TEntity>(e).State);
        }

        // this is done on the main thread unluckily
        // that means, using this a LOT will influence the performance
        protected override void OnUpdate()
        {
            EndSimulationScheduler schedule = new EndSimulationScheduler(Dependency);
            EntityCommandBuffer.ParallelWriter parallel = schedule.ScheduleParallel();

            NativeArray<Entity> entities = GetEntityQuery(new EntityQueryDesc
            {
                All = new ComponentType[] { ComponentType.ReadOnly<TEntity>(),  typeof(TState) }
            })
            .WithChangedVersionFilter<TEntity>()
            .ToEntityArray(Allocator.Temp);

            foreach (Entity e in entities)
            {   
                UpdateState(e);
            }
        }
    }
}