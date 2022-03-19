namespace BallGame
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using TMPro;

	public class UIGameplay : UIBase
	{
		[SerializeField]
		TextMeshProUGUI Score;

		[SerializeField]
		Button Pause;

		[SerializeField]
		TextMeshProUGUI PauseText;

		public override void Init(GameplaySystem gameplay)
		{
			base.Init(gameplay);

			GameplaySystem.OnScoreChanged += ProcessScore;
			Pause.onClick.AddListener(EnablePause);

			ProcessScore(GameplaySystem.Score);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			if (GameplaySystem != null)
			{
				GameplaySystem.OnScoreChanged -= ProcessScore;
			}

			Pause.onClick.RemoveListener(EnablePause);
		}

		void EnablePause()
		{
			GameplaySystem.SetPause(true);
		}

		void ProcessScore(int score)
		{
			Score.text = score.ToString();
		}
	}
}
