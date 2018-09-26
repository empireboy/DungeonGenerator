using UnityEngine;
using GridExtension;
using UnityEngine.UI;

namespace DungeonGenerator
{
	public class DungeonGeneratorView2DGridTile : DungeonGeneratorView2D<GridTile>
	{
		private void Start()
		{
			//Init(transform.root.GetComponentInChildren<DungeonGeneratorManager>().DungeonPlayground);
		}

		public override void Init(DungeonPlayground dungeonPlayground)
		{
			base.Init(dungeonPlayground);

			for (int i = 0; i < _grid.GetLength(0); i++)
			{
				for (int j = 0; j < _grid.GetLength(1); j++)
				{
					GridTile currentGridTile = _grid[i, j];
					currentGridTile.SetGridPosition(new Vector2(i, j));
					if (dungeonPlayground.Grid[i, j] == TileTypes.Floor)
					{
						currentGridTile.GetComponent<Image>().color = Color.gray;
					}
					else if (dungeonPlayground.Grid[i, j] == TileTypes.Wall)
					{
						currentGridTile.GetComponent<Image>().color = Color.black;
					}
				}
			}
		}
	}
}