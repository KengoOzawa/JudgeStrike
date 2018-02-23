using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedGunSimple : MonoBehaviour
{

    GameObject SpeedGunText;
    // Updateは1フレームごとに実装されることから数値を蓄積すると考える
    int frameCount = 0;

    // Use this for initialization
    void Start()
    {
        this.SpeedGunText = GameObject.Find("SpeedGunText");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        frameCount += 1;

        // 1フレーム目ではボールが動いていないため2フレーム目の球速を取得する
        if (frameCount == 2)
        {
            // 2フレーム目の処理
            var rb = GetComponent<Rigidbody>();

            // Unityのvelocityの定義は毎秒メートルであるから、球速表示のためには毎時間キロメートルに変換する必要がある
            float f = rb.velocity.magnitude * (3600f / 1000f);

            // 小数点以下を切り捨てる
            // UIにも球速を表示する
            this.SpeedGunText.GetComponent<Text>().text = Mathf.Floor(f) + "km/h";
        }


    }
}