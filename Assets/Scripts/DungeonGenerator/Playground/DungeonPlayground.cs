using UnityEngine;

namespace DungeonGenerator
{
	public abstract class DungeonPlayground : ScriptableObject
	{
		public enum TileTypes
		{
			Empty,
			Wall,
			Floor
		}

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

		protected TileTypes[,] _grid = new TileTypes[0, 0];
		public TileTypes[,] Grid
		{
			get
			{
				return _grid;
			}
		}

		public abstract void Randomize();

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