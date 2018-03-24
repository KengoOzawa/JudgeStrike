using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBallAction : MonoBehaviour {

    // 打球として生成されるオブジェクト
    public GameObject BattedBallPrefab;
    // 打球を生成する位置
    Vector3 BattedPoint;

    // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
    float Wide01;
    // 打球の角度。大きくなるほど高いフライになる
    float Angle;
    // 打球がファールになるか、フェアになるかを決める
    float Wide02;

    float stopTime;

    float gp = 0;

    [SerializeField]
    GameObject dummySphere;

    // 打球の性質と同値のTriggerがtrueになると対応した打球が生成される
    bool[] Trigger = new bool[10];

    // 対応するTriggerと本boolの双方がtrueになることで打球が生成される
    bool GenerateBattedBall;

    int BattedBallType;

    // 前の打球を消去する処理。
    public void DestroyFormerBall()
    {
        // 前の打球を削除
        GameObject[] FormerBallDestroy = GameObject.FindGameObjectsWithTag("BattedBallPrefab");
        foreach (GameObject obj in FormerBallDestroy)
        {
            Destroy(obj);
        }
    }

    // PassPointより受けた座標を打球の生成点とする処理。
    public void SetTrigger(int Assigned_Number)
    {

        BattedBallType = Assigned_Number;

        Trigger[BattedBallType] = true;
    }

    // PassPointより受けた座標を打球の生成点とする処理。
    public void SetBattedPoint(Vector3 Assigned_Vector3)
    {
        BattedPoint = Assigned_Vector3;

        // 生成した打球点の情報を伝える
        GenerateBattedBall = true;
    }

    // Trigger = 0
    // ファウルの打球の処理。
    public void Foul()
    {

        GameObject Batted = Instantiate(BattedBallPrefab) as GameObject;

        Batted.transform.position = BattedPoint;

        int BattedBallCourse = Random.Range(0, 6);

        // ファウルボールがどのコースに飛ぶか決める
        switch (BattedBallCourse)
        {
            
            // 後方に飛んだ場合
            case 0:

                // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
                Wide01 = Random.Range(-30.0f, 30.0f);
                // 打球の角度
                Angle = Random.Range(-5.0f, 20.0f);
                // 打球がファールになるか、フェアになるかを決める
                Wide02 = Random.Range(-10.0f, -30.0f);

                break;

            // レフト側(グラウンド外)に飛んだ場合
            case 1:

                // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
                Wide01 = Random.Range(-20.0f, -30.0f);
                // 打球の角度
                Angle = Random.Range(10.0f, 20.0f);
                // 打球がファールになるか、フェアになるかを決める
                Wide02 = Random.Range(10.0f, 15.0f);
                break;

            // ライト側(グラウンド外)に飛んだ場合
            case 2:

                // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
                Wide01 = Random.Range(20.0f, 30.0f);
                // 打球の角度
                Angle = Random.Range(10.0f, 20.0f);
                // 打球がファールになるか、フェアになるかを決める
                Wide02 = Random.Range(10.0f, 15.0f);

                break;

            // レフト側(グラウンド内ゴロ)に飛んだ場合
            case 3:

                // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
                Wide01 = Random.Range(-5.0f, -20.0f);
                // 打球の角度
                Angle = Random.Range(-5.0f, 0f);
                // 打球がファールになるか、フェアになるかを決める
                Wide02 = Random.Range(0f, Mathf.Abs(Wide01) - 1.0f);

                break;

            // ライト側(グラウンド内ゴロ)に飛んだ場合
            case 4:

                // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
                Wide01 = Random.Range(5.0f, 20.0f);
                // 打球の角度
                Angle = Random.Range(-5.0f, 0f);
                // 打球がファールになるか、フェアになるかを決める
                Wide02 = Random.Range(0f, Mathf.Abs(Wide01) - 1.0f);

                break;

                // レフト側(グラウンド内ライナー)に飛んだ場合
            case 5:

                // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
                Wide01 = Random.Range(-20.0f, -30.0f);
                // 打球の角度
                Angle = Random.Range(4.0f, 10.0f);
                // 打球がファールになるか、フェアになるかを決める
                Wide02 = Random.Range(10.0f, 20.0f);

                break;

            // ライト側(グラウンド内ライナー)に飛んだ場合
            case 6:

                // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
                Wide01 = Random.Range(20.0f, 30.0f);
                // 打球の角度
                Angle = Random.Range(4.0f, 10.0f);
                // 打球がファールになるか、フェアになるかを決める
                Wide02 = Random.Range(10.0f, 20.0f);

                break;

        }


        // Rigidbodyに力を加えて投球
        Batted.GetComponent<Rigidbody>().AddForce(Wide01, Angle, Wide02, ForceMode.Impulse);

        // 打球を生成したため、次の打球が判定されるまではfalseとする
        Trigger[0] = false;

    }

    // Trigger = 1
    // ホームランの打球の処理。
    public void HomeRun()
    {

        GameObject Batted = Instantiate(BattedBallPrefab) as GameObject;

        Batted.transform.position = BattedPoint;

        int BattedBallCourse = Random.Range(0, 4);

        // ホームランがどのコースに飛ぶか決める
        switch (BattedBallCourse)
        {

            // レフトに飛んだ場合
            case 0:

                // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
                Wide01 = Random.Range(-20.0f, -15.0f);
                // 打球の角度
                Angle = Random.Range(20.0f, 25.0f);
                // 打球がファールになるか、フェアになるかを決める
                Wide02 = Random.Range(20.0f, 25.0f);

                break;

            //左中間に飛んだ場合
            case 1:

                // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
                Wide01 = Random.Range(-15.0f, -10.0f);
                // 打球の角度
                Angle = Random.Range(20.0f, 25.0f);
                // 打球がファールになるか、フェアになるかを決める
                Wide02 = Random.Range(30.0f, 35.0f);

                break;

            // バックスクリーンに飛んだ場合
            case 2:

                // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
                Wide01 = Random.Range(-5.0f, 5.0f);
                // 打球の角度
                Angle = Random.Range(20.0f, 25.0f);
                // 打球がファールになるか、フェアになるかを決める
                Wide02 = Random.Range(35.0f, 45.0f);

                break;

            // 右中間に飛んだ場合
            case 3:

                // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
                Wide01 = Random.Range(10.0f, 15.0f);
                // 打球の角度
                Angle = Random.Range(20.0f, 25.0f);
                // 打球がファールになるか、フェアになるかを決める
                Wide02 = Random.Range(30.0f, 35.0f);

                break;

            // ライトに飛んだ場合
            case 4:

                // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
                Wide01 = Random.Range(15.0f, 20.0f);
                // 打球の角度
                Angle = Random.Range(20.0f, 25.0f);
                // 打球がファールになるか、フェアになるかを決める
                Wide02 = Random.Range(20.0f, 25.0f);

                break;

        }



        // Rigidbodyに力を加えて投球
        Batted.GetComponent<Rigidbody>().AddForce(Wide01, Angle, Wide02, ForceMode.Impulse);

        // 打球を生成したため、次の打球が判定されるまではfalseとする
        Trigger[1] = false;

    }

    // Trigger = 2
    // ゴロ性の打球がヒットになったときの処理。
    void GroundBallHit()
    {

        GameObject Batted = Instantiate(BattedBallPrefab) as GameObject;

        Batted.transform.position = BattedPoint;

        float CheckGroundBallHit = Random.Range(0, 1.0f) * 100;

        if (CheckGroundBallHit < 20.0)
        {
            Debug.Log("1塁線を抜けるゴロ性の当たり!");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(30.0f, 35.0f);
            // 打球の角度
            Angle = Random.Range(0f, -5.0f);
            // 一塁線を破るためにほぼ同値とする
            Wide02 = Wide01;

        }
        else if (CheckGroundBallHit < 15.0 + 20.0)
        {
            Debug.Log("1,2塁間を抜けるゴロ性の当たり!");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(25.0f, 30.0f);
            // 打球の角度
            Angle = Random.Range(-5.0f, 0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = Mathf.Abs(Wide01) + 20.0f;

        }
        else if (CheckGroundBallHit < 35.0 + 20.0)
        {
            Debug.Log("センター前に抜けるゴロ性の当たり!");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(-5.0f, 5.0f);
            // 打球の角度
            Angle = Random.Range(-5.0f, 0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = Random.Range(30.0f, 45.0f);

        }
        else if (CheckGroundBallHit < 55.0 + 20.0)
        {
            Debug.Log("三遊間を抜けるゴロ性の当たり!");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(-25.0f, -30.0f);
            // 打球の角度
            Angle = Random.Range(-5.0f, 0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = Mathf.Abs(Wide01) + 20.0f;

        }
        else if (CheckGroundBallHit < 75.0 + 15.0)
        {
            Debug.Log("3塁線を抜けるゴロ性の当たり!");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(-30.0f, -35.0f);
            // 打球の角度
            Angle = Random.Range(0f, -5.0f);
            // 三塁線を破るためにWide01の絶対値と同値とする
            Wide02 = Mathf.Abs(Wide01);


        }
        else
        {
            Debug.Log("ピッチャーの前に落ちるラッキーなゴロ性の当たり!");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(-10.0f, -0.0f);
            // 打球の角度
            Angle = Random.Range(-5.0f, 0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = 10.0f;
        }

        // Rigidbodyに力を加えて投球
        Batted.GetComponent<Rigidbody>().AddForce(Wide01, Angle, Wide02, ForceMode.Impulse);

        // 打球を生成したため、次の打球が判定されるまではfalseとする
        Trigger[2] = false;

    }

    // Trigger = 3
    // ライナー性の打球がシングルヒットになったときの処理。
    void LineBallHit()
    {

        GameObject Batted = Instantiate(BattedBallPrefab) as GameObject;

        Batted.transform.position = BattedPoint;

        float CheckLineBallHit = Random.Range(0, 1.0f) * 100;

        if (CheckLineBallHit < 20.0)
        {
            Debug.Log("1塁線を抜けるライナー性の当たり!");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(30.0f, 35.0f);
            // 打球の角度
            Angle = Random.Range(5.0f, 8.0f);
            // 一塁線を破るためにほぼ同値とする
            Wide02 = Wide01;

        }
        else if (CheckLineBallHit < 20.0 + 20.0)
        {
            Debug.Log("1,2塁間を抜けるライナー性の当たり!");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(30.0f, 35.0f);
            // 打球の角度
            Angle = Random.Range(5.0f, 8.0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = Mathf.Abs(Wide01) + 20.0f;

        }
        else if (CheckLineBallHit < 40.0 + 20.0)
        {
            Debug.Log("センター前に抜けるライナー性の当たり!");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(-5.0f, 5.0f);
            // 打球の角度
            Angle = Random.Range(0f, 8.0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = Random.Range(50.0f, 60.0f);

        }
        else if (CheckLineBallHit < 60.0 + 20.0)
        {
            Debug.Log("三遊間を抜けるライナー性の当たり!");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(-35.0f, -30.0f);
            // 打球の角度
            Angle = Random.Range(5.0f, 8.0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = Mathf.Abs(Wide01) + 20.0f;

        }
        else
        {
            Debug.Log("3塁線を抜けるライナー性の当たり!");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(-30.0f, -35.0f);
            // 打球の角度
            Angle = Random.Range(5.0f, 8.0f);
            // 三塁線を破るためにWide01の絶対値と同値とする
            Wide02 = Mathf.Abs(Wide01);

        }

        // Rigidbodyに力を加えて投球
        Batted.GetComponent<Rigidbody>().AddForce(Wide01, Angle, Wide02, ForceMode.Impulse);

        // 打球を生成したため、次の打球が判定されるまではfalseとする
        Trigger[3] = false;

    }

    // Trigger = 4
    // ポテンヒットの処理。
    void InFieldFlyHit()
    {
        
        GameObject Batted = Instantiate(BattedBallPrefab) as GameObject;

        Batted.transform.position = BattedPoint;

        float CheckLineBallHit = Random.Range(0, 1.0f) * 100;

        if (CheckLineBallHit < 20.0)
        {
            Debug.Log("ライト線へのポテンヒット!");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(8.0f, 12.0f);
            // 打球の角度
            Angle = Random.Range(14.0f, 16.0f);
            // 一塁線を破るためにほぼ同値とする
            Wide02 = Wide01;

        }
        else if (CheckLineBallHit < 20.0 + 20.0)
        {
            Debug.Log("ライト前へのポテンヒット!");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(7.0f, 9.0f);
            // 打球の角度
            Angle = Random.Range(14.0f, 16.0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = Random.Range(14.0f, 16.0f);

        }
        else if (CheckLineBallHit < 40.0 + 20.0)
        {
            Debug.Log("センター前へのポテンヒット!");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(-5.0f, 5.0f);
            // 打球の角度
            Angle = Random.Range(10.0f, 15.0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = Random.Range(15.0f, 22.0f);

        }
        else if (CheckLineBallHit < 60.0 + 20.0)
        {
            Debug.Log("レフト前へのポテンヒット!");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(-9.0f, -7.0f);
            // 打球の角度
            Angle = Random.Range(14.0f, 16.0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = Random.Range(14.0f, 16.0f);

        }
        else
        {
            Debug.Log("レフト線へのポテンヒット!");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(8.0f, 12.0f);
            // 打球の角度
            Angle = Random.Range(14.0f, 16.0f);
            // 一塁線を破るためにほぼ同値とする
            Wide02 = Wide01;

        }

        // Rigidbodyに力を加えて投球
        Batted.GetComponent<Rigidbody>().AddForce(Wide01, Angle, Wide02, ForceMode.Impulse);

        // 打球を生成したため、次の打球が判定されるまではfalseとする
        Trigger[4] = false;

    }

    // Trigger = 5
    // 外野を超える打球の処理。
    void FieldFlyHit()
    {
        GameObject Batted = Instantiate(BattedBallPrefab) as GameObject;

        Batted.transform.position = BattedPoint;

        float CheckFieldFlyHit = Random.Range(0, 1.0f) * 100;

        // レフト線
        if (CheckFieldFlyHit < 15.0)
        {
            Debug.Log("ライト線への当たり!");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(20.0f, 25.0f);
            // 打球の角度
            Angle = 10.0f;
            // 一塁線を破るためにほぼ同値とする
            Wide02 = Mathf.Abs(Wide01);

        }
        // 左中間
        else if (CheckFieldFlyHit < 15.0 + 25.0)
        {
            Debug.Log("右中間への当たり!");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(4.0f, 7.0f);
            // 打球の角度
            Angle = Random.Range(22.0f, 25.0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = Random.Range(18.0f, 20.0f);

        }
        //  センターオーバー
        else if (CheckFieldFlyHit < 40.0 + 20.0)
        {
            Debug.Log("センターオーバーの当たり!");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(-1.0f, 1.0f);
            // 打球の角度
            Angle = Random.Range(28.0f, 30.0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = Random.Range(18.0f, 20.0f);

        }
        // 右中間
        else if (CheckFieldFlyHit < 60.0 + 25.0)
        {
            Debug.Log("左中間への当たり!");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(-4.0f, -7.0f);
            // 打球の角度
            Angle = Random.Range(22.0f, 25.0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = Random.Range(18.0f, 20.0f);

        }
        // ライト線
        else
        {
            Debug.Log("レフト線への当たり!");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(-20.0f, -25.0f);
            // 打球の角度
            Angle = 10.0f;
            // 一塁線を破るためにほぼ同値とする
            Wide02 = Mathf.Abs(Wide01);
        }

        // Rigidbodyに力を加えて投球
        Batted.GetComponent<Rigidbody>().AddForce(Wide01, Angle, Wide02, ForceMode.Impulse);

        // 打球を生成したため、次の打球が判定されるまではfalseとする
        Trigger[5] = false;

    }

    // Trigger = 6
    // アウトになるゴロの打球の処理。
    void GroundBallOut()
    {

        GameObject Batted = Instantiate(BattedBallPrefab) as GameObject;

        Batted.transform.position = BattedPoint;

        float CheckGroundBallOut = Random.Range(0, 1.0f) * 100;

        if (CheckGroundBallOut < 15.0)
        {
            Debug.Log("ピッチャーへのゴロでアウトでした");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(-2.0f, 2.0f);
            // 打球の角度
            Angle = Random.Range(-5.0f, 0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = Random.Range(20.0f, 25.0f);

        }
        else if (CheckGroundBallOut < 15.0 + 20.0)
        {
            Debug.Log("ファーストへのゴロでアウトでした");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(13.0f, 20.0f);
            // 打球の角度
            Angle = Random.Range(-5.0f, 0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = Random.Range(20.0f, 25.0f);

        }
        else if (CheckGroundBallOut < 35.0 + 20.0)
        {
            Debug.Log("セカンドへのゴロでアウトでした");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(3.0f, 12.0f);
            // 打球の角度
            Angle = Random.Range(-5.0f, 0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = Random.Range(20.0f, 25.0f);

        }
        else if (CheckGroundBallOut < 55.0 + 20.0)
        {
            Debug.Log("サードへのゴロでアウトでした");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(-20.0f, -13.0f);
            // 打球の角度
            Angle = Random.Range(-5.0f, 0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = Random.Range(20.0f, 25.0f);

        }
        else if (CheckGroundBallOut < 75.0 + 20.0)
        {
            Debug.Log("ショートへのゴロでアウトでした");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(-12.0f, -3.0f);
            // 打球の角度
            Angle = Random.Range(-5.0f, 0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = Random.Range(20.0f, 25.0f);

        }
        else
        {
            Debug.Log("ライトへのゴロでアウトでした");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(25.0f, 30.0f);
            // 打球の角度
            Angle = Random.Range(-5.0f, 0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = Mathf.Abs(Wide01) + 20.0f;

        }

        // Rigidbodyに力を加えて投球
        Batted.GetComponent<Rigidbody>().AddForce(Wide01, Angle, Wide02, ForceMode.Impulse);

        // 打球を生成したため、次の打球が判定されるまではfalseとする
        Trigger[6] = false;

    }

    // Trigger = 7
    // ライナー性の打球がアウトになったときの処理。
    void LineBallOut()
    {

        GameObject Batted = Instantiate(BattedBallPrefab) as GameObject;

        Batted.transform.position = BattedPoint;

        float CheckLineBallOut = Random.Range(0, 1.0f) * 100;

        if (CheckLineBallOut < 10.0)
        {
            Debug.Log("ピッチャーへのライナーでアウトでした");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(-1.0f, 1.0f);
            // 打球の角度
            Angle = Random.Range(5.0f, 8.0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = Random.Range(20.0f, 40.0f);

        }
        else if (CheckLineBallOut < 10.0 + 15.0)
        {
            Debug.Log("ファーストへのライナーでアウトでした");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(20.0f, 23.0f);
            // 打球の角度
            Angle = Random.Range(5.0f, 8.0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = Random.Range(20.0f, 30.0f);

        }
        else if (CheckLineBallOut < 25.0 + 15.0)
        {
            Debug.Log("セカンドへのライナーでアウトでした");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(3.0f, 12.0f);
            // 打球の角度
            Angle = Random.Range(7.0f, 9.0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = Random.Range(20.0f, 25.0f);

        }
        else if (CheckLineBallOut < 40.0 + 15.0)
        {
            Debug.Log("サードへのライナーでアウトでした");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(-23.0f, -20.0f);
            // 打球の角度
            Angle = Random.Range(5.0f, 8.0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = Random.Range(20.0f, 30.0f);

        }
        else if (CheckLineBallOut < 60.0 + 10.0)
        {
            Debug.Log("ショートへのライナーでアウトでした");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(-12.0f, -3.0f);
            // 打球の角度
            Angle = Random.Range(7.0f, 9.0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = Random.Range(20.0f, 25.0f);

        }
        else if (CheckLineBallOut < 70.0 + 10.0)
        {
            Debug.Log("レフトへのライナーでアウトでした");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(-17.0f, -15.0f);
            // 打球の角度
            Angle = Random.Range(9.0f, 11.0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = Random.Range(34.0f, 36.0f);

        }
        else if (CheckLineBallOut < 80.0 + 10.0)
        {
            Debug.Log("センターへのライナーでアウトでした");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(-5.0f, 5.0f);
            // 打球の角度
            Angle = Random.Range(9.0f, 11.0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = Random.Range(40.0f, 45.0f);

        }
        else
        {
            Debug.Log("ライトへのライナーでアウトでした");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(15.0f, 17.0f);
            // 打球の角度
            Angle = Random.Range(9.0f, 11.0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = Random.Range(34.0f, 36.0f);

        }

        // Rigidbodyに力を加えて投球
        Batted.GetComponent<Rigidbody>().AddForce(Wide01, Angle, Wide02, ForceMode.Impulse);

        // 打球を生成したため、次の打球が判定されるまではfalseとする
        Trigger[7] = false;

    }

    // Trigger = 8
    // 内野フライの処理。
    void InFieldFlyOut()
    {

        GameObject Batted = Instantiate(BattedBallPrefab) as GameObject;

        Batted.transform.position = BattedPoint;

        float CheckInFieldFlyOut = Random.Range(0, 1.0f) * 100;

        if (CheckInFieldFlyOut < 10.0)
        {
            Debug.Log("ピッチャーへのフライでアウトでした");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(-1.0f, 1.0f);
            // 打球の角度
            Angle = Random.Range(14.0f, 16.0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = 6.0f;

        }
        else if (CheckInFieldFlyOut < 10.0 + 10.0)
        {
            Debug.Log("キャッチャーへのフライでアウトでした");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(-1.0f, 1.0f);
            // 打球の角度
            Angle = Random.Range(14.0f, 16.0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = Random.Range(-1.0f, 0f);

        }
        else if (CheckInFieldFlyOut < 20.0 + 20.0)
        {
            Debug.Log("ファーストへのフライでアウトでした");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(4.5f, 5.5f);
            // 打球の角度
            Angle = Random.Range(14.0f, 16.0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = Random.Range(7.5f, 8.5f);

        }
        else if (CheckInFieldFlyOut < 40.0 + 20.0)
        {
            Debug.Log("セカンドへのフライでアウトでした");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(2.5f, 3.5f);
            // 打球の角度
            Angle = Random.Range(19.0f, 21.0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = Random.Range(8.5f, 9.5f);

        }
        else if (CheckInFieldFlyOut < 60.0 + 20.0)
        {
            Debug.Log("サードへのフライでアウトでした");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(-5.5f, -4.5f);
            // 打球の角度
            Angle = Random.Range(14.0f, 16.0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = Random.Range(7.5f, 8.5f);

        }
        else
        {
            Debug.Log("ショートへのフライでアウトでした");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(-3.5f, -2.5f);
            // 打球の角度
            Angle = Random.Range(19.0f, 21.0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = Random.Range(8.5f, 9.5f);

        }

        // Rigidbodyに力を加えて投球
        Batted.GetComponent<Rigidbody>().AddForce(Wide01, Angle, Wide02, ForceMode.Impulse);

        // 打球を生成したため、次の打球が判定されるまではfalseとする
        Trigger[8] = false;

    }

    // Trigger = 9
    // 外野フライの処理。
    void FieldFlyOut()
    {

        GameObject Batted = Instantiate(BattedBallPrefab) as GameObject;

        Batted.transform.position = BattedPoint;

        Debug.Log(BattedPoint);

        float CheckLineFieldFlyOut = Random.Range(0, 1.0f) * 100;

        if (CheckLineFieldFlyOut < 30.0)
        {
            Debug.Log("レフトへのフライでアウトでした");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(-10.0f, -8.0f);
            // 打球の角度
            Angle = Random.Range(15.0f, 20.0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = Random.Range(18.0f, 23.0f);

        }
        else if (CheckLineFieldFlyOut < 30.0 + 40.0)
        {
            Debug.Log("センターへのフライでアウトでした");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(-5.0f, 5.0f);
            // 打球の角度
            Angle = Random.Range(18.0f, 23.0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = Random.Range(18.0f, 23.0f);

        }
        else
        {
            Debug.Log("ライトへのフライでアウトでした");

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            Wide01 = Random.Range(8.0f, 10.0f);
            // 打球の角度
            Angle = Random.Range(15.0f, 20.0f);
            // 打球がファールになるか、フェアになるかを決める
            Wide02 = Random.Range(18.0f, 23.0f);

        }

        // 初速
        Vector3 v0 = new Vector3(Wide01, Angle, Wide02);

        var t1 = (v0.y + Mathf.Sqrt(Mathf.Pow(v0.y, 2.0f) + (-2 * -Physics.gravity.y * (-transform.position.y + gp)))) / (-Physics.gravity.y);
        var t2 = (v0.y - Mathf.Sqrt(Mathf.Pow(-v0.y, 2.0f) + (-2 * -Physics.gravity.y * (-transform.position.y + gp)))) / (-Physics.gravity.y);

        if ((float.IsNaN(t1) && float.IsNaN(t2)) || (t1 < 0) && (t2 < 0))
            return;

        // どちらかの式を採用する
        stopTime = (t1 > 0) ? t1 : t2;

        //地面に落ちる位置
        var pos = new Vector3(v0.x * stopTime, gp, v0.z * stopTime);

        //ダミーの球を落ちる位置に表示する
        dummySphere.transform.position = pos;

        // 力を加えることで打球を飛ばす
        Batted.GetComponent<Rigidbody>().AddForce(v0, ForceMode.Impulse);

        // 毎フレームごとのボールの位置
        Transform BallPosition = Batted.transform;

        // 打球を生成したため、次の打球が判定されるまではfalseとする
        Trigger[9] = false;

    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (GenerateBattedBall == true)
        {

            // ファウルボールの処理。
            if (Trigger[0] == true)
            {
                Foul();
            }
            // ホームランの処理。
            else if (Trigger[1] == true)
            {
                HomeRun();
            }
            // ゴロ性のヒット処理。
            else if (Trigger[2] == true)
            {
                GroundBallHit();
            }
            // ライナー性のヒット処理。
            else if (Trigger[3] == true)
            {
                LineBallHit();
            }
            // ポテンヒットの処理。
            else if (Trigger[4] == true)
            {
                InFieldFlyHit();
            }
            // 外野を超える打球の処理。
            else if (Trigger[5] == true)
            {
                FieldFlyHit();
            }
            // 外野を超える打球の処理。
            else if (Trigger[6] == true)
            {
                GroundBallOut();
            }
            // 外野を超える打球の処理。
            else if (Trigger[7] == true)
            {
                LineBallOut();
            }
            // 外野を超える打球の処理。
            else if (Trigger[8] == true)
            {
                InFieldFlyOut();
            }
            // 外野を超える打球の処理。
            else if (Trigger[9] == true)
            {
                FieldFlyOut();
            }


            GenerateBattedBall = false;

        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            FieldFlyOut();
        }


	}
}
