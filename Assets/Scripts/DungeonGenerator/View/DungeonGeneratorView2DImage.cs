using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonGenerator
{
	public class DungeonGeneratorView2DImage : DungeonGeneratorView2D<Image>
	{
		public override void Init(DungeonPlayground dungeonPlayground)
		{
			base.Init(dungeonPlayground);

			for (int i = 0; i < _grid.GetLength(0); i++)
			{
				for (int j = 0; j < _grid.GetLength(1); j++)
				{
					Image currentTileImage = _grid[i, j];
					if (dungeonPlayground.Grid[i, j] == DungeonPlayground.TileTypes.Floor)
					{
						currentTileImage.color = Color.gray;
					}
					else if (dungeonPlayground.Grid[i, j] == DungeonPlayground.TileTypes.Wall)
					{
						currentTileImage.color = Color.black;
					}
				}
			}
		}
	}
}