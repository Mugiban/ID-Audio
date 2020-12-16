using System.Collections.Generic;
using UnityEngine;

namespace ID.Runtime.Utilities.Extensions
{
    public static class TransformExtensions
    {
        public static List<Transform> GetChildren(this Transform transform)
        {
            List<Transform> children = new List<Transform>();

            int childCount = transform.childCount;

            for (int i = 0; i < childCount; i++)
            {
                children.Add(transform.GetChild(i));
            }

            return children;
        }
        
        public static void DestroyChildren(this Transform transform)
        {
            var objects = transform.GetComponentsInChildren<Transform>();
            for (int i = 0; i < objects.Length; i++)
            {
                if(objects[i] == null)
                    continue;
                if(objects[i].transform == transform)
                    continue;
                if(Application.isPlaying)
                    Object.Destroy(objects[i].gameObject);
                else
                    Object.DestroyImmediate(objects[i].gameObject);
            }
        }
    }
}