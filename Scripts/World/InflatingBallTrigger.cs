using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InflatingBallTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ball ball))
            ball.InflateBall();
    }
}
