using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleBaseBall : MonoBehaviour
{


    /*投球ごとに打撃結果に移行するかの割合は異なる。カウントごとに異なる数値を渡してあげるための関数
    void Count
    {

    }

*/


    public void PitchingBatting()
    {

        TotalNP();

        float check = Random.Range(0, 1.0f) * 100;

        if (check < 40.0)
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
                TotalPA();
                TotalAB();
                TotalSO();
            }

        }
        else if (check < 40.0 + 40.0)
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

        }
        else
        {
            FairFaulJudge();
        }

    }

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

    // 打球がフェアゾーンに飛ぶかどうかを判定するための関数
    void FairFaulJudge()
    {
        float CheckFairFaulJudge = Random.Range(0, 1.0f) * 100;

        if (CheckFairFaulJudge < 50.0)
        {
            Faul();
        }
        else
        {
            TotalPA();
            TotalAB();
            BattedBall();
        }

    }

    // ファウルが打たれたときの処理を行う関数。
    void Faul()
    {
        Debug.Log("ファウル!");

        if (this.StrikeCount < 2)
        {
            StrikeCount++;
        }

    }

    // 打球の性質を決定する関数。ヒットかアウトかを判定するためにGroundBall,LineBall,FlyBall関数を呼び出す
    void BattedBall()
    {
        this.StrikeCount = 0;
        this.BallCount = 0;

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
            GroundBallOut();
        }
        else
        {
            GroundBallHit();
        }

    }

    // ゴロ性の打球がアウトになったときの処理。
    void GroundBallOut()
    {

        float CheckGroundBallOut = Random.Range(0, 1.0f) * 100;

        if (CheckGroundBallOut < 15.0)
        {
            Debug.Log("ピッチャーへのゴロでアウトでした");
        }
        else if (CheckGroundBallOut < 15.0 + 20.0)
        {
            Debug.Log("ファーストへのゴロでアウトでした");
        }
        else if (CheckGroundBallOut < 35.0 + 20.0)
        {
            Debug.Log("セカンドへのゴロでアウトでした");
        }
        else if (CheckGroundBallOut < 55.0 + 20.0)
        {
            Debug.Log("サードへのゴロでアウトでした");
        }
        else if (CheckGroundBallOut < 75.0 + 25.0)
        {
            Debug.Log("ショートへのゴロでアウトでした");
        }
        else
        {
            Debug.Log("ライトへのゴロでアウトでした");
        }

        OutCheck();

    }

    // ゴロ性の打球がヒットになったときの処理。
    void GroundBallHit()
    {

        float CheckGroundBallHit = Random.Range(0, 1.0f) * 100;

        if (CheckGroundBallHit < 20.0)
        {
            Debug.Log("1塁線を抜けるゴロ性の当たりでヒットでした");
        }
        else if (CheckGroundBallHit < 15.0 + 20.0)
        {
            Debug.Log("1,2塁間を抜けるゴロ性の当たりでヒットでした");
        }
        else if (CheckGroundBallHit < 35.0 + 20.0)
        {
            Debug.Log("センター前に抜けるゴロ性の当たりでヒットでした");
        }
        else if (CheckGroundBallHit < 55.0 + 20.0)
        {
            Debug.Log("三遊間を抜けるゴロ性の当たりでヒットでした");
        }
        else if (CheckGroundBallHit < 75.0 + 15.0)
        {
            Debug.Log("3塁線を抜けるゴロ性の当たりでヒットでした");
        }
        else
        {
            Debug.Log("ピッチャーの前に落ちるラッキーなゴロ性のヒットでした");
        }

        SingleHit();

    }

    // ライナー性の打球がヒットかアウトかを判定する
    void LineBall()
    {

        float CheckLineBall = Random.Range(0, 1.0f) * 100;

        if (CheckLineBall < 20.0)
        {
            LineBallOut();
        }
        else
        {
            LineBallHit();
        }

    }

    // ライナー性の打球がアウトになったときの処理。
    void LineBallOut()
    {

        float CheckLineBallOut = Random.Range(0, 1.0f) * 100;

        if (CheckLineBallOut < 10.0)
        {
            Debug.Log("ピッチャーへのライナーでアウトでした");
        }
        else if (CheckLineBallOut < 10.0 + 15.0)
        {
            Debug.Log("ファーストへのライナーでアウトでした");
        }
        else if (CheckLineBallOut < 25.0 + 15.0)
        {
            Debug.Log("セカンドへのライナーでアウトでした");
        }
        else if (CheckLineBallOut < 40.0 + 15.0)
        {
            Debug.Log("サードへのライナーでアウトでした");
        }
        else if (CheckLineBallOut < 60.0 + 10.0)
        {
            Debug.Log("ショートへのライナーでアウトでした");
        }
        else if (CheckLineBallOut < 70.0 + 10.0)
        {
            Debug.Log("レフトへのライナーでアウトでした");
        }
        else if (CheckLineBallOut < 80.0 + 10.0)
        {
            Debug.Log("センターへのライナーでアウトでした");
        }
        else
        {
            Debug.Log("ライトへのライナーでアウトでした");
        }

        OutCheck();

    }

    // ゴロ性の打球がヒットになったときの処理。
    void LineBallHit()
    {

        float CheckLineBallHit = Random.Range(0, 1.0f) * 100;

        if (CheckLineBallHit < 20.0)
        {
            Debug.Log("1塁線を抜けるライナー性の当たりでヒットでした");
        }
        else if (CheckLineBallHit < 20.0 + 15.0)
        {
            Debug.Log("1,2塁間を抜けるライナー性の当たりでヒットでした");
        }
        else if (CheckLineBallHit < 35.0 + 20.0)
        {
            Debug.Log("センター前に抜けるライナー性の当たりでヒットでした");
        }
        else if (CheckLineBallHit < 55.0 + 20.0)
        {
            Debug.Log("三遊間を抜けるライナー性の当たりでヒットでした");
        }
        else if (CheckLineBallHit < 75.0 + 15.0)
        {
            Debug.Log("3塁線を抜けるライナー性の当たりでヒットでした");
        }
        else
        {
            Debug.Log("ピッチャー強襲のライナー性のヒットでした");
        }

        SingleHit();

    }

    void FlyBall()
    {

        float CheckFlyBall = Random.Range(0, 1.0f) * 100;

        if (CheckFlyBall < 19.3)
        {
            InFieldFly();
        }
        else
        {
            FieldFly();
        }

    }

    void InFieldFly()
    {

        float CheckInFieldFlyBall = Random.Range(0, 1.0f) * 100;

        if (CheckInFieldFlyBall < 95.0)
        {
            Debug.Log("インフィールドフライアウトでした");
            OutCheck();
        }
        else
        {
            Debug.Log("内野フライヒットでした");
            SingleHit();
        }

    }

    void FieldFly()
    {

        float CheckFieldFlyBall = Random.Range(0, 1.0f) * 100;

        if (CheckFieldFlyBall < 63.9)
        {
            Debug.Log("フライアウトでした");
            OutCheck();
        }
        else if (CheckFieldFlyBall < 63.9 + 26.7)
        {
            FieldFlyHit();
        }
        else
        {
            Debug.Log("ホームラン!!");
            HomeRun();
        }

    }

    void FieldFlyHit()
    {
        float CheckFieldFlyHit = Random.Range(0, 1.0f) * 100;

        // レフト線
        if (CheckFieldFlyHit < 15.0)
        {
            Debug.Log("レフト線を抜ける当たり!");
            JudgeHit(50.0f, 50.0f);
        }
        // 左中間
        else if (CheckFieldFlyHit < 15.0 + 25.0)
        {
            Debug.Log("左中間を抜ける当たり!");
            JudgeHit(0f, 80.0f);
        }
        //  センターオーバー
        else if (CheckFieldFlyHit < 40.0 + 20.0)
        {
            Debug.Log("センターオーバーの当たり!");
            JudgeHit(0f, 100.0f);
        }
        // 右中間
        else if (CheckFieldFlyHit < 60.0 + 25.0)
        {
            Debug.Log("右中間を抜ける当たり!");
            JudgeHit(0f, 80.0f);
        }
        // ライト線
        else
        {
            Debug.Log("ライト線を抜ける当たり!");
            JudgeHit(50.0f, 50.0f);
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

    // シングルヒット 2塁打 3塁打を振り分ける
    void JudgeHit(float SingleHitProbability, float TwobaseHitProbability)
    {

        float JudgeHitStatus = Random.Range(0, 1.0f) * 100;

        if (JudgeHitStatus < SingleHitProbability)
        {
            SingleHit();
        }
        else if (JudgeHitStatus < SingleHitProbability + TwobaseHitProbability)
        {
            TwoBaseHit();
        }
        else
        {
            ThreeBaseHit();
        }

    }

    void GetScore(int ProvisionalScore)
    {
        // 記録用に全試合のスコアを加算
        ToTalScore += ProvisionalScore;

        if (InningJudge == 0)
        {
            ScoreTopTeam += ProvisionalScore;
        }
        else
        {
            ScoreBottomTeam += ProvisionalScore;
            CheckWalkOff();
        }

        Debug.Log(ProvisionalScore + "点追加!");
        Debug.Log("先攻チーム " + ScoreTopTeam + " - " + ScoreBottomTeam + "後攻チーム");

        ProvisionalScore = 0;

    }

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

            Inningcheck();
        }

    }

    void Inningcheck()
    {
        // 表の攻撃が終了したとき
        if (InningJudge == 0)
        {
            // 9回の表終了時で後攻チームがリードしている場合
            if (InningCount == 9 && ScoreTopTeam < ScoreBottomTeam)
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

            if (InningCount > 8 && ScoreTopTeam > ScoreBottomTeam)
            {
                GameSet();
            }
            else if (InningCount == 15)
            {
                Draw();
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

    void CheckWalkOff()
    {

        if (InningCount > 8 && ScoreTopTeam < ScoreBottomTeam)
        {
            Debug.Log("サヨナラ!");
            TotalInning++;
            GameSet();
        }

    }

    void GameSet()
    {
        if (InningJudge == 0)
        {
            // 「x-x 先攻チームが勝利しました」
        }
        else
        {
            // 「x-x 後攻チームが勝利しました」
        }

        NextGame();

        Debug.Log("試合終了!");
        GameSetJudge = true;

    }

    void Draw()
    {
        Debug.Log("延長15回引き分け!");
        NextGame();

        GameSetJudge = true;
    }

    // 連続で試合が行われる際に使用される
    void NextGame()
    {
        StrikeCount = 0;
        BallCount = 0;
        OutCount = 0;

        InningCount = 1;
        InningJudge = 0;

        FirstRunner = 0;
        SecondRunner = 0;
        Thirdrunner = 0;
    }

    // 記録用のメソッド

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



    // データ取得用

    int TotalInning;

    //データとして必要な打者の成績

    // 得点(実装済み)
    int ScoreTopTeam;
    int ScoreBottomTeam;

    int ToTalScore;
    // 打席(四死球等含む)
    int PATop;
    int PABottom;

    int PA;
    // 打数
    int ABTop;
    int ABBottom;

    int AB;
    // 三振した数
    int SOTop;
    int SOBottom;

    int SO;
    // 四死球を得た数
    int GetMissPitchedTop;
    int GetMissPitchedBottom;

    int GetMissPitched;
    // ヒットの総数
    int HitTotalTop;
    int HitTotalBottom;
    // ゴロヒット
    int GroundBallHitTop;
    int GroundBallHitBottom;
    // ライナーヒット
    int LineBallHitTop;
    int LineBallHitBottom;
    // フライヒット
    int FlyBallHitTop;
    int FlyBallHitBottom;
    // 単打
    int HTop;
    int HBottom;
    // 二塁打
    int TwoBTop;
    int TwoBBottom;
    // 三塁打
    int ThreeBTop;
    int ThreeBBottom;
    // ホームラン
    int HRTop;
    int HRBottom;
    // ゴロアウト
    int GroundBallOutTop;
    int GroundBallOutBottom;
    // ライナーアウト
    int LineBallOutTop;
    int LineBallOutBottom;
    // フライアウト
    int FlyBallOutTop;
    int FlyBallOutBottom;

    //データとして必要な投手の成績

    // 投球数
    int NPTop;
    int NPBottom;

    int NP;
    // うちストライク
    int TotalStrikeTop;
    int TotalStrikeBottom;
    // うちボール
    int TotalBallTop;
    int TotalBallBottom;
    // 被安打
    int HAllowedTop;
    int HAllowedBottom;
    // 被本塁打
    int HRAllowedTop;
    int HRAllowedBottom;
    // 奪三振数
    int KTop;
    int KBottom;
    // 与四死球数
    int MissPitchedTop;
    int MissPitchedBottom;

    // どちらのチームでも使われる
    int StrikeCount;
    int BallCount;
    int OutCount;

    int InningCount = 1;
    int InningJudge;

    int FirstRunner;
    int SecondRunner;
    int Thirdrunner;

    bool GameSetJudge;

    // Use this for initialization
    void Start()
    {

        for (int x = 1; x < 100; x++)
        {
            ScoreTopTeam = 0;
            ScoreBottomTeam = 0;

            GameSetJudge = false;

            Debug.Log("第" + x + "試合開始!");
            Debug.Log("試合開始!");
            Debug.Log(InningCount + "回表の攻撃");
            // Debug.Log("カウント :" + BallCount + "ボール" + StrikeCount + "ストライク");
            while (true)
            {
                PitchingBatting();
                if (GameSetJudge == true) break;
            }
            Debug.Log("第" + x + "試合の結果 ; 先攻チーム " + ScoreTopTeam + " - " + ScoreBottomTeam + "後攻チーム");

        }

        // 全試合の平均

        Debug.Log("総投球数 : " + NP);
        Debug.Log("総得点数 : " + ToTalScore);
        Debug.Log("総イニング数 : " + TotalInning);
        Debug.Log("総打席数(四死球含む) : " + PA);
        Debug.Log("総打数 : " + AB);
        Debug.Log("総三振数 : " + SO);
        Debug.Log("総獲得四死球数 : " + GetMissPitched);

        // float AverageTeamScore = ToTalScore / TotalInning * 9;
        // Debug.Log("1試合あたり1チームの平均得点 : " + AverageTeamScore);

    }

    // Update is called once per frame
    void Update()
    {

    }

}