using UnityEngine;

namespace Game.Character
{
    [CreateAssetMenu(fileName = "New ActionDeck Config", menuName = "Actions/Deck Config", order = 51)]
    public class ActionDeckConfig : ScriptableObject
    {
        [field: SerializeField]
        public Action[] Actions { get; private set; }
    }
}