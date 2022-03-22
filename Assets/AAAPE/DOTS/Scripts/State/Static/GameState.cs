using Unity.Entities;
using System;
using Unity.Collections;

namespace AAAPE.DOTS
{
    public static class GameState
    {
        private static GameStateSystem system;

        private static GameStateSystem getSystem()
        {
            if (system == null)
            {
                system = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<GameStateSystem>();
            }
            return system;
        }

        public static void RequireForUpdate<TSystemFlag>(ComponentSystemBase systemBase)
        {
            systemBase.RequireSingletonForUpdate<TSystemFlag>();
        }

        public static void SetFlag<T>() {
            getSystem().AddComponent<T>();
        }       
         
        public static void ToggleFlag<T>() {
            getSystem().ToggleComponent<T>();
        }
    }
}