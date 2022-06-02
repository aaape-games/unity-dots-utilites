using Unity.Entities;
using System;
using Unity.Collections;

namespace AAAPE.DOTS {
    public partial class InitStatefulComponentSystem<TEntity, TState, TEnum> : SystemBase 
        where TEntity: struct, StatefulComponent<TState,TEnum>
        where TState: struct, State<TEnum> 
        where TEnum : System.Enum 
    {
        protected void InitState(Entity e) {
            EntityManager.AddSharedComponentData(e, EntityManager.GetComponentData<TEntity>(e).State);
        }

        //this is done on the main thread unluckily
        protected override void OnUpdate()
        {
            EndSimulationScheduler schedule = new EndSimulationScheduler(Dependency);
            EntityCommandBuffer.ParallelWriter parallel = schedule.ScheduleParallel();

            NativeArray<Entity> entities = GetEntityQuery(new EntityQueryDesc{
                None = new ComponentType[] {typeof(TState)},
                All = new ComponentType[] {ComponentType.ReadOnly<TEntity>()}
            }).ToEntityArray(Allocator.Temp);

            foreach(Entity e in entities) {
                InitState(e);
            }
        }
    }
}