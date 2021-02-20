using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _jumpForce; // сила, с которой мы прыгаем
    [SerializeField] private Camera _playerCamera;

    private PlayerInput _input;
    private Vector2 _direction; // направление движения
    private Vector2 _rotate; // направление поворота мыши
    private Vector2 _rotation;
    private bool _inAir = false;
    private Rigidbody _playerRigidbody;

    private void Awake()
    {
        _input = new PlayerInput();
        _playerRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _input.Enable(); // включаем компонент PlayerInput

        _input.Player.Jump.performed += ctx => Jump(); // подписываемся на событие: "прыжок"
    }

    private void OnDisable()
    {
        _input.Disable(); // отключаем компонент PlayerInput

        _input.Player.Jump.performed -= ctx => Jump(); // подписываемся от события: "прыжок"
    }

    private void Update()
    {
        // Значения типа Value нужно считывать через Update, а не систему событий, так как иначе событием будет передано одно изменение значения при нажатии на клавишу движения игроком 
        // (например, 1 или -1 без сброса в 0 по окончанию нажатия на клавишу), что приведет к бесконечному зацикливанию движения игрока в последнем заданном направлении
        _rotate = _input.Player.Look.ReadValue<Vector2>(); // считываем значение поворота и записываем в Vector2
        _direction = _input.Player.Move.ReadValue<Vector2>(); // считываем значение движения и записываем в Vector2

        Look(_rotate);
        Move(_direction);
    }

    private void Look(Vector2 rotate)
    {
        if (rotate.sqrMagnitude < 0.1) // если квадрат длины вектора меньше 0.1. Берем именно квадрат длины, чтобы учитывать отрицательные значения (минимальное отклонение стика на геймпаде)
            return; // то наш метод не выполняется (движение не происходит)

        float scaledRotateSpeed = _rotateSpeed * Time.deltaTime; // задаем масштаб скорости поворота
        _rotation.y += rotate.x * scaledRotateSpeed; // поворот по оси X (изменяется на значение: вправо 0+1*10 или влево 0+(-1)*10)
        _rotation.x = Mathf.Clamp(_rotation.x - rotate.y * scaledRotateSpeed, -90, 90); // поворот по оси Y, залоченный при взгляде вверх 0-(1*10) и вниз 0-(-1*10) на углах -90 и 90 соответственно
        transform.localEulerAngles = new Vector2(transform.position.x, _rotation.y); // поворот игрока в локальной системе координат (в локальных углах Эйлера от -180 до 180 градусов, относительно себя) равен заданному повороту (используем localEulerAngles, а не transform.localrotation для работы с Vector2 в градусах)

        _playerCamera.transform.localEulerAngles = new Vector2(_rotation.x, transform.position.y); // поворот камеры игрока в локальной системе координат равен заданному повороту (используем localEulerAngles, а не transform.localrotation для работы с Vector2 в градусах)
    }

    private void Move(Vector2 direction)
    {
        if (direction.sqrMagnitude < 0.1)
            return;

        float scaledMoveSpeed = _moveSpeed * Time.deltaTime; // задаем масштаб скорости движения
        Vector3 move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(_direction.x, 0, _direction.y); // задаем направление движения игрока относительно (глобальной координаты угла Эйлера) поворота мыши по оси X (в углах Эйлера это ось Y, так как оси там перепутаны местами)
        transform.position += move * scaledMoveSpeed; // движение игрока будет принимать значение направления нормализованного вектора в пространстве * скорость игрока * время кадра
    }

    private void Jump()
    {
        if (!_inAir)
        {
            _inAir = true;

            _playerRigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse); // прыгаем вверх с силой равной _jumpForce
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _inAir = false;
    }
}

