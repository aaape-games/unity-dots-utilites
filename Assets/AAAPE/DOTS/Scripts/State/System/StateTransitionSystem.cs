using Unity.Entities;

namespace AAAPE.DOTS
{
    public partial class StateTransitionSystem: SystemBase
    {
        protected override void OnUpdate()
        {
            EntityCommandBuffer buffer = new EntityCommandBuffer();
            Entities.WithAll<StateTransition>()
                .ForEach((Entity e, in SharedScriptableState ScriptableState, in StateTransition transition) =>
                {
                    buffer.SetSharedComponent(e, new SharedScriptableState()
                    {
                        state = ScriptableState.state,
                        Current =  transition.Current,
                        ID = ScriptableState.ID
                    });
                    // @todo:: callbacks for transitions
                }).WithoutBurst().Run();
        }
    }
}