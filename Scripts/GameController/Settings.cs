using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    private const string SoundVolume = "SoundVolume";

    private static float _volume = PlayerPrefs.GetFloat(SoundVolume, 1);

    public static float Volume => _volume;

    public static void ChangeVolume(float volume)
    {
        _volume = volume;
        PlayerPrefs.SetFloat(SoundVolume, _volume);
    }
}