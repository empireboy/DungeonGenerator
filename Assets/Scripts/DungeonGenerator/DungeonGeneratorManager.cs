using UnityEngine;

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
				_dungeonPlayground.Randomize();
		}
	}
}