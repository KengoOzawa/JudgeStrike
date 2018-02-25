using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeBattedBall : MonoBehaviour {
    
    GameObject JudgePoint;

    GameObject GetBaseBall;

    bool isCalled = false;

	// Use this for initialization
	void Start () {
        this.JudgePoint = GameObject.Find("JudgePoint");

        this.GetBaseBall = GameObject.Find("BaseBall2");
	}
	
	// Update is called once per frame
	void Update () {
		
        // HittingPivotと同一点上に存在するJudgePointと座標で比較を行う
        if (this.gameObject.transform.position.z - this.JudgePoint.transform.position.z < 0)
        {

            // bool型を使うことで一度だけ実行される
            if (isCalled == false)
            {
                isCalled = true;
                // 他のオブジェクトにアタッチされたスクリプトを呼び出す
                GetBaseBall.GetComponent<BaseBall>().PitchingBatting();
            }

        }

	}
}
