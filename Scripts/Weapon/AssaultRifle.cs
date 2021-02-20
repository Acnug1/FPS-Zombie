using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : Weapon
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Transform _barrelGun;
    [SerializeField] private ParticleSystem _fireWeapon;
    [SerializeField] [Range(0, 1f)] private float _radiusSpread;

    public override void Shoot()
    {
        var spread = (Vector3)Random.insideUnitCircle * _radiusSpread;
        var shootPointPositionWithSpread = _shootPoint.TransformPoint(_shootPoint.InverseTransformPoint(_shootPoint.position) + spread);
        Instantiate(_fireWeapon, _barrelGun.position, _barrelGun.rotation, transform);
        Instantiate(Bullet, shootPointPositionWithSpread, _shootPoint.rotation);
        ChangeAmmunition();
    }
}
