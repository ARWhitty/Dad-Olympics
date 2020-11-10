// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Menu/PauseMenu.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PauseMenu : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PauseMenu()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PauseMenu"",
    ""maps"": [
        {
            ""name"": ""Menu"",
            ""id"": ""f6654966-0b5e-40f5-be63-29de9e10f9e8"",
            ""actions"": [
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""4541a51e-a657-460f-b07d-ea2007986da3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Return"",
                    ""type"": ""Button"",
                    ""id"": ""3c70abd5-c3f3-46f7-9ea6-246083f9832a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ScrollDown"",
                    ""type"": ""Button"",
                    ""id"": ""b6cac9a0-018c-4485-a4df-256226a52ac8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ScrollUp"",
                    ""type"": ""Button"",
                    ""id"": ""55c5bc84-5cbb-424f-bc24-24a764eb4662"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Go"",
                    ""type"": ""Button"",
                    ""id"": ""91337847-6b90-4713-96d1-77cdf1aa2606"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d9632991-24ad-4fcb-b2d2-ab20d4b52ca0"",
                    ""path"": ""<XInputController>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""56a53842-87bc-4338-b5dc-df5f02d183e7"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Return"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2eb6da55-8fa1-4bbe-bc8b-fe32c34be650"",
                    ""path"": ""<XInputController>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScrollDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""762a7405-d2a7-4363-8134-a70e55f35aa6"",
                    ""path"": ""<XInputController>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScrollUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ce60e699-cd90-4493-bbe4-e2c85b5c1f89"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Go"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Testing"",
            ""bindingGroup"": ""Testing"",
            ""devices"": []
        }
    ]
}");
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_Pause = m_Menu.FindAction("Pause", throwIfNotFound: true);
        m_Menu_Return = m_Menu.FindAction("Return", throwIfNotFound: true);
        m_Menu_ScrollDown = m_Menu.FindAction("ScrollDown", throwIfNotFound: true);
        m_Menu_ScrollUp = m_Menu.FindAction("ScrollUp", throwIfNotFound: true);
        m_Menu_Go = m_Menu.FindAction("Go", throwIfNotFound: true);
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

    // Menu
    private readonly InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private readonly InputAction m_Menu_Pause;
    private readonly InputAction m_Menu_Return;
    private readonly InputAction m_Menu_ScrollDown;
    private readonly InputAction m_Menu_ScrollUp;
    private readonly InputAction m_Menu_Go;
    public struct MenuActions
    {
        private @PauseMenu m_Wrapper;
        public MenuActions(@PauseMenu wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pause => m_Wrapper.m_Menu_Pause;
        public InputAction @Return => m_Wrapper.m_Menu_Return;
        public InputAction @ScrollDown => m_Wrapper.m_Menu_ScrollDown;
        public InputAction @ScrollUp => m_Wrapper.m_Menu_ScrollUp;
        public InputAction @Go => m_Wrapper.m_Menu_Go;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                @Pause.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnPause;
                @Return.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnReturn;
                @Return.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnReturn;
                @Return.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnReturn;
                @ScrollDown.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnScrollDown;
                @ScrollDown.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnScrollDown;
                @ScrollDown.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnScrollDown;
                @ScrollUp.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnScrollUp;
                @ScrollUp.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnScrollUp;
                @ScrollUp.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnScrollUp;
                @Go.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnGo;
                @Go.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnGo;
                @Go.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnGo;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Return.started += instance.OnReturn;
                @Return.performed += instance.OnReturn;
                @Return.canceled += instance.OnReturn;
                @ScrollDown.started += instance.OnScrollDown;
                @ScrollDown.performed += instance.OnScrollDown;
                @ScrollDown.canceled += instance.OnScrollDown;
                @ScrollUp.started += instance.OnScrollUp;
                @ScrollUp.performed += instance.OnScrollUp;
                @ScrollUp.canceled += instance.OnScrollUp;
                @Go.started += instance.OnGo;
                @Go.performed += instance.OnGo;
                @Go.canceled += instance.OnGo;
            }
        }
    }
    public MenuActions @Menu => new MenuActions(this);
    private int m_TestingSchemeIndex = -1;
    public InputControlScheme TestingScheme
    {
        get
        {
            if (m_TestingSchemeIndex == -1) m_TestingSchemeIndex = asset.FindControlSchemeIndex("Testing");
            return asset.controlSchemes[m_TestingSchemeIndex];
        }
    }
    public interface IMenuActions
    {
        void OnPause(InputAction.CallbackContext context);
        void OnReturn(InputAction.CallbackContext context);
        void OnScrollDown(InputAction.CallbackContext context);
        void OnScrollUp(InputAction.CallbackContext context);
        void OnGo(InputAction.CallbackContext context);
    }
}
