﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSprict : MonoBehaviour
{
    public Camera playerCamera;//プレイヤーカメラ
    public TongController tong;//トング
    public ScoreDrawer scoreManager;//スコアを管理するクラス
    public GameObject dysonText;//ダイソンモード時に有効化するオブジェクト
    public Text moveText;//エフェクトテキスト

    public float canCatchTimeSec = 0.125f;//クリックしてからキャッチ可能な時間
    public float canComboTimeSec = 0.5f;//キャッチしてからコンボ可能な時間
    public float catchRenge = 6.0f;//キャッチ可能な最大距離

    float inputTime = 0.0f;//クリック入力時間
    bool isConbo = false;//コンボ中か?

    bool isDyson = false;//ダイソンか?

    bool isExplosion = false;

    //掴んでいる肉々
    List<Rigidbody> grabRigidBodys = new List<Rigidbody>();
    List<GameObject> grabGameObjects = new List<GameObject>();

    //レイヤー
    int nikuLayer;
    int grabbingNikuLayer;
    int hoboDeathNikuLayer;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;//マウスカーソルを非表示に

        //レイヤー初期化
        nikuLayer = LayerMask.NameToLayer("NikuYasai");
        grabbingNikuLayer = LayerMask.NameToLayer("GrabbingNikuYasai");
        hoboDeathNikuLayer = LayerMask.NameToLayer("HoboDeathNiku");
    }

    // Update is called once per frame
    void Update()
    {
        //左クリックで取る
        if (Input.GetMouseButton(0))
        {
            bool isCatch = false;

            //コンボ終了
            if (inputTime > canComboTimeSec) {
                ConboEnd();
            }

            //入力時間がキャッチ可能時間以内なら
            //またはコンボ中なら
            if (inputTime < canCatchTimeSec || isConbo)
            {
                //入力時間加算
                inputTime += Time.deltaTime;

                //肉たちとレイで判定
                int layerMask;
                if (isDyson)// || isConbo && inputTime < canComboTimeSec)//ダイソンモード or コンボ中 なら
                {
                    //肉の上に落ちた肉掴める
                    layerMask = LayerMask.GetMask(new string[] { LayerMask.LayerToName(nikuLayer), LayerMask.LayerToName(hoboDeathNikuLayer) });
                }
                else
                {
                    layerMask = LayerMask.GetMask(new string[] { LayerMask.LayerToName(nikuLayer) });
                }
                Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray.origin, ray.direction, out hit, catchRenge, layerMask))//Mathf.Infinity
                {
                    //落下中の肉ならスコア加算
                    if (hit.collider.gameObject.layer == nikuLayer)
                    {
                        int score=1;
                        //高さに応じたスコア加算
                        const float nikuHeightMax = 5.0f - 1.0f;//高いところの敷居
                        const float nikuHeightMin = 1.0f;       //低いところの敷居
                        if (hit.rigidbody.position.y > nikuHeightMax)//高いところで取った
                        {
                            score += 9; scoreManager.AddFastKillCnt();
                            //エフェクト
                            Text text = Instantiate(moveText);
                            text.transform.parent = scoreManager.gameObject.transform;
                            MovingText txt = text.GetComponent<MovingText>();
                            txt.Init("FastKill +9", hit.rigidbody.position);
                            txt.ReverxeDirection();
                        }
                        if (hit.rigidbody.position.y < nikuHeightMin)//地面スレスレで取った
                        {
                            score += 2; scoreManager.AddGiriGiriKillCnt();
                            //エフェクト
                            Text text = Instantiate(moveText);
                            text.transform.parent = scoreManager.gameObject.transform;
                            text.GetComponent<MovingText>().Init("GiriGiri +2", hit.rigidbody.position);
                        }
                        //加算
                        scoreManager.AddScore(score);
                    }

                    isCatch = true;
                    grabRigidBodys.Add(hit.rigidbody);//リストに掴んだものを追加
                    grabGameObjects.Add(hit.collider.gameObject);
                    scoreManager.AddLivingMeetCount();//スコア加算
                    hit.collider.gameObject.layer = grabbingNikuLayer;//掴んだ肉のレイヤー変更

                    //肉の抗力変更
                    hit.rigidbody.drag = 0.0f;

                    //爆弾モードなら焼き肉にする
                    if (isExplosion)
                    {
                        NikuScript niku = hit.collider.gameObject.GetComponent<NikuScript>();
                        if (niku) { niku.Yakiniku(); }                        
                    }

                    //入力時間リセット
                    inputTime = 0.0f;
                    //コンボ中にする
                    isConbo = true;
                    scoreManager.AddConbo();
                    if (scoreManager.GetConbo() > 1)
                    {
                        //コンボの追加特典
                        scoreManager.AddScore(scoreManager.GetConbo()-1);
                    }

                    //トングのアニメーション
                    //tong.Shot((hit.rigidbody.position - tong.GetTongHandPosition()).magnitude);
                }
            }

            //肉をキャッチできなかった
            if (!isCatch)
            {

            }

            //中クリックで肉を爆弾にする
            if ((Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)) && grabGameObjects.Count > 0)
            {
                isExplosion = true;
                foreach (GameObject go in grabGameObjects)
                {
                    NikuScript niku = go.GetComponent<NikuScript>();
                    if (niku) { niku.Yakiniku(); }
                }
            }
        }
        else {
            //入力時間リセット
            inputTime = 0.0f;
            //コンボ終了
            ConboEnd();

            //肉を放す
            foreach (GameObject go in grabGameObjects)
            {
                go.layer = nikuLayer;//掴んでる肉のレイヤー戻す
                //肉を爆弾にする
                if (isExplosion)
                {
                    NikuScript niku = go.GetComponent<NikuScript>();
                    if (niku) { niku.SetIsExplosion(); }
                }
            }
            grabRigidBodys.Clear();//リストをクリア
            grabGameObjects.Clear();
            isExplosion = false;
        }

        //掴んだ肉の位置を固定
        foreach (Rigidbody body in grabRigidBodys)
        {
            body.position = tong.GetTongHandPosition();
        }

        //ダイソンモード
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isDyson = true;
            dysonText.SetActive(true);
            canCatchTimeSec = Mathf.Infinity;
            catchRenge = Mathf.Infinity;
        }
    }

    /// <summary>
    /// コンボ終了
    /// </summary>
    void ConboEnd()
    {
        isConbo = false;
        scoreManager.ResetConbo();
    }
}