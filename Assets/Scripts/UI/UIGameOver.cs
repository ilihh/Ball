namespace BallGame
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using TMPro;

	public class UIGameOver : UIBase
	{
		[SerializeField]
		TextMeshProUGUI Score;

		[SerializeField]
		Button buttonRestart;

		public override void Init(GameplaySystem gameplay)
		{
			base.Init(gameplay);

			buttonRestart.onClick.AddListener(Restart);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			buttonRestart.onClick.RemoveListener(Restart);
		}

		protected override void ProcessState(GameplayState state)
		{
			base.ProcessState(state);

			if (state == VisibleState)
			{
				Score.text = string.Format("Score: {0}", GameplaySystem.Score);
			}
		}

		void Restart()
		{
			GameplaySystem.Reset();
			GameplaySystem.Start();
		}
	}
}