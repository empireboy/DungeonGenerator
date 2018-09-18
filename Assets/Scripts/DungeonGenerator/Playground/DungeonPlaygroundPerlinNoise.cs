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
		}
	}
}