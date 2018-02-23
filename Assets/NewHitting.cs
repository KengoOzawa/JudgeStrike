using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewHitting : MonoBehaviour
{

    public GameObject HittingBallPrefab;
    public Transform HittingPivot;

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.N))
        {
            // 目標である全方向への打球の実装完了。
            // 本スクリプトは全ての打球が必ずフェアになるように設定されている

            float check = Random.Range(0, 1.0f) * 100;

            if (check < 48.4)
            {
                Debug.Log("ゴロでした");

                // プレハブの野球ボールを複製
                GameObject Pitching = GameObject.Instantiate(HittingBallPrefab) as GameObject;
                // どこからボールを投げるかを指定する
                Pitching.transform.position = HittingPivot.position;

                // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
                float Wide01 = Random.Range(-1.0f, 1.0f);
                // Debug.Log(Wide01);
                // 打球の角度
                float Angle = Random.Range(-0.1f, 0f);
                // 打球がファールになるか、フェアになるかを決める
                // このケースでは絶対値を取得してx<zのため必ずフェアになる
                float Wide02 = Random.Range(Mathf.Abs(Wide01), 1.0f);
                // Debug.Log(Wide02);

                // Rigidbodyに力を加えて投球
                // AddForceは最後にForceMode.Force(何も書かなければこちらがデフォルト)かForceMode.Impulseを選べる
                Pitching.GetComponent<Rigidbody>().AddForce(Wide01, Angle, Wide02, ForceMode.Impulse);
            }
            else if (check < 48.4 + 8.3)
            {
                Debug.Log("ライナーでした");

                // プレハブの野球ボールを複製
                GameObject Pitching = GameObject.Instantiate(HittingBallPrefab) as GameObject;
                // どこからボールを投げるかを指定する
                Pitching.transform.position = HittingPivot.position;

                // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
                float Wide01 = Random.Range(-1.0f, 1.0f);
                // Debug.Log(Wide01);
                // 打球の角度
                float Angle = Random.Range(0f, 0.2f);
                // 打球がファールになるか、フェアになるかを決める
                // このケースでは絶対値を取得してx<zのため必ずフェアになる
                float Wide02 = Random.Range(Mathf.Abs(Wide01), 1.0f);
                // Debug.Log(Wide02);

                // Rigidbodyに力を加えて投球
                // AddForceは最後にForceMode.Force(何も書かなければこちらがデフォルト)かForceMode.Impulseを選べる
                Pitching.GetComponent<Rigidbody>().AddForce(Wide01 * 1.1f, Angle, Wide02 * 1.1f, ForceMode.Impulse);
            
            }
            else
            {
                Debug.Log("フライでした");

                // プレハブの野球ボールを複製
                GameObject Pitching = GameObject.Instantiate(HittingBallPrefab) as GameObject;
                // どこからボールを投げるかを指定する
                Pitching.transform.position = HittingPivot.position;

                // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
                float Wide01 = Random.Range(-1.0f, 1.0f);
                // Debug.Log(Wide01);
                // 打球の角度
                float Angle = Random.Range(0f, 1.0f);
                // 打球がファールになるか、フェアになるかを決める
                // このケースでは絶対値を取得してx<zのため必ずフェアになる
                float Wide02 = Random.Range(Mathf.Abs(Wide01), 1.0f);
                // Debug.Log(Wide02);

                // Rigidbodyに力を加えて投球
                // AddForceは最後にForceMode.Force(何も書かなければこちらがデフォルト)かForceMode.Impulseを選べる
                Pitching.GetComponent<Rigidbody>().AddForce(Wide01, Angle, Wide02, ForceMode.Impulse);
            
            }

        }


    }
}