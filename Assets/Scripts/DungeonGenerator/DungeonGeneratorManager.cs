using UnityEngine;
using GridExtension;

namespace DungeonGenerator
{
	public class DungeonGeneratorManager : MonoBehaviour
	{
		[Tooltip("Current DungeonPlayground")]
		[SerializeField] private DungeonPlayground _dungeonPlayground;
		public DungeonPlayground DungeonPlayground
		{
			get
			{
				return _dungeonPlayground;
			}
		}

		private void Awake()
		{
			if (_dungeonPlayground != null)
				_dungeonPlayground.Generate();
		}

		private void Start()
		{
			//Pathfinding<DungeonTile> pathfinding = new Pathfinding<DungeonTile>(_dungeonPlayground.Grid, new Vector2(2, 13), new Vector2(25, 8));
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
				Application.Quit();
		}
	}

	public enum TileTypes
	{
		Empty,
		Wall,
		Floor
	}

}