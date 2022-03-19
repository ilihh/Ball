namespace BallGame
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class Main : MonoBehaviour
	{
		[SerializeField]
		UIBase[] screens;

		[SerializeField]
		GameplayConfig gameplayConfig;

		public GameplaySystem GameplaySystem
		{
			get;
			private set;
		}

		protected void Start()
		{
			GameplaySystem = new GameplaySystem(gameplayConfig);

			foreach (var x in screens)
			{
				x.Init(GameplaySystem);
			}
		}

		protected void OnDestroy()
		{
			if (GameplaySystem != null)
			{
				GameplaySystem.Destroy();
			}
		}

		protected void Update()
		{
			GameplaySystem.Update(Time.deltaTime);
		}

		protected void FixedUpdate()
		{
			GameplaySystem.FixedUpdate(Time.deltaTime);
		}

		public void ToggleGravity()
		{
			GameplaySystem.ToggleGravity();
		}

		public void TogglePause()
		{
			GameplaySystem.SetPause(!GameplaySystem.Pause);
		}
	}
}