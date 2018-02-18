using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedGun : MonoBehaviour {

    GameObject SpeedGunText;

	// Use this for initialization
	void Start () {
        this.SpeedGunText = GameObject.Find("SpeedGunText");
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        var rb = GetComponent<Rigidbody>();
        // Unityのvelocityの定義は毎秒メートルであるから、球速表示のためには毎時間キロメートルに変換する必要がある
        float f = rb.velocity.magnitude * (3600f / 1000f);

        // 小数点以下を切り捨てる
        // LogFormatは(表現したい文字列,表現したい数値)の順に記述する
        Debug.LogFormat("球速 = {0}km/h", Mathf.Floor(f));
        this.SpeedGunText.GetComponent<Text>().text = Mathf.Floor(f) + "km/h";

	}
}
