using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private float _deathTime;

    private int _currentHealth;
    private Animator _animator;
    private int _money;

    public int CurrentHealth => _currentHealth;
    public int Money => _money;

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int> MoneyChanged;
    public event UnityAction Died;

    private void Start()
    {
        _currentHealth = _health;
        Time.timeScale = 1f;
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth, _health);
        _animator.SetTrigger("Hit");

        if (_currentHealth <= 0)
        {
            _animator.SetTrigger("Death");
            StartCoroutine(DeathPlayer());
        }
    }

    private IEnumerator DeathPlayer()
    {
        var waitForSeconds = new WaitForSeconds(_deathTime);
        yield return waitForSeconds;

        _animator.StopPlayback();
        Destroy(gameObject);
        Died?.Invoke();
    }

    public void AddMoney(int reward)
    {
        _money += reward;
        MoneyChanged?.Invoke(_money);
    }

    public void SpendMoney(int price)
    {
        _money -= price;
        MoneyChanged?.Invoke(_money);
    }
}
