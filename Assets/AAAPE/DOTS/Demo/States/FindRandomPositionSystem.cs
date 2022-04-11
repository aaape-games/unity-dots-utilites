using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
using Unity.Collections;
using AAAPE.DOTS;

namespace AAAPE.DOTS.Demo
{
    [WithGameFlag(typeof(LevelStartedFlag))]
    public class FindRandomPositionSystem : SystemBase
    {
        private RandomSystem randomSystem;
        protected override void OnCreate()
        {
            this.randomSystem = World.GetOrCreateSystem<RandomSystem>();
        }

        // this is only ran when the levelStartedFlag exists somewhere
        protected override void OnUpdate()
        {
            float DeltaTime = Time.DeltaTime;


            NativeArray< Unity.Mathematics.Random > randoms = randomSystem.Randoms;
           
            NativeQueue<StateChangeAction<GuyState, GuyStates>> queue = new NativeQueue<StateChangeAction<GuyState, GuyStates>>(Allocator.TempJob);
            NativeQueue<StateChangeAction<GuyState, GuyStates>>.ParallelWriter writer = queue.AsParallelWriter();

            Dependency = Entities
                .WithAll<Guy>()
                .WithNativeDisableParallelForRestriction(randoms)
                .WithSharedComponentFilter(new GuyState { Value = GuyStates.IDLE })
                .ForEach((int entityInQueryIndex, Entity e, int nativeThreadIndex, ref MoveTo moveTo, in Translation translation, in FindRandomPosition position) =>
                {
                    Unity.Mathematics.Random random = randoms[nativeThreadIndex];
                    moveTo.Position = random.NextFloat3(
                        position.Min,
                        position.Max
                    );
                    writer.Enqueue(GuyState.Action(GuyStates.WALKING, e));
                    randoms[nativeThreadIndex] = random;
                })
                .ScheduleParallel(Dependency)
                .WithStateChange(queue, EntityManager);
        }
    }
}
