using Unity.Entities;
using UnityEngine;
using Unity.Transforms;
using Unity.Collections;
using Unity.Rendering;

namespace AAAPE.DOTS
{

    [UpdateInGroup(typeof(GameObjectBeforeConversionGroup))]
    [UpdateAfter(typeof(GameObjectProxyConversionSystem))]
    [UpdateBefore(typeof(MeshRendererProxyConversionSystem))]
    public class MeshRendererProxyConversionSystem : GameObjectConversionSystem
    {
        protected override void OnUpdate()
        {
            // Iterate over all authoring components of type FooAuthoring
            Entities.ForEach((Entity newEntity, MeshRendererProxyAuthoring input) =>
            {
                Entity entity = GetPrimaryEntity(input);
                DstEntityManager.AddComponentObject(entity, new MeshRendererProxy
                {
                    MeshRenderer = input.GetProxiedComponent<MeshRenderer>()
                });

                if(input.RemoveEntityMesh) {
                    EntityManager.RemoveComponent<MeshRenderer>(entity);
                }
            });
        }
    }
}