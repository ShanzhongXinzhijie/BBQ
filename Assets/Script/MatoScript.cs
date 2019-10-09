using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatoScript : MonoBehaviour
{
    public ScoreDrawer scoreManager;//スコアを管理するクラス
    public int hitScore = -1000000;//この的の点数

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        NikuScript nikuScript = col.gameObject.GetComponent<NikuScript>();
        if (nikuScript)//あたったのが肉なら
        {
            if (!nikuScript.ContainsHitedMatoID(gameObject.GetInstanceID()))//まだこの肉とはあたっていない
            {
                nikuScript.AddHitedMatoID(gameObject.GetInstanceID());//肉にこの的のIDを追加
                scoreManager.AddScore(hitScore);//スコア加算
            }

        }
    }
}
