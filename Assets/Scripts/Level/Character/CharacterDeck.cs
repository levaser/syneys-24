using System.Collections.Generic;
using Game.Levels;
using VContainer;
using System.Linq;
using UnityEngine;
using VContainer.Unity;
using System;

namespace Game.Character
{
    public class CharacterDeck
    {
        public event Action<DefaultAction> ActionHanded;

        [field: SerializeField]
        public Stack<DefaultAction> ActionsDeck;

        [Inject]
        public CharacterDeck
        (
            LevelStarter levelStarter
        )
        {
            List<DefaultAction> actionsList = levelStarter.CurrentLevelConfig.ActionDeck.Actions.ToList();
            actionsList.Shuffle();

            ActionsDeck = new Stack<DefaultAction>(actionsList);
        }

        // void IStartable.Start()
        // {
        //     if (_handedActions.Count < _levelConfig.HandedCardsNumber)
        //     {
        //         for (int i = 0; i < _levelConfig.HandedCardsNumber - _handedActions.Count; i++)
        //         {
        //             DefaultAction newAction = ActionsDeck.Pop();
        //             _handedActions.Add(newAction);
        //             ActionHanded?.Invoke(newAction);
        //         }
        //     }
        // }
    }
}