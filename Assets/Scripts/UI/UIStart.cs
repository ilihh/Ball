namespace BallGame
{
	using UnityEngine;
	using UnityEngine.UI;
	
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