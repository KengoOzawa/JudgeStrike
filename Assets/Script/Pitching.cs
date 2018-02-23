using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pitching : MonoBehaviour
{

    public GameObject BallPrefab;
    public Transform Pitcher;
    public float BallSpeed = 1.0f;

    /*

    // 投球コースのもっとも高いところ
    public float UpperLimits = 5.0f;
    // 投球コースのもっとも低いところ
    public float LowerLimits = -2.0f;

    */

    // Update is called once per frame
    void Update()
    {

        /*

        // 投球スピードを加速し、投球角度を下げる
        if (Input.GetKeyDown(KeyCode.K))
        {
            BallSpeed += 0.001f;
            UpperLimits -= 1.0f;
            LowerLimits -= 1.0f;
        }
        // 投球スピードを減速し、投球角度をあげる
        if (Input.GetKeyDown(KeyCode.H))
        {
            BallSpeed -= 0.001f;
            UpperLimits += 1.0f;
            LowerLimits += 1.0f;
        }

        */

        if (Input.GetKeyDown(KeyCode.J))
        {
            // プレハブの野球ボールを複製
            GameObject PitchingStart = Instantiate(BallPrefab) as GameObject;
            // どこからボールを投げるかを指定する
            PitchingStart.transform.position = Pitcher.position;


            /*
            // 投球のコースをランダムに決める
            float Wide01 = Random.Range(-43.0f, -47.0f);
            // 投球のコースをランダムに決める(Wide01との違いがよくわかっていない)
            float Wide02 = Random.Range(-43.0f, -47.0f);
            // 投球の角度
            float Angle = Random.Range(UpperLimits, LowerLimits);
            */

            // Rigidbodyに力を加えて投球
            // AddForceは最後にForceMode.Force(何も書かなければこちらがデフォルト)かForceMode.Impulseを選べる
            // Pitching.GetComponent<Rigidbody>().AddForce(Wide02 * BallSpeed, Angle * BallSpeed, Wide01 * BallSpeed, ForceMode.Impulse);

            PitchingStart.GetComponent<Rigidbody>().AddForce(0f * BallSpeed, 0f * BallSpeed, -1.8105f * BallSpeed, ForceMode.Impulse);

        }


    }
}