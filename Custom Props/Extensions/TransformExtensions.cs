using UnityEngine;

namespace CustomProps.Extensions
{
    public static class TransformExtensions
    {
        public static Transform FindRecursive(this Transform transform, string transformName)
        {
            if (transform.name == transformName)
                return transform;

            for (var i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i).FindRecursive(transformName);

                if (child != null)
                    return child;
            }

            return null;
        }
    }
}
