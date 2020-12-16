using UnityEngine;

namespace ID.Runtime.Utilities.Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null)
        {
            return new Vector3(x ?? original.x, y ?? original.y, z ?? original.z);
        }
        
        public static Vector2 ToVector2(this Vector3 v)
        {
            return new Vector2(v.x, v.y);
        }
        
        public static Vector2 With(this Vector2 original, float? x = null, float? y = null)
        {
            return new Vector2(x ?? original.x, y ?? original.y);
        }
        
        public static Vector2 ChangeX(this Vector2 v, float x)
        {
            return new Vector2(x, v.y);
        }
        
        public static Vector2 ChangeY(this Vector2 v, float y)
        {
            return new Vector2(v.x, y);
        }
        
        public static Vector3 ChangeX(this Vector3 v, float x)
        {
            return new Vector3(x, v.y, v.z);
        }
        
        public static Vector3 ChangeY(this Vector3 v, float y)
        {
            return new Vector3(v.x, y, v.z);
        }
        
        public static Vector3 ChangeZ(this Vector3 v, float z)
        {
            return new Vector3(v.x, v.y, z);
        }
    }
}