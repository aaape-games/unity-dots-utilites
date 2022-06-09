using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

namespace AAAPE.DOTS.Demo
{

    public class FlickerLightSystem : SystemBase
    {
        private RandomSystem randomSystem;
        protected override void OnCreate()
        {
            this.randomSystem = World.GetOrCreateSystem<RandomSystem>();
        }


        protected override void OnUpdate()
        {
            Entities
            .ForEach((Light light, ref FlickeringLightComponent flicker) =>
            {
                if(flicker.shouldSet()) {
                    // unity is using managed class light internally, so we are doing this on the main thread again
                    light.intensity = UnityEngine.Random.Range(flicker.IntensityFromTo.x, flicker.IntensityFromTo.y);
                }
              
            }).WithoutBurst().Run();
        }
    }

}