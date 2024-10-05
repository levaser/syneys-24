using Game.Levels;
using UnityEngine;
using VContainer;

namespace Game.Character
{
    public class CharacterMover
    {
        /// <summary>
        /// transform сетки игрового поля (1 юнит - 1 клетка)
        /// </summary>
        private readonly Transform _levelGridTransform;
        private readonly Transform _characterTransform;
        private readonly LevelConfig _levelConfig;

        [Inject]
        public CharacterMover(
            Transform levelGridTransform,
            Transform characterTransform,
            LevelStarter levelStarter
        )
        {
            _levelGridTransform = levelGridTransform;
            _characterTransform = characterTransform;
            _levelConfig = levelStarter.CurrentLevelConfig;
        }

        public void MoveByAction(MoveAction action)
        {

        }
    }
}