namespace BallGame
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class WallsSystem : MoveSystem
	{
		List<Transform> Walls = new List<Transform>();

		WallsConfig config;

		public WallsSystem(WallsConfig config, float speed)
		{
			this.config = config;
			Speed = speed;

			for (int i = 0; i < config.Count; i++)
			{
				Walls.Add(UnityEngine.Object.Instantiate(config.Wall).transform);
			}

			Reset();
		}

		public override void Update(float deltaTime)
		{
		}

		public override void FixedUpdate(float deltaTime)
		{
			if (Pause)
			{
				return;
			}

			foreach (var p in Walls)
			{
				Move(p, deltaTime);
			}

			
			if (NeedSwap())
			{
				foreach (var wall in Walls)
				{
					var width = config.WallWidth * wall.localScale.x;
					var position = wall.position;
					position.x += width;
					wall.position = position;
				}
			}
		}

		public bool NeedSwap()
		{
			var wall = Walls[0];
			var width = config.WallWidth * wall.localScale.x;
			return wall.position.x < (-width);
		}

		public override void Reset()
		{
			var base_pos = config.Wall.position;
			for (int i = 0; i < config.Count; i++)
			{
				var wall = Walls[i];
				var position = base_pos;
				position.x += (config.WallWidth * wall.localScale.x) * i;
				wall.position = position;
			}
		}

		public override void Destroy()
		{
			foreach (var w in Walls)
			{
				if (w != null)
				{
					UnityEngine.Object.Destroy(w.gameObject);
				}
			}

			Walls.Clear();
		}
	}
}