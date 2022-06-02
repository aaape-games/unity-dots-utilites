using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Unity.Collections;

namespace AAAPE.DOTS
{
    public struct PlayAnimationClipAuthoring : IComponentData
    {
        public FixedString64 clip;

        public PlayAnimationClipAuthoring(string clip) {
            this.clip = new FixedString64(clip);
        }
    }

    [DisallowMultipleComponent]
    public class PlayAnimationClip: MonoBehaviour, IConvertGameObjectToEntity
    {
        public string clip;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData<PlayAnimationClipAuthoring>(entity,new PlayAnimationClipAuthoring(clip));
        }
    }

}
