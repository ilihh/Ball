namespace BallGame
{
	using UnityEngine;
	using UnityEngine.UI;

	abstract public class UIBase : MonoBehaviour
	{
		[SerializeField]
		protected GameplayState VisibleState;

		[SerializeField]
		Button buttonQuit;

		protected GameplaySystem GameplaySystem
		{
			get;
			private set;
		}

		public virtual void Init(GameplaySystem gameplay)
		{
			GameplaySystem = gameplay;

			GameplaySystem.OnStateChanged += ProcessState;

			if (buttonQuit != null)
			{
				buttonQuit.onClick.AddListener(GameQuit);
			}

			ProcessState(GameplaySystem.State);
		}

		protected virtual void OnDestroy()
		{
			if (GameplaySystem != null)
			{
				GameplaySystem.OnStateChanged -= ProcessState;
			}

			if (buttonQuit != null)
			{
				buttonQuit.onClick.RemoveListener(GameQuit);
			}
		}

		protected virtual void ProcessState(GameplayState state)
		{
			gameObject.SetActive(state == VisibleState);
		}

		void GameQuit()
		{
			Application.Quit();
		}
	}
}