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

		[SerializeField]
		string ceilingTag = "Ceiling";

		[SerializeField]
		string floorTag = "Floor";

		public Action OnDeath;

		public Action OnScore;

		public Action<BallState> OnStateChanged;

		private void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.CompareTag(ceilingTag))
			{
				OnStateChanged?.Invoke(BallState.Ceiling);
			}

			if (collision.gameObject.CompareTag(floorTag))
			{
				OnStateChanged?.Invoke(BallState.Floor);
			}
		}

		private void OnCollisionExit(Collision collision)
		{
			if (collision.gameObject.CompareTag(ceilingTag))
			{
				OnStateChanged?.Invoke(BallState.Air);
			}

			if (collision.gameObject.CompareTag(floorTag))
			{
				OnStateChanged?.Invoke(BallState.Air);
			}
		}

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