using UnityEngine;

namespace Game.Character
{
    public abstract class DefaultAction : ScriptableObject
    {
        [field: SerializeField]
        public GameObject Prefab { get; private set; }
    }
}