using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDrawer : MonoBehaviour
{
    public Text livingText, deathText, scoreText, conboText;

    int livingMeet = 0;
    int deathMeet = 0;
    int score = 0;
    int conboNum = 0;

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
        //テキストの更新
        livingText.text = livingMeet.ToString() + "肉生存";
        deathText.text = deathMeet.ToString() + "肉死亡";
        scoreText.text = "SCORE : " + score.ToString();
        conboText.text = conboNum.ToString();
    }

    /// <summary>
    /// コンボ数の取得
    /// </summary>
    /// <returns></returns>
    public int GetConbo()
    {
        return conboNum;
    }
    /// <summary>
    /// コンボ数の増加
    /// </summary>
    public void AddConbo()
    {
        conboNum++;
        if (conboNum > 1)
        {
            conboText.gameObject.SetActive(true);
        }
    }
    /// <summary>
    /// コンボ数のリセット
    /// </summary>
    public void ResetConbo()
    {
        conboNum = 0;
        conboText.gameObject.SetActive(false);
    }

    /// <summary>
    /// スコアの増加
    /// </summary>
    public void AddScore(int addPoint)
    {
        score += addPoint;
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
