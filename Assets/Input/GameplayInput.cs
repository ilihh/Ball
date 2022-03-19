// GENERATED AUTOMATICALLY FROM 'Assets/_game/Input/GameplayInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @GameplayInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameplayInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameplayInput"",
    ""maps"": [
        {
            ""name"": ""Map"",
            ""id"": ""60ceabe3-ae75-48a1-bd21-571e9daadaf2"",
            ""actions"": [
                {
                    ""name"": ""ToggleGravity"",
                    ""type"": ""Button"",
                    ""id"": ""469fd029-a3bd-412d-9cae-99df97b886d3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""07953c25-a84e-4592-b88b-f2754c3787c6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""85db7416-53aa-4292-a321-931c9db18eee"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f0ffa02d-74ef-4289-9023-301286ec30a1"",
                    ""path"": ""<Touchscreen>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleGravity"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""52b68ea4-f073-4f88-bf41-fb79bc88da23"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleGravity"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b063ad41-9b3f-4a8c-a9f2-b515e7c5e8d8"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""65a70621-f4cc-47ed-9a10-90e6ade23eab"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Map
        m_Map = asset.FindActionMap("Map", throwIfNotFound: true);
        m_Map_ToggleGravity = m_Map.FindAction("ToggleGravity", throwIfNotFound: true);
        m_Map_Pause = m_Map.FindAction("Pause", throwIfNotFound: true);
        m_Map_Menu = m_Map.FindAction("Menu", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Map
    private readonly InputActionMap m_Map;
    private IMapActions m_MapActionsCallbackInterface;
    private readonly InputAction m_Map_ToggleGravity;
    private readonly InputAction m_Map_Pause;
    private readonly InputAction m_Map_Menu;
    public struct MapActions
    {
        private @GameplayInput m_Wrapper;
        public MapActions(@GameplayInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @ToggleGravity => m_Wrapper.m_Map_ToggleGravity;
        public InputAction @Pause => m_Wrapper.m_Map_Pause;
        public InputAction @Menu => m_Wrapper.m_Map_Menu;
        public InputActionMap Get() { return m_Wrapper.m_Map; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MapActions set) { return set.Get(); }
        public void SetCallbacks(IMapActions instance)
        {
            if (m_Wrapper.m_MapActionsCallbackInterface != null)
            {
                @ToggleGravity.started -= m_Wrapper.m_MapActionsCallbackInterface.OnToggleGravity;
                @ToggleGravity.performed -= m_Wrapper.m_MapActionsCallbackInterface.OnToggleGravity;
                @ToggleGravity.canceled -= m_Wrapper.m_MapActionsCallbackInterface.OnToggleGravity;
                @Pause.started -= m_Wrapper.m_MapActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_MapActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_MapActionsCallbackInterface.OnPause;
                @Menu.started -= m_Wrapper.m_MapActionsCallbackInterface.OnMenu;
                @Menu.performed -= m_Wrapper.m_MapActionsCallbackInterface.OnMenu;
                @Menu.canceled -= m_Wrapper.m_MapActionsCallbackInterface.OnMenu;
            }
            m_Wrapper.m_MapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ToggleGravity.started += instance.OnToggleGravity;
                @ToggleGravity.performed += instance.OnToggleGravity;
                @ToggleGravity.canceled += instance.OnToggleGravity;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Menu.started += instance.OnMenu;
                @Menu.performed += instance.OnMenu;
                @Menu.canceled += instance.OnMenu;
            }
        }
    }
    public MapActions @Map => new MapActions(this);
    public interface IMapActions
    {
        void OnToggleGravity(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnMenu(InputAction.CallbackContext context);
    }
}
