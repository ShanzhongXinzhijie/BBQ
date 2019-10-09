using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NikuScript : MonoBehaviour
{
    //あたった的のID
    HashSet<int> hitedMatoSet = new HashSet<int>();

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

    /// <summary>
    /// あたった的の追加
    /// </summary>
    public void AddHitedMatoID(int ID)
    {
        hitedMatoSet.Add(ID);
    }
    /// <summary>
    /// 引数の的とすでにあたっているか判定
    /// </summary>
    public bool ContainsHitedMatoID(int ID)
    {
        return hitedMatoSet.Contains(ID);
    }
}
