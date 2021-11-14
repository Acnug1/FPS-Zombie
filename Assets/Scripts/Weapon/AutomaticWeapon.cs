using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticWeapon : Weapon
{
    [SerializeField] private Transform _barrelGun;
    [SerializeField] private ParticleSystem _fireWeapon;
    [SerializeField] private ParticleSystem _sleevesEffect;
    [SerializeField] [Range(0, 1f)] private float _radiusSpread;

    public override void Shoot(Transform shootPoint, int shootsCount, bool playerMoves)
    {
        if (!playerMoves)
        {
            switch (shootsCount)
            {
                case 1:
                    Instantiate(Bullet, shootPoint.position, shootPoint.rotation);
                    break;
                case 2:
                    Spread(shootPoint, 3);
                    break;
                case 3:
                    Spread(shootPoint, 2);
                    break;
                default:
                    Spread(shootPoint);
                    break;
            }
        }
        else
        {
            Spread(shootPoint);
        }

        if (_sleevesEffect != null)
            _sleevesEffect.Emit(1);

        Instantiate(_fireWeapon, _barrelGun.position, _barrelGun.rotation, transform);
        ChangeAmmunition();
    }

    private void Spread(Transform shootPoint, float modifier = 1)
    {
        var spread = (Vector3)Random.insideUnitCircle * _radiusSpread / modifier;
        var shootPointPositionWithSpread = shootPoint.TransformPoint(shootPoint.InverseTransformPoint(shootPoint.position) + spread);
        Instantiate(Bullet, shootPointPositionWithSpread, shootPoint.rotation);
    }
}
