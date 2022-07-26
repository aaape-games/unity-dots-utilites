using Unity.Entities;
using Unity.Jobs;

namespace AAAPE.DOTS
{
    public class EndSimulationScheduler : Scheduler<EndSimulationEntityCommandBufferSystem>
    {
        public EndSimulationScheduler(Unity.Jobs.JobHandle handle) : base(handle)
        {
        }
    }
}