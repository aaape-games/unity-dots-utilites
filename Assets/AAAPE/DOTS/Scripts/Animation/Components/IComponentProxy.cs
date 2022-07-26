using Unity.Entities;
using UnityEngine;
using Unity.Rendering;

namespace AAAPE.DOTS
{
    public interface IComponentProxy : IComponentData
    {
    }

    public class ComponentProxyAuthoring : MonoBehaviour
    {
        [HideInInspector] public GameObject proxiedSelf;

        public T GetProxiedComponent<T>()
        {
            return proxiedSelf.GetComponent<T>();
        }
    }
}