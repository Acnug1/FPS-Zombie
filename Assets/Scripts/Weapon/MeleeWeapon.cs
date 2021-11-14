using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public override void Shoot(Transform shootPoint, int shootsCount, bool playerMoves)
    {
        Instantiate(Bullet, shootPoint.position, shootPoint.rotation);
    }
}
