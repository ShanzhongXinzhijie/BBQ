using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprict : MonoBehaviour
{
    public Camera playerCamera;//プレイヤーカメラ
    public GameObject tangPoint;//掴んだ肉を配置する位置
    public ScoreDrawer scoreManager;//スコアを管理するクラス

    List<Rigidbody> grabRigidBodys = new List<Rigidbody>();//掴んでいる肉々

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;//マウスカーソルを非表示に
    }

    // Update is called once per frame
    void Update()
    {
        //左クリックで取る
        if (Input.GetMouseButton(0))
        {
            //肉たちとレイで判定
            int layerMask = LayerMask.GetMask(new string[] { "NikuYasai" });
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, layerMask))
            {
                //Debug.Log(hit.point);
                grabRigidBodys.Add(hit.rigidbody);//リストに掴んだものを追加
                scoreManager.AddLivingMeetCount();
            }
        }
        else {
            //肉を放す
            grabRigidBodys.Clear();
        }

        foreach (Rigidbody body in grabRigidBodys)
        {
            body.position = tangPoint.transform.position;
        }
    }
}