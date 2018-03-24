using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {


    // チーム力によって変動する変数一覧。

    // ストライク率。

    // 打率。メソッド
    float BattingAverage;

    // データの取得のために必要とされる変数一覧。

    // イニング数
    int InningTop;
    int InningBottom;
     int TotalInning; 
    //データとして必要な打者の成績      // 得点     int ScoreTop;     int ScoreBottom;      int ToTalScore;
    // 打席(四死球等含む)     int PATop;     int PABottom;      int PA;     // 打数     int ABTop;     int ABBottom;      int AB;     // 三振した数     int SOTop;     int SOBottom;      int SO;     // 四死球を得た数     int GetMissPitchedTop;     int GetMissPitchedBottom;      int GetMissPitched;     // ヒットの総数     int HitTotalTop;     int HitTotalBottom;     // ゴロヒット     int GroundBallHitTop;     int GroundBallHitBottom;     // ライナーヒット     int LineBallHitTop;     int LineBallHitBottom;     // フライヒット     int FlyBallHitTop;     int FlyBallHitBottom;     // 単打     int HTop;     int HBottom;

    int H;     // 二塁打     int TwoBTop;     int TwoBBottom;     // 三塁打     int ThreeBTop;     int ThreeBBottom;     // ホームラン     int HRTop;     int HRBottom;     // ゴロアウト     int GroundBallOutTop;     int GroundBallOutBottom;     // ライナーアウト     int LineBallOutTop;     int LineBallOutBottom;     // フライアウト     int FlyBallOutTop;     int FlyBallOutBottom;      //データとして必要な投手の成績      // 投球数     int NPTop;     int NPBottom;      int NP;     // うちストライク     int StrikeTop;     int StrikeBottom;     // うちボール     int TotalBallTop;     int TotalBallBottom;     // 被安打     int HAllowedTop;     int HAllowedBottom;     // 被本塁打     int HRAllowedTop;     int HRAllowedBottom;     // 奪三振数     int KTop;     int KBottom;     // 与四死球数     int MissPitchedTop;     int MissPitchedBottom;      // どちらのチームでも使われる     int StrikeCount;     int BallCount;     int OutCount;      int InningCount = 1;     int InningJudge;      int FirstRunner;     int SecondRunner;     int Thirdrunner;

    // 試合が終了した際にtrueとなる。
    bool GameSetJudge;

    // 野球の試合を行うにあたって呼び出される最上位のメソッド。本メソッドが繰り返されることで試合が行われる。
    void BaseBallGame(){

        // 前の打球を消去する
        GetComponent<BaseBallAction>().DestroyFormerBall();

        // 投球を開始するメソッドを呼び出す
        PitchingBatting();

    }

    // 打撃が行われるかを判定する。
    void PitchingBatting(){

        // 試合の総投球数を加算する
        TotalNP();

        float check = Random.Range(0, 1.0f) * 100;

        if (check < 30.0)
        {
            // 打撃が行われた
            FairFoulJudge();
        }
        else
        {
            // 打撃が行われなかった
            Pitching();
        }

    }

    // 打球がフェアゾーンに飛ぶかどうかを判定する。
    void FairFoulJudge()
    {

        float CheckFairFaulJudge = Random.Range(0, 1.0f) * 100;

        if (CheckFairFaulJudge < 50.0)
        {
            // 打席の加算
            TotalPA();
            // 打数の加算
            TotalAB();

            Debug.Log("打球判定が生じる投球です");

            HitOut();
        }
        else
        {
            Foul();
        }

    }

    // ファウルが打たれたときの処理を行う。
    void Foul()
    {

        if (this.StrikeCount < 2)
        {
            StrikeCount++;
        }

        // 投球の前に打撃のアクションを実行するための処理をtrueにしておく
        GetComponent<BaseBallAction>().SetTrigger(0);
        // 投球のアクションを実行する
        GetComponent<Pitch>().PitchAction(0);
        Debug.Log("ファウル!");

        // ファウルがフライアウトになるかどうかを判定したのち


    }

    // 投球の最上級のメソッド。ストライク,ボールに振り分けられる。
    public void Pitching()
    {
        
        float check = Random.Range(0, 1.0f) * 100;

        if (check < 50.0)
        {

            if (this.StrikeCount < 2)
            {
                Debug.Log("ストライク!");
                StrikeCount++;
            }
            else
            {
                Debug.Log("三振!");
                this.StrikeCount = 0;
                this.BallCount = 0;
                OutCheck();
                // 打席の加算
                TotalPA();
                // 打数の加算
                TotalAB();
                // 三振数の加算
                TotalSO();
            }

            // 投球のアクションを実行する
            GetComponent<Pitch>().PitchAction(1);

        }
        else
        {

            if (this.BallCount < 3)
            {
                Debug.Log("ボール!");
                BallCount++;
            }
            else
            {
                Debug.Log("フォアボール!");
                this.StrikeCount = 0;
                this.BallCount = 0;
                FourballRunner();
                TotalPA();
                TotalGetMissPitched();
            }

            // 投球のアクションを実行する
            GetComponent<Pitch>().PitchAction(2);

        }

    }

    // 打球がヒットかアウトかを判定する処理。
    void HitOut()
    {

        // ストライク,ボールのカウントをリセットする
        StrikeCount = 0;
        BallCount = 0;

        float JudgeHitOut = Random.Range(0, 1.0f) * 100;

        if (JudgeHitOut < 30.29)
        {
            Hit();
        }
        else
        {
            Out();
        }

    }

    // 単打,二塁打,三塁打,ホームランに分け、打球がゴロ,ライナー,フライのいずれかに分類する処理。
    void Hit()
    {

        TotalH();

        float JudgeHitStatus = Random.Range(0, 1.0f) * 100;

        // ホームラン
        if (JudgeHitStatus < 6.63)
        {
            // 投球の前に打撃のアクションを実行するための処理をtrueにしておく
            GetComponent<BaseBallAction>().SetTrigger(1);
            // 本来は投球をここで行う。

            // 数的な処理を行うメソッド。
            HomeRun();
        }
        // 三塁打
        else if (JudgeHitStatus < 6.63 + 4.10)
        {
            JudgeHit(0f, 26.80f);
            ThreeBaseHit();
        }
        // 二塁打
        else if (JudgeHitStatus < 10.73 + 17.95)
        {
            JudgeHit(11.80f, 22.80f);
            TwoBaseHit();
        }
        else
        {
            JudgeHit(56.32f, 4.36f);
            SingleHit();
        }

    }

    // ゴロヒット ライナーヒット フライヒットを振り分ける
    void JudgeHit(float GroundBallProbability, float LineBallProbability)
    {

        float JudgeHitStatus = Random.Range(0, 1.0f) * 100;

        if (JudgeHitStatus < GroundBallProbability)
        {
            GroundBallHit();
        }
        else if (JudgeHitStatus < GroundBallProbability + LineBallProbability)
        {
            LineBallHit();
        }
        else
        {
            CheckFieldBallHit();
        }

    }

    // ゴロ性の打球がヒットになったときの処理。
    void GroundBallHit()
    {
        // 投球の前に打撃のアクションを実行するための処理をtrueにしておく
        GetComponent<BaseBallAction>().SetTrigger(2);
        // 投球のアクションを実行する
        GetComponent<Pitch>().PitchAction(0);
    }

    // ライナー性の打球がヒットになったときの処理。
    void LineBallHit()
    {
        // 投球の前に打撃のアクションを実行するための処理をtrueにしておく
        GetComponent<BaseBallAction>().SetTrigger(3);
        // 投球のアクションを実行する
        GetComponent<Pitch>().PitchAction(0);
    }

    // フライ性のヒットがポテンヒットか外野を超える打球かを処理する。
    void CheckFieldBallHit()
    {

        float FieldBallHitCheck = Random.Range(0, 1.0f) * 100;

        if (FieldBallHitCheck < 19.3)
        {
            InFieldFlyHit();
        }
        else
        {
            FieldFlyHit();
        }

    }

    // フライ性の打球がポテンヒットになったときの処理。
    void InFieldFlyHit()
    {
        // 投球の前に打撃のアクションを実行するための処理をtrueにしておく
        GetComponent<BaseBallAction>().SetTrigger(4);
        // 投球のアクションを実行する
        GetComponent<Pitch>().PitchAction(0);
    }

    void FieldFlyHit()
    {
        // 投球の前に打撃のアクションを実行するための処理をtrueにしておく
        GetComponent<BaseBallAction>().SetTrigger(5);
        // 投球のアクションを実行する
        GetComponent<Pitch>().PitchAction(0);
    }

    void Out()
    {

        // アウトになる打球の性質を割り振る
        float CheckBallCharacter = Random.Range(0, 1.0f) * 100;

        if (CheckBallCharacter < 48.4)
        {
            GroundBallOut();
        }
        else if (CheckBallCharacter < 48.4 + 8.3)
        {
            LineBallOut();
        }
        else
        {
            FlyBallOut();
        }

        // ダブルプレーや犠牲フライなどの処理が増えた場合は各分岐の最後にOutcheckを加えるか、本メソッド内最後に処理する
        OutCheck();

    }

    // ゴロ性の打球がアウトになったときの処理。
    void GroundBallOut()
    {
        // 投球の前に打撃のアクションを実行するための処理をtrueにしておく
        GetComponent<BaseBallAction>().SetTrigger(6);
        // 投球のアクションを実行する
        GetComponent<Pitch>().PitchAction(0);
    }

    // ライナー性の打球がアウトになったときの処理。
    void LineBallOut()
    {
        // 投球の前に打撃のアクションを実行するための処理をtrueにしておく
        GetComponent<BaseBallAction>().SetTrigger(7);
        // 投球のアクションを実行する
        GetComponent<Pitch>().PitchAction(0);
    }

    // フライ性の打球を内野と外野に振り分ける処理。
    void FlyBallOut()
    {

        float CheckFlyBallOut = Random.Range(0, 1.0f) * 100;

        if (CheckFlyBallOut < 19.3)
        {
            InFieldFlyOut();
        }
        else
        {
            FieldFlyOut();
        }

    }

    // 内野フライの処理。
    void InFieldFlyOut()
    {
        // 投球の前に打撃のアクションを実行するための処理をtrueにしておく
        GetComponent<BaseBallAction>().SetTrigger(8);
        // 投球のアクションを実行する
        GetComponent<Pitch>().PitchAction(0);
    }

    // 外野フライの処理。
    void FieldFlyOut()
    {
        // 投球の前に打撃のアクションを実行するための処理をtrueにしておく
        GetComponent<BaseBallAction>().SetTrigger(9);
        // 投球のアクションを実行する
        GetComponent<Pitch>().PitchAction(0);
        CheckSF();
    }

    // 犠牲フライの処理。
    void CheckSF()
    {

        if (Thirdrunner == 1 && OutCount < 2)
        {
            ThirdRunnerDestroy();

            if (SecondRunner == 1)
            {
                GenerateThirdRunner();
                SecondRunnerDestroy();
            }

            float JudgeSF = Random.Range(0, 1.0f) * 100;

            if (JudgeSF < 80.0)
            {
                Debug.Log("タッチアップ成功!");
                GetScore(1);
            }
            else
            {
                Debug.Log("タッチアップ失敗!");
                OutCheck();
            }

        }

    }

    void SingleHit()
    {

        Debug.Log("シングルヒットです!");

        if (Thirdrunner == 1)
        {
            GetScore(1);
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

    void TwoBaseHit()
    {

        int ProvisionalTwoBaseScore = 0;

        Debug.Log("ツーベースです!");

        if (Thirdrunner == 1)
        {
            ProvisionalTwoBaseScore++;
            ThirdRunnerDestroy();
        }
        if (SecondRunner == 1)
        {
            ProvisionalTwoBaseScore++;
            SecondRunnerDestroy();
        }
        if (FirstRunner == 1)
        {
            GenerateThirdRunner();
            FirstRunnerDestroy();
        }

        GenerateSecondRunner();

        if (ProvisionalTwoBaseScore > 0)
        {
            GetScore(ProvisionalTwoBaseScore);
        }

    }

    void ThreeBaseHit()
    {

        int ProvisionalThreeBaseScore = 0;

        Debug.Log("スリーベースです!");

        if (Thirdrunner == 1)
        {
            ProvisionalThreeBaseScore++;
            ThirdRunnerDestroy();
        }
        if (SecondRunner == 1)
        {
            ProvisionalThreeBaseScore++;
            SecondRunnerDestroy();
        }
        if (FirstRunner == 1)
        {
            ProvisionalThreeBaseScore++;
            FirstRunnerDestroy();
        }

        GenerateThirdRunner();

        if (ProvisionalThreeBaseScore > 0)
        {
            GetScore(ProvisionalThreeBaseScore);
        }

    }

    void HomeRun()
    {

        int ProvisionalHomeRunScore = 1;

        Debug.Log("ホームラン!!");

        if (Thirdrunner == 1)
        {
            ProvisionalHomeRunScore++;
            ThirdRunnerDestroy();
        }
        if (SecondRunner == 1)
        {
            ProvisionalHomeRunScore++;
            SecondRunnerDestroy();
        }
        if (FirstRunner == 1)
        {
            ProvisionalHomeRunScore++;
            FirstRunnerDestroy();
        }

        GetScore(ProvisionalHomeRunScore);

    }

    // 試合や得点,走者に関するメソッド一覧。

    // 走者の生成、消去を行う。
    void GenerateFirstRunner()
    {
        FirstRunner = 1;
    }

    void GenerateSecondRunner()
    {
        SecondRunner = 1;
    }

    void GenerateThirdRunner()
    {
        Thirdrunner = 1;
    }

    void FirstRunnerDestroy()
    {
        FirstRunner = 0;
    }

    void SecondRunnerDestroy()
    {
        SecondRunner = 0;
    }

    void ThirdRunnerDestroy()
    {
        Thirdrunner = 0;
    }

    // フォアボールとなったときの処理。走者が一人ずつ進められ、満塁の場合は1点が加算される。
    void FourballRunner()
    {
        if (FirstRunner == 0)
        {
            GenerateFirstRunner();
        }
        else
        {
            if (SecondRunner == 0)
            {
                GenerateSecondRunner();
            }
            else
            {
                if (Thirdrunner == 0)
                {
                    GenerateThirdRunner();
                }
                else
                {
                    GetScore(1);
                }
            }
        }
    }

    // アウトとなったときの処理。
    void OutCheck()
    {

        if (this.OutCount < 2)
        {
            OutCount++;
        }
        else
        {
            this.OutCount = 0;

            FirstRunnerDestroy();
            SecondRunnerDestroy();
            ThirdRunnerDestroy();

            // 3アウトになったため、攻撃を交代する。
            Inningcheck();
        }

    }

    // 得点が加算されたときの処理。
    void GetScore(int ProvisionalScore)
    {
        // 記録用に全試合のスコアを加算
        ToTalScore += ProvisionalScore;

        if (InningJudge == 0)
        {
            ScoreTop += ProvisionalScore;
        }
        else
        {
            ScoreBottom += ProvisionalScore;
            // 裏の攻撃で得点が加算された場合,サヨナラかどうかを判定する。
            WalkOff();
        }

        Debug.Log(ProvisionalScore + "点追加!");
        Debug.Log("先攻チーム " + ScoreTop + " - " + ScoreBottom + "後攻チーム");

        // 加算が終了したため、一時的な得点は消去する。
        ProvisionalScore = 0;

    }

    // 3アウトチェンジとなった時の処理。
    void Inningcheck()
    {
        // 表の攻撃が終了したとき
        if (InningJudge == 0)
        {
            // 9回の表終了時で後攻チームがリードしている場合は試合終了。
            if (InningCount == 9 && ScoreTop < ScoreBottom)
            {
                GameSet();
            }
            else
            {
                InningJudge++;
                Debug.Log(InningCount + "回裏の攻撃");
                // Debug「x回の裏の攻撃」
            }

        }
        // 裏の攻撃が終了したとき
        else
        {

            // 9回の裏終了時点で先攻チームがリードしている場合は試合終了。
            if (InningCount > 8 && ScoreTop > ScoreBottom)
            {
                GameSet();
            }
            // 15回の裏が終了し、両者の得点が同点の場合は引き分けで試合終了とする。
            else if (InningCount == 15)
            {
                GameSet();
            }
            else
            {
                InningJudge = 0;
                InningCount++;
                Debug.Log(InningCount + "回表の攻撃");
            }

        }

        // 回の終了時とサヨナラのときはイニングを加算
        TotalInning++;

    }

    // 試合がサヨナラかどうかを判定する。
    void WalkOff()
    {

        // 試合が9回以降で、かつ後攻チームの得点が先攻チームを上回っていれば試合終了。
        if (InningCount > 8 && ScoreTop < ScoreBottom)
        {
            Debug.Log("サヨナラ!");
            TotalInning++;
            GameSet();
        }

    }

    // 試合が終了した際の処理。
    void GameSet()
    {
        if (ScoreTop > ScoreBottom)
        {
            Debug.Log(ScoreTop + "-" + ScoreBottom + "で先攻チームが勝利しました");
        }
        else if((ScoreTop < ScoreBottom))
        {
            Debug.Log(ScoreBottom + "-" + ScoreTop + "で後攻チームが勝利しました");
        }
        else{
            Debug.Log("延長15回の熱闘は引き分け再試合となりました!");
        }

        // NextGame();

        Debug.Log("試合終了!");
        GameSetJudge = true;

    }

    // 記録用のメソッド一覧。

    // 投球数
    void TotalNP()
    {
        if (InningJudge == 0)
        {
            NPBottom++;
        }
        else
        {
            NPTop++;
        }

        NP++;

    }

    // 打席(四死球等含む)
    void TotalPA()
    {
        if (InningJudge == 0)
        {
            PATop++;
        }
        else
        {
            PABottom++;
        }

        PA++;

    }

    // 打数
    void TotalAB()
    {
        if (InningJudge == 0)
        {
            ABTop++;
        }
        else
        {
            ABBottom++;
        }

        AB++;

    }

    // 三振数
    void TotalSO()
    {
        if (InningJudge == 0)
        {
            SOTop++;
        }
        else
        {
            SOBottom++;
        }

        SO++;

    }

    void TotalGetMissPitched()
    {
        if (InningJudge == 0)
        {
            GetMissPitchedTop++;
        }
        else
        {
            GetMissPitchedBottom++;
        }

        GetMissPitched++;

    }

    void TotalH()
    {
        if (InningJudge == 0)
        {
            HTop++;
        }
        else
        {
            HBottom++;
        }

        H++;

    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.U))
        {
            BaseBallGame();
        }

		
	}
}
