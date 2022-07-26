using Unity.Entities;
using System;

namespace AAAPE.DOTS
{
    public interface State<TEnum> : ISharedComponentData where TEnum : System.Enum
    {
    }
}