using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    public ScoreDrawer scoreManager;
    //Textのゲームオブジェクト
    public Text m_timeText;
    //経過時間
    public static float time;
    
    void Start()
    {
        time = 60;
        UpdateText();

        //初期化したらこのコンポーネント無効化
        //後でStartCountDownに有効化してもらう
        enabled = false;
    }
    void Update()
    {
        if (time > -1)
        {
            time -= Time.deltaTime;
        }
        UpdateText();

        //ゲーム終了
        if (time < 0.0f)
        {
            scoreManager.GameEnd();//スコア計測
            SceneManager.LoadScene("ResultFinal");
        }
    }

    void UpdateText()
    {
        int t = Mathf.FloorToInt(time);
        m_timeText.text = "Time:" + t;
    }

}