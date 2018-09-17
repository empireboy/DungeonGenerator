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
		[Tooltip("Used to initialize a pseudorandom Dungeon Generator")]
		[SerializeField] private int _seed = 0;
		public int Seed
		{
			get
			{
				return _seed;
			}
		}

		public GameObject test;

		public int[,] grid = new int[0, 0];

		private void Start()
		{
			Randomize();

			//DebugGrid();
		}

		public void Randomize()
		{
			grid = new int[_width, _height];

			for (int i = 0; i < _width; i++)
			{
				for (int j = 0; j < _height; j++)
				{
					grid[i, j] = (int)(Mathf.PerlinNoise(i / 10f + _seed, j / 10f + _seed) * 10f);
					GameObject test2 = Instantiate(test);
					test2.transform.position = new Vector3(transform.position.x + i * test.transform.localScale.x, transform.position.y + j * test.transform.localScale.y, transform.position.z);
					Color tmp = test2.GetComponent<SpriteRenderer>().color;
					tmp.a = grid[i, j]/10f;
					test2.GetComponent<SpriteRenderer>().color = tmp;
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