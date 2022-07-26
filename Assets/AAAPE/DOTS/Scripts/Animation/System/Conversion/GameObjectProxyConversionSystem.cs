using Unity.Entities;
using UnityEngine;
using Unity.Transforms;
using Unity.Collections;

namespace AAAPE.DOTS
{
    [UpdateInGroup(typeof(GameObjectBeforeConversionGroup))]
    public class GameObjectProxyConversionSystem: GameObjectConversionSystem {
        protected override void OnUpdate()
        {
            Entities.ForEach((GameObjectProxyAuthoring input) =>
            {
                Entity entity = GetPrimaryEntity(input);
                
                if (input.proxy == null)
                {
                    input.proxy = GameObject.Instantiate(input.gameObject);
                }
              
                input.proxy.AddComponent<ProxiedComponent>();
                input.proxy.name = input.gameObject.name + "_ProxyGameObject";

                if(input.hasProxyComponents) {
                    AddProxyRecursive(input.gameObject, input.proxy);
                }
              
                input.proxy.transform.position = input.transform.position;
                input.proxy.transform.rotation = input.transform.rotation;
                input.proxy.transform.localScale = input.transform.localScale;

                DstEntityManager.AddComponentObject(entity, new GameObjectProxy
                {
                    GameObject = input.proxy
                });
                
            });
        }

        private void AddProxyRecursive(GameObject originalGameObject, GameObject proxiedGameObject) {
            ComponentProxyAuthoring[] originalCopmonents = originalGameObject.GetComponents<ComponentProxyAuthoring>();
            ComponentProxyAuthoring[] proxiedCopmontes = proxiedGameObject.GetComponents<ComponentProxyAuthoring>();

            for(int i = 0; i < originalCopmonents.Length; i++) {
                ComponentProxyAuthoring entitiesComponent = originalCopmonents[i];
                entitiesComponent.proxiedSelf = proxiedCopmontes[i].gameObject;
            }

            for(int i = 0; i < originalGameObject.transform.childCount; i ++){
                AddProxyRecursive(originalGameObject.transform.GetChild(i).gameObject, proxiedGameObject.transform.GetChild(i).gameObject);
            } 
        } 

    }
}