using Unity.Entities;
using Unity.Rendering;
using UnityEngine;

namespace AAAPE.DOTS
{
    [UpdateInGroup(typeof(GameObjectBeforeConversionGroup))]
    [UpdateAfter(typeof(GameObjectProxyConversionSystem))]
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