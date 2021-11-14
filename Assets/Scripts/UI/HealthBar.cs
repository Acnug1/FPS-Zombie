using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _speedChangeSliderValue; // скорость изменения значения слайдера
    [SerializeField] private ParticleSystem _flameEffect;

    private Slider _slider;
    private float _value; // заносим сюда значение

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.interactable = false;
        _slider.value = 0;
        _value = 1;
    }

    private void OnEnable()
    {
        _player.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
    }

    private void Update()
    {
        ChangeSliderValue();
    }

    private void ChangeSliderValue()
    {
        if (_slider.value != _value) // если значение слайдера не равно _value
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _value, _speedChangeSliderValue * Time.deltaTime); // двигаем значение слайдера к значению _value со скоростью Speed
            if (_slider.value > _value) // если значение слайдера больше, чем значение _value (количество отображаемых жизней в данный момент больше, чем их на самом деле)
            {
                PlayEffect();
            }
            else
                StopPlayingEffect();
        }
        else
            StopPlayingEffect();
    }

    private void PlayEffect()
    {
        if (!_flameEffect.isEmitting)
            _flameEffect.Play(true); // воспроизводим эффект сгорания жизней
    }

    private void StopPlayingEffect()
    {
        if (_flameEffect.isPlaying)
            _flameEffect.Stop(true); // отключаем эффект
    }

    public void OnHealthChanged(int value, int maxValue)
    {
        _value = (float)value / maxValue;
    }
}
