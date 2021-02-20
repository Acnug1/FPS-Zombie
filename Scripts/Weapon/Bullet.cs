using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _maxDistance = 100;
    [SerializeField] private GameObject _decalHitWall;
    [SerializeField] private GameObject _bloodEffect;
    [SerializeField] private LayerMask _ignoreLayer;

    private float _floatInFrontOfWall = 0.01f;
    private RaycastHit hit;
    private Ray ray;

    private void FixedUpdate()
    {
        ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit, _maxDistance, ~_ignoreLayer))
        {
            if (hit.collider.gameObject.TryGetComponent(out Enemy enemy))
            {
                if (_bloodEffect)
                    Instantiate(_bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));

                Destroy(gameObject);
            }
            else
            if (_decalHitWall)
            {
                if (!hit.collider.gameObject.isStatic && hit.collider.gameObject.transform.localScale == Vector3.one)
                    Instantiate(_decalHitWall, hit.point + hit.normal * _floatInFrontOfWall, Quaternion.LookRotation(hit.normal), hit.collider.gameObject.transform);
                else
                    Instantiate(_decalHitWall, hit.point + hit.normal * _floatInFrontOfWall, Quaternion.LookRotation(hit.normal));
            }

            Destroy(gameObject);
        }

        Destroy(gameObject);
    }
}
