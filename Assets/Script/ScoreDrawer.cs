using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDrawer : MonoBehaviour
{
    public Text livingText, deathText;

    int livingMeet = 0;
    int deathMeet = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void LateUpdate()
    {
        livingText.text = livingMeet.ToString() + "肉生存";
        deathText.text = deathMeet.ToString() + "肉死亡";
    }

    /// <summary>
    /// 肉カウントの増加
    /// </summary>
    public void AddLivingMeetCount()
    {
        livingMeet++;
    }
    public void AddDeathMeetCount()
    {
        deathMeet++;
    }
}
