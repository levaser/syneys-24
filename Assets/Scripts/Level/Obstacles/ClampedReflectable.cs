using UnityEngine;
using Utility;

namespace Game.Levels
{
    public abstract class ClampedReflectable : MarkerClass, IReflectable
    {
        private const float minReflectionDegree = 20f;
        private const float maxReflectionDegree = 70f;

        public Vector2 GetReflectedDirection(Vector2 direction, RaycastHit2D hit)
        {
            return Vector2.Reflect(direction, hit.normal).ClampAngle(
                hit.normal,
                minReflectionDegree,
                maxReflectionDegree
            );
        }

        public abstract void OnContactPerformed(LevelStats levelStats);
    }
}