//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/Input/Test.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Test: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Test()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Test"",
    ""maps"": [
        {
            ""name"": ""Boat"",
            ""id"": ""be6c4bf5-cce8-4353-ac99-d5a013e6256a"",
            ""actions"": [
                {
                    ""name"": ""PaddleLeft"",
                    ""type"": ""Button"",
                    ""id"": ""ec7d4462-88fd-4f43-8928-11e27232ea53"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""PaddleRight"",
                    ""type"": ""Button"",
                    ""id"": ""90c3aca1-1a2b-4b05-a9a6-0ecf98f6952b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""StaticRotateLeft"",
                    ""type"": ""Button"",
                    ""id"": ""31fcf997-f244-4d71-9596-0801daf74d3f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""StaticRotateRight"",
                    ""type"": ""Button"",
                    ""id"": ""68f74b2f-e772-4377-9710-58135b74f550"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""AnyButton"",
                    ""type"": ""Button"",
                    ""id"": ""b3be2c60-e795-4356-be41-3ca008864ecb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3cfa1491-41a9-4ca7-a280-5a43d52d446e"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""PaddleLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ecc743e3-3393-43b9-8bb9-fc74b1d7df00"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""PaddleLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""84d99ad0-1484-4aab-991c-2d9fa37ffddc"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""PaddleRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""552cac4e-9249-4cbe-9e57-4af35a458181"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""PaddleRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""98e456ff-58ef-4d20-bd85-6f2f8c69cc97"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""StaticRotateLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9080299f-91ab-47cf-883d-fb860410a663"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""StaticRotateLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""62a3c127-3f1f-4b5a-9e99-e4c86b6c0755"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""StaticRotateRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5ef8087e-4505-4945-aeb6-aff821b2bc0b"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""StaticRotateRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9dee4e71-f509-4925-9891-c1e3d4c1481b"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cb925449-d034-4472-aa46-2802020f0195"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d37dfde2-4e46-41a8-83d4-6ca8b1875790"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b24a0c3b-1182-464e-91ba-a316c3e19f8d"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""59e80cec-44e8-41b2-8d23-09968a57195c"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""708abcc1-67dd-401b-9053-aed3ee764be1"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""25d1c613-2589-4294-bd01-146957eb1740"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e7363c46-23eb-4604-bfbf-2301c4562d81"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""36697f02-c518-43f4-883c-14b4613c77f4"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""18926ef6-9253-4a58-a31b-f6b84a5d03aa"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""88c59786-78d1-4d05-af76-63f8bc9ae923"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2586fc0c-d44c-419c-b08a-38843a043393"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1abba791-f0b2-453c-9431-acecdd1f56a7"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d4f7afb1-765d-4cec-98e9-327ca8b0f522"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""29b1a08b-94a9-46bb-8b80-0234ae5a642c"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""60252cbd-46f5-4a66-9b10-0c9278b85146"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4b0a8787-d871-40fa-9099-be313bc3d54c"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ac37bca4-147e-4668-ad7d-8707d582c7bc"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e36b726e-a042-45b1-9fbe-ba86ec0c7005"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a8ae0d82-c867-4c13-9a03-f9b32d9dc4e1"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""AnyButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyboardMouse"",
            ""bindingGroup"": ""KeyboardMouse"",
            ""devices"": []
        },
        {
            ""name"": ""GamePad"",
            ""bindingGroup"": ""GamePad"",
            ""devices"": []
        }
    ]
}");
        // Boat
        m_Boat = asset.FindActionMap("Boat", throwIfNotFound: true);
        m_Boat_PaddleLeft = m_Boat.FindAction("PaddleLeft", throwIfNotFound: true);
        m_Boat_PaddleRight = m_Boat.FindAction("PaddleRight", throwIfNotFound: true);
        m_Boat_StaticRotateLeft = m_Boat.FindAction("StaticRotateLeft", throwIfNotFound: true);
        m_Boat_StaticRotateRight = m_Boat.FindAction("StaticRotateRight", throwIfNotFound: true);
        m_Boat_AnyButton = m_Boat.FindAction("AnyButton", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Boat
    private readonly InputActionMap m_Boat;
    private List<IBoatActions> m_BoatActionsCallbackInterfaces = new List<IBoatActions>();
    private readonly InputAction m_Boat_PaddleLeft;
    private readonly InputAction m_Boat_PaddleRight;
    private readonly InputAction m_Boat_StaticRotateLeft;
    private readonly InputAction m_Boat_StaticRotateRight;
    private readonly InputAction m_Boat_AnyButton;
    public struct BoatActions
    {
        private @Test m_Wrapper;
        public BoatActions(@Test wrapper) { m_Wrapper = wrapper; }
        public InputAction @PaddleLeft => m_Wrapper.m_Boat_PaddleLeft;
        public InputAction @PaddleRight => m_Wrapper.m_Boat_PaddleRight;
        public InputAction @StaticRotateLeft => m_Wrapper.m_Boat_StaticRotateLeft;
        public InputAction @StaticRotateRight => m_Wrapper.m_Boat_StaticRotateRight;
        public InputAction @AnyButton => m_Wrapper.m_Boat_AnyButton;
        public InputActionMap Get() { return m_Wrapper.m_Boat; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BoatActions set) { return set.Get(); }
        public void AddCallbacks(IBoatActions instance)
        {
            if (instance == null || m_Wrapper.m_BoatActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_BoatActionsCallbackInterfaces.Add(instance);
            @PaddleLeft.started += instance.OnPaddleLeft;
            @PaddleLeft.performed += instance.OnPaddleLeft;
            @PaddleLeft.canceled += instance.OnPaddleLeft;
            @PaddleRight.started += instance.OnPaddleRight;
            @PaddleRight.performed += instance.OnPaddleRight;
            @PaddleRight.canceled += instance.OnPaddleRight;
            @StaticRotateLeft.started += instance.OnStaticRotateLeft;
            @StaticRotateLeft.performed += instance.OnStaticRotateLeft;
            @StaticRotateLeft.canceled += instance.OnStaticRotateLeft;
            @StaticRotateRight.started += instance.OnStaticRotateRight;
            @StaticRotateRight.performed += instance.OnStaticRotateRight;
            @StaticRotateRight.canceled += instance.OnStaticRotateRight;
            @AnyButton.started += instance.OnAnyButton;
            @AnyButton.performed += instance.OnAnyButton;
            @AnyButton.canceled += instance.OnAnyButton;
        }

        private void UnregisterCallbacks(IBoatActions instance)
        {
            @PaddleLeft.started -= instance.OnPaddleLeft;
            @PaddleLeft.performed -= instance.OnPaddleLeft;
            @PaddleLeft.canceled -= instance.OnPaddleLeft;
            @PaddleRight.started -= instance.OnPaddleRight;
            @PaddleRight.performed -= instance.OnPaddleRight;
            @PaddleRight.canceled -= instance.OnPaddleRight;
            @StaticRotateLeft.started -= instance.OnStaticRotateLeft;
            @StaticRotateLeft.performed -= instance.OnStaticRotateLeft;
            @StaticRotateLeft.canceled -= instance.OnStaticRotateLeft;
            @StaticRotateRight.started -= instance.OnStaticRotateRight;
            @StaticRotateRight.performed -= instance.OnStaticRotateRight;
            @StaticRotateRight.canceled -= instance.OnStaticRotateRight;
            @AnyButton.started -= instance.OnAnyButton;
            @AnyButton.performed -= instance.OnAnyButton;
            @AnyButton.canceled -= instance.OnAnyButton;
        }

        public void RemoveCallbacks(IBoatActions instance)
        {
            if (m_Wrapper.m_BoatActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IBoatActions instance)
        {
            foreach (var item in m_Wrapper.m_BoatActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_BoatActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public BoatActions @Boat => new BoatActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("KeyboardMouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_GamePadSchemeIndex = -1;
    public InputControlScheme GamePadScheme
    {
        get
        {
            if (m_GamePadSchemeIndex == -1) m_GamePadSchemeIndex = asset.FindControlSchemeIndex("GamePad");
            return asset.controlSchemes[m_GamePadSchemeIndex];
        }
    }
    public interface IBoatActions
    {
        void OnPaddleLeft(InputAction.CallbackContext context);
        void OnPaddleRight(InputAction.CallbackContext context);
        void OnStaticRotateLeft(InputAction.CallbackContext context);
        void OnStaticRotateRight(InputAction.CallbackContext context);
        void OnAnyButton(InputAction.CallbackContext context);
    }
}