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

    //肉焼き器の4辺の初期位置
    Vector3 m_meetPos = new Vector3( 0.0f, 0.0f, 0.0f );

    //肉焼き器の4辺の初期角度
    Vector3 m_meetRot = new Vector3( 0.0f, 0.0f, 0.0f );

    //時間の計測
    float m_cntTime = 0;

    //エフェクトのゲームオブジェクト
     public GameObject m_effect;

    //ハイスピード肉を落としたか?
    bool isSpawnHighSpeedNiku = false;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(m_effect);
    }

    // Update is called once per frame
    void Update()
    {
        //秒数の加算
        m_cntTime += Time.deltaTime;
        Debug.Log(m_cntTime);

        //肉焼き器の4辺から1辺をランダムで決める
        //注：maxは含まれないので+1
        int meetDirection = UnityEngine.Random.Range(0, 4);

        //落ちる肉のポジションをセット
        m_meetPos = m_nikuyaki[meetDirection];

        //加減量をランダムで決める
        float meetAdd = UnityEngine.Random.Range(-2.5f, 2.5f);

        //肉の横移動
        if (meetDirection == 0 || meetDirection == 2)
        {
            m_meetPos.x += meetAdd;
        }
        else
        {
            m_meetPos.z += meetAdd;
        }

        //ハイスピード肉の生成
        if (!isSpawnHighSpeedNiku && m_cntTime > 0.5f)
        {
            //    isSpawnHighSpeedNiku = true;
            //    GameObject go = Instantiate(m_meet, m_meetPos, Random.rotation);
            //
            ////ランダムで焼き肉にする
            //if (Random.Range(0, 1) > 0)
            //{
            //    NikuScript nikuS = go.GetComponent<NikuScript>();
            //    if (nikuS) { nikuS.Yakiniku(); }
            //}
            //    Rigidbody rigidbody = go.GetComponent<Rigidbody>();
            //    if (rigidbody)
            //    {
            //        rigidbody.drag = 0.0f;//抗力をゼロに
            //    }
        }

        //5秒経ったら
        if (m_cntTime > 1.0f)
        {
            //肉を生成
            GameObject go = Instantiate(m_meet, m_meetPos, Random.rotation);
            Instantiate(m_effect, m_meetPos, Quaternion.identity);
            //ランダムで焼き肉にする
            if (Random.Range(0,1+1) > 0)
            {
                NikuScript nikuS = go.GetComponent<NikuScript>();
                if (nikuS) { nikuS.Yakiniku(); }
            }
            //秒数をリセット
            m_cntTime = 0.0f;
            isSpawnHighSpeedNiku = false;
        }
    }
}
