using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeetGenerator : MonoBehaviour
{
    //肉
    public GameObject m_meet;
    //肉焼き器の4辺の初期位置
    Vector3[] m_nikuyaki = { 
        new Vector3(0.0f, 5.0f, 4.0f),
        new Vector3(4.0f, 5.0f, 0.0f),
        new Vector3(0.0f, 5.0f, -4.0f),
        new Vector3(-4.0f, 5.0f, 0.0f),
    };
    Vector3 m_meetPos = new Vector3( 0.0f, 0.0f, 0.0f );

    //フレーム数
    int m_cntTime = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //秒数の加算
        m_cntTime++;

        //肉焼き器の4辺から1辺をランダムで決める
        //注：maxは含まれないので+1
        int meetDirection = UnityEngine.Random.Range(0, 4);

        //落ちる肉のポジションをセット
        m_meetPos = m_nikuyaki[meetDirection];

        //加減量をランダムで決める
        float meetAdd = UnityEngine.Random.Range(2.5f, -2.5f);

        //肉の横移動
        if (meetDirection == 0 || meetDirection == 2)
        {
            m_meetPos.x += meetAdd;
        }
        else
        {
            m_meetPos.y += meetAdd;
        }

        
        //もし20フレーム経ったら
        if (m_cntTime > 20)
        {
            //肉を生成
            Instantiate(m_meet, m_meetPos, Quaternion.identity);
            //秒数をリセット
            m_cntTime = 0;
        }
    }
}
