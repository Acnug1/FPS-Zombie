using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Grenade : MonoBehaviour
{
    [SerializeField] private float _projectileForce;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(transform.forward * _projectileForce, ForceMode.Impulse);
    }
}
