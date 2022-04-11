using Unity.Entities;
using AAAPE.DOTS;



public enum BoyStates {
    IDLE = 0,
}

// you only need this when you are using a hybrid-renderer or creating entitys from prefabs
// if you do the PURE ECS (not recommended) way, you can also manually add the BoyState shared component to the entity on spawn
[GenerateAuthoringComponent]
public struct Boy : StatefulComponent<BoyState, BoyStates>
{
    public BoyState state;
    BoyState StatefulComponent<BoyState, BoyStates>.State { get => state; set => state =value; }
}

[System.Serializable]
public struct BoyState: State<BoyStates>{
    public BoyStates Value;

    public BoyState(BoyStates stateValue) {
        this.Value = stateValue;
    }

#region overriding Equals for EVEN FASTER comparison in query filtering
    public override bool Equals(object obj)
    {
        if(!(obj is BoyState)) {
            return false;
        }

        return ((BoyState)obj).Value == this.Value;
    }
    
    // override object.GetHashCode
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

#endregion

    public static StateChangeAction<BoyState, BoyStates> Action (BoyStates stateValue, Entity e) {
        return new StateChangeAction<BoyState, BoyStates>(new BoyState{Value=stateValue}, e);
    }
}


// you only need this when you are using a hybrid-renderer or creating entitys from prefabs
// if you do the PURE ECS (not recommended) way, you can also manually add the BoyState shared component to the entity on spawn
public class InitBoyStateSystem: InitStatefulComponentSystem<Boy, BoyState, BoyStates>{}

// if you want to fiddle with the ComponentStates, uncomment this system.
// This will update the BoyState Shared Component from Boy.State 
// when using hybrid renderer, you can switch the states from within the unity editor
//public class UpdateBoyStateSystem: UpdateStatefulComponentSystem<Boy, BoyState, BoyStates>{}