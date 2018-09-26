using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;
using GridExtension;

namespace DungeonGenerator
{
	public abstract class DungeonPlayground : ScriptableObject
	{
		public delegate void GenerationHandler(DungeonPlayground dungeonPlayground);
		public event GenerationHandler OnGenerationFinished;

		[Header("Dungeon Settings")]
		[Tooltip("Dungeon horizontal amount of grid tiles")]
		[SerializeField] protected int _width = 10;
		public int Width
		{
			get
			{
				return _width;
			}
		}
		[Tooltip("Dungeon vertical amount of grid tiles")]
		[SerializeField] protected int _height = 10;
		public int Height
		{
			get
			{
				return _height;
			}
		}
		[Tooltip("Used to initialize a pseudorandom Dungeon Generator")]
		[SerializeField] protected int _seed = 0;
		public int Seed
		{
			get
			{
				return _seed;
			}
		}

		[SerializeField] protected bool debug;
		[ShowIf("debug")]
		[SerializeField] protected Vector2 debugTilePosition;

		protected TileTypes[,] _grid = new TileTypes[0, 0];
		public TileTypes[,] Grid
		{
			get
			{
				return _grid;
			}
		}

		public abstract void Generate();
		protected virtual void GenerateFinished()
		{
			if (OnGenerationFinished != null)
				OnGenerationFinished(this);
		}

		public void SetWidth(int width)
		{
			_width = width;
		}
		public void SetHeight(int height)
		{
			_height = height;
		}
		public void SetSeed(int seed)
		{
			_seed = seed;
		}

		protected TileTypes[,] Connectivity(TileTypes[,] grid, int connectivitySize)
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

			// Loop trough every tile
			for (int tileX = 0; tileX < _width; tileX++)
			{
				for (int tileY = 0; tileY < _height; tileY++)
				{
					if (grid[tileX, tileY] == TileTypes.Wall)
						continue;

					GridNeighbours<TileTypes> gridNeighbours = new GridNeighbours<TileTypes>(new Vector2(tileX, tileY), grid);

					List<TileTypes> currentNeighbours = gridNeighbours.GetNeighbours8(1);

					if (tileX == debugTilePosition.x && tileY == debugTilePosition.y && debug)
					{
						Debug.Log("Looping trough neighbours");
					} // Debug

					// Loop trough every neighbour of a single tile
					for (int neighbourIndex = 0; neighbourIndex < currentNeighbours.Count; neighbourIndex++)
					{

						if (tileX == debugTilePosition.x && tileY == debugTilePosition.y && debug)
						{
							for (int debugMessage0 = 0; debugMessage0 < currentNeighbours.Count; debugMessage0++)
							{
								Debug.Log("Neighbour with index " + debugMessage0 + ": " + currentNeighbours[debugMessage0]);
							}
						} // Debug

						// If this tile is an edge tile
						if (currentNeighbours[neighbourIndex] == TileTypes.Wall)
						{
							List<TileTypes> currentNeighboursInLine = gridNeighbours.GetNeighboursInLine(neighbourIndex, connectivitySize);

							if (tileX == debugTilePosition.x && tileY == debugTilePosition.y && debug)
							{
								Debug.Log("neighbourIndex: " + neighbourIndex);
								Debug.Log("currentNeighbours.Count: " + currentNeighbours.Count);
								for (int debugMessage1 = 0; debugMessage1 < currentNeighboursInLine.Count; debugMessage1++)
								{
									Debug.Log(currentNeighboursInLine[debugMessage1]);
								}
							} // Debug

							// Loop trough every neighbour in a single line of a single tile
							for (int neighbourInLineIndex = 0; neighbourInLineIndex < currentNeighboursInLine.Count; neighbourInLineIndex++)
							{
								// If there is a connectivity found at the other side of a wall
								// TODO: check if the tile is within the same grid room
								if (currentNeighboursInLine[neighbourInLineIndex] == TileTypes.Floor)
								{

									if (tileX == debugTilePosition.x && tileY == debugTilePosition.y && debug)
									{
										Debug.Log("Connectivity found at index: " + neighbourInLineIndex);
									} // Debug

									// Loop trough every tile between two connected grid tiles
									for (int connectivityInLineIndex = 0; connectivityInLineIndex < neighbourInLineIndex; connectivityInLineIndex++)
									{
										Vector2 connectivityNeighbourTile = new Vector2(tileX + (int)gridNeighbours.GetNeighboursPosition(neighbourIndex, connectivityInLineIndex + 1).x, tileY + (int)gridNeighbours.GetNeighboursPosition(neighbourIndex, connectivityInLineIndex + 1).y);

										if (tileX == debugTilePosition.x && tileY == debugTilePosition.y && debug)
										{
											Debug.Log("connectivityNeighbourTile: " + connectivityNeighbourTile);
										} // Debug

										if (GridExt.IsInRange(grid, connectivityNeighbourTile))
											tempGrid[(int)connectivityNeighbourTile.x, (int)connectivityNeighbourTile.y] = TileTypes.Floor;

										GridNeighbours<TileTypes> connectivityGridNeighbours = new GridNeighbours<TileTypes>(connectivityNeighbourTile, grid);

										List<TileTypes> currentConnectivityNeighbours = connectivityGridNeighbours.GetNeighbours8(1);

										if (tileX == debugTilePosition.x && tileY == debugTilePosition.y && debug)
										{
											Debug.Log("connectivityTilePosition in GridNeighbours: " + new Vector2(connectivityGridNeighbours.TilePosition.x, connectivityGridNeighbours.TilePosition.y));
											for (int debugMessage2 = 0; debugMessage2 < currentConnectivityNeighbours.Count; debugMessage2++)
											{
												Debug.Log("currentConnectivityNeighbour at index " + debugMessage2 + ": " + currentConnectivityNeighbours[debugMessage2]);
											}
										} // Debug

										for (int connectivityNeighbourIndex = 0; connectivityNeighbourIndex < currentConnectivityNeighbours.Count; connectivityNeighbourIndex++)
										{
											Vector2 connectivityNeighbourPosition = new Vector2(connectivityGridNeighbours.TilePosition.x + (int)connectivityGridNeighbours.GetNeighboursPosition(connectivityNeighbourIndex).x, connectivityGridNeighbours.TilePosition.y + (int)connectivityGridNeighbours.GetNeighboursPosition(connectivityNeighbourIndex).y);

											if (tileX == debugTilePosition.x && tileY == debugTilePosition.y && debug)
											{
												Debug.Log("connectivityNeighbourPosition: " + connectivityNeighbourPosition);
											} // Debug

											if (GridExt.IsInRange(grid, connectivityNeighbourPosition))
												tempGrid[(int)connectivityNeighbourPosition.x, (int)connectivityNeighbourPosition.y] = TileTypes.Floor;
										}
									}

									break;
								}
							}
						}
					}
				}
			}

			return tempGrid;
		}

		public virtual void DebugGrid()
		{
			for (int i = 0; i < _width; i++)
			{
				string message = "";
				for (int j = 0; j < _height; j++)
				{
					message += _grid[i, j];
					message += " ";
				}
				Debug.Log(message);
			}
		}
	}
}