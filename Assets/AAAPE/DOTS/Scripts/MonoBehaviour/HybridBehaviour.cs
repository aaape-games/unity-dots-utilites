using UnityEngine;
using System;
using Unity.Entities;
using System.Reflection;
using System.Collections;


namespace AAAPE.DOTS {
    public class HybridBehaviour: MonoBehaviour {

        protected EntityManager manager;

        protected BlobAssetStore store;

        protected GameObjectConversionSettings settings;

        protected void Start() {

            this.store = new BlobAssetStore();
            this.manager = World.DefaultGameObjectInjectionWorld.EntityManager;
            this.settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, this.store);
        }  

        void onDestroy() {
            this.store.Dispose();
        }
    }
}