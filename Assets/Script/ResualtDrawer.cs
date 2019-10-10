using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResualtDrawer : MonoBehaviour
{
    public Text livingText, deathText, hoboDeathText, saraText, scoreText, iroiroText;
    ResultData resultData;

    // Start is called before the first frame update
    void Start()
    {
        resultData = ScoreDrawer.GetResultData();
    }

    // Update is called once per frame
    void Update()
    {
        livingText.text = resultData.livingMeet.ToString() + "肉取った";
        hoboDeathText.text = resultData.hoboDeathMeat.ToString() + "肉ほぼ死亡";
        deathText.text = resultData.deathMeet.ToString() + "肉死亡";
        saraText.text = resultData.saraNiku.ToString() + "肉皿に乗った";
        scoreText.text = "SCORE : " + resultData.score.ToString();

        iroiroText.text = "ファストキル:" + resultData.fastKill.ToString("D3") + "\n"
            + "ギリギリキル:" + resultData.girigiriKill.ToString("D3") + "\n"
            + "最大コンボ:" + resultData.maxConbo.ToString("D3") + "\n"
            + "\n" + "的" + "\n"
            + "10点×" + resultData.matoCnt10.ToString("D3") + "\n"
            + "100点×" + resultData.matoCnt100.ToString("D3") + "\n"
            + "100000点×" + resultData.matoCnt100000.ToString("D3") + "\n";
    }
}
