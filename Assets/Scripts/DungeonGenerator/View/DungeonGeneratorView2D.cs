using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GridExtension;

namespace DungeonGenerator
{
	[RequireComponent(typeof(GridLayoutGroup))]
	[RequireComponent(typeof(RectTransform))]
	public abstract class DungeonGeneratorView2D<T> : MonoBehaviour where T : GridTile
	{
		[SerializeField] private GameObject _tile;

		private Dictionary<string, Sprite> _tileSprites = new Dictionary<string, Sprite>();

		protected T[,] _grid;

		private void Awake()
		{
			transform.root.GetComponentInChildren<DungeonGeneratorManager>().DungeonPlayground.OnGenerationFinished += Init;
		}

		public virtual void Init(DungeonPlayground dungeonPlayground)
		{
			RemoveAll();

			GetComponent<GridLayoutGroup>().constraintCount = dungeonPlayground.Width;

			_grid = new T[dungeonPlayground.Width, dungeonPlayground.Height];

			for (int i = 0; i < dungeonPlayground.Width; i++)
			{
				for (int j = 0; j < dungeonPlayground.Height; j++)
				{
					GameObject newGridTile = Instantiate(_tile, transform);
					_grid[i, j] = newGridTile.GetComponent<T>();
				}
			}
		}

		public void AddTileSprite(string name, Sprite tileSprite)
		{
			_tileSprites.Add(name, tileSprite);
		}

		private void RemoveAll()
		{
			T[] children = GetComponentsInChildren<T>();

			if (children == null)
				return;

			for (int i = 0; i < children.Length; i++)
			{
				Destroy(children[i].gameObject);
			}
		}
	}
}