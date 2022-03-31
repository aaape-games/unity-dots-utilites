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

        public int UnitsX = 200;

        public int UnitsY = 200;

        public int Gap = 2;

        public Text text;

        public bool isAddOnePerFrame = false;

        public void ToggleOnePerFrame() {
            isAddOnePerFrame = !isAddOnePerFrame;
        }

        public void Update() {
            if(isAddOnePerFrame) {
                Spawn(1);
            }            
        }

        public void Spawn(int Amount) {

            for(int i = 0; i < Amount; i++) {
                Entity entity = GameObjectConversionUtility.ConvertGameObjectHierarchy(prefab, this.settings);
                int x = AmountUnits % UnitsY;
                int y = AmountUnits / UnitsX;
                manager.AddComponentData<Translation>(entity, new Translation{
                    Value = new float3(x * Gap, 0, y * Gap)
                });
                manager.Instantiate(entity);
                AmountUnits ++;
                this.text.text = AmountUnits.ToString() + " Units";
            }
           
        }
    }

}