using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperRifle : Weapon
{
    [SerializeField] private Transform _barrelGun;
    [SerializeField] private ParticleSystem _fireWeapon;
    [SerializeField] private ParticleSystem _sleevesEffect;
    [SerializeField] [Range(0, 1f)] private float _radiusSpread;
    [SerializeField] private Texture2D _target;
    [SerializeField] private Texture2D _targetBackground;
    [SerializeField] private OpticalSight _opticalSight;

    private bool _isZoom;

    private void OnEnable()
    {
        _opticalSight.TryGetSniperRifle(true);
        _opticalSight.ZoomStarted += OnZoomStarted;
    }

    private void OnDisable()
    {
        _opticalSight.TryGetSniperRifle(false);
        _opticalSight.ZoomStarted -= OnZoomStarted;
    }

    public override void Shoot(Transform shootPoint, int shootsCount, bool playerMoves)
    {
        if (!playerMoves)
        {
            Instantiate(Bullet, shootPoint.position, shootPoint.rotation);
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

    private void OnGUI()
    {
        if (_isZoom)
        {
            GUI.depth = 999;
            int hor = Screen.width;
            int ver = Screen.height;
            GUI.DrawTexture(new Rect((hor - ver) / 2, 0, ver, ver), _target);
            GUI.DrawTexture(new Rect((hor / 2) + (ver / 2), 0, hor / 2, ver), _targetBackground);
            GUI.DrawTexture(new Rect(0, 0, (hor / 2) - (ver / 2), ver), _targetBackground);
        }
    }

    private void OnZoomStarted(bool isZoom)
    {
        _isZoom = isZoom;
    }
}
