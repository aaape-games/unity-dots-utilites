using Unity.Entities;
using AAAPE.DOTS;

public class GuyState {
    public static SharedScriptableState With(GuyStates withState) {
        // the NEXT should be the same always, or else they are in a transition state 
        return new SharedScriptableState() { Current  = withState.ToString(), ID = "GuyStates"};
    }
}

public enum GuyStates {
IDLE = 0,
WALKING = 1,
STOP = 2,
}