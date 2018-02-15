using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pitching : MonoBehaviour
{

    public GameObject BallPrefab;
    public Transform Pitcher;
    public float BallSpeed = 2.0f; 
 
    // Update is called once per frame
    void Update()
    {

        // 投球スピードを加速
        if (Input.GetKeyDown(KeyCode.K))
        {
            BallSpeed += 0.1f;
        }
        // 投球スピードを減速
        if (Input.GetKeyDown(KeyCode.H))
        {
            BallSpeed -= 0.1f;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            // プレハブの野球ボールを複製
            GameObject Pitching = GameObject.Instantiate(BallPrefab) as GameObject;
            // どこからボールを投げるかを指定する
            Pitching.transform.position = Pitcher.position;

            // 投球のコースをランダムに決める
            float Wide01 = Random.Range(-43, -47);
            // 投球のコースをランダムに決める(Wide01との違いがよくわかっていない)
            float Wide02 = Random.Range(-43, -47);
            // 投球の角度は少し前投げ下ろす程度
            float Angle = Random.Range(3, -10);

            // Rigidbodyに力を加えて投球
            // AddForceは最後にForceMode.Force(何も書かなければこちらがデフォルト)かForceMode.Impulseを選べる
            Pitching.GetComponent<Rigidbody>().AddForce(Wide02 * BallSpeed, Angle, Wide01 * BallSpeed, ForceMode.Force);
        }

    }
}