using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //プレイヤーのポジションのY方向
    public float m_playerPosY = 7.75f;

    //4箇所のプレイヤーの位置
    Vector3[] m_playerPos = {
        new Vector3( 0.0f, 0.0f, 14.9f ),
        new Vector3(5.0f, 0.0f, 9.5f),
        new Vector3(0.0f, 0.0f, -1.9f),
        new Vector3(-5.0f, 0.0f, 9.5f)
    };

    //移動するポジション番号
    int m_playerPosNumber = 0;

    //ポジション番号の最小値
    int m_min = 0;

    //ポジション番号の最大値
    int m_max = 3;

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
        if(Input.GetKeyDown(KeyCode.W))
        {
            if (m_playerPosNumber == 0 || m_playerPosNumber == 1)
            {
                m_playerPosNumber += 2;
            }
            else
            {
                m_playerPosNumber -= 2;
            }
        }

        //左方向
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (m_playerPosNumber == m_max)
            {
                m_playerPosNumber = m_min;
            }
            else
            {
                m_playerPosNumber++;
            }
        }
        
        //右方向
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (m_playerPosNumber == m_min)
            {
                m_playerPosNumber = m_max;
            }
            else
            {
                m_playerPosNumber--;
            }
        }

        //カメラの更新
        gameObject.transform.position = m_playerPos[m_playerPosNumber];
        Debug.Log(gameObject.transform.position);

    }
}
