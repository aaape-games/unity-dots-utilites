using Unity.Entities;
using System;
using Unity.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AAAPE.DOTS {

    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial class GameStateSystem : SystemBase {
        public enum TaskAction {
            ADD = 0,
            REMOVE = 1,
            TOGGLE = 2
        }
        public struct ComponentTask {
            public TaskAction Action;
            
            public Type Component;
        }

        private Entity gameStateEntity;

        private Queue<ComponentTask> tasks;


        public void AddComponent<T>() where T : struct, IComponentData {
            this.tasks.Enqueue(new ComponentTask {
                Action = TaskAction.ADD,
                Component = typeof(T)
            });
        }


        public void AddState<T>() where T : struct {
            this.tasks.Enqueue(new ComponentTask {
                Action = TaskAction.ADD,
                Component = typeof(T)
            });
        }

        public void ToggleComponent<T>() where T : struct, IComponentData {
            this.tasks.Enqueue(new ComponentTask {
                Action = TaskAction.TOGGLE,
                Component = typeof(T)
            });
        
        }

        public void RemoveComponent<T>() where T : struct, IComponentData{
             this.tasks.Enqueue(new ComponentTask {
                Action = TaskAction.REMOVE,
                Component = typeof(T)
            });
        }

        protected override void OnCreate() {
            this.tasks = new Queue<ComponentTask>();
            gameStateEntity = EntityManager.CreateEntity();  
            EntityManager.Instantiate(gameStateEntity);
        } 

        protected override void OnUpdate()
        {
            // on main thread
            for(int i = 0 ; i < this.tasks.Count; i++) {
                ComponentTask task = this.tasks.Dequeue();
                if(task.Action == TaskAction.ADD) {
                    EntityManager.AddComponent(this.gameStateEntity, task.Component);
                } else if(task.Action == TaskAction.REMOVE) {
                    EntityManager.RemoveComponent(this.gameStateEntity, task.Component);
                } else if (task.Action == TaskAction.TOGGLE) {
                     if(this.gameStateEntity != null && EntityManager.HasComponent(this.gameStateEntity, task.Component)) {
                        EntityManager.RemoveComponent(this.gameStateEntity, task.Component);
                    } else {
                        EntityManager.AddComponent(this.gameStateEntity, task.Component);
                    }
                }
            }
        }

        protected override void OnDestroy()
        {
            this.tasks.Clear();
        }
    }
}
