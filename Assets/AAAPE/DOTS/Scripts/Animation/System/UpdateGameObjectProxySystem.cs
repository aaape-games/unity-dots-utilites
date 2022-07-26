using Unity.Entities;
using UnityEngine;
using Unity.Transforms;
using Unity.Collections;

namespace AAAPE.DOTS
{
    public partial class UpdateGameObjectProxySystem : SystemBase
    {
        public void UpdateGameObjectProxy(GameObjectProxy proxy, in LocalToWorld ltw, in Rotation rotation)
        {
            if (proxy.GameObject == null)
            {
                return;
            }

            proxy.GameObject.transform.position = ltw.Position;
            proxy.GameObject.transform.localRotation = rotation.Value;
        }


        // this is done on the main thread unluckily
        // that means, using this a LOT will influence the performance
        protected override void OnUpdate()
        {
            Entities.ForEach((GameObjectProxy proxy, in LocalToWorld ltw, in Rotation rotation) =>
            {
                UpdateGameObjectProxy(proxy, ltw, rotation);
            }).WithoutBurst().Run();
        }
    }
}