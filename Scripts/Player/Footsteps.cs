using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Footsteps : MonoBehaviour
{
    public enum StepsOn { Concrete, Metal, Wood, Sand, Ground };

    private const string Sounds = "Sounds";
    private const string MainFolder = "Footsteps", ConcreteFolder = "Concrete", MetalFolder = "Metal", WoodFolder = "Wood", SandFolder = "Sand", GroundFolder = "Ground";
    private AudioSource _audioSource;
    private AudioClip[] _concrete, _metal, _wood, _sand, _ground;
    private AudioClip _clip;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.mute = false;
        _audioSource.loop = false;
        LoadSounds();
    }

    private void LoadSounds()
    {
        _concrete = Resources.LoadAll<AudioClip>(Sounds + "/" + MainFolder + "/" + ConcreteFolder);
        _metal = Resources.LoadAll<AudioClip>(Sounds + "/" + MainFolder + "/" + MetalFolder);
        _wood = Resources.LoadAll<AudioClip>(Sounds + "/" + MainFolder + "/" + WoodFolder);
        _sand = Resources.LoadAll<AudioClip>(Sounds + "/" + MainFolder + "/" + SandFolder);
        _ground = Resources.LoadAll<AudioClip>(Sounds + "/" + MainFolder + "/" + GroundFolder);
    }

    public void PlayStep(StepsOn stepsOn, float volume)
    {
        switch (stepsOn)
        {
            case StepsOn.Concrete:
                if (_concrete.Length > 0)
                    _clip = _concrete[Random.Range(0, _concrete.Length)];
                break;
            case StepsOn.Metal:
                if (_metal.Length > 0)
                    _clip = _metal[Random.Range(0, _metal.Length)];
                break;
            case StepsOn.Wood:
                if (_wood.Length > 0)
                    _clip = _wood[Random.Range(0, _wood.Length)];
                break;
            case StepsOn.Sand:
                if (_sand.Length > 0)
                    _clip = _sand[Random.Range(0, _sand.Length)];
                break;
            case StepsOn.Ground:
                if (_ground.Length > 0)
                    _clip = _ground[Random.Range(0, _ground.Length)];
                break;
        }

        if (_clip)
            _audioSource.PlayOneShot(_clip, volume);
    }
}
