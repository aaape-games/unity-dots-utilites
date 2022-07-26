using Unity.Entities;

namespace AAAPE.DOTS
{
    public static class ParallelWriterExtension
    {
        public static void AddStateTransition(this EntityCommandBuffer.ParallelWriter writer, int entityInQueryIndex, Entity entity, SharedScriptableState state)
        {
            writer.AddSharedComponent(entityInQueryIndex, entity, new StateTransition()
            {
               Current = state.Current
            });
        }
    }
}