using Unity.Entities;
using System;
using Unity.Collections;

namespace AAAPE.DOTS {
    public class GameStateSystem : SystemBase {

        private Entity gameStateEntity;

        public void AddComponent<T>() {
            EntityManager.AddComponent<T>(this.gameStateEntity);
        }

        public void ToggleComponent<T>() {
            if(EntityManager.HasComponent<T>(this.gameStateEntity)) {
                this.RemoveComponent<T>();
            } else {
                this.AddComponent<T>();
            }
        }

        public void RemoveComponent<T>() {
            EntityManager.RemoveComponent<T>(this.gameStateEntity);
        }

        
        protected override void OnCreate() {
            gameStateEntity = EntityManager.CreateEntity();  
        } 

        protected override void OnUpdate()
        {
        }
    }
}