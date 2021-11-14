using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndPoint : MonoBehaviour
{
    public event UnityAction<Player> Reached;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            if (player.CurrentHealth > 0)
            {
                player.HidePlayerWeapon(true);
                Reached?.Invoke(player);
            }
        }
    }
}
