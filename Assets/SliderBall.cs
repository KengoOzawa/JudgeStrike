using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderBall : MonoBehaviour {

    GameObject JudgePoint;

	// Use this for initialization
	void Start () {
        JudgePoint = GameObject.Find("JudgePoint");
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {

        // マウンドとホームベース上の距離から変化する地点を決定する
        if ((this.gameObject.transform.position.z - this.JudgePoint.transform.position.z < 18.0) && (0 < this.gameObject.transform.position.z - this.JudgePoint.transform.position.z))
        {
            GetComponent<Rigidbody>().AddForce(-0.0015f, 0f, 0f, ForceMode.Impulse);

        }

    }
}
