using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pitch : MonoBehaviour {

    // ボールの性質を受け取る一時的な数値
    int BallType;

    // 投球するボール
    public GameObject BallPrefab;
    // 投球する場所
    public Transform Pitcher;

    // ボールの横への投げ分け
    float InOut;
    // ボールの高低差の投げ分け
    float HighLow;
    // ボールのスピード
    float BallSpeed = -5.27f;
    /*
    public void SetBreakingBall(int AssignedNumber)
    {
        BallType = AssignedNumber;

    }
    */

    public void PitchAction(int AssignedNumber)
    {

        // ボールスピードは投球コースの大勢に影響を与えないので先に決める
        BallSpeed = Random.Range(-5.27f, -6.03f);

        BallType = AssignedNumber;

        // プレハブの野球ボールを複製
        GameObject PitchingStart = Instantiate(BallPrefab) as GameObject;
        // どこからボールを投げるかを指定する
        PitchingStart.transform.position = Pitcher.position;

        switch (BallType)
        {
            // 打撃判定の場合 
            case 0:

                InOut = Random.Range(-0.07f, 0.07f);
                HighLow = Random.Range(-0.05f, 0.25f);

                break;

            // ストライクの場合
            case 1:

                InOut = Random.Range(-0.05f, 0.05f);
                HighLow = Random.Range(0f, 0.2f);

                break;

            // ボールの場合
            case 2:

                int BallCourse = Random.Range(0, 3);

                switch (BallCourse)
                {
                    // 全て右打者から見て

                    // 外角に外れた場合
                    case 0:

                        InOut = Random.Range(0.06f, 0.1f);
                        HighLow = Random.Range(-0.1f, 0.3f);

                        break;

                    // 内角に外れた場合
                    case 1:

                        InOut = Random.Range(-0.1f, -0.06f);
                        HighLow = Random.Range(-0.1f, 0.3f);

                        break;

                    // 高めに浮いた場合
                    case 2:

                        InOut = Random.Range(-0.1f, 0.1f);
                        HighLow = Random.Range(0.2f, 0.3f);

                        break;

                    // 低すぎた場合
                    case 3:

                        InOut = Random.Range(-0.1f, 0.1f);
                        HighLow = Random.Range(-0.1f, 0f);

                        break;

                }

                break;

            // デッドボールの場合
            case 3:

                InOut = Random.Range(-0.15f, -0.2f);
                HighLow = Random.Range(-0.05f, 0.25f);

                break;

            // 暴投の場合 
            case 4:

                InOut = Random.Range(0f, 0.3f);
                HighLow = Random.Range(0.35f, 0.5f);

                break;

        }

        // 投球の力を加える
        PitchingStart.GetComponent<Rigidbody>().AddForce(InOut, HighLow, BallSpeed, ForceMode.Impulse);

        // 投球する変化球をランダムに決定
        int WantThrowBall = Random.Range(0, 4);

        // 変化球のメソッドを呼び出す
        PitchingStart.GetComponent<BreakingBall>().SetBreakingBall(WantThrowBall);

        // ボールの性質とswitchに関連づけられたボールは同じであるから、そのまま伝える
        PitchingStart.GetComponent<PassPoint>().SetBallType(BallType);

    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
