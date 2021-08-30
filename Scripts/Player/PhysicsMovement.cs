using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SurfaceSlider))]

public class PhysicsMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private SurfaceSlider _surfaceSlider;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _surfaceSlider = GetComponent<SurfaceSlider>();
    }

    public void Move(Vector3 direction, float speed)
    {
        Vector3 directionAlongSurface = _surfaceSlider.Project(direction);
        Vector3 offset = directionAlongSurface * (speed * Time.deltaTime);

        _rigidbody.MovePosition(_rigidbody.position + offset);
    }
}
