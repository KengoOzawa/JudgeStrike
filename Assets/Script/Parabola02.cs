﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabola02 : MonoBehaviour {

    private Rigidbody FirstPosition;

	// Use this for initialization
	void Start () {

        FirstPosition = GetComponent<Rigidbody>();
        // シーン開始時は重力を働かせない
        FirstPosition.isKinematic = true;
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.P))
        {
                
            FirstPosition.isKinematic = false;

            // 打球のコースをランダムに決める
            float Wide01 = Random.Range(0f, 90.0f);
            // 打球のコースをランダムに決める(Wide01との違いがよくわかっていない)
            float Wide02 = Random.Range(0f, 90.0f);
            // 打球の角度
            float Angle = Random.Range(180.0f, 60.0f);
            // 打球の距離
            float BallPower = Random.Range(0f, 2.0f);

            Vector3 force;
            // アタッチされた物体の位置情報を取得
            force = this.gameObject.transform.forward;
            // Rigidbodyに力を加えて発射
            GetComponent<Rigidbody>().AddForce(Wide02 * BallPower, Angle, Wide01 * BallPower, ForceMode.Force);

        }

	}
}
