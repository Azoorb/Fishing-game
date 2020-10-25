// GENERATED AUTOMATICALLY FROM 'Assets/PlayerController.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerController : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerController"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""85950e0e-4dc9-45b8-a80c-a9963bf063b4"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""513a7a5d-7cd1-458a-a2e6-3ba3019ddbac"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LaunchDecoy"",
                    ""type"": ""Button"",
                    ""id"": ""b88c53c6-a8f4-4fc0-bf1d-9148035325eb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RideUpDecoy"",
                    ""type"": ""Button"",
                    ""id"": ""100e770c-5052-46a1-97fc-fb530bb63356"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""394f3bd0-d710-4f8e-9b6b-072d180d0f8a"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""b2a4824f-2e28-41f7-9f8e-e800e65dad72"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""bec83b38-6bcf-4727-9e6b-78e55ac49d86"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""5021ec00-dd31-45e9-a8c0-fb404f603769"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LaunchDecoy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5df38e6b-d75d-4960-96e9-532fe41cbd37"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RideUpDecoy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_LaunchDecoy = m_Player.FindAction("LaunchDecoy", throwIfNotFound: true);
        m_Player_RideUpDecoy = m_Player.FindAction("RideUpDecoy", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_LaunchDecoy;
    private readonly InputAction m_Player_RideUpDecoy;
    public struct PlayerActions
    {
        private @PlayerController m_Wrapper;
        public PlayerActions(@PlayerController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @LaunchDecoy => m_Wrapper.m_Player_LaunchDecoy;
        public InputAction @RideUpDecoy => m_Wrapper.m_Player_RideUpDecoy;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @LaunchDecoy.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLaunchDecoy;
                @LaunchDecoy.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLaunchDecoy;
                @LaunchDecoy.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLaunchDecoy;
                @RideUpDecoy.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRideUpDecoy;
                @RideUpDecoy.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRideUpDecoy;
                @RideUpDecoy.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRideUpDecoy;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @LaunchDecoy.started += instance.OnLaunchDecoy;
                @LaunchDecoy.performed += instance.OnLaunchDecoy;
                @LaunchDecoy.canceled += instance.OnLaunchDecoy;
                @RideUpDecoy.started += instance.OnRideUpDecoy;
                @RideUpDecoy.performed += instance.OnRideUpDecoy;
                @RideUpDecoy.canceled += instance.OnRideUpDecoy;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLaunchDecoy(InputAction.CallbackContext context);
        void OnRideUpDecoy(InputAction.CallbackContext context);
    }
}
