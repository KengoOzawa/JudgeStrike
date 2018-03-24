using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeBattedBall : MonoBehaviour {
    
    GameObject JudgePoint;

    GameObject GetBaseBall;

    public GameObject TestBallPrefab3;

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

                // 通過した時点のボールの座標を取得する
                Vector3 FindOwnPosition = this.transform.position;
                this.transform.position = new Vector3(FindOwnPosition.x, FindOwnPosition.y, FindOwnPosition.z);

                // ホームベースの前方(JudgePointと同位置)から打球が生成されるような座標を生成する
                // 現在は使用していない
                Vector3 PreHittingPivot = new Vector3(FindOwnPosition.x, FindOwnPosition.y, 0.4316676f);

                // 他のオブジェクトにアタッチされたスクリプトを呼び出す
                GetBaseBall.GetComponent<BaseBall>().PitchingBatting(FindOwnPosition);

            }

        }

	}
}
