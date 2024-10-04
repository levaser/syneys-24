using UnityEngine;

namespace Game.Character
{
    public abstract class Action : ScriptableObject
    {
        [field: SerializeField]
        public GameObject Prefab { get; private set; }
    }
}