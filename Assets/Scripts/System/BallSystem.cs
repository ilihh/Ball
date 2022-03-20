namespace BallGame
{
	using System;
	using UnityEngine;

	public class BallSystem : BaseSystem
	{
		struct AnimatorNames
		{
			public readonly int Ceiling;

			public readonly int Air;

			public readonly int Floor;

			public readonly int SpeedRate;

			AnimatorNames(int ceiling, int air, int floor, int speedRate)
			{
				Ceiling = ceiling;
				Air = air;
				Floor = floor;

				SpeedRate = speedRate;
			}

			public static AnimatorNames Create()
			{
				return new AnimatorNames(
					Animator.StringToHash("Ceiling"),
					Animator.StringToHash("Air"),
					Animator.StringToHash("Floor"),
					Animator.StringToHash("SpeedRate")
				);
			}
		}

		public Action OnDeath;

		public Action OnScore;

		BallConfig config;

		GameObject ceiling;

		GameObject floor;

		Rigidbody rigidbody;

		BallCollision collision;

		Animator animator;

		AnimatorNames animatorNames;

		int triggerId;

		protected int TriggerId
		{
			get => triggerId;

			set
			{
				if (triggerId != value)
				{
					animator.ResetTrigger(triggerId);
					animator.SetTrigger(value);

					triggerId = value;
				}
			}
		}

		public BallSystem(BallConfig config, float speed)
		{
			animatorNames = AnimatorNames.Create();

			this.config = config;

			rigidbody = UnityEngine.Object.Instantiate(config.Ball);
			collision = rigidbody.GetComponent<BallCollision>();
			collision.OnDeath += ProcessDeath;
			collision.OnScore += ProcessScore;
			collision.OnStateChanged += ProcessState;
			animator = rigidbody.GetComponent<Animator>();

			ceiling = UnityEngine.Object.Instantiate(config.CeilingCollider);
			floor = UnityEngine.Object.Instantiate(config.FloorCollider);

			triggerId = animatorNames.Floor;
			animator.SetTrigger(triggerId);

			SetSpeed(speed);
			SetPause(true);
		}

		void ProcessState(BallState state)
		{
			switch (state)
			{
				case BallState.Ceiling:
					TriggerId = animatorNames.Ceiling;
					break;
				case BallState.Air:
					TriggerId = animatorNames.Air;
					break;
				case BallState.Floor:
					TriggerId = animatorNames.Floor;
					break;
			}
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
			if (collision != null)
			{
				collision.OnDeath -= ProcessDeath;
				collision.OnScore -= ProcessScore;
			}

			if (rigidbody != null)
			{
				UnityEngine.Object.Destroy(rigidbody.gameObject);
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
			var circumference = config.Size * rigidbody.transform.localScale.x * Mathf.PI;
			var rotation_time = circumference / speed;

			animator.SetFloat(animatorNames.SpeedRate, 1f / rotation_time);
		}

		public override void SetPause(bool pause)
		{
			base.SetPause(pause);
			rigidbody.isKinematic = pause;
			animator.enabled = !pause;
		}
		
		public override void Reset()
		{
			rigidbody.MovePosition(config.Ball.transform.position);
			rigidbody.MoveRotation(config.Ball.transform.rotation);
			ProcessState(BallState.Floor);
		}
	}
}