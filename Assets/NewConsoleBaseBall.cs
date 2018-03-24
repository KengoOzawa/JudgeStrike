using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewConsoleBaseBall : MonoBehaviour
{


    /*投球ごとに打撃結果に移行するかの割合は異なる。カウントごとに異なる数値を渡してあげるための関数
    void Count
    {

    }

*/

    public void JudgeBant()
    {

        if (StatusBunt == true)
        {

            // 送りバントの処理。
            if (Thirdrunner == 0 && StrikeCount < 2 && OutCount < 2)
            {

                if (OutCount == 0)
                {
                    JudgeDoBunt(70.0f);
                }
                else
                {
                    JudgeDoBunt(30.0f);
                }

            }
            // スクイズができる状況ならそちらへ、追い込まれているなら通常のバッティングに切り替える
            else
            {

                // スクイズの処理を記入
                PitchingBatting();

            }


        }
        else
        {
            PitchingBatting();
        }

    }

    void JudgeDoBunt(float DoBuntRate)
    {

        float DoBunt = Random.Range(0, 1.0f) * 100;

        if (DoBunt < DoBuntRate)
        {
            Debug.Log("送りバントの構えを見せています!");
            JudgeSuccessBunt();
        }
        // 打者の能力が高い場合には最初からヒッティング
        else
        {
            Debug.Log("ここはヒッティングでくるようです!");
            // 無死1塁で7番バッターは打力があるためヒッティングだが、8番にはバント
            StatusBunt = false;
            PitchingBatting();
        }

    }

    void JudgeSuccessBunt()
    {

        TotalNP();

        float SuccessBunt = Random.Range(0, 1.0f) * 100;

        if (SuccessBunt < 60.0)
        {

            if (SecondRunner == 1)
            {
                GenerateThirdRunner();
                SecondRunnerDestroy();
            }

            Debug.Log("送りバント成功!");
            GenerateSecondRunner();
            FirstRunnerDestroy();
            StatusBunt = false;
            TotalBunt++;
            OutCheck();

        }
        else if (SuccessBunt < 60.0 + 20.0)
        {

            if (StrikeCount < 1)
            {
                Faul();
                JudgeSuccessBunt();
            }
            else
            {
                JudgeBant();
            }

        }
        else
        {
            Debug.Log("送りバント失敗!");
            OutCheck();
        }

    }

    void PitchingBatting()
    {

        TotalNP();

        float check = Random.Range(0, 1.0f) * 100;

        if (check < 25.0)
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
        else if (check < 25.0 + 30.0)
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
                MissPitched();
                TotalPA();
                TotalGetMissPitched();
            }

        }
        else if (check < 55.0 + 0.3)
        {
            Debug.Log("デッドボール!");
            this.StrikeCount = 0;
            this.BallCount = 0;
            MissPitched();
            TotalPA();
            TotalGetMissPitched();
            DeadBall++;
        }
        else
        {
            FairFaulJudge();
        }

    }

    void MissPitched()
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
            HitOut();
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

    void HitOut()
    {

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

    void Hit()
    {

        TotalH();

        float JudgeHitStatus = Random.Range(0, 1.0f) * 100;

        if (JudgeHitStatus < 6.63)
        {
            FieldFlyHit();
            HomeRun();
        }
        else if (JudgeHitStatus < 6.63 + 4.10)
        {
            JudgeHit(0f, 26.80f);
            ThreeBaseHit();
        }
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

    // ゴロ性の打球がシングルヒットになったときの処理。
    void GroundBallHit()
    {

        float CheckGroundBallHit = Random.Range(0, 1.0f) * 100;

        if (CheckGroundBallHit < 20.0)
        {
            Debug.Log("1塁線を抜けるゴロ性の当たり!");
        }
        else if (CheckGroundBallHit < 15.0 + 20.0)
        {
            Debug.Log("1,2塁間を抜けるゴロ性の当たり!");
        }
        else if (CheckGroundBallHit < 35.0 + 20.0)
        {
            Debug.Log("センター前に抜けるゴロ性の当たり!");
        }
        else if (CheckGroundBallHit < 55.0 + 20.0)
        {
            Debug.Log("三遊間を抜けるゴロ性の当たり!");
        }
        else if (CheckGroundBallHit < 75.0 + 15.0)
        {
            Debug.Log("3塁線を抜けるゴロ性の当たり!");
        }
        else
        {
            Debug.Log("ピッチャーの前に落ちるラッキーなゴロ性の当たり!");
        }

    }

    // ライナー性の打球がシングルヒットになったときの処理。
    void LineBallHit()
    {

        float CheckLineBallHit = Random.Range(0, 1.0f) * 100;

        if (CheckLineBallHit < 20.0)
        {
            Debug.Log("1塁線を抜けるライナー性の当たり!");
        }
        else if (CheckLineBallHit < 20.0 + 15.0)
        {
            Debug.Log("1,2塁間を抜けるライナー性の当たり!");
        }
        else if (CheckLineBallHit < 35.0 + 20.0)
        {
            Debug.Log("センター前に抜けるライナー性の当たり!");
        }
        else if (CheckLineBallHit < 55.0 + 20.0)
        {
            Debug.Log("三遊間を抜けるライナー性の当たり!");
        }
        else if (CheckLineBallHit < 75.0 + 15.0)
        {
            Debug.Log("3塁線を抜けるライナー性の当たり!");
        }
        else
        {
            Debug.Log("ピッチャー強襲のライナー性の当たり!");
        }

    }

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

    void InFieldFlyHit()
    {
        Debug.Log("内野と外野の間に落ちる打球!");
    }

    void FieldFlyHit()
    {
        float CheckFieldFlyHit = Random.Range(0, 1.0f) * 100;

        // レフト線
        if (CheckFieldFlyHit < 15.0)
        {
            Debug.Log("レフト線への当たり!");
        }
        // 左中間
        else if (CheckFieldFlyHit < 15.0 + 25.0)
        {
            Debug.Log("左中間への当たり!");
        }
        //  センターオーバー
        else if (CheckFieldFlyHit < 40.0 + 20.0)
        {
            Debug.Log("センターオーバーの当たり!");
        }
        // 右中間
        else if (CheckFieldFlyHit < 60.0 + 25.0)
        {
            Debug.Log("右中間への当たり!");
        }
        // ライト線
        else
        {
            Debug.Log("ライト線への当たり!");
        }

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
        else if (CheckGroundBallOut < 75.0 + 20.0)
        {
            Debug.Log("ショートへのゴロでアウトでした");
        }
        else
        {
            Debug.Log("ライトへのゴロでアウトでした");
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

        float CheckInFieldFlyOut = Random.Range(0, 1.0f) * 100;

        if (CheckInFieldFlyOut < 10.0)
        {
            Debug.Log("ピッチャーへのフライでアウトでした");
        }
        else if (CheckInFieldFlyOut < 10.0 + 10.0)
        {
            Debug.Log("キャッチャーへのフライでアウトでした");
        }
        else if (CheckInFieldFlyOut < 20.0 + 20.0)
        {
            Debug.Log("ファーストへのフライでアウトでした");
        }
        else if (CheckInFieldFlyOut < 40.0 + 20.0)
        {
            Debug.Log("セカンドへのフライでアウトでした");
        }
        else if (CheckInFieldFlyOut < 60.0 + 20.0)
        {
            Debug.Log("サードへのフライでアウトでした");
        }
        else
        {
            Debug.Log("ショートへのフライでアウトでした");
        }

    }

    // 外野フライの処理。
    void FieldFlyOut()
    {

        float CheckLineFieldFlyOut = Random.Range(0, 1.0f) * 100;

        if (CheckLineFieldFlyOut < 30.0)
        {
            Debug.Log("レフトへのフライでアウトでした");
        }
        else if (CheckLineFieldFlyOut < 30.0 + 40.0)
        {
            Debug.Log("センターへのフライでアウトでした");
        }
        else
        {
            Debug.Log("ライトへのフライでアウトでした");
        }

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
        StatusBunt = true;
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

            StatusBunt = false;

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

    int H;
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

    // 犠打数
    int TotalBunt;

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

    // 与四球数
    int DeadBall;

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
    bool StatusBunt;

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
                JudgeBant();
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
        Debug.Log("総死球数 : " + DeadBall);
        Debug.Log("総安打数 : " + H);

        // float AverageTeamScore = ToTalScore / TotalInning * 9;
        // Debug.Log("1試合あたり1チームの平均得点 : " + AverageTeamScore);

    }

    // Update is called once per frame
    void Update()
    {

    }

}