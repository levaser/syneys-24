using System;
using Game.Character;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace Game.Levels
{
    public sealed class LevelInstaller : LifetimeScope
    {
        [SerializeField] private CellPrefab[] _cellPrefabs;

        [SerializeField] private Transform _levelGridTransform;
        [SerializeField] private Transform _characterTransform;
        [SerializeField] private Transform _handGridTransform;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<LevelInput>(Lifetime.Scoped).AsSelf();

            builder.Register<LevelStats>(Lifetime.Scoped);
            builder.RegisterEntryPoint<LevelLoader>(Lifetime.Scoped)
                .WithParameter(_levelGridTransform)
                .WithParameter(_cellPrefabs);

            ConfigureCharacter(builder);
        }

        private void ConfigureCharacter(IContainerBuilder builder)
        {
            builder.Register<CharacterMover>(Lifetime.Scoped)
                .WithParameter("levelGridTransform", _levelGridTransform)
                .WithParameter("characterTransform", _characterTransform);

            builder.Register<CharacterDeck>(Lifetime.Scoped);

            builder.RegisterEntryPoint<CharacterActionHandler>(Lifetime.Scoped)
                .WithParameter("handGridTransform", _handGridTransform);
        }
    }
}