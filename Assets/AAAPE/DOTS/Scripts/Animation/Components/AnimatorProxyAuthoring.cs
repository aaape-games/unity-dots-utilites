using Unity.Entities;
using UnityEngine;
using Unity.Rendering;

namespace AAAPE.DOTS
{
    public class AnimatorProxy: IComponentProxy {
        public Animator Animator;
    }

    public class AnimatorProxyAuthoring : ComponentProxyAuthoring
    {

    }
}
