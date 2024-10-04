using UnityEngine;

namespace Utility
{
    public static class Vector2Extensions
    {
        public static Vector2 Rotate(this Vector2 v, float degrees)
        {
            float rad = degrees * Mathf.Deg2Rad;
            return new Vector2(
                (Mathf.Cos(rad) * v.x) - (Mathf.Sin(rad) * v.y),
                (Mathf.Sin(rad) * v.x) + (Mathf.Cos(rad) * v.y)
            );
        }

        public static Vector2 ClampAngle(this Vector2 v, Vector2 normal, float minDeg, float maxDeg)
        {
            float angle = Vector2.Angle(v, normal);
            float clampedAngle = Mathf.Clamp(angle, minDeg, maxDeg);
            return v.Rotate(v.AngleSign(normal) * (clampedAngle - angle));
        }

        public static float AngleSign(this Vector2 from, Vector2 to)
        {
            return Mathf.Sign(from.x * to.y - from.y * to.x);
        }
    }
}