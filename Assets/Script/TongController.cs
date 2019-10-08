using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongController : MonoBehaviour
{
    public GameObject tangPoint;//掴んだ肉を配置する位置

    bool isShooting = false;
    float shotTime = 0.0f;

    Vector3 originPos;

    float targetDistance;

    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;//原点初期化
    }

    // Update is called once per frame
    void Update()
    {
        //マウスが狙うところにトングを向ける
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.LookAt(mousePos);
        transform.Rotate(Vector3.right*90.0f);

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
            transform.position = originPos;
        }
    }

    public void Shot(float distance)
    {
        isShooting = true; shotTime = 0.0f;
        targetDistance = distance;
    }

    public Vector3 GetTongHandPosition()
    {
        return tangPoint.transform.position;
    }
}
