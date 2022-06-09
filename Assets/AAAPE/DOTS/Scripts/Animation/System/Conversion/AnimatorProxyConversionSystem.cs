using Unity.Entities;
using UnityEngine;
using Unity.Transforms;
using Unity.Collections;

namespace AAAPE.DOTS
{

    [UpdateInGroup(typeof(GameObjectBeforeConversionGroup))]
    [UpdateAfter(typeof(GameObjectProxyConversionSystem))]
    public class AnimatorProxyConversionSystem: GameObjectConversionSystem {
        protected override void OnUpdate()
        {
            // Iterate over all authoring components of type FooAuthoring
            Entities.ForEach((AnimatorProxyAuthoring input) =>
            {

                Entity entity = GetPrimaryEntity(input);
                DstEntityManager.AddComponentObject(entity, new AnimatorProxy
                {
                    Animator = input.GetProxiedComponent<Animator>(),
                });
            });
        }
    }
}