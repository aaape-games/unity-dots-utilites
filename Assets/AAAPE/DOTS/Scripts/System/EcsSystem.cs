using Unity.Entities;
using Unity.Collections;
using System;

namespace AAAPE.DOTS
{
    public abstract class EcsSystem : SystemBase
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            foreach (object attr in this.GetType().GetCustomAttributes(true))
            {
                if (attr is WithGameFlagAttribute)
                {
                    GameState.RequireForUpdate(this, World.EntityManager.CreateEntityQuery(
                        ((WithGameFlagAttribute)attr).Flag
                    ));
                }

            }
        }

    }
}
