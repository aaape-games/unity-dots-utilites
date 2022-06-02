using Unity.Entities;
using UnityEngine;
using Unity.Transforms;
using Unity.Collections;

namespace AAAPE.DOTS
{
    public partial class UpdateAnimationProxySystem: SystemBase
    {
        public void UpdateAnimationProxy(AnimatorProxyAuthoring proxy, in LocalToWorld ltw, in Rotation rotation){
            proxy.Transform.position = ltw.Position;
            proxy.Transform.localRotation = rotation.Value;
        }

        // this is done on the main thread unluckily
        // that means, using this a LOT will influence the performance
        protected override void OnUpdate()
        {
            Entities.ForEach((AnimatorProxyAuthoring proxy, in LocalToWorld ltw, in Rotation rotation) => {
                  UpdateAnimationProxy(proxy, ltw, rotation);
            }).WithoutBurst().Run();
        }
    }
}