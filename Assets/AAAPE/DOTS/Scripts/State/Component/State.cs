using Unity.Entities;
using System; 

namespace AAAPE.DOTS
{
    public interface State<T> : ISharedComponentData where T: System.Enum
    {
    }
}