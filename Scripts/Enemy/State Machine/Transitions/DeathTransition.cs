using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTransition : Transition
{
    private void Update()
    {
        if (Target == null || Enemy.Health <= 0)
        {
            NeedTransit = true;
        }
    }
}
