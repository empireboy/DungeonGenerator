using UnityEngine;

namespace DungeonGenerator
{
	[CreateAssetMenu(menuName = "DungeonGenerator/New DungeonPlaygroundPerlinNoise", fileName = "DungeonPlaygroundPerlinNoise.asset")]
	public class DungeonPlaygroundPerlinNoise : DungeonPlayground
	{
		public override void Randomize()
		{
			_grid = new TileTypes[_width, _height];

			for (int i = 0; i < _width; i++)
			{
				for (int j = 0; j < _height; j++)
				{
					float temp = (Mathf.PerlinNoise(i / 10f + _seed, j / 10f + _seed));
					_grid[i, j] = (temp < .5f) ? TileTypes.Wall : TileTypes.Floor;
				}
			}

			_grid = Connectivity(_grid);
		}

		private TileTypes[,] Connectivity(TileTypes[,] grid)
		{
			TileTypes[,] tempGrid = new TileTypes[grid.GetLength(0), grid.GetLength(1)];

			// Clone grid
			for (int i = 0; i < grid.GetLength(0); i++)
			{
				for (int j = 0; j < grid.GetLength(1); j++)
				{
					tempGrid[i, j] = grid[i, j];
				}
			}

			for (int i = 0; i < _width ; i++)
			{
				for (int j = 0; j < _height; j++)
				{
					if (grid[i, j] == TileTypes.Wall)
						continue;

					int[] neighboursHorizontal = new int[] { -1, -1, -1, 0, 0, 1, 1, 1 };
					int[] neighboursVertical = new int[] { -1, 0, 1, -1, 1, -1, 0, 1 };
					int neighboursLength = 6;

					for (int neighbourIndex = 0; neighbourIndex < neighboursHorizontal.Length; neighbourIndex++)
					{
						for (int neighbourRow = 1; neighbourRow < neighboursLength; neighbourRow++)
						{
							int currentNeighbourHorizontal = neighboursHorizontal[neighbourIndex];
							int currentNeighbourVertical = neighboursVertical[neighbourIndex];

							// Check if current tile to check is inside the grid
							if (
								i + (currentNeighbourHorizontal * neighbourRow) < _width &&
								i + (currentNeighbourHorizontal * neighbourRow) >= 0 &&
								j + (currentNeighbourVertical * neighbourRow) < _height &&
								j + (currentNeighbourVertical * neighbourRow) >= 0
							) {} else continue;

							TileTypes currentGridType = grid[i + (currentNeighbourHorizontal * neighbourRow), j + (currentNeighbourVertical * neighbourRow)];

							/*
							Debug.Log("i: " + i);
							Debug.Log("j: " + j);
							Debug.Log("currentNeighbourHorizontal: " + currentNeighbourHorizontal);
							Debug.Log("currentNeighbourVertical: " + currentNeighbourVertical);
							Debug.Log("neighbourRow: " + neighbourRow);
							Debug.Log("currentGridType: " + currentGridType.ToString());
							Debug.Log("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
							*/

							// If this tile is not an edge tile
							if (neighbourRow == 1)
							{
								if (currentGridType == TileTypes.Floor)
								{
									break;
								}
							}
							// If there is a connectivity found
							if (neighbourRow > 1 && currentGridType == TileTypes.Floor)
							{
								Debug.Log("Found a connectivity!");
								tempGrid[i, j] = TileTypes.Empty;
								break;
							}
						}
					}

				}
			}

			return tempGrid;
		}
	}
}