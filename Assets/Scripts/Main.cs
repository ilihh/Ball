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
			UnityEngine.InputSystem.EnhancedTouch.EnhancedTouchSupport.Enable();

			GameplaySystem = new GameplaySystem(gameplayConfig);

			foreach (var x in screens)
			{
				x.Init(GameplaySystem);
			}

			playerInput = GetComponent<PlayerInput>();
			playerInput.onActionTriggered += ProcessInputAction;
		}

		bool IsPointerOverGameObject()
		{
			if (EventSystem.current.IsPointerOverGameObject())
			{
				return true;
			}

			var n = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count;
			for (int i = 0; i < n; i++)
			{
				var touch = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[i];
				if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
				{
					if (EventSystem.current.IsPointerOverGameObject(touch.touchId))
					{
						return true;
					}
				}
			}

			return false;
		}
		void ProcessInputAction(InputAction.CallbackContext context)
		{
			if (!context.performed)
			{
				return;
			}

			switch (context.action.name)
			{
				case "ToggleGravity":
					if (IsPointerOverGameObject())
					{
						return;
					}

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