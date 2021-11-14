using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Footsteps))]
[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PhysicsMovement))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _sprintSpeedModifier;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _jumpForce; // сила, с которой мы прыгаем
    [SerializeField] private float _stepDelay;
    [SerializeField] private float _speedFallWithoutDamage;
    [SerializeField] private int _fallDamageModifier;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private PlayerShooter _playerShooter;
    [SerializeField] private AudioClip _jump;
    [SerializeField] private AudioClip _landing;
    [SerializeField] private AudioClip _fall;
    [SerializeField] private LayerMask _ignoreLayer;
    [SerializeField] private Transform _groundCheck; // позиция сфера под игроком
    [SerializeField] private float _groundDistance; // радиус сферы под игроком

    private PlayerInput _input;
    private Vector2 _direction; // направление движения
    private Vector2 _rotate; // направление поворота мыши
    private Vector2 _rotation;
    private bool _inAir;
    private Rigidbody _rigidbody;
    private float _defaultMoveSpeed;
    private Animator _animator;
    private bool _isSprint;
    private AudioSource _audioSource;
    private Footsteps _footSteps;
    private float _lastStepTime;
    private bool _playerMoves;
    private RaycastHit _hit;
    private Ray _ray;
    private Player _player;
    private Vector3 _rotationCamera;
    private float _pivotOffset = 0.5f;
    private const string Concrete = "Concrete";
    private const string Metal = "Metal";
    private const string Wood = "Wood";
    private const string Dirt = "Dirt";
    private const string Sand = "Sand";
    private const string Cloth = "Cloth";
    private const string Softbody = "Softbody";
    private Vector3 _moveDirection;
    private PhysicsMovement _physicsMovement;

    public float RotateSpeed => _rotateSpeed;
    public bool IsSprint => _isSprint;
    public bool PlayerMoves => _playerMoves;
    public bool InAir => _inAir;
    public Vector3 RotationCamera => _rotationCamera;
    public float PivotOffset => _pivotOffset;
    public LayerMask IgnoreLayer => _ignoreLayer;

    private void Awake()
    {
        _input = new PlayerInput();
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _footSteps = GetComponent<Footsteps>();
        _player = GetComponent<Player>();
        _physicsMovement = GetComponent<PhysicsMovement>();
        _audioSource.playOnAwake = false;
        _defaultMoveSpeed = _moveSpeed;
        _rotationCamera = _playerCamera.transform.localEulerAngles;
    }

    private void OnEnable()
    {
        _input.Enable(); // включаем компонент PlayerInput

        _input.Player.Jump.performed += ctx => Jump(); // подписываемся на событие: "прыжок"

        _input.Player.Sprint.performed += ctx =>
        {
            if (ctx.interaction is PressInteraction)
                Sprint();
        };

        _input.Player.Sprint.canceled += ctx =>
        {
            if (ctx.interaction is PressInteraction)
                Walk();
        };

        _playerShooter.WeaponChanged += OnWeaponChanged;
    }

    private void OnDisable()
    {
        _input.Disable(); // отключаем компонент PlayerInput

        _input.Player.Jump.performed -= ctx => Jump(); // подписываемся от события: "прыжок"

        _input.Player.Sprint.performed -= ctx =>
        {
            if (ctx.interaction is PressInteraction)
                Sprint();
        };

        _input.Player.Sprint.canceled -= ctx =>
        {
            if (ctx.interaction is PressInteraction)
                Walk();
        };

        _playerShooter.WeaponChanged -= OnWeaponChanged;
    }

    private void Update()
    {
        // Значения типа Value нужно считывать через Update, а не систему событий, так как иначе событием будет передано одно изменение значения при нажатии на клавишу движения игроком 
        // (например, 1 или -1 без сброса в 0 по окончанию нажатия на клавишу), что приведет к бесконечному зацикливанию движения игрока в последнем заданном направлении

        if (_player.CurrentHealth > 0 && Time.timeScale > 0)
        {
            // считываем значение движения и записываем в Vector2
            _direction = _input.Player.Move.ReadValue<Vector2>(); // если игра запущена на Android с геймпадом, с эмуляцией геймпада тачскрином или на ПК
            // считываем значение поворота и записываем в Vector2
            _rotate = _input.Player.Look.ReadValue<Vector2>(); // если игра запущена на Android с геймпадом, с эмуляцией геймпада тачскрином или на ПК

            Move(_direction);
            Look(_rotate);

            InAirCheck();

            _lastStepTime -= Time.deltaTime;
        }
    }

    private void Look(Vector2 rotate)
    {
        if (rotate.sqrMagnitude < 0.1) // если квадрат длины вектора меньше 0.1. Берем именно квадрат длины, чтобы учитывать отрицательные значения (минимальное отклонение стика на геймпаде)
            return; // то наш метод не выполняется (движение не происходит)

        float scaledRotateSpeed = _rotateSpeed * Time.deltaTime; // задаем масштаб скорости поворота
        _rotation.y += rotate.x * scaledRotateSpeed; // поворот по оси X (изменяется на значение: вправо 0+1*10 или влево 0+(-1)*10)
        _rotation.x = Mathf.Clamp(_rotation.x - rotate.y * scaledRotateSpeed, -90, 90); // поворот по оси Y, залоченный при взгляде вверх 0-(1*10) и вниз 0-(-1*10) на углах -90 и 90 соответственно

        Quaternion deltaRotation = Quaternion.Euler(new Vector2(0, _rotation.y)); // поворот игрока в локальной системе координат (в локальных углах Эйлера от -180 до 180 градусов, относительно себя) 
        _rigidbody.MoveRotation(deltaRotation); // делаем поворот с помощью физики

        _rotationCamera = new Vector2(_rotation.x, 0); // поворот камеры игрока в локальной системе координат равен заданному повороту (используем localEulerAngles, а не transform.localrotation для работы с Vector2 в градусах)
        _playerCamera.transform.localEulerAngles = _rotationCamera;
    }

    private void Move(Vector2 direction)
    {
        if (_inAir)
        {
            _animator.SetFloat(AnimatorPlayerShooter.Params.Speed, 0);
            _playerMoves = true;
        }
        else if (direction.sqrMagnitude < 0.1)
        {
            _animator.SetFloat(AnimatorPlayerShooter.Params.Speed, 0);
            _playerMoves = false;
            return;
        }
        else if (!_isSprint)
        {
            _animator.SetFloat(AnimatorPlayerShooter.Params.Speed, 1);
            _playerMoves = true;
            GetStep();
        }
        else
        {
            _animator.SetFloat(AnimatorPlayerShooter.Params.Speed, 2);
            _playerMoves = true;
            GetStep(_sprintSpeedModifier);
        }

        _moveDirection = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(direction.normalized.x, 0, direction.normalized.y); // задаем направление движения игрока относительно (глобальной координаты угла Эйлера) поворота мыши по оси X (в углах Эйлера это ось Y, так как оси там перепутаны местами)

        _physicsMovement.Move(_moveDirection, _moveSpeed);
    }

    private void GetStep(float sprintSpeedModifier = 1)
    {
        if (_lastStepTime <= 0)
        {
            _ray = new Ray(transform.position + new Vector3(0, _pivotOffset, 0), -transform.up);

            if (Physics.Raycast(_ray, out _hit, 1f, ~_ignoreLayer))
            {
                switch (_hit.transform.tag) // берем тег поверхности
                {
                    case Concrete:
                        _footSteps.PlayStep(Footsteps.StepsOn.Concrete, Settings.Volume);
                        break;
                    case Metal:
                        _footSteps.PlayStep(Footsteps.StepsOn.Metal, Settings.Volume);
                        break;
                    case Wood:
                        _footSteps.PlayStep(Footsteps.StepsOn.Wood, Settings.Volume);
                        break;
                    case Sand:
                        _footSteps.PlayStep(Footsteps.StepsOn.Sand, Settings.Volume);
                        break;
                    case Dirt:
                    case Cloth:
                    case Softbody:
                    default:
                        _footSteps.PlayStep(Footsteps.StepsOn.Ground, Settings.Volume);
                        break;
                }
            }

            _lastStepTime = _stepDelay / sprintSpeedModifier;
        }
    }

    private void Sprint()
    {
        _isSprint = true;
        _moveSpeed *= _sprintSpeedModifier;
    }

    private void Walk()
    {
        _isSprint = false;
        _moveSpeed = _defaultMoveSpeed;
    }

    private void Jump()
    {
        if (!_inAir && _player.CurrentHealth > 0 && Time.timeScale > 0)
        {
            _rigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse); // прыгаем вверх с силой равной _jumpForce
            _audioSource.PlayOneShot(_jump, Settings.Volume);
        }
    }

    private bool IsOnGround()
    {
        return Physics.CheckSphere(_groundCheck.position, _groundDistance, ~_ignoreLayer);
    }

    private void InAirCheck()
    {
        if (IsOnGround())
            _inAir = false;
        else
            _inAir = true;
    }

    public void RotateSpeedChange(float rotateSpeed)
    {
        _rotateSpeed = rotateSpeed;
    }

    private void OnWeaponChanged(Weapon currentWeapon)
    {
        _animator = currentWeapon.GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Mathf.Abs(collision.impulse.y) > _speedFallWithoutDamage) // если игрок падает с большой скоростью, чем приемлимая скорость падения вниз (значение берется по модулю, т.к. на не статичных объектах импульс коллизии считается в отрицательных значениях, а в нестатичных в положительных)
        {
            _player.ApplyDamage(Mathf.Abs((int)collision.impulse.y * _fallDamageModifier)); // игрок получает удвоенный урон от импульса
            _audioSource.PlayOneShot(_fall, Settings.Volume);
        }
        else
        if (Mathf.Abs(collision.impulse.y) > 3 && Mathf.Abs(collision.impulse.y) <= _speedFallWithoutDamage)
            _audioSource.PlayOneShot(_landing, Settings.Volume);
    }
}