using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonGenerator
{
	//[CreateAssetMenu(menuName = "DungeonGenerator/New DungeonPlayground", fileName = "DungeonPlayground.asset")]
	public class DungeonPlayground : MonoBehaviour
	{
		[Header("Dungeon Settings")]
		[Tooltip("Dungeon horizontal amount of grid tiles")]
		[SerializeField] private int _width = 10;
		public int Width
		{
			get
			{
				return _width;
			}
		}
		[Tooltip("Dungeon vertical amount of grid tiles")]
		[SerializeField] private int _height = 10;
		public int Height
		{
			get
			{
				return _height;
			}
		}

		public int[,] grid = new int[0, 0];

		private void Start()
		{
			Randomize();

			DebugGrid();
		}

		public void Randomize()
		{
			grid = new int[_width, _height];

			for (int i = 0; i < _width; i++)
			{
				for (int j = 0; j < _height; j++)
				{
					grid[i, j] = Random.Range(0, 2);
				}
			}
		}

		public void DebugGrid()
		{
			for (int i = 0; i < _width; i++)
			{
				string message = "";
				for (int j = 0; j < _height; j++)
				{
					message += grid[i, j];
				}
				Debug.Log(message);
			}
		}
	}
}