using UnityEngine;

namespace Game.Character
{
    public enum MoveDirection
    {
        Forward,
        Left,
        Right
    }

    [CreateAssetMenu(fileName = "New MoveAction Config", menuName = "Actions/MoveAction Config", order = 51)]
    public class MoveAction : DefaultAction
    {
        [field: SerializeField]
        public int Distance { get; private set; }

        [field: SerializeField]
        public MoveDirection Direction { get; private set; }
    }
}