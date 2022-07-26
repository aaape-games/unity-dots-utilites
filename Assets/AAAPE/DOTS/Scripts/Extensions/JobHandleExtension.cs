using Unity.Entities;
using Unity.Jobs;
using Unity.Collections;
using UnityEngine;

namespace AAAPE.DOTS
{
    public static class JobHandleExtension
    {
        public static JobHandle WithContainer<T>(this JobHandle handle, NativeArray<T> container) where T : struct
        {
            handle.Complete();
            container.Dispose();

            return handle;
        }

        public static JobHandle WithContainers<T>(this JobHandle handle, params NativeArray<T>[] container)
            where T : struct
        {
            handle.Complete();
            foreach (NativeArray<T> array in container)
            {
                array.Dispose();
            }

            return handle;
        }


        public static JobHandle WithScheduler<T>(this JobHandle handle, Scheduler<T> scheduler)
            where T : EntityCommandBufferSystem
        {
            scheduler.AddJobHandle(handle);

            return handle;
        }
    }
}