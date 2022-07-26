using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Unity.Collections;

namespace AAAPE.DOTS
{
    public struct PlayAnimationClip : IComponentData
    {
        public FixedString64Bytes clip;

        public PlayAnimationClip(string clip) {
            this.clip = new FixedString64Bytes(clip);
        }
    }

    [DisallowMultipleComponent]
    public class PlayAnimationClipAuthoring: MonoBehaviour, IConvertGameObjectToEntity
    {
        public string clip;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData<PlayAnimationClip>(entity,new PlayAnimationClip(clip));
        }
    }

}
