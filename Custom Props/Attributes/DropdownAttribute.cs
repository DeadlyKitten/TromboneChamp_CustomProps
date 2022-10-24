using System;
using UnityEngine;

namespace CustomProps.Attributes
{
    public class DropdownAttribute : PropertyAttribute
    {
        public delegate string[] GetStringList();

        public DropdownAttribute(params string[] list) => Strings = list;

        public DropdownAttribute(Type type, string methodName)
        {
            var method = type.GetMethod(methodName);
            if (method != null)
            {
                Strings = method.Invoke(null, null) as string[];
            }
            else
            {
                Debug.LogError("NO SUCH METHOD " + methodName + " FOR " + type);
            }
        }

        public string[] Strings
        {
            get;
            private set;
        }
    }
}
