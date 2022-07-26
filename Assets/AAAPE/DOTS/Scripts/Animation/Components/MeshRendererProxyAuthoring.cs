using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace AAAPE.DOTS
{
    public class MeshRendererProxy : IComponentData
    {
        public MeshRenderer MeshRenderer;
    }

    public class MeshRendererProxyAuthoring : ComponentProxyAuthoring
    {
        public bool RemoveEntityMesh = true;
    }
}