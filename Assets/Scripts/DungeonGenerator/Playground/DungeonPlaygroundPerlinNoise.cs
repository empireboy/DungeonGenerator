using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using GridExtension;

namespace DungeonGenerator
{
	[CreateAssetMenu(menuName = "DungeonGenerator/New DungeonPlaygroundPerlinNoise", fileName = "DungeonPlaygroundPerlinNoise.asset")]
	public class DungeonPlaygroundPerlinNoise : DungeonPlayground
	{
		[Tooltip("An algorithm to connect grid rooms")]
		[SerializeField] private bool _useConnectivityAlgorithm;
		[Tooltip("Used to connect grid rooms")]
		[ShowIf("_useConnectivityAlgorithm")]
		[SerializeField] private int _connectivitySize = 0;
		public int ConnectivitySize
		{
			get
			{
				return _connectivitySize;
			}
		}

		public override void Generate()
		{
			TileTypes[,] grid = new TileTypes[_width, _height];

			for (int i = 0; i < _width; i++)
			{
				for (int j = 0; j < _height; j++)
				{
					float temp = (Mathf.PerlinNoise(i / 10f + _seed, j / 10f + _seed));
					grid[i, j] = (temp < .5f) ? TileTypes.Wall : TileTypes.Floor;
				}
			}

			if (_useConnectivityAlgorithm)
				grid = Connectivity(grid, _connectivitySize);

			for (int i = 0; i < _width; i++)
			{
				for (int j = 0; j < _height; j++)
				{
					if (i == 0 || j == 0 || i == _width - 1 || j == _height - 1)
						grid[i, j] = TileTypes.Wall;
				}
			}

			_grid = grid;

			GenerateFinished();
		}
	}
}