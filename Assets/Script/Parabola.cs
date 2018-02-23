using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabola : MonoBehaviour
{

    public GameObject HittingBallPrefab;
    public Transform HittingPivot;

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.T))
        {
            // プレハブの野球ボールを複製
            GameObject Pitching = Instantiate(HittingBallPrefab) as GameObject;
            // どこからボールを投げるかを指定する
            Pitching.transform.position = HittingPivot.position;

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            float Wide01 = Random.Range(-30.0f, 30.0f);
            // 打球の角度
            float Angle = Random.Range(5.0f, 50.0f);
            // 打球がファールになるか、フェアになるかを決める
            // このケースではx<zのため必ずフェアになる
            float Wide02 = Random.Range(Wide01, 30.0f);

            // Rigidbodyに力を加えて投球
            // AddForceは最後にForceMode.Force(何も書かなければこちらがデフォルト)かForceMode.Impulseを選べる
            Pitching.GetComponent<Rigidbody>().AddForce(Wide01, Angle, Wide02, ForceMode.Impulse);
        }


    }
}