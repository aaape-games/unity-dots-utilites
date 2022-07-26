using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace AAAPE.DOTS
{
    public class SkinnedMeshRendererProxy : IComponentData
    {
        public SkinnedMeshRenderer SkinnedMeshRenderer;
    }

    public class SkinnedMeshRendererProxyAuthoring : ComponentProxyAuthoring
    {
        public bool RemoveEntityMesh = true;
    }
}