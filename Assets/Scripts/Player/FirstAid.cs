using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAid : MonoBehaviour
{
    [SerializeField] private int _healLevel;
    [SerializeField] private AudioClip _healSound;

    public int HealLevel => _healLevel;
    public AudioClip HealSound => _healSound;
}
