using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NikuScript : MonoBehaviour
{
    public GameObject yakiniku;

    bool isYakiniku = false;

    //爆発
    bool isExplosion = false;

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

        //爆発
        if (isExplosion)
        {
            const float radius = 25.0f;
            const float power = 1000.0f;
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
                }
                if (hit.gameObject.layer == deathNikuLayer || hit.gameObject.layer == hoboDeathNikuLayer)
                {
                    //吹っ飛ぶので蘇生
                    hit.gameObject.layer = nikuLayer;
                    ScoreDrawer.SubDeathMeetCount();

                    //焼かれる
                    NikuScript niku = hit.GetComponent<NikuScript>();
                    if (niku != null)
                    {
                        niku.Yakiniku();
                    }
                }
                //肉蘇生
                //if (hit.gameObject.layer == deathNikuLayer || hit.gameObject.layer == hoboDeathNikuLayer)
                //{
                //    hit.gameObject.layer = nikuLayer;
                //}
            }
            isExplosion = false;
        }
    }

    public void Yakiniku()
    {
        if (isYakiniku) { return; }

        isYakiniku = true;

        if (gameObject.transform.childCount > 0)
        {
            Vector3 pos = gameObject.transform.GetChild(0).gameObject.transform.localPosition;
            Quaternion rot = gameObject.transform.GetChild(0).gameObject.transform.localRotation;
            Vector3 scale = gameObject.transform.GetChild(0).gameObject.transform.localScale;
            Destroy(gameObject.transform.GetChild(0).gameObject);

            GameObject newModel = Instantiate(yakiniku);
            newModel.transform.SetParent(gameObject.transform);
            newModel.transform.localPosition = pos;
            newModel.transform.localRotation = rot;
            newModel.transform.localScale = scale;
        }
    }

    /// <summary>
    /// 爆発属性を付与
    /// </summary>
    public void SetIsExplosion()
    {
        if (!isYakiniku) { return; }
        isExplosion = true;
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
