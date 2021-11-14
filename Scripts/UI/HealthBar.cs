using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _speedChangeSliderValue;
    [SerializeField] private ParticleSystem _flameEffect;

    private Slider _slider;
    private float _value;

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
        if (_slider.value != _value)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _value, _speedChangeSliderValue * Time.deltaTime);
            if (_slider.value > _value)
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
            _flameEffect.Play(true);
    }

    private void StopPlayingEffect()
    {
        if (_flameEffect.isPlaying)
            _flameEffect.Stop(true);
    }

    public void OnHealthChanged(int value, int maxValue)
    {
        _value = (float)value / maxValue;
    }
}
