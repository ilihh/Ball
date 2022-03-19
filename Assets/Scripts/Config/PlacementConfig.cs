namespace BallGame
{
	using System;
	using UnityEngine;

	[Serializable]
	public class PlacementConfig
	{
		[SerializeField]
		public float StartPosition = -3f;

		[SerializeField]
		public float DisableAtPoint = -30f; // pos.x + scale.x

		[SerializeField]
		public float ActiveLength = 100f;

		[SerializeField]
		public float PlatformMinSizeFirst = 4f;

		[SerializeField]
		public float PlatformMinSize = 1f;

		[SerializeField]
		public float PlatformMaxSize = 10f;

		[SerializeField]
		public float PitMinSize = 2.5f;

		[SerializeField]
		public float PitMaxSize = 7f;

		public float GenerateFirstPlatform()
		{
			return UnityEngine.Random.Range(PlatformMinSizeFirst, PlatformMaxSize);
		}

		public float GeneratePlatform()
		{
			return UnityEngine.Random.Range(PlatformMinSize, PlatformMaxSize);
		}

		public float GeneratePit()
		{
			return UnityEngine.Random.Range(PitMinSize, PitMaxSize);
		}
	}
}