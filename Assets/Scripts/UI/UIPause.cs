namespace BallGame
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using TMPro;

	public class UIPause : UIBase
	{
		[SerializeField]
		Button Pause;

		public override void Init(GameplaySystem gameplay)
		{
			base.Init(gameplay);

			Pause.onClick.AddListener(DisablePause);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			Pause.onClick.RemoveListener(DisablePause);
		}

		void DisablePause()
		{
			GameplaySystem.SetPause(false);
		}
	}
}