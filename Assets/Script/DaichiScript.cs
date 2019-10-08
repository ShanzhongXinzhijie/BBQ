using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaichiScript : MonoBehaviour
{
    public ScoreDrawer scoreManager;//スコアを管理するクラス

    //レイヤー
    int deathNikuLayer;
    int nikuLayer;

    // Start is called before the first frame update
    void Start()
    {
        //レイヤー初期化
        deathNikuLayer = LayerMask.NameToLayer("DeathNiku");
        nikuLayer = LayerMask.NameToLayer("NikuYasai");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        //肉が地面に落ちた
        if(col.gameObject.layer == nikuLayer)
        {
            col.gameObject.layer = deathNikuLayer;
            scoreManager.AddDeathMeetCount();
        }
    }
}
