// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Player/InputActions/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""577e19f4-1cad-4992-8559-917bfc822bfb"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""d15a54a9-0072-4425-9ebb-df5631e916f2"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""bb51b8c6-a155-4501-ac80-57f5a61fa756"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""InvertVector2(invertX=false,invertY=false)"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""669039fb-7f45-483f-ad46-02d9ecabbebc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""PickUp"",
                    ""type"": ""Button"",
                    ""id"": ""dcac7897-f00d-461e-a360-15a7cb6fcb26"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Drop"",
                    ""type"": ""Button"",
                    ""id"": ""5698380d-0c60-425a-9596-afc3ea07cef9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Throw"",
                    ""type"": ""Button"",
                    ""id"": ""541deaaa-0c2c-4b57-800c-1b1c1da4c58e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""f395bcb0-c704-4e15-8608-62927a2e41cd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""89688925-423a-41fc-9686-e1ceeb25c964"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Button"",
                    ""id"": ""41563f99-dd78-4690-8117-084084c20561"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""11eed2f5-9d53-412f-9cf9-b98582a4ae8f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""NextWeapon"",
                    ""type"": ""Button"",
                    ""id"": ""c24744ba-48df-4d78-acf8-f6af8def0e9b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PreviousWeapon"",
                    ""type"": ""Button"",
                    ""id"": ""bb9cdd28-c617-4b57-9311-d7dccc571b26"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Heal"",
                    ""type"": ""Button"",
                    ""id"": ""147aee6a-35e2-4711-a978-8bd6b8d6f8bf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OpenMenu"",
                    ""type"": ""Button"",
                    ""id"": ""25b7bf32-7252-452c-9fd6-55230e18182f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OpenShop"",
                    ""type"": ""Button"",
                    ""id"": ""be3688b9-9917-41cb-950a-07fb0b288e80"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2927ed0c-dd81-453f-b57d-9bc75f3fa6e4"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fba47c70-631c-446e-a601-9abab0cfaa01"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""83c232d0-8f61-4dd1-a384-c961cad83eca"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""2c1bb5a4-9435-4090-af56-f9d5abb254b8"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""02b3f347-989b-474d-81bc-631b104b8270"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""39310eeb-deb0-4283-8eed-f7b46bb6dbae"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""433cf161-ac42-492b-97a0-11a85b019bda"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""3f5b23f4-cba6-4d37-b6aa-f9a4315fa311"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b39c6a5f-4227-4008-ab3f-60fd212c5cdc"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=0.3,y=0.3)"",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""001b0305-ada4-4fed-a034-8c6d120fb9c6"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d9d23ab8-471b-44bc-b668-8f54baf692be"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""PickUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eeed9f82-d812-4c69-8838-7615302886c6"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PickUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""439ada6f-37d5-440b-8d8f-aa5a96c0a8e8"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Drop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8f98899a-3872-406d-b120-aee4a9d938ee"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Drop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5cd78a45-cde7-4d11-8649-878761b533bf"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Throw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""616d2de6-ce1b-4a14-a091-b36816addd88"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Throw"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6832381c-5726-479c-8fd4-1cdef43c49b6"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1d256a0e-1d58-4908-8c39-796adb213c02"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fa9384dc-d5d8-49ce-9cd7-a942ce221a90"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1f79dc08-eb52-4f92-ad80-3187eefda47d"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""320e6fe6-8fe1-4c31-b220-8c4904a4b2be"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""30dae788-b489-4cd7-a534-2b1fbe983853"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""18c0fa9b-e5e7-439c-83bf-8ff95dacc872"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""08de9fcb-2a4e-4a2c-b29f-499e8a3c3f59"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a377bf44-fb6c-4d95-8a9f-d110f53eb797"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""NextWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ddb0e43a-b39d-4aae-b992-52cd28f5b87d"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""NextWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bd467ea8-ec86-4424-ad8a-e3a1191706ed"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""PreviousWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bba9fc61-a309-4b26-8c46-331690c784a3"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PreviousWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""52916cbc-8a4c-43d9-8c3d-e91da4c0c8ed"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""Heal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f364811a-989c-4b5b-a955-39de35664ac5"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Heal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e0337593-286e-4952-ada0-6454cad3d7b1"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""OpenMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ca0d5870-f328-42ad-a255-6d350966590c"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""OpenMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""20493d64-aeca-444e-8f8c-21f2c7d70f38"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Mouse and Keyboard"",
                    ""action"": ""OpenShop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d0e7f568-66c1-469f-93fb-8ee3b64b6b1c"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""OpenShop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Mouse and Keyboard"",
            ""bindingGroup"": ""Mouse and Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Look = m_Player.FindAction("Look", throwIfNotFound: true);
        m_Player_Click = m_Player.FindAction("Click", throwIfNotFound: true);
        m_Player_PickUp = m_Player.FindAction("PickUp", throwIfNotFound: true);
        m_Player_Drop = m_Player.FindAction("Drop", throwIfNotFound: true);
        m_Player_Throw = m_Player.FindAction("Throw", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Reload = m_Player.FindAction("Reload", throwIfNotFound: true);
        m_Player_Aim = m_Player.FindAction("Aim", throwIfNotFound: true);
        m_Player_Sprint = m_Player.FindAction("Sprint", throwIfNotFound: true);
        m_Player_NextWeapon = m_Player.FindAction("NextWeapon", throwIfNotFound: true);
        m_Player_PreviousWeapon = m_Player.FindAction("PreviousWeapon", throwIfNotFound: true);
        m_Player_Heal = m_Player.FindAction("Heal", throwIfNotFound: true);
        m_Player_OpenMenu = m_Player.FindAction("OpenMenu", throwIfNotFound: true);
        m_Player_OpenShop = m_Player.FindAction("OpenShop", throwIfNotFound: true);
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
    private readonly InputAction m_Player_Look;
    private readonly InputAction m_Player_Click;
    private readonly InputAction m_Player_PickUp;
    private readonly InputAction m_Player_Drop;
    private readonly InputAction m_Player_Throw;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Reload;
    private readonly InputAction m_Player_Aim;
    private readonly InputAction m_Player_Sprint;
    private readonly InputAction m_Player_NextWeapon;
    private readonly InputAction m_Player_PreviousWeapon;
    private readonly InputAction m_Player_Heal;
    private readonly InputAction m_Player_OpenMenu;
    private readonly InputAction m_Player_OpenShop;
    public struct PlayerActions
    {
        private @PlayerInput m_Wrapper;
        public PlayerActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Look => m_Wrapper.m_Player_Look;
        public InputAction @Click => m_Wrapper.m_Player_Click;
        public InputAction @PickUp => m_Wrapper.m_Player_PickUp;
        public InputAction @Drop => m_Wrapper.m_Player_Drop;
        public InputAction @Throw => m_Wrapper.m_Player_Throw;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Reload => m_Wrapper.m_Player_Reload;
        public InputAction @Aim => m_Wrapper.m_Player_Aim;
        public InputAction @Sprint => m_Wrapper.m_Player_Sprint;
        public InputAction @NextWeapon => m_Wrapper.m_Player_NextWeapon;
        public InputAction @PreviousWeapon => m_Wrapper.m_Player_PreviousWeapon;
        public InputAction @Heal => m_Wrapper.m_Player_Heal;
        public InputAction @OpenMenu => m_Wrapper.m_Player_OpenMenu;
        public InputAction @OpenShop => m_Wrapper.m_Player_OpenShop;
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
                @Look.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Click.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnClick;
                @PickUp.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPickUp;
                @PickUp.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPickUp;
                @PickUp.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPickUp;
                @Drop.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDrop;
                @Drop.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDrop;
                @Drop.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDrop;
                @Throw.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnThrow;
                @Throw.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnThrow;
                @Throw.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnThrow;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Reload.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                @Aim.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                @Sprint.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @NextWeapon.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNextWeapon;
                @NextWeapon.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNextWeapon;
                @NextWeapon.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNextWeapon;
                @PreviousWeapon.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPreviousWeapon;
                @PreviousWeapon.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPreviousWeapon;
                @PreviousWeapon.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPreviousWeapon;
                @Heal.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHeal;
                @Heal.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHeal;
                @Heal.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHeal;
                @OpenMenu.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenMenu;
                @OpenMenu.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenMenu;
                @OpenMenu.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenMenu;
                @OpenShop.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenShop;
                @OpenShop.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenShop;
                @OpenShop.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnOpenShop;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
                @PickUp.started += instance.OnPickUp;
                @PickUp.performed += instance.OnPickUp;
                @PickUp.canceled += instance.OnPickUp;
                @Drop.started += instance.OnDrop;
                @Drop.performed += instance.OnDrop;
                @Drop.canceled += instance.OnDrop;
                @Throw.started += instance.OnThrow;
                @Throw.performed += instance.OnThrow;
                @Throw.canceled += instance.OnThrow;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @NextWeapon.started += instance.OnNextWeapon;
                @NextWeapon.performed += instance.OnNextWeapon;
                @NextWeapon.canceled += instance.OnNextWeapon;
                @PreviousWeapon.started += instance.OnPreviousWeapon;
                @PreviousWeapon.performed += instance.OnPreviousWeapon;
                @PreviousWeapon.canceled += instance.OnPreviousWeapon;
                @Heal.started += instance.OnHeal;
                @Heal.performed += instance.OnHeal;
                @Heal.canceled += instance.OnHeal;
                @OpenMenu.started += instance.OnOpenMenu;
                @OpenMenu.performed += instance.OnOpenMenu;
                @OpenMenu.canceled += instance.OnOpenMenu;
                @OpenShop.started += instance.OnOpenShop;
                @OpenShop.performed += instance.OnOpenShop;
                @OpenShop.canceled += instance.OnOpenShop;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_MouseandKeyboardSchemeIndex = -1;
    public InputControlScheme MouseandKeyboardScheme
    {
        get
        {
            if (m_MouseandKeyboardSchemeIndex == -1) m_MouseandKeyboardSchemeIndex = asset.FindControlSchemeIndex("Mouse and Keyboard");
            return asset.controlSchemes[m_MouseandKeyboardSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnClick(InputAction.CallbackContext context);
        void OnPickUp(InputAction.CallbackContext context);
        void OnDrop(InputAction.CallbackContext context);
        void OnThrow(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnNextWeapon(InputAction.CallbackContext context);
        void OnPreviousWeapon(InputAction.CallbackContext context);
        void OnHeal(InputAction.CallbackContext context);
        void OnOpenMenu(InputAction.CallbackContext context);
        void OnOpenShop(InputAction.CallbackContext context);
    }
}
