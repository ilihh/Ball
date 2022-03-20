namespace BallGame
{
	using UnityEngine;
	using UnityEngine.EventSystems;
	using UnityEngine.InputSystem;

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

		PlayerInput playerInput;

		protected void Start()
		{
			GameplaySystem = new GameplaySystem(gameplayConfig);

			foreach (var x in screens)
			{
				x.Init(GameplaySystem);
			}

			playerInput = GetComponent<PlayerInput>();
			playerInput.onActionTriggered += ProcessInputAction;
		}

		void ProcessInputAction(InputAction.CallbackContext context)
		{
			if (!context.performed)
			{
				return;
			}

			if (EventSystem.current.IsPointerOverGameObject())
			{
				return;
			}

			switch (context.action.name)
			{
				case "ToggleGravity":
					GameplaySystem.ToggleGravity();
					break;
				case "TogglePause":
					GameplaySystem.SetPause(!GameplaySystem.Pause);
					break;
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
	}
}