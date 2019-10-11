using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDown : MonoBehaviour
{
    public ScoreDrawer scoreManager;
    public Text m_timeText, m_owariText;
    public static float time;

    public GameObject[] inactiveGameObject;//カウントダウン終了時に無効化するゲームオブジェクト

    bool isEnded = false;

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
        if (isEnded) { return; }

        if (time > -1)
        {
            time -= Time.deltaTime;
        }
        UpdateText();

        //ゲーム終了
        if (time < 0.0f)
        {
            isEnded = true;
            //コルーチンでゲーム終了
            StartCoroutine("GameEnd");
        }
    }

    void UpdateText()
    {
        int t = Mathf.FloorToInt(time);
        m_timeText.text = "Time:" + t;
    }

    /// <summary>
    /// 数秒後ゲーム終了(コルーチン)
    /// </summary>
    IEnumerator GameEnd()
    {
        GetComponent<AudioSource>().Play();

        //メインカメラを子オブジェクトじゃなくす
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mainCamera.transform.parent = null;

        //登録されているゲームオブジェクトを無効化
        foreach (GameObject go in inactiveGameObject)
        {
            go.SetActive(false);
        }

        //テキスト表示
        m_owariText.gameObject.SetActive(true);
        m_owariText.transform.localScale = Vector3.zero;
       
        //テキスト拡大
        for (int i = 0; i < 20; i++)
        {
            //テキスト拡大
            m_owariText.transform.localScale += Vector3.one * 0.1f;
            //0.1秒停止
            yield return new WaitForSeconds(0.1f);           
        }

        //2.0秒停止
        yield return new WaitForSeconds(2.0f);

        //スコア計測
        scoreManager.GameEnd();
        //シーン切り替え
        SceneManager.LoadScene("ResultFinal");
    }

}