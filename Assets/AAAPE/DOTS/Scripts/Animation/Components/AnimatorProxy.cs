using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace AAAPE.DOTS
{
    public class AnimatorProxyAuthoring : IComponentData
    {
        public Animator Animator;

        public Transform Transform;
    }

    [DisallowMultipleComponent]
    [UpdateInGroup(typeof(GameObjectAfterConversionGroup))] // after everything converts to entities
    public class AnimatorProxy : MonoBehaviour, IConvertGameObjectToEntity
    {
        public Animator animator;
        public MeshRenderer meshRenderer;
        public bool DisableEcsMesh = true;
        public MeshFilter meshFilter;


        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            // create a real gameobject with animator
            GameObject go = new GameObject(this.gameObject.name + "AnimatorProxyGameObject");
            go.AddComponent<Animator>();
            go.AddComponent<MeshRenderer>();
            go.AddComponent<MeshFilter>();

            Animator anim = go.GetComponent<Animator>();
            anim.runtimeAnimatorController = animator.runtimeAnimatorController;

            MeshRenderer renderer = go.GetComponent<MeshRenderer>();
            renderer.materials = meshRenderer.materials;

            MeshFilter filter = go.GetComponent<MeshFilter>();
            filter.mesh = meshFilter.mesh;

            if (DisableEcsMesh)
            {
                dstManager.RemoveComponent<MeshRenderer>(entity);
            }

            dstManager.AddComponentObject(entity, new AnimatorProxyAuthoring
            {
                Animator = go.GetComponent<Animator>(),
                Transform = go.transform
                // add entity?
            });
        }
    }

}
