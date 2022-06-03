using Unity.Entities;
using Unity.Collections;
namespace AAAPE.DOTS.Demo {

    [GenerateAuthoringComponent]
    public struct Guy : StatefulComponent<GuyState, GuyStates>
    {
        public GuyState state;
        GuyState StatefulComponent<GuyState, GuyStates>.State { get => state; set => state =value; }
    }

    [System.Serializable]
    public struct GuyState: State<GuyStates>{
        public GuyStates Value;

        public GuyState(GuyStates state) {
            this.Value = state;
        }


        #region overriding Equals for EVEN FASTER comparison in query filtering
                public override bool Equals(object obj)
                {
                    if(!(obj is GuyState)) {
                        return false;
                    }

                    return ((GuyState)obj).Value == this.Value;
                }
                
                // override object.GetHashCode
                public override int GetHashCode()
                {
                    return Value.GetHashCode();
                }

        #endregion

        public static StateChangeAction<GuyState, GuyStates> Action (GuyStates stateValue, int sortKey, Entity e) {
            return new StateChangeAction<GuyState, GuyStates>(new GuyState{Value=stateValue}, sortKey, e);
        }

        public static NativeQueue<StateChangeAction<GuyState, GuyStates>> InitStateChange() {
            return new NativeQueue<StateChangeAction<GuyState, GuyStates>>(Allocator.TempJob);
        }

    }
    
    public enum GuyStates {
        IDLE = 0,
        WALKING = 1,
        STOP = 2
    }

    public class InitGuyStateSystem: InitStatefulComponentSystem<Guy, GuyState, GuyStates>{}
    //public class UpdateGuyStateSystem: UpdateStatefulComponentSystem<Guy, GuyState, GuyStates>{}
}