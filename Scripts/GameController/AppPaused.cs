using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppPaused : MonoBehaviour
{
    private bool isPaused = false;

    private void OnGUI()
    {
        if (isPaused)
            GUI.Label(new Rect(100, 100, 50, 30), "Game paused");
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        isPaused = !hasFocus;
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        isPaused = pauseStatus;
    }
}
