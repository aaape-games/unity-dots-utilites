using Unity.Entities;
using Unity.Jobs;
using Unity.Collections;
using System;
using UnityEngine;

namespace AAAPE.DOTS
{
    public static class EntityQueryExtension
    {
        public static EntityQuery WithChangedVersionFilter<T>(this EntityQuery query) where T : IComponentData
        {
            query.SetChangedVersionFilter(typeof(T));

            return query;
        }


        public static EntityQuery WithChangedVersionFilter(this EntityQuery query, Type type)
        {
            query.SetChangedVersionFilter(type);

            return query;
        }


        public static EntityQuery WithSharedComponentFilter<T>(this EntityQuery query, T sharedData) where T : struct, ISharedComponentData
        {
            query.SetSharedComponentFilter(sharedData);

            return query;
        }
    }
}