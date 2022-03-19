namespace BallGame
{
	using System;
	using UnityEngine;

	public class BallCollision : MonoBehaviour
	{
		[SerializeField]
		string deathTag = "Death";

		[SerializeField]
		string scoreTag = "Score";

		public Action OnDeath;

		public Action OnScore;

		protected void OnTriggerEnter(Collider collider)
		{
			if (collider.CompareTag(deathTag))
			{
				OnDeath?.Invoke();
			}

			if (collider.CompareTag(scoreTag))
			{
				OnScore?.Invoke();
			}
		}
	}
}