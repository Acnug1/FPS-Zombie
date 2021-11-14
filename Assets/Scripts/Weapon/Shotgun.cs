using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] private Transform[] _barrelsGun;
    [SerializeField] private ParticleSystem _fireWeapon;
    [SerializeField] private ParticleSystem _sleevesEffect;
    [SerializeField] [Range(0, 1f)] private float _radiusSpread;
    [SerializeField] private int _shootsCount;

    public override void Shoot(Transform shootPoint, int shootsCount, bool playerMoves)
    {
        if (!playerMoves)
        {
            for (int i = 0; i < _shootsCount; i++)
            {
                Spread(shootPoint, 2);
            }
        }
        else
        {
            for (int i = 0; i < _shootsCount; i++)
            {
                Spread(shootPoint);
            }
        }

        if (_sleevesEffect != null)
            _sleevesEffect.Emit(1);

        foreach (var barrelGun in _barrelsGun)
        {
            Instantiate(_fireWeapon, barrelGun.position, barrelGun.rotation, transform);
        }

        ChangeAmmunition();
    }

    private void Spread(Transform shootPoint, float modifier = 1)
    {
        var spread = (Vector3)Random.insideUnitCircle * _radiusSpread / modifier;
        var shootPointPositionWithSpread = shootPoint.TransformPoint(shootPoint.InverseTransformPoint(shootPoint.position) + spread);
        Instantiate(Bullet, shootPointPositionWithSpread, shootPoint.rotation);
    }
}
