using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public Text m_timeText;
    public static float time;
    void Start()
    {
        time = 60;
    }
    void Update()
    {
        if (time > -1)
        {
            time -= Time.deltaTime;
        }
        int t = Mathf.FloorToInt(time);
        m_timeText.text = "Time:" + t;
    }
    
}