using UnityEngine;

namespace Game.Character
{
    public abstract class DefaultAction : ScriptableObject
    {
        [field: SerializeField]
        public ActionPerformObserver Prefab { get; private set; }
    }
}