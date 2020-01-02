using UnityEngine;

namespace Utility
{
    public static class Vector
    {
        public static Vector2 ToXZ(this Vector3 vec)
        {
            return new Vector2(vec.x, vec.z);
        }

        public static Vector3 ToXYZ(this Vector2 vec)
        {
            return new Vector3(vec.x, 0f, vec.y);
        }

        public static Vector3 OnlyXZ(this Vector3 vec)
        {
            return new Vector3(vec.x, 0f, vec.y);
        }
    }
}