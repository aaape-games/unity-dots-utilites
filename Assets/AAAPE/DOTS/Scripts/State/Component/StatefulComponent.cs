using Unity.Entities;
using System;

namespace AAAPE.DOTS
{
    public interface StatefulComponent<TState, TEnum> : IComponentData
        where TState : struct, State<TEnum> where TEnum : System.Enum
    {
        public TState State { get; set; }
    }
}