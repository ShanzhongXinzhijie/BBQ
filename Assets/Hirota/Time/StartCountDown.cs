using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCountDown : MonoBehaviour
{
    public Text m_timeText;
    public static float time;
    void Start()
    {
        time = 4;
    }
    void Update()
    {
        if (time > 1)
        {
            time -= Time.deltaTime;
        }
        int t = Mathf.FloorToInt(time);
        m_timeText.text = t.ToString();

        if (t == 0)
        {
            m_timeText.text = "始めろ";
        }
    }

}