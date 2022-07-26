using System;
using Unity.Entities;

namespace AAAPE.DOTS
{
    public struct StateTransition : ISharedComponentData, IEquatable<StateTransition>
    {
        public string Current;

        public string ID;

        public bool Equals(StateTransition other)
        {
            return Current == other.Current;
        }

        public override bool Equals(object obj)
        {
            return obj is StateTransition other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Current != null ? Current.GetHashCode() : 0);
        }
    }
}