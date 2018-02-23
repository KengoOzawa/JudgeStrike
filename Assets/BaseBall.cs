using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BaseBall : MonoBehaviour
{
    
    /*投球ごとに打撃結果に移行するかの割合は異なる。カウントごとに異なる数値を渡してあげるための関数
    void Count
    {

    }

*/

    void PitchingBatting()
    {

        float check = Random.Range(0, 1.0f) * 100;

        if (check < 40.0)
        {

            if (this.StrikeCount < 2)
            {
                this.Ampire.GetComponent<Text>().text = "ストライク!";
                StrikeCount++;
                this.StrikeCountText.GetComponent<Text>().text = this.StrikeCount.ToString();
            }
            else
            {
                this.Ampire.GetComponent<Text>().text = "三振!";
                this.StrikeCount = 0;
                this.BallCount = 0;
                this.StrikeCountText.GetComponent<Text>().text = this.StrikeCount.ToString();
                this.BallCountText.GetComponent<Text>().text = this.BallCount.ToString();
                OutCheck();
            }

        }
        else if (check < 40.0 + 40.0)
        {

            if (this.BallCount < 3)
            {
                this.Ampire.GetComponent<Text>().text = "ボール!";
                BallCount++;
                this.BallCountText.GetComponent<Text>().text = this.BallCount.ToString();
            }
            else
            {
                this.Ampire.GetComponent<Text>().text = "フォアボール!";
                this.StrikeCount = 0;
                this.BallCount = 0;
                this.StrikeCountText.GetComponent<Text>().text = this.StrikeCount.ToString();
                this.BallCountText.GetComponent<Text>().text = this.BallCount.ToString();
                FourballRunner();
            }

        }
        else
        {
            GameObject[] Battingdestroy = GameObject.FindGameObjectsWithTag("NewBallPrefab");
            foreach (GameObject obj in Battingdestroy)
            {
                Destroy(obj);
            }

            this.Ampire.GetComponent<Text>().text = "打球判定";
            Debug.Log("打撃判定を行います!");
            FairFallJudge();
        }

    }

    void FourballRunner(){
        if(FirstRunner == 0){
            GenerateFirstRunner();
        }else{
            if(SecondRunner == 0){
                GenerateSecondRunner();
            }else{
                if(Thirdrunner == 0){
                    GenerateThirdRunner();
                }else{
                    GetScore();
                }
            }
        }
    }

    // 打球がフェアゾーンに飛ぶかどうかを判定するための関数
    void FairFallJudge()
    {
        float CheckFairFallJudge = Random.Range(0, 1.0f) * 100;

        if (CheckFairFallJudge < 50.0)
        {
            Fall();
        }
        else
        {
            BattedBall();
        }

    }

    // ファウルが打たれたときの処理を行う関数。
    void Fall()
    {
        this.Ampire.GetComponent<Text>().text = "ファウル!";
        Debug.Log("ファウルでした!");

        // プレハブの野球ボールを複製
        GameObject Pitching = GameObject.Instantiate(HittingBallPrefab) as GameObject;
        // どこからボールを投げるかを指定する
        Pitching.transform.position = HittingPivot.position;

        // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
        float Wide01 = Random.Range(-1.0f, 1.0f);
        // Debug.Log(Wide01);
        // 打球の角度
        float Angle = Random.Range(-0.1f, 1.0f);
        // 将来的には前方へ飛ぶファウルも実装したい
        float Wide02 = Random.Range(-1.0f, -0.1f);
        // Debug.Log(Wide02);

        // Rigidbodyに力を加えて投球
        // AddForceは最後にForceMode.Force(何も書かなければこちらがデフォルト)かForceMode.Impulseを選べる
        Pitching.GetComponent<Rigidbody>().AddForce(Wide01, Angle, Wide02, ForceMode.Impulse);

        if (this.StrikeCount < 2)
        {
            StrikeCount++;
            this.StrikeCountText.GetComponent<Text>().text = this.StrikeCount.ToString();
        }

    }

    // 打球の性質を決定する関数。ヒットかアウトかを判定するためにGroundBall,LineBall,FlyBall関数を呼び出す
    void BattedBall()
    {
        Debug.Log("フェアです!ボールを判定します!");

        this.StrikeCount = 0;
        this.BallCount = 0;
        this.StrikeCountText.GetComponent<Text>().text = this.StrikeCount.ToString();
        this.BallCountText.GetComponent<Text>().text = this.BallCount.ToString();

        // 打球の性質を割り振る
        float CheckBallCharacter = Random.Range(0, 1.0f) * 100;

        if (CheckBallCharacter < 48.4)
        {
            GroundBall();
        }
        else if (CheckBallCharacter < 48.4 + 8.3)
        {
            LineBall();
        }
        else
        {
            FlyBall();
        }

    }

    // 最終的な打球結果が判定される関数。

    // ゴロ性の打球かヒットかアウトかを判定する
    void GroundBall()
    {
        float CheckGroundBall = Random.Range(0, 1.0f) * 100;

        if (CheckGroundBall < 73.6)
        {
            this.Ampire.GetComponent<Text>().text = "ゴロアウト!";
            Debug.Log("ゴロアウトでした");
            OutCheck();

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
        else
        {
            this.Ampire.GetComponent<Text>().text = "ゴロヒット!";
            Debug.Log("ゴロヒットでした");
            SingleHit();

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

    }

    // ライナー性の打球がヒットかアウトかを判定する
    void LineBall()
    {

        float CheckLineBall = Random.Range(0, 1.0f) * 100;

        if (CheckLineBall < 20.0)
        {
            this.Ampire.GetComponent<Text>().text = "ライナーアウト!";
            Debug.Log("ライナーアウトでした");
            OutCheck();

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
            this.Ampire.GetComponent<Text>().text = "ライナーヒット!";
            Debug.Log("ライナーヒットでした");
            SingleHit();

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

    }



    void FlyBall()
    {
        float CheckFlyBall = Random.Range(0, 1.0f) * 100;

        if (CheckFlyBall < 63.9)
        {
            this.Ampire.GetComponent<Text>().text = "フライアウト!";
            Debug.Log("フライアウトでした");
            OutCheck();

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
        else
        {
            this.Ampire.GetComponent<Text>().text = "フライヒット!";
            Debug.Log("フライヒットでした");
            SingleHit();

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

    void SingleHit()
    {

        if (Thirdrunner == 1)
        {
            GetScore();
            ThirdRunnerDestroy();
        }
        if (SecondRunner == 1)
        {
            GenerateThirdRunner();
            SecondRunnerDestroy();
        }
        if (FirstRunner == 1)
        {
            GenerateSecondRunner();
            FirstRunnerDestroy();
        }

        GenerateFirstRunner();

    }

    void OutCheck()
    {

        if (this.OutCount < 2)
        {
            OutCount++;
            this.OutCountText.GetComponent<Text>().text = this.OutCount.ToString();
        }
        else
        {
            this.OutCount = 0;

            this.FirstRunner = 0;
            this.SecondRunner = 0;
            this.Thirdrunner = 0;

            FirstRunnerDestroy();
            SecondRunnerDestroy();
            ThirdRunnerDestroy();

            this.OutCountText.GetComponent<Text>().text = this.OutCount.ToString();
            Inningcheck();
        }

    }

    void GetScore()
    {

        if (InningJudge == 0)
        {
            ScoreTopTeam++;
            this.ScoreTopTeamText.GetComponent<Text>().text = this.ScoreTopTeam.ToString();
        }
        else
        {
            ScoreBottomTeam++;
            this.ScoreBottomTeamText.GetComponent<Text>().text = this.ScoreBottomTeam.ToString();
        }

    }

    void GenerateFirstRunner(){
        FirstRunner = 1;

        // ランナーを生成
        GameObject MakeFirstRunner = GameObject.Instantiate(FirstRunnerPrefab) as GameObject;
            MakeFirstRunner.transform.position = FirstRunnerPivot.position;
    }

    void GenerateSecondRunner()
    {
        SecondRunner = 1;

        // ランナーを生成
        GameObject MakeSecondRunner = GameObject.Instantiate(SecondRunnerPrefab) as GameObject;
        MakeSecondRunner.transform.position = SecondRunnerPivot.position;
    }

    void GenerateThirdRunner()
    {
        Thirdrunner = 1;

        // ランナーを生成
        GameObject MakeThirdRunner = GameObject.Instantiate(ThirdRunnerPrefab) as GameObject;
        MakeThirdRunner.transform.position = ThirdRunnerPivot.position;
    }

    void FirstRunnerDestroy(){
        FirstRunner = 0;
        GameObject[] FirstRunnerDestroyOccur = GameObject.FindGameObjectsWithTag("FirstRunnerPrefab");
        foreach (GameObject obj in FirstRunnerDestroyOccur)
        {
            Destroy(obj);
        }
    }

    void SecondRunnerDestroy()
    {
        SecondRunner = 0;
        GameObject[] SecondRunnerDestroyOccur = GameObject.FindGameObjectsWithTag("SecondRunnerPrefab");
        foreach (GameObject obj in SecondRunnerDestroyOccur)
        {
            Destroy(obj);
        }
    }

    void ThirdRunnerDestroy()
    {
        Thirdrunner = 0;
        GameObject[] ThirdRunnerDestroyOccur = GameObject.FindGameObjectsWithTag("ThirdRunnerPrefab");
        foreach (GameObject obj in ThirdRunnerDestroyOccur)
        {
            Destroy(obj);
        }
    }


    void Inningcheck(){

        Debug.Log(InningJudge);

        // 表の攻撃が終了したとき
        if (InningJudge == 0)
        {
            InningJudge++;
            this.TopBottom.GetComponent<Text>().text = "裏";
        }
        // 裏の攻撃が終了したとき
        else
        {
            InningJudge = 0;
            InningCount++;
            this.Inning.GetComponent<Text>().text = this.InningCount.ToString();
            this.TopBottom.GetComponent<Text>().text = "表";
        }

    }


    public GameObject BallPrefab;
    public Transform Pitcher;
    public float BallSpeed = 1.0f;

    // アクション用に打撃用の発射口を準備する(あとでActionBatting関数に含めてみる)
    public GameObject HittingBallPrefab;
    public Transform HittingPivot;

    public GameObject FirstRunnerPrefab;
    public GameObject SecondRunnerPrefab;
    public GameObject ThirdRunnerPrefab;

    public Transform FirstRunnerPivot;
    public Transform SecondRunnerPivot;
    public Transform ThirdRunnerPivot;

    // 画面左下の審判
    GameObject Ampire;
    // 画面右下のストライク、ボール、アウトの各カウント
    GameObject StrikeCountText;
    GameObject BallCountText;
    GameObject OutCountText;
    // イニングの進行
    GameObject Inning;
    GameObject TopBottom;

    GameObject ScoreTopTeamText;
    GameObject ScoreBottomTeamText;

    int StrikeCount;
    int BallCount;
    int OutCount;

    int InningCount = 1;
    int InningJudge;

    int FirstRunner;
    int SecondRunner;
    int Thirdrunner;

    int ScoreTopTeam;
    int ScoreBottomTeam;

    // Use this for initialization
    void Start()
    {
        this.Ampire = GameObject.Find("Ampire");
        this.StrikeCountText = GameObject.Find("StrikeCountText");
        this.BallCountText = GameObject.Find("BallCountText");
        this.OutCountText = GameObject.Find("OutCountText");
        this.Inning = GameObject.Find("Inning");
        this.TopBottom = GameObject.Find("TopBottom");
        this.ScoreTopTeamText = GameObject.Find("ScoreTopTeamText");
        this.ScoreBottomTeamText = GameObject.Find("ScoreBottomTeamText");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.B))
        {
            // 目に見える投球モーション部分

            // プレハブの野球ボールを複製
            GameObject Pitching = GameObject.Instantiate(BallPrefab) as GameObject;
            // どこからボールを投げるかを指定する
            Pitching.transform.position = Pitcher.position;

            Pitching.GetComponent<Rigidbody>().AddForce(0f * BallSpeed, 0f * BallSpeed, -1.8105f * BallSpeed, ForceMode.Impulse);

            // 前の打球を削除
            GameObject[] FormerBallDestroy = GameObject.FindGameObjectsWithTag("HittingBallPrefab");
            foreach (GameObject obj in FormerBallDestroy)
            {
                Destroy(obj);
            }

            // シミュレーション部分

            this.Ampire.GetComponent<Text>().text = "";

            Invoke("PitchingBatting", 0.5f);
        }

    }
}
