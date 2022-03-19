namespace BallGame
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using TMPro;
	
	public class UIStart : UIBase
	{
		[SerializeField]
		Button buttonStart;

		public override void Init(GameplaySystem gameplay)
		{
			base.Init(gameplay);

			buttonStart.onClick.AddListener(GameStart);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			buttonStart.onClick.RemoveListener(GameStart);
		}

		void GameStart()
		{
			GameplaySystem.Start();
		}
	}
}
