using System.Collections.Generic;
using Game.Levels;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Character
{
    public class CharacterActionHandler : IStartable
    {
        private readonly LevelConfig _levelConfig;
        private readonly CharacterDeck _characterDeck;
        private readonly CharacterMover _characterMover;
        private readonly Transform _handGridTransform;

        private List<ActionPerformObserver> _handedActions;

        [Inject]
        public CharacterActionHandler
        (
            LevelStarter levelStarter,
            CharacterDeck characterDeck,
            CharacterMover characterMover
        )
        {
            _levelConfig = levelStarter.CurrentLevelConfig;
            _characterDeck = characterDeck;
            _characterMover = characterMover;

            _handedActions = new List<ActionPerformObserver>();
        }

        void IStartable.Start()
        {
            for (int i = 0; i < _levelConfig.HandedCardsNumber - _handedActions.Count; i++)
            {
                DefaultAction newAction = _characterDeck.ActionsDeck.Pop();
                ActionPerformObserver newActionInstance = Object.Instantiate(
                    newAction.Prefab,
                    _handGridTransform
                );
                newActionInstance.Initialize(newAction);

                _handedActions.Add(newActionInstance);

                newActionInstance.ActionPerformed += OnActionPerformed;
            }
        }

        private void OnActionPerformed(DefaultAction action)
        {
            if (action is MoveAction moveAction)
            {
                _characterMover.MoveByAction(moveAction);
            }
        }
    }
}