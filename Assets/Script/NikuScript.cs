using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NikuScript : MonoBehaviour
{
    //レイヤー
    int deathNikuLayer;
    int hoboDeathNikuLayer;
    int nikuLayer;

    // Start is called before the first frame update
    void Start()
    {
        //レイヤー初期化
        nikuLayer = LayerMask.NameToLayer("NikuYasai");
        hoboDeathNikuLayer = LayerMask.NameToLayer("HoboDeathNiku");
        deathNikuLayer = LayerMask.NameToLayer("DeathNiku");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision col)
    {
        //自分は死んだ肉か？
        if (this.gameObject.layer == deathNikuLayer || this.gameObject.layer == hoboDeathNikuLayer)
        {
            //肉が落ちた
            if (col.gameObject.layer == nikuLayer)
            {
                col.gameObject.layer = hoboDeathNikuLayer;
            }
        }
    }
}
