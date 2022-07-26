using Unity.Entities;
using UnityEngine;
using Unity.Rendering;

namespace AAAPE.DOTS
{
    public class GameObjectProxy : IComponentData
    {
        public GameObject GameObject;
        
        public bool isDestroyed = false;
    }

    
    public class GameObjectProxyAuthoring : MonoBehaviour
    {
        public GameObject proxy;

        public bool hasProxyComponents;
    }
}
