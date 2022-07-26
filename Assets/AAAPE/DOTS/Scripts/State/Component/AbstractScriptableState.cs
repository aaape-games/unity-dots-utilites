using System;
using ICSharpCode.NRefactory.Ast;
using UnityEngine;

namespace AAAPE.DOTS
{
    public abstract class AbstractScriptableState: ScriptableObject
    {
        [HideInInspector]
        public string ID;

        public string[] states;

        public int DefaultStateIndex;
        
        public string DefaultState()
        {
            return this.states[DefaultStateIndex];
        }
    }
}