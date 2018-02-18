using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabola : MonoBehaviour
{

    public GameObject HittingBallPrefab;
    public Transform HittingPivot;

    // 打ち上げたボールのもっとも高い角度
    public float UpperLimits = 180.0f;
    // ゴロを打ったとき
    public float LowerLimits = 60.0f;


    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.T))
        {
            // プレハブの野球ボールを複製
            GameObject Pitching = GameObject.Instantiate(HittingBallPrefab) as GameObject;
            // どこからボールを投げるかを指定する
            Pitching.transform.position = HittingPivot.position;

            // 投球のコースをランダムに決める
            float Wide01 = Random.Range(0f, 90.0f);
            // 投球のコースをランダムに決める(Wide01との違いがよくわかっていない)
            float Wide02 = Random.Range(0f, 90.0f);
            // 打球の角度
            float Angle = Random.Range(UpperLimits, LowerLimits);
            // 打球の距離
            float BallPower = Random.Range(0f, 2.0f);

            // Rigidbodyに力を加えて投球
            // AddForceは最後にForceMode.Force(何も書かなければこちらがデフォルト)かForceMode.Impulseを選べる
            Pitching.GetComponent<Rigidbody>().AddForce(Wide02 * BallPower, Angle, Wide01 * BallPower, ForceMode.Force);
        }


    }
}