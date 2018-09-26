using System;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonGenerator
{
	[RequireComponent(typeof(RectTransform))]
	public class DungeonPlaygroundSetter : MonoBehaviour
	{
		public void SetWidth()
		{
			int width = Convert.ToInt32(GetComponent<InputField>().text);
			width = Mathf.Clamp(width, 1, 300);
			GetComponent<InputField>().text = width.ToString();
			transform.root.GetComponentInChildren<DungeonGeneratorManager>().DungeonPlayground.SetWidth(width);

			Regenerate();
		}

		public void SetHeight()
		{
			int height = Convert.ToInt32(GetComponent<InputField>().text);
			height = Mathf.Clamp(height, 1, 80);
			GetComponent<InputField>().text = height.ToString();
			transform.root.GetComponentInChildren<DungeonGeneratorManager>().DungeonPlayground.SetHeight(height);

			Regenerate();
		}

		public void SetSeed()
		{
			int seed = Convert.ToInt32(GetComponent<InputField>().text);
			seed = Mathf.Clamp(seed, 0, 1000);
			GetComponent<InputField>().text = seed.ToString();
			transform.root.GetComponentInChildren<DungeonGeneratorManager>().DungeonPlayground.SetSeed(seed);

			Regenerate();
		}

		private void Regenerate()
		{
			transform.root.GetComponentInChildren<DungeonGeneratorManager>().DungeonPlayground.Generate();
		}
	}
}