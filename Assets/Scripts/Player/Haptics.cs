using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Haptics : MonoBehaviour
{
    [SerializeField] [Range(0, 1f)] private float _lowFrequencyLeftMotor = 0.25f;
    [SerializeField] [Range(0, 1f)] private float _highFrequencyRightMotor = 0.25f;

    public void PlayVibration(float seconds = 0.25f)
    {
        StartCoroutine(PlayHaptics(seconds));
    }

    private IEnumerator PlayHaptics(float seconds)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(seconds);
        Gamepad.current.SetMotorSpeeds(_lowFrequencyLeftMotor, _highFrequencyRightMotor);
        yield return waitForSeconds;
        InputSystem.ResetHaptics();
    }

    public void PauseVibration()
    {
        InputSystem.PauseHaptics();
    }

    public void ResumeVibration()
    {
        InputSystem.ResumeHaptics();
    }
}
