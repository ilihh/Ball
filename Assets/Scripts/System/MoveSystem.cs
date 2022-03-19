namespace BallGame
{
	using UnityEngine;

	abstract public class MoveSystem : BaseSystem
	{
		protected float Speed;

		public void SetSpeed(float speed)
		{
			Speed = speed;
		}

		protected void Move(Transform t, float deltaTime)
		{
			var pos = t.position;
			pos.x += Speed * deltaTime;
			t.position = pos;
		}
	}
}