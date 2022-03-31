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

            Entities
                .WithAll<Guy>()
                .WithNativeDisableParallelForRestriction(randoms)
                .WithSharedComponentFilter(new GuyState { Value = GuyStates.IDLE })
                .ForEach((int nativeThreadIndex, ref MoveTo moveTo, ref Guy guy, in Translation translation, in FindRandomPosition position) =>
                {
                    Unity.Mathematics.Random random = randoms[nativeThreadIndex];
                    moveTo.Position = random.NextFloat3(
                        position.Min,
                        position.Max
                    );
                    guy.state = new GuyState{Value =GuyStates.WALKING};
                    randoms[nativeThreadIndex] = random;
                }).ScheduleParallel();
        }
    }
}
