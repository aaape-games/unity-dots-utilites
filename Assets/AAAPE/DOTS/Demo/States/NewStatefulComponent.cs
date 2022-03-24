using Unity.Entities;
using AAAPE.DOTS;

[GenerateAuthoringComponent]
public struct NewStatefulComponent : StatefulComponent<NewStatefulComponentState, NewStatefulComponentStates>
{
    public NewStatefulComponentState state;
    NewStatefulComponentState StatefulComponent<NewStatefulComponentState, NewStatefulComponentStates>.State { get => state; set => state =value; }
}

[System.Serializable]
public struct NewStatefulComponentState: State<NewStatefulComponentStates>{
    public NewStatefulComponentStates Value;
}

public enum NewStatefulComponentStates {
    IDLE = 0,
}

public class InitNewStatefulComponentStateSystem: InitStatefulComponentSystem<NewStatefulComponent, NewStatefulComponentState, NewStatefulComponentStates>{}
public class UpdateNewStatefulComponentStateSystem: UpdateStatefulComponentSystem<NewStatefulComponent, NewStatefulComponentState, NewStatefulComponentStates>{}