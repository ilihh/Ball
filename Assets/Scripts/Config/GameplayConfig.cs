namespace BallGame
{
	using UnityEngine;

	[CreateAssetMenu]
	public class GameplayConfig : ScriptableObject
	{
		[SerializeField]
		public float Speed = -0.2f;

		[SerializeField]
		public Vector3 GravityDown = new Vector3(0f, -9.81f, 0f);

		[SerializeField]
		public Vector3 GravityUp = new Vector3(0f, 9.81f, 0f);

		[SerializeField]
		public BallConfig BallConfig;

		[SerializeField]
		public PlatformsConfig PlatformsConfig;

		[SerializeField]
		public WallsConfig WallsConfig;

		[SerializeField]
		public PlacementConfig PlatformsSettings;
	}
}