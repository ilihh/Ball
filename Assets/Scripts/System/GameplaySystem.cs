namespace BallGame
{
	using System;
	using System.Collections.Generic;
	using UnityEngine;

	public class GameplaySystem
	{
		GameplayConfig config;
		
		BallSystem ballSystem;

		PlatformsSystem platformsSystem;

		WallsSystem wallsSystem;

		List<BaseSystem> activeSystems = new List<BaseSystem>();

		bool gravityDown = true;

		public bool Pause
		{
			get => state == GameplayState.Pause;
		}

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

		public void Destroy()
		{
			ballSystem.OnDeath -= ProcessGameOver;

			foreach (var s in activeSystems)
			{
				s.Destroy();
			}
		}

		public void Start()
		{
			if (State != GameplayState.Start)
			{
				return;
			}

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

		public void SetPause(bool pause)
		{
			var can_pause = (state == GameplayState.Pause) || (state == GameplayState.Playing);
			if (!can_pause)
			{
				return;
			}

			foreach (var s in activeSystems)
			{
				s.SetPause(pause);
			}

			State = pause ? GameplayState.Pause : GameplayState.Playing;
		}

		public void Update(float deltaTime)
		{
			if (State != GameplayState.Playing)
			{
				return;
			}

			foreach (var s in activeSystems)
			{
				s.Update(deltaTime);
			}
		}

		public void FixedUpdate(float deltaTime)
		{
			if (State != GameplayState.Playing)
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
			if (State != GameplayState.Playing)
			{
				return;
			}

			gravityDown = !gravityDown;
			Physics.gravity = gravityDown ? config.GravityDown : config.GravityUp;
		}

		public void Reset()
		{
			foreach (var s in activeSystems)
			{
				s.Reset();
			}

			State = GameplayState.Start;
		}
	}
}