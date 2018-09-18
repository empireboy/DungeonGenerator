using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonGenerator
{
	[RequireComponent(typeof(GridLayoutGroup))]
	[RequireComponent(typeof(RectTransform))]
	public abstract class DungeonGeneratorView2D<T> : MonoBehaviour where T : Component
	{
		[SerializeField] private T _tile;

		private Dictionary<string, Sprite> _tileSprites = new Dictionary<string, Sprite>();

		protected T[,] _grid;

		private void Start()
		{
			Init(transform.root.GetComponentInChildren<DungeonGeneratorManager>().DungeonPlayground);
		}

		public virtual void Init(DungeonPlayground dungeonPlayground)
		{
			GetComponent<GridLayoutGroup>().constraintCount = dungeonPlayground.Width;

			_grid = new T[dungeonPlayground.Width, dungeonPlayground.Height];

			for (int i = 0; i < dungeonPlayground.Width; i++)
			{
				for (int j = 0; j < dungeonPlayground.Height; j++)
				{
					_grid[i, j] = Instantiate(_tile, transform).GetComponent<T>();
				}
			}
		}

		public void AddTileSprite(string name, Sprite tileSprite)
		{
			_tileSprites.Add(name, tileSprite);
		}
	}
}