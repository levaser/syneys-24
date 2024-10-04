using System.Collections.Generic;
using Game.Levels;
using VContainer;
using System.Linq;
using UnityEngine;

namespace Game.Character
{
    public class CharacterDeck
    {
        [field: SerializeField]
        public Stack<DefaultAction> Actions { get; private set; }

        [Inject]
        public CharacterDeck
        (
            LevelConfig levelConfig
        )
        {
            List<DefaultAction> actionsList = levelConfig.ActionDeck.Actions.ToList();
            actionsList.Shuffle();

            Actions = new Stack<DefaultAction>(actionsList);
        }
    }
}