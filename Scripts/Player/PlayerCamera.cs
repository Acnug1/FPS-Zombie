using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float _takeDistance;
    [SerializeField] private float _holdDistance;
    [SerializeField] private float _throwForce;
    [SerializeField] private Player _player;
    [SerializeField] private LayerMask _ignoreLayer;

    private PlayerInput _input;
    private GameObject _currentObject;

    private void Awake()
    {
        _input = new PlayerInput();
    }

    private void OnEnable()
    {
        _input.Enable();

        _input.Player.Interact.performed += ctx => InteractionWithItem();
        _input.Player.Throw.performed += ctx => Throw();
    }

    private void OnDisable()
    {
        _input.Disable();

        _input.Player.Interact.performed -= ctx => InteractionWithItem();
        _input.Player.Throw.performed -= ctx => Throw();
    }

    private void InteractionWithItem()
    {
        if (!_currentObject)
            TryPickUp();
        else
            Throw(true);
    }

    private void TryPickUp()
    {
        if (Physics.Raycast(transform.position, transform.forward, out var hitInfo, _takeDistance, ~_ignoreLayer)
            && !hitInfo.collider.gameObject.isStatic && hitInfo.collider.TryGetComponent(out Rigidbody rigidbody)
            && _currentObject == null && _player.CurrentHealth > 0 && Time.timeScale > 0)
        {
            _currentObject = hitInfo.collider.gameObject;

            _currentObject.transform.position = default;
            _currentObject.transform.SetParent(transform, worldPositionStays: true);
            _currentObject.transform.localPosition = new Vector3(0, 0, _holdDistance);

            _currentObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void Throw(bool drop = false)
    {
        if (_currentObject != null && _player.CurrentHealth > 0 && Time.timeScale > 0)
        {
            _currentObject.transform.parent = null;

            var rigidbody = _currentObject.GetComponent<Rigidbody>();
            rigidbody.isKinematic = false;

            if (!drop)
            {
                rigidbody.AddForce(transform.forward * _throwForce, ForceMode.Impulse);
            }

            _currentObject = null;
        }
    }
}
