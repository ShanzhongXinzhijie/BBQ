using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct ResultData
{
    public int livingMeet;
    public int deathMeet;
    public int score;

    public int hoboDeathMeat;
    public int fastKill;
    public int girigiriKill;
    public int maxConbo;
    public int matoCnt10, matoCnt100, matoCnt100000;
    public int saraNiku;
}

public class ScoreDrawer : MonoBehaviour
{
    public Text livingText, deathText, scoreText, conboText;

    static ResultData resultData;
    public static ResultData GetResultData()
    {
        return resultData;
    }

    int conboNum = 0;

    //レイヤー
    int hoboDeathNikuLayer;

    // Start is called before the first frame update
    void Start()
    {
        resultData = new ResultData();

        hoboDeathNikuLayer = LayerMask.NameToLayer("HoboDeathNiku");
    }

    public void GameEnd()
    {
        CountHoboDeathMeetCount();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void LateUpdate()
    {
        //テキストの更新
        livingText.text = resultData.livingMeet.ToString() + "肉生存";
        deathText.text = resultData.deathMeet.ToString() + "肉死亡";
        scoreText.text = "SCORE : " + resultData.score.ToString();
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
        //最大コンボ更新
        if(conboNum > resultData.maxConbo)
        {
            resultData.maxConbo = conboNum;
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
        resultData.score += addPoint;
    }

    /// <summary>
    /// 肉カウントの増加
    /// </summary>
    public void AddLivingMeetCount()
    {
        resultData.livingMeet++;
    }
    public void CountHoboDeathMeetCount()
    {
        resultData.hoboDeathMeat = 0;
        GameObject[] hobos = GameObject.FindGameObjectsWithTag("Niku");
        foreach (GameObject hobo in hobos)
        {
            if(hobo.layer == hoboDeathNikuLayer) { resultData.hoboDeathMeat++; }
        }
    }
    public void AddDeathMeetCount()
    {
        resultData.deathMeet++;
    }
    static public void SubDeathMeetCount()
    {
        resultData.deathMeet--;
    }

    public void AddFastKillCnt()
    {
        resultData.fastKill++;
    }
    public void AddGiriGiriKillCnt()
    {
        resultData.girigiriKill++;
    }
    public void AddMatoCnt(int matoPoint)
    {
        switch (matoPoint)
        {
            case 10:
                resultData.matoCnt10++;
                break;
            case 100:
                resultData.matoCnt100++;
                break;
            case 100000:
                resultData.matoCnt100000++;
                break;
            default:
                Debug.LogError("何だこの的は");
                break;
        }
    }
    //public void AddMatoCnt100000()
    //{
    //    resultData.saraNiku++;
    //}
    //public int ;
}
