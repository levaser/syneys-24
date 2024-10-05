using System.Linq;
using Array2DEditor;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Levels
{
    [System.Serializable]
    public class CellPrefab
    {
        [field: SerializeField]
        public CellType Type { get; private set; }

        [field: SerializeField]
        public GameObject Prefab { get; private set; }
    }

    public sealed class LevelLoader : IStartable
    {
        private readonly LevelConfig _levelConfig;
        private readonly Transform _gridTransform;
        private readonly CellPrefab[] _cellPrefabs;
        private readonly LevelStats _levelStats;

        [Inject]
        public LevelLoader(
            LevelStarter levelStarter,
            Transform gridTransform,
            CellPrefab[] cellPrefabs,
            LevelStats levelStats
        )
        {
            if (levelStarter == null)
                throw new System.ArgumentNullException(nameof(LevelStarter), "Parameter 'levelStarter' cannot be null");

            _levelConfig = levelStarter.CurrentLevelConfig;
            _gridTransform = gridTransform;
            _cellPrefabs = cellPrefabs;
            _levelStats = levelStats;
        }

        void IStartable.Start()
        {
            BuildLevel();
        }

        private void BuildLevel()
        {
            for (int row = 0; row < _levelConfig.Grid.GridSize.y; row++)
            {
                for (int column = 0; column < _levelConfig.Grid.GridSize.x; column++)
                {
                    CellType cell = _levelConfig.Grid.GetCell(column, row);
                    GameObject prefab = _cellPrefabs[0].Prefab;
                    if (cell == CellType.None)
                    {
                        prefab = _cellPrefabs.First((x) => x.Type == CellType.None).Prefab;
                    }
                    else if (cell == CellType.Wall)
                    {

                    }
                    else if (cell == CellType.Enemy)
                    {

                    }

                    Object.Instantiate(
                        prefab,
                        new Vector3(column, 0, row) + _gridTransform.localPosition,
                        Quaternion.Euler(0, Random.Range(0, 359), 0),
                        _gridTransform
                    );

                    // if (_levelConfig.Grid.GetCell(column, row) == true)
                    // {
                    //     GameObject enemy = Object.Instantiate(
                    //         Random.Range(0, 10) == 0 ? _forcedEnemyPrefab : _defaultEnemyPrefab,
                    //         new Vector3(column, -row * 0.75f, 0) + _gridTransform.localPosition,
                    //         Quaternion.identity,
                    //         _gridTransform
                    //     );
                    //     enemy.GetComponent<SpriteRenderer>().sortingOrder = row;
                    //     _levelStats.EnemiesNumber++;
                    // }
                }
            }
        }
    }
}