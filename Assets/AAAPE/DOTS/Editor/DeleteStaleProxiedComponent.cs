using UnityEditor;
using UnityEngine;

namespace AAAPE.DOTS
{
    [InitializeOnLoad]
    public static class DeleteStaleProxiedComponents
    {
        static DeleteStaleProxiedComponents()
        {
            EditorApplication.playModeStateChanged += ModeChanged;
        }

        static void ModeChanged(PlayModeStateChange change)
        {
            if (change == PlayModeStateChange.EnteredEditMode)
            {
                ProxiedComponent[] components = GameObject.FindObjectsOfType<ProxiedComponent>();

                foreach (ProxiedComponent component in components)
                {
                    if (component != null && component.gameObject != null)
                    {
                        GameObject.DestroyImmediate(component.gameObject);
                    }
                }
            }
        }
    }
}