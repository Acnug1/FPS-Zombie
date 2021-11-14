using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLog : MonoBehaviour
{
    [SerializeField] private AdSettings _adSettings;
    [SerializeField] private Text _logText;

    private void OnEnable()
    {
        _adSettings.OnRewarded += OnRewardedVideoFinished;
    }

    private void OnDisable()
    {
        _adSettings.OnRewarded -= OnRewardedVideoFinished;
    }

    private void OnRewardedVideoFinished(double amount, string name)
    {
        _logText.text = "Reward: " + amount.ToString() + " " + name;
    }
}
