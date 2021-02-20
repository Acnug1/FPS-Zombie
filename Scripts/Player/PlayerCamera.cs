using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float _takeDistance; // дистанция на которой мы можем взять объект
    [SerializeField] private float _holdDistance; // дистанция на которой мы держим объект
    [SerializeField] private float _throwForce; // сила, с которой мы бросаем поднятый объект
    [SerializeField] private LayerMask _ignoreLayer;

    private PlayerInput _input;
    private GameObject _currentObject; // храним в данном поле объект попадания Raycast

    private void Awake()
    {
        _input = new PlayerInput();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        _input.Enable(); // включаем компонент PlayerInput

        _input.Player.PickUp.performed += ctx => TryPickUp(); // подписываемся на событие: "попытаться поднять предмет"
        _input.Player.Throw.performed += ctx => Throw(); // подписываемся на событие: "бросить предмет"
        _input.Player.Drop.performed += ctx => Throw(true); // подписываемся на событие: "положить предмет"
    }

    private void OnDisable()
    {
        _input.Disable(); // отключаем компонент PlayerInput

        _input.Player.PickUp.performed -= ctx => TryPickUp(); // отписываемся от события: "попытаться поднять предмет"
        _input.Player.Throw.performed -= ctx => Throw(); // отписываемся от события: "бросить предмет"
        _input.Player.Drop.performed -= ctx => Throw(true); // отписываемся от события: "положить предмет"
    }

    private void TryPickUp()
    {
        if (Physics.Raycast(transform.position, transform.forward, out var hitInfo, _takeDistance, ~_ignoreLayer) && !hitInfo.collider.gameObject.isStatic && _currentObject == null) // делаем рейкаст из камеры и записываем значения по попаданию по объектам (проверяем также, чтобы коллайдер объекта в который мы попали был нестатический)
        {
            _currentObject = hitInfo.collider.gameObject; // записываем в _currentObject ссылку на объект столкновения Raycast

            _currentObject.transform.position = default; // нашему текущему объекту, который мы держим, мы обнуляем позицию
            _currentObject.transform.SetParent(transform, worldPositionStays: false); // после чего устанавливаем ему родителя (делаем доп параметр, чтобы объект двигался, относительно родителя в его координатах)
            _currentObject.transform.localPosition = new Vector3(0, 0, _holdDistance); // сдвинем ему немного позицию на _holdDistance в локальных координатах игрока, чтобы он находился на небольшой дистанции по оси Z (спереди) от игрока 

            _currentObject.GetComponent<Rigidbody>().isKinematic = true; // делаем его Rigidbody isKinematic (чтобы он не падал на землю при подъеме)
        }
    }

    private void Throw(bool drop = false) // делаем необязательный параметр "положить (Drop)", по умолчанию равный false, в методе "бросание (Throw)"
    {
        if (_currentObject != null) // если объект поднят игроком
        {
            _currentObject.transform.parent = null; // оставляем текущий объект без родителя

            var rigidbody = _currentObject.GetComponent<Rigidbody>(); // берем Rigidbody у _currentObject
            rigidbody.isKinematic = false; // отключаем isKinematic у rigidbody нашего объекта (чтобы на него снова действовала обычная физика)

            if (!drop) // если нужно бросить объект
            {
                rigidbody.AddForce(transform.forward * _throwForce, ForceMode.Impulse); // бросаем наш объект вперед с силой равной _throwForce
            }

            _currentObject = null; // обнуляем текущий объект, подобранный игроком
        }
    }
}
