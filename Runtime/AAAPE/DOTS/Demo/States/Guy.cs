using Unity.Entities;

namespace AAAPE.DOTS.StateDemo {

    [GenerateAuthoringComponent]
    public struct Guy : StatefulComponent<GuyState, GuyStates>
    {
        public GuyState state;
        GuyState StatefulComponent<GuyState, GuyStates>.State { get => state; set => state =value; }
    }

    [System.Serializable]
    public struct GuyState: State<GuyStates>{
        public GuyStates Value;
    }
    
    public enum GuyStates {
        IDLE = 0,
        WALKING = 1
    }

    public class InitGuyStateSystem: InitStatefulComponentSystem<Guy, GuyState, GuyStates>{}
    public class UpdateGuyStateSystem: UpdateStatefulComponentSystem<Guy, GuyState, GuyStates>{}
}