namespace BallGame
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
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