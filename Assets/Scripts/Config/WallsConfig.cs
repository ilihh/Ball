namespace BallGame
{
	using UnityEngine;

	[CreateAssetMenu]
	public class WallsConfig : ScriptableObject
	{
		[SerializeField]
		public Transform Wall;

		[SerializeField]
		public float Count;

		[SerializeField]
		public float WallWidth;
	}
}