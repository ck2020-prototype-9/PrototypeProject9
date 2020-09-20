using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool isPause = false;
    public void IsPause()
    {
        isPause = !isPause;
        if(isPause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
        Time.fixedDeltaTime = 0.002f * Time.timeScale;
    }
}
