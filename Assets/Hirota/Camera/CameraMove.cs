using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //プレイヤーのポジションのY方向
    public float m_playerPosY;

    //4箇所のプレイヤーの位置
    Vector3[] m_playerPos = {
        new Vector3(0.0f, 0.0f, 7.58f),
        new Vector3(7.58f, 0.0f, 0.0f),
        new Vector3(0.0f, 0.0f, -7.58f),
        new Vector3(-7.58f, 0.0f, 0.0f),
    };

    //移動するポジション番号
    int m_playerPosNumber = 2;

    //ポジション番号の最小値
    int m_min = 0;

    //ポジション番号の最大値
    int m_max = 3;

    //回転の角度
    //0　初期値
    //1　スライディング
    //2　左方向
    //3　右方向
    Vector3[] m_kaiten = {
        new Vector3(0.0f, 0.0f, 0.0f),
        new Vector3(0.0f, 180.0f, 0.0f),
        new Vector3(0.0f, 90.0f, 0.0f),
        new Vector3(0.0f, -90.0f, 0.0f),
    };

    //回転の種類
    int m_kaitenNumber = 0;

    //動いたかどうか
    bool m_isMove = false;

    // Start is called before the first frame update
    void Start()
    {
        //4箇所のプレイヤーの位置
        for (int i = 0; i < 4; i++)
        {
            m_playerPos[i].y = m_playerPosY;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //スライディング
        //前方方向
        if(Input.GetKeyDown(KeyCode.W) && !m_isMove)
        {
            if (m_playerPosNumber == 0 || m_playerPosNumber == 1)
            {
                m_playerPosNumber += 2;
            }
            else
            {
                m_playerPosNumber -= 2;
            }
            m_kaitenNumber = 1;
            m_isMove = true;
        }

        //左方向
        if (Input.GetKeyDown(KeyCode.A) && !m_isMove)
        {
            if (m_playerPosNumber == m_max)
            {
                m_playerPosNumber = m_min;
            }
            else
            {
                m_playerPosNumber++;
            }
            m_kaitenNumber = 2;
            m_isMove = true;
        }
        
        //右方向
        if (Input.GetKeyDown(KeyCode.D) && !m_isMove)
        {
            if (m_playerPosNumber == m_min)
            {
                m_playerPosNumber = m_max;
            }
            else
            {
                m_playerPosNumber--;
            }
            m_kaitenNumber = 3;
            m_isMove = true;
        }

        //カメラの更新
        gameObject.transform.position = m_playerPos[m_playerPosNumber];
        transform.Rotate(m_kaiten[m_kaitenNumber]);

        //値の初期化
        m_kaitenNumber = 0;
        m_isMove = false;

        Debug.Log(gameObject.transform.position);

    }
}
