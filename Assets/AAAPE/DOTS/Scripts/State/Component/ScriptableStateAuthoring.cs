using System;
using Unity.Entities;
using UnityEditor;
using System.IO;
using UnityEngine;

namespace AAAPE.DOTS
{
    public struct SharedScriptableState : ISharedComponentData, IEquatable<SharedScriptableState>
    {
        public AbstractScriptableState state;

        public string Current;

        public string ID;

        public bool Equals(SharedScriptableState other)
        {
            return ID == other.ID && this.Current == other.Current;
        }

        public override bool Equals(object obj)
        {
            return obj is SharedScriptableState other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (state != null ? state.GetHashCode() : 0);
        }
    }

    public class ScriptableStateAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        public AbstractScriptableState scriptableState;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddSharedComponentData(entity,
                new SharedScriptableState()
                {
                    state = scriptableState,
                    ID = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(scriptableState)),
                    Current = scriptableState.DefaultState()
                });
        }
    }
}