using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class SoundBar : MonoBehaviour
{
    private Slider _soundBar;

    private void Awake()
    {
        _soundBar = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        if (_soundBar.value != Settings.Volume)
            _soundBar.value = Settings.Volume;
    }

    private void Update()
    {
        ChangeSoundVolume();
    }

    private void ChangeSoundVolume()
    {
        if (_soundBar.value != Settings.Volume)
            Settings.ChangeVolume(_soundBar.value);
    }
}
