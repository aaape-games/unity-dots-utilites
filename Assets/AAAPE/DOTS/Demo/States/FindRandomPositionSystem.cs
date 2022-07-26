using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
using Unity.Collections;
using AAAPE.DOTS;

namespace AAAPE.DOTS.Demo
{
    [WithGameFlag(typeof(LevelStartedFlag))]
    public partial class FindRandomPositionSystem : SystemBase
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

            // get randoms into nativarray to be able to pass it to a job
            NativeArray<Unity.Mathematics.Random> randoms = randomSystem.Randoms;

            EndSimulationScheduler scheduler = new EndSimulationScheduler(Dependency);
            EntityCommandBuffer.ParallelWriter writer = scheduler.ScheduleParallel();

            Dependency = Entities
                .WithAll<Guy>()
                .WithNativeDisableParallelForRestriction(randoms)
                .WithSharedComponentFilter(new GuyState { Value = GuyStates.IDLE })
                .ForEach((int entityInQueryIndex, Entity entity, int nativeThreadIndex, ref MoveTo moveTo,
                    in Translation translation, in FindRandomPosition position) =>
                {
                    Unity.Mathematics.Random random = randoms[nativeThreadIndex];
                    moveTo.Position = random.NextFloat3(
                        position.Min,
                        position.Max
                    );


                    writer.AddSharedComponent<GuyState>(entityInQueryIndex, entity, new GuyState(GuyStates.WALKING));
                    randoms[nativeThreadIndex] = random;
                })
                .ScheduleParallel(Dependency)
                .WithScheduler(scheduler);
        }
    }
}