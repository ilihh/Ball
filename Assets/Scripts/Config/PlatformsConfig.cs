namespace BallGame
{
	using UnityEngine;

	[CreateAssetMenu]
	public class PlatformsConfig : ScriptableObject
	{
		[SerializeField]
		public GameObject Ceiling;

		[SerializeField]
		public GameObject Floor;
	}
}