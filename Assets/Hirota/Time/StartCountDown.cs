using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCountDown : MonoBehaviour
{
    public Text m_timeText;
    public static float time;

    public GameObject[] activeGameObject;//カウントダウン終了時に有効化するゲームオブジェクト
    CountDown countDown;//制限時間カウントダウンのコンポーネント

    bool isStarted = false;

    void Start()
    {
        time = 4;
        countDown = GetComponent<CountDown>();
    }
    void Update()
    {
        if (isStarted) { return; }

        if (time > 1)
        {
            time -= Time.deltaTime;
        }
        int t = Mathf.FloorToInt(time);
        m_timeText.text = t.ToString();

        if (t == 0)
        {
            isStarted = true;
            m_timeText.text = "始めろ";
            //コルーチンでゲーム開始
            StartCoroutine("GameStart");
        }
    }

    /// <summary>
    /// 数秒後ゲーム開始(コルーチン)
    /// </summary>
    IEnumerator GameStart()
    {
        //1秒停止
        yield return new WaitForSeconds(1);
        //テキストを無効化
        m_timeText.gameObject.SetActive(false);

        //登録されているゲームオブジェクトを有効化
        foreach (GameObject go in activeGameObject)
        {
            go.SetActive(true);
        }
        //制限時間カウントダウンを有効化
        countDown.enabled = true;
    }
}