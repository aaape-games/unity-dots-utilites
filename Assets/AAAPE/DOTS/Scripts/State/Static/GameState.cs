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
# if UNITY_EDITOR
            return World.DefaultGameObjectInjectionWorld.GetExistingSystem<GameStateSystem>();
#endif
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

        public static void RequireForUpdate(ComponentSystemBase systemBase, EntityQuery query)
        {
            systemBase.RequireForUpdate(query);
        }

        public static void SetFlag<T>() where T : struct, IComponentData
        {
            getSystem().AddComponent<T>();
        }


        public static void UnsetFlag<T>() where T : struct, IComponentData
        {
            getSystem().RemoveComponent<T>();
        }

        public static void ToggleFlag<T>() where T : struct, IComponentData
        {
            getSystem().ToggleComponent<T>();
        }
    }
}