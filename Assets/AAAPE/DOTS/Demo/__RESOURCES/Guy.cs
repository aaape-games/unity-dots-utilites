using System;
using Unity.Entities;
using Unity.Collections;
namespace AAAPE.DOTS.Demo {

    [GenerateAuthoringComponent]
    public struct Guy : IComponentData
    {
    }

    //public class UpdateGuyStateSystem: UpdateStatefulComponentSystem<Guy, GuyState, GuyStates>{}
}