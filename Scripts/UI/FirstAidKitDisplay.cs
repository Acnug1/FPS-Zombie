using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FirstAidKitDisplay : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] TMP_Text _firstAidKitDisplay;

    private void OnEnable()
    {
        _firstAidKitDisplay.text = _player.FirstAidKitCount.ToString();
        _player.FirstAidKitChanged += OnFirstAidKitChanged;
    }

    private void OnDisable()
    {
        _player.FirstAidKitChanged -= OnFirstAidKitChanged;
    }

    private void OnFirstAidKitChanged(int firstAidKitCount)
    {
        _firstAidKitDisplay.text = firstAidKitCount.ToString();
    }
}
