namespace BallGame
{
	using System;
	using UnityEngine;

	public class BallSystem : BaseSystem
	{
		public Action OnDeath;

		public Action OnScore;

		BallConfig config;

		GameObject ceiling;

		GameObject floor;

		Rigidbody ball;

		BallCollision ballCollision;

		float angularVelocity;

		public BallSystem(BallConfig config, float speed)
		{
			this.config = config;

			ball = UnityEngine.Object.Instantiate(config.Ball);
			ballCollision = ball.GetComponent<BallCollision>();
			ballCollision.OnDeath += ProcessDeath;
			ballCollision.OnScore += ProcessScore;

			ceiling = UnityEngine.Object.Instantiate(config.CeilingCollider);
			floor = UnityEngine.Object.Instantiate(config.FloorCollider);

			SetSpeed(speed);
			SetPause(true);
		}

		void ProcessDeath()
		{
			OnDeath?.Invoke();
		}

		void ProcessScore()
		{
			OnScore?.Invoke();
		}

		public override void Destroy()
		{
			if (ballCollision != null)
			{
				ballCollision.OnDeath -= ProcessDeath;
				ballCollision.OnScore -= ProcessScore;
			}

			if (ball != null)
			{
				UnityEngine.Object.Destroy(ball.gameObject);
			}

			if (ceiling != null)
			{
				UnityEngine.Object.Destroy(ceiling);
			}

			if (floor != null)
			{
				UnityEngine.Object.Destroy(floor);
			}
		}

		public void SetSpeed(float speed)
		{
			var circumference = config.Size * ball.transform.localScale.x * Mathf.PI;
			var rotation_time = circumference / speed;
			angularVelocity = 360f / rotation_time;
		}

		public override void SetPause(bool pause)
		{
			base.SetPause(pause);
			ball.isKinematic = pause;
		}

		/// <summary>
		/// Position.y to height in range -1 (floor) .. 1 (ceiling)
		/// </summary>
		/// <param name="y">Y axis.</param>
		/// <returns>Height.</returns>
		float RelativeHeight(float y)
		{
			var range = config.MaxY - config.MinY;
			var rate01 = (y - config.MinY) / range; // 0..1
			return Mathf.Clamp((rate01 * 2f) - 1f, -1f, 1f);
		}

		public override void FixedUpdate(float deltaTime)
		{
			var rate = -RelativeHeight(ball.position.y); 
			var r = new Vector3(0f, 0f, ball.rotation.eulerAngles.z + angularVelocity * deltaTime * rate);
			if (r.z > 360f)
			{
				r.z -= 360f;
			}
			if (r.z <= -360f)
			{
				r.z += 360f;
			}

			ball.MoveRotation(Quaternion.Euler(r));
		}
		
		public override void Reset()
		{
			ball.MovePosition(config.Ball.transform.position);
			ball.MoveRotation(config.Ball.transform.rotation);
		}
	}
}