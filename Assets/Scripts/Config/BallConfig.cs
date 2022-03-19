namespace BallGame
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	[CreateAssetMenu]
	public class BallConfig : ScriptableObject
	{
		[SerializeField]
		public GameObject CeilingCollider;

		[SerializeField]
		public GameObject FloorCollider;

		[SerializeField]
		public Rigidbody Ball;

		[SerializeField]
		public float Size = 1f;
	}
}
