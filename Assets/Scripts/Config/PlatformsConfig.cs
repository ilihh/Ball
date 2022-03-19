namespace BallGame
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
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
