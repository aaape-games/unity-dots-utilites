using UnityEditor;
using System.IO;
using UnityEngine;
using File = System.IO.File;

namespace AAAPE.DOTS
{
    [CustomEditor(typeof(ScriptableState))]
    public class ScriptableStateEditor : Editor {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            ScriptableState state = (ScriptableState)target;

            if (GUILayout.Button("Create/Save Enum"))
            {
                File.Delete(Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(target)) + "generated.cs");
                StateCodeGenerator.Generate(AssetDatabase.GetAssetPath(target), state.states);
                
                AssetDatabase.Refresh();
            }
        }
    }
}