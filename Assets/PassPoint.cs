using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassPoint : MonoBehaviour
{
    int BallType;

    // PassPointはボールにアタッチされたスクリプトのため、BaseBallActionを見つける必要がある
    GameObject GetBaseBallAction;

    // public GameObject TestBallPrefab3;

    // 判定を一度だけ実行する
    bool Judge;

    // ボールの性質がどのようであるかを判定側に渡す
    public void SetBallType(int AssignedNumber)
    {
        BallType = AssignedNumber;
    }

    // Use this for initialization
    void Start()
    {
        GetBaseBallAction = GameObject.Find("BaseBall3");
    }

    // Update is called once per frame
    public void FixedUpdate()
    {

        // 投じられたボールの座標がホームベースの先端に侵入したら判定を行う
        if (this.gameObject.transform.position.z < 0.4316676)
        {

            switch (BallType)
            {
                // 打撃判定の場合 
                case 0:
                    
                    if (Judge == false)
                    {
                        Judge = true;

                        // 通過した時点のボールの座標を取得する
                        Vector3 BattedPoint = this.transform.position;
                        this.transform.position = new Vector3(BattedPoint.x, BattedPoint.y, BattedPoint.z);

                        // BaseBallActionに打球を生成する点をわたす
                        GetBaseBallAction.GetComponent<BaseBallAction>().SetBattedPoint(BattedPoint);

                        Destroy(this.gameObject);

                    }

                    break;

                // ストライクの場合
                case 1:

                    if (Judge == false)
                    {
                        Judge = true;


                        Debug.Log("ストライク!(ポイント判定)");

                    }

                    break;

                // ボールの場合
                case 2:

                    if (Judge == false)
                    {
                        Judge = true;


                        Debug.Log("ボール!(ポイント判定)");

                    }

                    break;

                // デッドボールの場合
                case 3:

                    break;

                // 暴投の場合 
                case 4:

                    break;

            }

        }

    }
}
