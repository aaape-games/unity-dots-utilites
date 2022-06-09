using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

namespace AAAPE.DOTS.Demo
{
    [GenerateAuthoringComponent]
    public struct FlickeringLightComponent : IComponentData
    {
        public float2 IntensityFromTo;

        public int smooth;

        private int lastFrame;

        public bool shouldSet() {

            if(lastFrame >= smooth) {
                lastFrame = 0;
                return true;
            }

            lastFrame ++;
            return false;
        }
    }

}