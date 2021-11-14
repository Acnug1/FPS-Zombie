using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cloth))]

public class Ball : MonoBehaviour
{
    [SerializeField] private float _deflatingBallSpeed;

    private Cloth _cloth;

    private void Start()
    {
        _cloth = GetComponent<Cloth>();
        InflateBall();
    }

    public void InflateBall()
    {
        _cloth.bendingStiffness = 1f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_cloth.bendingStiffness > 0)
            _cloth.bendingStiffness -= _deflatingBallSpeed;

        if (_cloth.bendingStiffness < 0)
            _cloth.bendingStiffness = 0;
    }
}
