using UnityEngine;


namespace AAAPE.DOTS {
    public  static class GameObjectUtils{
        public static T CopyComponent<T>(T from, GameObject to) where T: Component {
            Component copy = to.AddComponent<T>();
            System.Type type = from.GetType();
            System.Reflection.FieldInfo[] fields = type.GetFields();
            foreach (System.Reflection.FieldInfo field in fields)
            {
                field.SetValue(copy, field.GetValue(from));
            }
            return copy as T;
        }
    }
}