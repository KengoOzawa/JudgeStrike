using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabola : MonoBehaviour
{
    
    public GameObject HittingBallPrefab;
    public Transform HittingPivot;

    // 守備連携関係

    // 全ての守備側のプレイヤーを取得
    GameObject CallFirst;
    GameObject CallSecond;
    GameObject CallThird;
    GameObject CallShort;
    GameObject CallLeft;
    GameObject CallCenter;
    GameObject CallRight;

    GameObject CallGroundJudgePoint;


    // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
      public      float Wide03;

            // 打球の角度
      public      float Angle02;

            // 打球がセンター方向へどのくらい飛ぶか決める
      public      float Wide04;

    // 落下点予測のために使用

    // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
    float Wide011;
    // 打球の角度。大きくなるほど高いフライになる
    float Angle1;
    // 打球がファールになるか、フェアになるかを決める
    float Wide021;

    //ダミーの球
     [SerializeField]
    private GameObject dummySphere;

    //初速
    // [SerializeField]
    private Vector3 v0;

    //地面の座標
    [SerializeField]
    private float gp;

    private Rigidbody rigid;
    private bool isShot = false;
    // private float timeCnt = 0.0f;
    private float stopTime = 0.0f;


    void DefaultPitchingBatting(float a,float b, float c, float d, float e, float f){

        // VisualStudio側からの引数を使用しないことによる警告を防ぐ
        float unused = e;

        Debug.Log("引数代入テストを行います!");

        GameObject Pitching = Instantiate(HittingBallPrefab) as GameObject;

        Pitching.transform.position = HittingPivot.position;

        // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
        float Wide01 = Random.Range(a, b);
        Debug.LogFormat("横方向への力 = {0}", Wide01);
        // 打球の角度
        float Angle = Random.Range(c, d);
        Debug.LogFormat("上方向への力 = {0}", Angle);
        // 打球がファールになるか、フェアになるかを決める
        // このケースでは絶対値を取得してx<zのため必ずフェアになる

        // 本来はRandom.Range(e, f);となるところだが、本ケースではeが使用されない
        float Wide02 = Random.Range(Mathf.Abs(Wide01), f);
        Debug.LogFormat("センター方向への力 = {0}", Wide02);

        // Rigidbodyに力を加えて投球
        Pitching.GetComponent<Rigidbody>().AddForce(Wide01, Angle, Wide02, ForceMode.Impulse);

    }

    void TestPitchingBatting(float a, float b, float c)
    {
        
        Debug.Log("引数代入テストを行います!");

        GameObject Pitching = Instantiate(HittingBallPrefab) as GameObject;

        Pitching.transform.position = HittingPivot.position;

        // Rigidbodyに力を加えて投球
        Pitching.GetComponent<Rigidbody>().AddForce(a, b, c, ForceMode.Impulse);

        Transform BallPosition = Pitching.transform;

    }

    void HomePosition(){

        // 守備側を定位置に戻す
        CallFirst.GetComponent<FieldingFirst>().ReturnHomePosition();
        CallSecond.GetComponent<FieldingSecond>().ReturnHomePosition();
        CallThird.GetComponent<FieldingThird>().ReturnHomePosition();
        CallShort.GetComponent<FieldingShort>().ReturnHomePosition();
        CallLeft.GetComponent<FieldingLeft>().ReturnHomePosition();
        CallCenter.GetComponent<FieldingCenter>().ReturnHomePosition();
        CallRight.GetComponent<FieldingRight>().ReturnHomePosition();

    }

    void Start()
    {
        this.CallFirst = GameObject.Find("SampleFirst");
        this.CallSecond = GameObject.Find("SampleSecond");
        this.CallThird = GameObject.Find("NewSampleThird");
        this.CallShort = GameObject.Find("SampleShort");
        this.CallLeft = GameObject.Find("SampleLeft");
        this.CallCenter = GameObject.Find("SampleCenter");
        this.CallRight = GameObject.Find("SampleRight");

        this.CallGroundJudgePoint = GameObject.Find("GroundJudgePoint");
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.M))
        {
            DefaultPitchingBatting(-1.0f, 1.0f, 0f, 1.0f, 0f, 1.0f);
        }

        // 守備側との連携テスト
        // ランダムな打球を発射
        if (Input.GetKeyDown(KeyCode.T))
        {

            // 前の打球を削除
            GameObject[] FormerBallDestroy = GameObject.FindGameObjectsWithTag("HittingBallPrefab");
            foreach (GameObject obj in FormerBallDestroy)
            {
                Destroy(obj);
            }

            Debug.Log("自動打球テストを行います!");
            Debug.Log("守備側との連携テストを行います!");

            GameObject Pitching = Instantiate(HittingBallPrefab) as GameObject;
           
            Pitching.transform.position = HittingPivot.position;

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            float Wide01 = Random.Range(-1.0f, 1.0f);
            Debug.LogFormat("横方向への力 = {0}", Wide01);
            // 打球の角度
            float Angle = Random.Range(0f, 1.0f);
            // 打球がファールになるか、フェアになるかを決める
            // このケースでは絶対値を取得してx<zのため必ずフェアになる
            float Wide02 = Random.Range(Mathf.Abs(Wide01), 1.0f);
            Debug.LogFormat("センター方向への力 = {0}", Wide02);

            // Rigidbodyに力を加えて投球
            Pitching.GetComponent<Rigidbody>().AddForce(Wide01, Angle, Wide02, ForceMode.Impulse);

            Transform BallPosition = Pitching.transform;

            // ここから守備側との連携に関するスクリプト

            // 守備側を定位置に戻す
            CallThird.GetComponent<FieldingThird>().ReturnHomePosition();

            //ボールの生成と同時に守備に追いかけさせるスクリプトを呼び出し、打球の座標を渡す
            CallThird.GetComponent<FieldingThird>().ChaseStartTrigger(BallPosition);

        }

        // 守備連携テスト ここから



        // ファーストゴロ
        if (Input.GetKeyDown(KeyCode.A))
        {
            HomePosition();

            // 前回の予測点を削除
            GameObject[] FormerBallDestroy = GameObject.FindGameObjectsWithTag("PredictionPoint");
            foreach (GameObject obj in FormerBallDestroy)
            {
                Destroy(obj);
            }

            // 打球の方向をランダムに決める
            float x = Random.Range(10.0f, 20.0f);
            float y = Random.Range(2.0f, -5.0f);
            float z = Random.Range(10.0f, 50.0f);

            GameObject Pitching = Instantiate(HittingBallPrefab) as GameObject;

            Pitching.transform.position = HittingPivot.position;

            // Rigidbodyに力を加えて投球
            Pitching.GetComponent<Rigidbody>().AddForce(x, y, z, ForceMode.Impulse);

            // 毎秒ごとのボールの位置
            Transform BallPosition = Pitching.transform;

            Pitching.GetComponent<PredictionGroundBall>().PredictionTrigger();

            // CallFirst.GetComponent<FieldingFirst>().ChaseStartTrigger(BallPosition);
            // CallSecond.GetComponent<FieldingSecond>().ChaseStartTrigger(BallPosition);
        }

        // センターフライ
        if (Input.GetKeyDown(KeyCode.S))
        {
            

            HomePosition();

            // 打球の方向をランダムに決める
            float x = Random.Range(-10.0f, 10.0f);
            float y = Random.Range(10.0f, 25.0f);
            float z = Random.Range(10.0f, 30.0f);

            // 初速
            Vector3 syosoku = new Vector3(x, y, z);

            GameObject Hitting = Instantiate(HittingBallPrefab) as GameObject;

            // 発射地点を決める
            Hitting.transform.position = HittingPivot.position;

            var t1 = (syosoku.y + Mathf.Sqrt(Mathf.Pow(syosoku.y, 2.0f) + (-2 * -Physics.gravity.y * (-transform.position.y + gp)))) / (-Physics.gravity.y);
            var t2 = (syosoku.y - Mathf.Sqrt(Mathf.Pow(-syosoku.y, 2.0f) + (-2 * -Physics.gravity.y * (-transform.position.y + gp)))) / (-Physics.gravity.y);

            //秒数がNaNの時か秒数がマイナスの時は処理しない
            // NaN（Not a Number、非数、ナン）は、コンピュータにおいて、主に浮動小数点演算の結果として、不正なオペランドを与えられたために生じた結果を表す値またはシンボルである。
            if ((float.IsNaN(t1) && float.IsNaN(t2)) || (t1 < 0) && (t2 < 0))
            return;

            //地面に落ちるまでの時間

            // 条件演算（「～ ? ～ : ～」）
            // この演算子は3つの値を持ち、それらを「?」記号と「:」記号で分離する。「式1?式2:式3」という順番に書き、式1の結果次第で、式2と式3のどちらを解釈するかを決める。

            stopTime = (t1 > 0) ? t1 : t2;

            //地面に落ちる位置
            var pos = new Vector3(syosoku.x * stopTime, gp, syosoku.z * stopTime);
            pos.x += transform.position.x;
            pos.z += transform.position.z;
            
            // 毎秒ごとのボールの位置
            Transform BallPosition = Hitting.transform;

            Debug.Log("これ" + Hitting.transform.position.y);

            //ダミーの球を落ちる位置に表示する
            dummySphere.transform.position = pos;

            Debug.Log(syosoku);

            Hitting.GetComponent<Rigidbody>().AddForce(syosoku, ForceMode.Impulse);

            CallCenter.GetComponent<FieldingCenter>().ChaseFlyBallStartTrigger(dummySphere.transform, BallPosition);



            if (Hitting.transform.position.y < CallGroundJudgePoint.transform.position.y)
            {
                Debug.Log("ボールが地面に落ちたよ!");
            }



        }

        // センターフライスクリプト検証用
        if (Input.GetKeyDown(KeyCode.F))
        {


            GameObject Batted = Instantiate(HittingBallPrefab) as GameObject;

            Batted.transform.position = HittingPivot.position;

            float CheckLineFieldFlyOut = Random.Range(0, 1.0f) * 100;

            if (CheckLineFieldFlyOut < 30.0)
            {
                Debug.Log("レフトへのフライでアウトでした");

                // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
                Wide011 = Random.Range(-10.0f, -8.0f);
                // 打球の角度
                Angle1 = Random.Range(15.0f, 20.0f);
                // 打球がファールになるか、フェアになるかを決める
                Wide021 = Random.Range(18.0f, 23.0f);

            }
            else if (CheckLineFieldFlyOut < 30.0 + 40.0)
            {
                Debug.Log("センターへのフライでアウトでした");

                // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
                Wide011 = Random.Range(-5.0f, 5.0f);
                // 打球の角度
                Angle1 = Random.Range(18.0f, 23.0f);
                // 打球がファールになるか、フェアになるかを決める
                Wide021 = Random.Range(18.0f, 23.0f);

            }
            else
            {
                Debug.Log("ライトへのフライでアウトでした");

                // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
                Wide011 = Random.Range(8.0f, 10.0f);
                // 打球の角度
                Angle1 = Random.Range(15.0f, 20.0f);
                // 打球がファールになるか、フェアになるかを決める
                Wide021 = Random.Range(18.0f, 23.0f);

            }

            // 初速
            Vector3 v01 = new Vector3(Wide011, Angle1, Wide021);

            var t1 = (v01.y + Mathf.Sqrt(Mathf.Pow(v01.y, 2.0f) + (-2 * -Physics.gravity.y * (-transform.position.y + gp)))) / (-Physics.gravity.y);
            var t2 = (v01.y - Mathf.Sqrt(Mathf.Pow(-v01.y, 2.0f) + (-2 * -Physics.gravity.y * (-transform.position.y + gp)))) / (-Physics.gravity.y);

            if ((float.IsNaN(t1) && float.IsNaN(t2)) || (t1 < 0) && (t2 < 0))
                return;

            // どちらかの式を採用する
            stopTime = (t1 > 0) ? t1 : t2;

            //地面に落ちる位置
            var pos = new Vector3(v01.x * stopTime, gp, v01.z * stopTime);
            pos.x += transform.position.x;
            pos.z += transform.position.z;

            //ダミーの球を落ちる位置に表示する
            dummySphere.transform.position = pos;

            // 力を加えることで打球を飛ばす
            Batted.GetComponent<Rigidbody>().AddForce(v01, ForceMode.Impulse);

            // 毎フレームごとのボールの位置
            Transform BallPosition = Batted.transform;



        }


        // 守備連携テスト ここまで


        if (Input.GetKeyDown(KeyCode.W))
        {

            Debug.Log("手動打球テストを行います!");

            GameObject Pitching = Instantiate(HittingBallPrefab) as GameObject;

            Pitching.transform.position = HittingPivot.position;

            // Rigidbodyに力を加えて投球
            Pitching.GetComponent<Rigidbody>().AddForce(Wide03, Angle02, Wide04, ForceMode.Impulse);

            Debug.LogFormat("横方向への力 = {0}", Wide03);
            Debug.LogFormat("上方向への力 = {0}", Angle02);
            Debug.LogFormat("センター方向への力 = {0}", Wide04);

        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Wide03 += 5.0f;
            Debug.LogFormat("横方向への力 : {0}", Wide03);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Angle02 += 5.0f;
            Debug.LogFormat("上方向への力 : {0}", Angle02);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Wide04 += 5.0f;
            Debug.LogFormat("センター方向への力 : {0}", Wide04);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Wide03 += 1.0f;
            Debug.LogFormat("横方向への力 : {0}", Wide03);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Wide04 += 1.0f;
            Debug.LogFormat("センター方向への力 : {0}", Wide04);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Wide03 -= 5.0f;
            Debug.LogFormat("横方向への力 : {0}", Wide03);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Angle02 -= 5.0f;
            Debug.LogFormat("上方向への力 : {0}", Angle02);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Wide04 -= 5.0f;
            Debug.LogFormat("センター方向への力 : {0}", Wide04);
        }

    }
}