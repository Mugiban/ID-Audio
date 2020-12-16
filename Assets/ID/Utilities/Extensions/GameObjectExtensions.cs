using UnityEngine;

namespace ID.Runtime.Utilities.Extensions
{
    public static  class GameObjectExtensions
    {
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            if (gameObject.TryGetComponent(out T requestedComponent))
            {
                return requestedComponent;
            }

            return gameObject.AddComponent<T>();
        }
    }
}