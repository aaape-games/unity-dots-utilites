using Unity.Entities;
using UnityEngine;

namespace AAAPE.DOTS.StateDemo {
    public struct LevelStartedFlag: IComponentData{ }
    public struct NoGravityFlag: IComponentData{ }

    public class Flags : MonoBehaviour{
        public void SetStarted() {
            GameState.SetFlag<LevelStartedFlag>();
        }
        public void ToggleGravity() {      
            GameState.ToggleFlag<NoGravityFlag>();
        }
    }
}