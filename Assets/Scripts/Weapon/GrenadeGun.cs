using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeGun : Weapon
{
    [SerializeField] private Transform _barrelGun;
    [SerializeField] private ParticleSystem _fireWeapon;
    [SerializeField] private Grenade _grenade;
    [SerializeField] [Range(0, 1f)] private float _radiusSpread;

    public override void Shoot(Transform shootPoint, int shootsCount, bool playerMoves)
    {
        if (!playerMoves)
        {
            switch (shootsCount)
            {
                case 1:
                    Instantiate(_grenade, _barrelGun.position, _barrelGun.rotation);
                    break;
                default:
                    Spread();
                    break;
            }
        }
        else
        {
            Spread();
        }

        Instantiate(_fireWeapon, _barrelGun.position, _barrelGun.rotation, transform);
        ChangeAmmunition();
    }

    private void Spread(float modifier = 1)
    {
        var spread = (Vector3)Random.insideUnitCircle * _radiusSpread / modifier;
        var shootPointPositionWithSpread = _barrelGun.TransformPoint(_barrelGun.InverseTransformPoint(_barrelGun.position) + spread);
        Instantiate(_grenade, shootPointPositionWithSpread, _barrelGun.rotation);
    }
}
