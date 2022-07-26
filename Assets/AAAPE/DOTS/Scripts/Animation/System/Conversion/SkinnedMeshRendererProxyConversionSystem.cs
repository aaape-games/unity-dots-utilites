using Unity.Entities;
using UnityEngine;

namespace AAAPE.DOTS
{
    [UpdateInGroup(typeof(GameObjectBeforeConversionGroup))]
    [UpdateAfter(typeof(GameObjectProxyConversionSystem))]
    public class SkinnedMeshRendererProxyConversionSystem : GameObjectConversionSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((Entity newEntity, SkinnedMeshRendererProxyAuthoring input) =>
            {
                Entity entity = GetPrimaryEntity(input);
                DstEntityManager.AddComponentObject(entity, new SkinnedMeshRendererProxy
                {
                    SkinnedMeshRenderer = input.GetProxiedComponent<SkinnedMeshRenderer>()
                });

                if (input.RemoveEntityMesh)
                {
                    EntityManager.RemoveComponent<SkinnedMeshRenderer>(newEntity);
                }
            });
        }
    }
}