using UnityEditor;
using UnityEngine;

namespace Game.Levels
{
    public sealed class Wall : ClampedReflectable, IReflectable
    {
        public override void OnContactPerformed(LevelStats levelStats) {}
    }
}