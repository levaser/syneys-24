using Array2DEditor;
using Game.Character;
using UnityEngine;

namespace Game.Levels
{
    [CreateAssetMenu(fileName = "New Level Config", menuName = "Level Config", order = 51)]
    public sealed class LevelConfig : ScriptableObject
    {
        [field: SerializeField]
        public Array2DCellType Grid { get; private set; }

        [field: SerializeField]
        public ActionDeckConfig ActionDeck { get; private set; }
    }
}