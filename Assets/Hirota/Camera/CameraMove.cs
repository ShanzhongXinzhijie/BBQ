using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //4箇所のカメラの位置
    Vector3[] m_cameraPos = {
        new Vector3(0.0f, -5.0f, 9.5f),
        new Vector3(9.5f, -5.0f, 0.0f),
        new Vector3(0.0f, -5.0f, -9.5f),
        new Vector3(-9.5f, -5.0f, 0.0f),
    };

    //移動後の位置
    Vector3 m_moveCameraPos = new Vector3(0.0f, 0.0f, 0.0f);

    //移動するポジション番号
    int m_cameraPosNumber = 0;

    //ポジション番号の最小値
    int m_min = 0;

    //ポジション番号の最大値
    int m_max = 3;

    // Start is called before the first frame update
    void Start()
    {
        //初期位置の設定
        m_moveCameraPos = m_cameraPos[m_cameraPosNumber];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            if (m_cameraPosNumber == m_max)
            {
                m_cameraPosNumber = m_min;
            }
        }
    }
}
