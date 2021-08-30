using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inflammable : MonoBehaviour
{
    [SerializeField] private ParticleSystem _mainFireEffect;
    [SerializeField] private int _damage;
    [SerializeField] private float _hitDelay;
    [SerializeField] private float _burningDuration;

    private ParticleSystem _burning;
    private float _lastHitDamageTime;
    private Coroutine _previousTask;

    private void Update()
    {
        GetDamageFire();
    }

    private void GetDamageFire()
    {
        if (_burning && _lastHitDamageTime <= 0)
        {
            if (transform.root.TryGetComponent(out Enemy enemy) && enemy.Health > 0)
                enemy.TakeDamage(_damage);
            else
            if (TryGetComponent(out Player player) && player.CurrentHealth > 0)
                player.ApplyDamage(_damage);
            else
            if (TryGetComponent(out Explosion explosion))
                explosion.ExplodeWithDelay();

            _lastHitDamageTime = _hitDelay;
        }

        _lastHitDamageTime -= Time.deltaTime;
    }

    public void OnStayFire()
    {
        if (_previousTask != null)
            StopCoroutine(_previousTask);

        if (!_burning)
        {
            _burning = Instantiate(_mainFireEffect, transform.position, Quaternion.Euler(-90, 0, 0), transform);
        }
        else if (!_burning.isEmitting)
            _burning.Play(true);
    }

    public void OnExitFire()
    {
        _previousTask = StartCoroutine(StopEffect(_burningDuration));
    }

    private IEnumerator StopEffect(float burningDuration)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(burningDuration);
        yield return waitForSeconds;

        if (_burning.isPlaying)
            _burning.Stop(true);
    }
}
