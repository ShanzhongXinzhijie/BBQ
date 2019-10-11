using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongController : MonoBehaviour
{
    public GameObject tangPoint;//掴んだ肉を配置する位置
    public GameObject fireEffect;//炎エフェクト

    GameObject fire;

    bool isShooting = false;
    float shotTime = 0.0f;

    Vector3 originPos;

    float targetDistance;

    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.localPosition;//原点初期化
    }

    // Update is called once per frame
    void Update()
    {
        //マウスが狙うところにトングを向ける
        Vector3 mousePos = Input.mousePosition;
        //トングを持つ手切り替え
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
        {
            transform.localPosition = new Vector3(transform.localPosition.x  * - 1.0f, transform.localPosition.y, transform.localPosition.z);
            originPos = transform.localPosition;
            //mousePos.x -= Screen.width / 2; mousePos.y -= Screen.height / 2;
            //mousePos *= 2.0f;
            //mousePos.x += Screen.width / 2; mousePos.y += Screen.height / 2;
        }
        mousePos.z = 10;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.LookAt(mousePos);
        transform.Rotate(Vector3.right*(90.0f));
        transform.Rotate(Vector3.forward * 90.0f);

        //射撃
        const float shotMaxTimeSec = 0.1f;
        if (isShooting)
        {
            shotTime += Time.deltaTime;
            if(shotTime > shotMaxTimeSec) { shotTime = shotMaxTimeSec; isShooting = false; }
            transform.position = originPos + transform.up * targetDistance;// *(shotTime / shotMaxTimeSec));
        }
        else
        {
            transform.localPosition = originPos;
        }

        //ファイアエフェクト位置
        if (fire)
        {
            fire.transform.position = GetTongHandPosition();
        }
    }

    //射撃アニメーションする
    public void Shot(float distance)
    {
        isShooting = true; shotTime = 0.0f;
        targetDistance = distance;
    }

    //トングの先端座標取得
    public Vector3 GetTongHandPosition()
    {
        return tangPoint.transform.position;
    }

    //エンチャントファイア
    public void EnchantFire()
    {
        if (!fire)
        {
            fire = Instantiate(fireEffect, GetTongHandPosition(), Quaternion.identity);
            fire.transform.localScale = Vector3.one * 2.0f;
            GetComponent<AudioSource>().Play();//効果音
        }
    }
    public void DisenchantFire()
    {
        if (fire)
        {
            Destroy(fire);
        }
    }
}
