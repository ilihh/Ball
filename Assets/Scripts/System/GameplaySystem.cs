namespace BallGame
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class GameplaySystem : BaseSystem
	{
		GameplayConfig config;
		
		BallSystem ballSystem;

		PlatformsSystem platformsSystem;

		WallsSystem wallsSystem;

		List<BaseSystem> activeSystems = new List<BaseSystem>();

		bool gravityDown = true;

		public Action<int> OnScoreChanged;

		int score = 0;

		public int Score
		{
			get => score;
			set
			{
				if (score != value)
				{
					score = value;
					OnScoreChanged?.Invoke(score);
				}
			}
		}


		public Action<GameplayState> OnStateChanged;

		GameplayState state = GameplayState.Start;

		public GameplayState State
		{
			get => state;

			private set
			{
				if (state != value)
				{
					state = value;
					OnStateChanged?.Invoke(state);
				}
			}
		}

		public GameplaySystem(GameplayConfig config)
		{
			this.config = config;

			ballSystem = new BallSystem(config.BallConfig, config.Speed);
			ballSystem.OnDeath += ProcessGameOver;
			ballSystem.OnScore += ProcessScore;
			activeSystems.Add(ballSystem);

			platformsSystem = new PlatformsSystem(config.PlatformsConfig, config.PlatformsSettings, config.Speed);
			activeSystems.Add(platformsSystem);

			wallsSystem = new WallsSystem(config.WallsConfig, config.Speed);
			activeSystems.Add(wallsSystem);
		}

		public override void Destroy()
		{
			ballSystem.OnDeath -= ProcessGameOver;

			foreach (var s in activeSystems)
			{
				s.Destroy();
			}
		}

		public override void Start()
		{
			base.Start();

			Score = 0;

			foreach (var s in activeSystems)
			{
				s.Start();
			}

			State = GameplayState.Playing;
		}

		void ProcessScore()
		{
			Score += 1;
		}

		void ProcessGameOver()
		{
			SetPause(true);
			Reset();

			State = GameplayState.GameOver;
		}

		public override void SetPause(bool pause)
		{
			base.SetPause(pause);

			foreach (var s in activeSystems)
			{
				s.SetPause(pause);
			}

			State = pause ? GameplayState.Pause : GameplayState.Playing;
		}

		public override void Update(float deltaTime)
		{
			if (Pause)
			{
				return;
			}

			foreach (var s in activeSystems)
			{
				s.Update(deltaTime);
			}
		}

		public override void FixedUpdate(float deltaTime)
		{
			if (Pause)
			{
				return;
			}

			foreach (var s in activeSystems)
			{
				s.FixedUpdate(deltaTime);
			}
		}

		public void ToggleGravity()
		{
			if (Pause)
			{
				return;
			}

			gravityDown = !gravityDown;
			Physics.gravity = gravityDown ? config.GravityDown : config.GravityUp;
		}

		public override void Reset()
		{
			foreach (var s in activeSystems)
			{
				s.Reset();
			}

			State = GameplayState.Start;
		}
	}
}
