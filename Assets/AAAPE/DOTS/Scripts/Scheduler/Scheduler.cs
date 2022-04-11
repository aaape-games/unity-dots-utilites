using Unity.Entities;
using Unity.Jobs;

namespace AAAPE.DOTS {

    public abstract class Scheduler<T> where T: EntityCommandBufferSystem {

        private EntityCommandBufferSystem bufferSystem;
        private EntityCommandBuffer buffer;

        public Scheduler() {
            this.bufferSystem = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<T>();
            this.buffer = this.bufferSystem.CreateCommandBuffer();
        }
        
        public Scheduler(JobHandle handle) {
            this.bufferSystem = World.DefaultGameObjectInjectionWorld.GetOrCreateSystem<T>();
            this.buffer = this.bufferSystem.CreateCommandBuffer();
            this.bufferSystem.AddJobHandleForProducer(handle);
        }

        public void AddJobHandle(JobHandle handle) {
            this.bufferSystem.AddJobHandleForProducer(handle);
        }
        
        public EntityCommandBuffer Schedule() {
            return this.buffer;
        }

        public EntityCommandBuffer.ParallelWriter ScheduleParallel() {
            return this.buffer.AsParallelWriter();
        }

        
    }
}