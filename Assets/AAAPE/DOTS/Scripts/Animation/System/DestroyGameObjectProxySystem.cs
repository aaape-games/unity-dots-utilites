using Unity.Entities;
using UnityEngine;
using Unity.Transforms;
using Unity.Collections;

namespace AAAPE.DOTS
{
    [AlwaysUpdateSystem]
    [UpdateAfter(typeof(UpdateGameObjectProxySystem))]
    public partial class DestroyGameObjectProxySystem : SystemBase
    {
        protected override void OnDestroy()
        {
            Entities.ForEach((Entity entity, GameObjectProxy proxy) =>
            {
                proxy.isDestroyed = true;

                if (proxy.GameObject == null)
                {
                    return;
                }

                GameObject.DestroyImmediate(proxy.GameObject);
                EntityManager.RemoveComponent<GameObjectProxy>(entity);
            }).WithStructuralChanges().WithoutBurst().Run();
        }

        protected override void OnUpdate()
        {
        }
    }
}