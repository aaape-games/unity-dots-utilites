using Unity.Entities;
using UnityEngine;
using Unity.Rendering;

namespace AAAPE.DOTS
{
    public class GameObjectProxy : IComponentData
    {
        public GameObject GameObject;
    }

    
    public class GameObjectProxyAuthoring : MonoBehaviour
    {
        [HideInInspector]
        public GameObject proxy;

        public bool hasProxyComponents;
    }
}
