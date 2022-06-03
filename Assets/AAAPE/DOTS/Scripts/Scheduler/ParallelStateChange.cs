using Unity.Entities;
using Unity.Jobs;
using Unity.Collections;

namespace AAAPE.DOTS
{
    public struct StateChangeAction<TState, TEnum> where TState : struct, State<TEnum> where TEnum : System.Enum
    {
        public TState state;
        public Entity entity;
        public int sortKey;

        public StateChangeAction(TState state, int sortKey, Entity entity) {
            this.entity = entity;
            this.state = state;
            this.sortKey = sortKey;
        }
    }
    public struct ParallelStateChange<TState, TEnum> where TState : struct, State<TEnum> where TEnum : System.Enum
    {
        public struct StateChangeAction
        {
            public TState state;
            public Entity entity;
        }


        public NativeQueue<StateChangeAction>.ParallelWriter actions;

        public ParallelStateChange(NativeQueue<StateChangeAction>.ParallelWriter queue)
        {
            this.actions = queue;
        }

        public void SetState(int index, Entity e, TState state)
        {
            this.actions.Enqueue(new StateChangeAction()
            {
                entity = e,
                state = state
            });
        }


        // Deprecated
        public struct WithoutBurst
        {
            private EntityCommandBuffer.ParallelWriter writer;

            public WithoutBurst(EntityCommandBuffer.ParallelWriter writer)
            {
                this.writer = writer;
            }

            public void SetState(int index, Entity e, TState state)
            {
                this.writer.SetSharedComponent(index, e, state);
            }
        }
    }
}