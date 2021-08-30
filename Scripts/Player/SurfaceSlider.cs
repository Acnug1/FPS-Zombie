using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]

public class SurfaceSlider : MonoBehaviour
{
    private Vector3 _normal;
    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    public Vector3 Project(Vector3 direction)
    {
        return direction - Vector3.Dot(direction, _normal) * _normal;
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position + new Vector3(0, _playerController.PivotOffset, 0), -transform.up, out RaycastHit hit, 1f, ~_playerController.IgnoreLayer))
            _normal = hit.normal;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + _normal * 3);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Project(transform.forward * 3));
    }
}
