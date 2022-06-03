using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Entities;
using UnityEngine.UI;

namespace AAAPE.DOTS.Demo {

    public class ArmySpawner : HybridBehaviour
    {
        public GameObject prefab;

        private int AmountUnits = 0;

        public Text text;

        private Entity entityPrefab;

        private bool isAddOnePerFrame = false;

        public void ToggleOnePerFrame() {
            isAddOnePerFrame = !isAddOnePerFrame;
        }

        public void Update() {
            if(isAddOnePerFrame) {
                Spawn(10);
            }            
        }

        public void Start() {
            base.Start();

            entityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(prefab, this.settings);
        }

        public void Spawn(int Amount) {
            Unity.Mathematics.Random random = new Unity.Mathematics.Random( (uint)new System.Random().Next());
            for(int i = 0; i < Amount; i++) {
                
                Entity entity = manager.Instantiate(entityPrefab);

                manager.AddComponentData<Translation>(entity, new Translation{
                    Value = new float3(random.NextFloat(0,800f), 0, random.NextFloat(0,800f))
                });

                if(i % 2 == 0) {
                    manager.SetComponentData<Guy>(entity, new Guy{state = new GuyState(GuyStates.STOP)});
                }

                AmountUnits ++;
                this.text.text = AmountUnits.ToString() + " Units";
            }
           
        }
    }

}