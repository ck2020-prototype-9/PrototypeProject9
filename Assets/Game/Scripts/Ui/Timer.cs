using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text text;
    private float time=0;
    private float currentTime;
    void Awake()
    {
        currentTime = time;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        text.text = "시간 : "+currentTime.ToString("N2");
    }
}
