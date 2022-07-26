using System.Collections.Generic;
using System.IO;
using System;

namespace AAAPE.DOTS
{
    public class StateCodeGenerator
    {
        public const string template = @"using Unity.Entities;
using AAAPE.DOTS;

public class $NAMEState {
    public static SharedScriptableState With($NAMEStates withState) {
        // the NEXT should be the same always, or else they are in a transition state 
        return new SharedScriptableState() { Current  = withState.ToString(), ID = ""$NAMEStates""};
    }
}

";
        
        public static void Generate(string assetFilePath, string[] valueNames)
        {
            string name = Path.GetFileNameWithoutExtension(assetFilePath);
        
            string dir = Path.GetDirectoryName(assetFilePath);

            string text = template.Replace("$NAME", name);

            text += "public enum " + name + "States {" + Environment.NewLine;

            int i = 0;
            foreach (string valueName in valueNames)
            {
                text += valueName.ToUpper() + " = " + i.ToString() + "," + Environment.NewLine;
                i++;
            }

            text += "}";

            File.WriteAllText(dir + Path.DirectorySeparatorChar + name + ".generated.cs", text);
        }
    }
}