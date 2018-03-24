using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictionGroundBall : MonoBehaviour {

    public GameObject Point;

    bool isCalled;

    bool PredictionStart;

    GameObject CallFirst;

    public void PredictionTrigger(){
        PredictionStart = true;
    }

	// Use this for initialization
	void Start () {
        this.CallFirst = GameObject.Find("SampleFirst");
	}
	
	// Update is called once per frame
	void Update () {
		
        if(PredictionStart == true){

            if (this.transform.position.z > 4)
            {

                if (isCalled == false)
                {
                    isCalled = true;

                    // 通過した時点のボールの座標を取得する
                    Vector3 FindOwnPosition = this.transform.position;
                    this.transform.position = new Vector3(FindOwnPosition.x, FindOwnPosition.y, FindOwnPosition.z);

                    Debug.Log("生成した位置 : " + FindOwnPosition);

                    // ゴロの打球をたてをz,よこをyとするの比例式としてとらえ、係数をかけることで予測点とする
                    Vector3 PredictionPoint = new Vector3(FindOwnPosition.x * 6, FindOwnPosition.y, FindOwnPosition.z * 6);

                    GameObject Pitching = Instantiate(Point) as GameObject;
                    Pitching.transform.position = PredictionPoint;

                    // ここがミス。親オブジェクトからボールの現在位置を取得してくる必要がある
                    Transform BallPosition = Pitching.transform;

                    Debug.Log("予測した位置 : " + PredictionPoint);

                    Transform x = Pitching.transform;

                    CallFirst.GetComponent<FieldingFirst>().ChaseStartTrigger(x, BallPosition);

                }

            }

        }


	}
}
