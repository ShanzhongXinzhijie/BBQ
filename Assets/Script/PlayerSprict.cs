using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprict : MonoBehaviour
{
    public Camera playerCamera;//プレイヤーカメラ
    public TongController tong;//トング
    public ScoreDrawer scoreManager;//スコアを管理するクラス

    //掴んでいる肉々
    List<Rigidbody> grabRigidBodys = new List<Rigidbody>();
    List<GameObject> grabGameObjects = new List<GameObject>();

    //レイヤー
    int nikuLayer;
    int grabbingNikuLayer;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;//マウスカーソルを非表示に

        //レイヤー初期化
        nikuLayer = LayerMask.NameToLayer("NikuYasai");
        grabbingNikuLayer = LayerMask.NameToLayer("GrabbingNikuYasai");
    }

    // Update is called once per frame
    void Update()
    {
        //左クリックで取る
        if (Input.GetMouseButton(0))
        {
            //肉たちとレイで判定
            int layerMask = LayerMask.GetMask(new string[] { LayerMask.LayerToName(nikuLayer) });
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, layerMask))
            {
                //Debug.Log(hit.point);
                grabRigidBodys.Add(hit.rigidbody);//リストに掴んだものを追加
                grabGameObjects.Add(hit.collider.gameObject);
                scoreManager.AddLivingMeetCount();//スコア加算
                hit.collider.gameObject.layer = grabbingNikuLayer;//掴んだ肉のレイヤー変更

                //トングのアニメーション
                //tong.Shot((hit.rigidbody.position - tong.GetTongHandPosition()).magnitude);
            }
        }
        else {
            //肉を放す
            foreach (GameObject go in grabGameObjects)
            {
                go.layer = nikuLayer;//掴んでる肉のレイヤー戻す
            }
            grabRigidBodys.Clear();//リストをクリア
            grabGameObjects.Clear();
        }

        //掴んだ肉の位置を固定
        foreach (Rigidbody body in grabRigidBodys)
        {
            body.position = tong.GetTongHandPosition();
        }
    }
}