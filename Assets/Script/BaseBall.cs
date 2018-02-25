﻿using System.Collections; using System.Collections.Generic; using UnityEngine; using UnityEngine.UI;   // データをひっぱってくるとき // インターネットの通信を使いたいとき // 参照する必要しない場合はローカル変数で書いてしまって良い  public class BaseBall : MonoBehaviour {          /*投球ごとに打撃結果に移行するかの割合は異なる。カウントごとに異なる数値を渡してあげるための関数     void Count     {      }  */      void BaseBallAction(float Left, float Right, float HeightMin, float HeightMax, float PowerMin, float PowerMax){          GameObject Pitching = Instantiate(HittingBallPrefab) as GameObject;          Pitching.transform.position = HittingPivot.position;          // 打球がレフトに飛ぶか、ライトに飛ぶかを決める         float Wide01 = Random.Range(Left, Right);         // 打球の角度         float Angle = Random.Range(HeightMin, HeightMax);         // 打球がファールになるか、フェアになるかを決める         float Wide02 = Random.Range(PowerMin, PowerMax);          // Rigidbodyに力を加えて投球         Pitching.GetComponent<Rigidbody>().AddForce(Wide01, Angle, Wide02, ForceMode.Impulse);      }      void BaseBallActionFair(float Left, float Right, float HeightMin, float HeightMax, float PowerMax)     {          GameObject Pitching = Instantiate(HittingBallPrefab) as GameObject;          Pitching.transform.position = HittingPivot.position;          // 打球がレフトに飛ぶか、ライトに飛ぶかを決める         float Wide01 = Random.Range(Left, Right);         // 打球の角度         float Angle = Random.Range(HeightMin, HeightMax);         // 打球がファールになるか、フェアになるかを決める         // このケースでは絶対値を取得してx<zのため必ずフェアになる         float Wide02 = Random.Range(Mathf.Abs(Wide01), PowerMax);          // Rigidbodyに力を加えて投球         Pitching.GetComponent<Rigidbody>().AddForce(Wide01, Angle, Wide02, ForceMode.Impulse);      }      public void PitchingBatting()     {          float check = Random.Range(0, 1.0f) * 100;          if (check < 40.0)         {              if (this.StrikeCount < 2)             {                 this.Ampire.GetComponent<Text>().text = "ストライク!";                 StrikeCount++;                 this.StrikeCountText.GetComponent<Text>().text = this.StrikeCount.ToString();             }             else             {                 this.Ampire.GetComponent<Text>().text = "三振!";                 this.StrikeCount = 0;                 this.BallCount = 0;                 this.StrikeCountText.GetComponent<Text>().text = this.StrikeCount.ToString();                 this.BallCountText.GetComponent<Text>().text = this.BallCount.ToString();                 OutCheck();             }          }         else if (check < 40.0 + 40.0)         {              if (this.BallCount < 3)             {                 this.Ampire.GetComponent<Text>().text = "ボール!";                 BallCount++;                 this.BallCountText.GetComponent<Text>().text = this.BallCount.ToString();             }             else             {                 this.Ampire.GetComponent<Text>().text = "フォアボール!";                 this.StrikeCount = 0;                 this.BallCount = 0;                 this.StrikeCountText.GetComponent<Text>().text = this.StrikeCount.ToString();                 this.BallCountText.GetComponent<Text>().text = this.BallCount.ToString();                 FourballRunner();             }          }         else         {             GameObject[] Battingdestroy = GameObject.FindGameObjectsWithTag("NewBallPrefab");             foreach (GameObject obj in Battingdestroy)             {                 Destroy(obj);             }              this.Ampire.GetComponent<Text>().text = "打球判定";             Debug.Log("打撃判定を行います!");             FairFaulJudge();         }      }      void FourballRunner(){         if(FirstRunner == 0){             GenerateFirstRunner();         }else{             if(SecondRunner == 0){                 GenerateSecondRunner();             }else{                 if(Thirdrunner == 0){                     GenerateThirdRunner();                 }else{                     Get1Score();                     AddTeamScoreText();                 }             }         }     }      // 打球がフェアゾーンに飛ぶかどうかを判定するための関数     void FairFaulJudge()     {         float CheckFairFaulJudge = Random.Range(0, 1.0f) * 100;          if (CheckFairFaulJudge < 50.0)         {             Faul();         }         else         {             BattedBall();         }      }      // ファウルが打たれたときの処理を行う関数。     void Faul()     {         this.Ampire.GetComponent<Text>().text = "ファウル!";         Debug.Log("ファウルでした!");          // 将来的には前方へ飛ぶファウルも実装したい         BaseBallAction(-1.0f, 1.0f, -0.1f, 1.0f, -1.0f, -0.1f);          if (this.StrikeCount < 2)         {             StrikeCount++;             this.StrikeCountText.GetComponent<Text>().text = this.StrikeCount.ToString();         }      }      // 打球の性質を決定する関数。ヒットかアウトかを判定するためにGroundBall,LineBall,FlyBall関数を呼び出す     void BattedBall()     {         Debug.Log("フェアです!ボールを判定します!");          this.StrikeCount = 0;         this.BallCount = 0;         this.StrikeCountText.GetComponent<Text>().text = this.StrikeCount.ToString();         this.BallCountText.GetComponent<Text>().text = this.BallCount.ToString();          // 打球の性質を割り振る         float CheckBallCharacter = Random.Range(0, 1.0f) * 100;          if (CheckBallCharacter < 48.4)         {             GroundBall();         }         else if (CheckBallCharacter < 48.4 + 8.3)         {             LineBall();         }         else         {             FlyBall();         }      }      // 最終的な打球結果が判定される関数。      // ゴロ性の打球かヒットかアウトかを判定する     void GroundBall()     {         float CheckGroundBall = Random.Range(0, 1.0f) * 100;          if (CheckGroundBall < 73.6)         {             GroundBallOut();         }         else         {             GroundBallHit();         }      }      // ゴロ性の打球がアウトになったときの処理。     void GroundBallOut(){          float CheckGroundBallOut = Random.Range(0, 1.0f) * 100;          if (CheckGroundBallOut < 15.0)         {             this.Ampire.GetComponent<Text>().text = "ピッチャーゴロ!";             Debug.Log("ピッチャーへのゴロでアウトでした");             OutCheck();              BaseBallActionFair(-1.0f, 1.0f, -0.1f, 0f, 1.0f);         }         else if (CheckGroundBallOut < 15.0 + 20.0)         {             this.Ampire.GetComponent<Text>().text = "ファーストゴロ!";             Debug.Log("ファーストへのゴロでアウトでした");             OutCheck();              BaseBallActionFair(-1.0f, 1.0f, -0.1f, 0f, 1.0f);         }         else if (CheckGroundBallOut < 35.0 + 20.0){             this.Ampire.GetComponent<Text>().text = "セカンドゴロ!";             Debug.Log("セカンドへのゴロでアウトでした");             OutCheck();              BaseBallActionFair(-1.0f, 1.0f, -0.1f, 0f, 1.0f);         }         else if (CheckGroundBallOut < 55.0 + 20.0)         {             this.Ampire.GetComponent<Text>().text = "サードゴロ!";             Debug.Log("サードへのゴロでアウトでした");             OutCheck();              BaseBallActionFair(-1.0f, 1.0f, -0.1f, 0f, 1.0f);         }         else if (CheckGroundBallOut < 75.0 + 25.0){             this.Ampire.GetComponent<Text>().text = "ショートゴロ!";             Debug.Log("ショートへのゴロでアウトでした");             OutCheck();              BaseBallActionFair(-1.0f, 1.0f, -0.1f, 0f, 1.0f);         }         else         {             this.Ampire.GetComponent<Text>().text = "ライトゴロ!";             Debug.Log("ライトへのゴロでアウトでした");             OutCheck();              BaseBallActionFair(-1.0f, 1.0f, -0.1f, 0f, 1.0f);         }      }      // ゴロ性の打球がヒットになったときの処理。     void GroundBallHit()     {          float CheckGroundBallHit = Random.Range(0, 1.0f) * 100;          if (CheckGroundBallHit < 20.0)         {             this.Ampire.GetComponent<Text>().text = "1塁線へのヒット!";             Debug.Log("1塁線を抜けるゴロ性の当たりでヒットでした");             SingleHit();              BaseBallActionFair(1.1f, 1.3f, -0.3f, 0f, 1.3f);         }         else if (CheckGroundBallHit < 15.0 + 20.0)         {             this.Ampire.GetComponent<Text>().text = "1,2塁間を抜けた!";             Debug.Log("1,2塁間を抜けるゴロ性の当たりでヒットでした");             SingleHit();              BaseBallAction(0.6f, 0.8f, -0.3f, 0f, 1.7f, 1.9f);         }         else if (CheckGroundBallHit < 35.0 + 20.0)         {             this.Ampire.GetComponent<Text>().text = "センター前へ抜けた!";             Debug.Log("センター前に抜けるゴロ性の当たりでヒットでした");             SingleHit();              BaseBallAction(-0.1f, 0.1f, -0.3f, 0f, 1.7f, 2.0f);         }         else if (CheckGroundBallHit < 55.0 + 20.0)         {             this.Ampire.GetComponent<Text>().text = "三遊間を抜けた!";             Debug.Log("三遊間を抜けるゴロ性の当たりでヒットでした");             SingleHit();              BaseBallAction(-0.6f, -0.8f, -0.3f, 0f, 1.7f, 1.9f);         }         else if (CheckGroundBallHit < 75.0 + 15.0)         {             this.Ampire.GetComponent<Text>().text = "3塁線を抜けた!";             Debug.Log("3塁線を抜けるゴロ性の当たりでヒットでした");             SingleHit();              BaseBallActionFair(-1.1f, -1.3f, -0.3f, 0f, 1.3f);         }         else         {             this.Ampire.GetComponent<Text>().text = "ピッチャー前のヒット!";             Debug.Log("ピッチャーの前に落ちるラッキーなゴロ性のヒットでした");             SingleHit();              BaseBallActionFair(-0.1f, 0.1f, -0.3f, 0f, 0.3f);         }      }      // ライナー性の打球がヒットかアウトかを判定する     void LineBall()     {          float CheckLineBall = Random.Range(0, 1.0f) * 100;          if (CheckLineBall < 20.0)         {             this.Ampire.GetComponent<Text>().text = "ライナーアウト!";             Debug.Log("ライナーアウトでした");             OutCheck();              // プレハブの野球ボールを複製             GameObject Pitching = Instantiate(HittingBallPrefab) as GameObject;             // どこからボールを投げるかを指定する             Pitching.transform.position = HittingPivot.position;              // 打球がレフトに飛ぶか、ライトに飛ぶかを決める             float Wide01 = Random.Range(-1.0f, 1.0f);             // Debug.Log(Wide01);             // 打球の角度             float Angle = Random.Range(0f, 0.2f);             // 打球がファールになるか、フェアになるかを決める             // このケースでは絶対値を取得してx<zのため必ずフェアになる             float Wide02 = Random.Range(Mathf.Abs(Wide01), 1.0f);             // Debug.Log(Wide02);              // Rigidbodyに力を加えて投球             // AddForceは最後にForceMode.Force(何も書かなければこちらがデフォルト)かForceMode.Impulseを選べる             Pitching.GetComponent<Rigidbody>().AddForce(Wide01 * 1.1f, Angle, Wide02 * 1.1f, ForceMode.Impulse);         }         else         {             this.Ampire.GetComponent<Text>().text = "ライナーヒット!";             Debug.Log("ライナーヒットでした");             SingleHit();              // プレハブの野球ボールを複製             GameObject Pitching = Instantiate(HittingBallPrefab) as GameObject;             // どこからボールを投げるかを指定する             Pitching.transform.position = HittingPivot.position;              // 打球がレフトに飛ぶか、ライトに飛ぶかを決める             float Wide01 = Random.Range(-1.0f, 1.0f);             // Debug.Log(Wide01);             // 打球の角度             float Angle = Random.Range(0f, 0.2f);             // 打球がファールになるか、フェアになるかを決める             // このケースでは絶対値を取得してx<zのため必ずフェアになる             float Wide02 = Random.Range(Mathf.Abs(Wide01), 1.0f);             // Debug.Log(Wide02);              // Rigidbodyに力を加えて投球             // AddForceは最後にForceMode.Force(何も書かなければこちらがデフォルト)かForceMode.Impulseを選べる             Pitching.GetComponent<Rigidbody>().AddForce(Wide01 * 1.1f, Angle, Wide02 * 1.1f, ForceMode.Impulse);         }      }        void FlyBall()     {          float CheckFlyBall = Random.Range(0, 1.0f) * 100;          if (CheckFlyBall < 19.3){             InFieldFly();         }         else         {             FieldFly();         }      }      void InFieldFly(){          float CheckInFieldFlyBall = Random.Range(0, 1.0f) * 100;          if (CheckInFieldFlyBall < 95.0)         {             this.Ampire.GetComponent<Text>().text = "内野フライアウト!";             Debug.Log("インフィールドフライアウトでした");             OutCheck();              BaseBallActionFair(-1.0f, 1.0f, 0f, 1.0f, 1.0f);          }         else         {             this.Ampire.GetComponent<Text>().text = "内野フライヒット!";             Debug.Log("内野フライヒットでした");             SingleHit();              BaseBallActionFair(-1.0f, 1.0f, 0f, 1.0f, 1.0f);          }      }      void FieldFly(){          float CheckFieldFlyBall = Random.Range(0, 1.0f) * 100;          if (CheckFieldFlyBall < 63.9)         {             this.Ampire.GetComponent<Text>().text = "フライアウト!";             Debug.Log("フライアウトでした");             OutCheck();              BaseBallActionFair(-1.0f, 1.0f, 0f, 1.0f, 1.0f);          }         else if (CheckFieldFlyBall < 63.9 + 26.7)         {             this.Ampire.GetComponent<Text>().text = "フライヒット!";             Debug.Log("フライヒットでした");             SingleHit();              BaseBallActionFair(-1.0f, 1.0f, 0f, 1.0f, 1.0f);          }         else         {             this.Ampire.GetComponent<Text>().text = "ホームラン!!";             Debug.Log("ホームランでした");             Get1Score();             ClearTheBases();             AddTeamScoreText();              // プレハブの野球ボールを複製             GameObject Pitching = Instantiate(HittingBallPrefab) as GameObject;             // どこからボールを投げるかを指定する             Pitching.transform.position = HittingPivot.position;              // 打球がレフトに飛ぶか、ライトに飛ぶかを決める             float Wide01 = Random.Range(-1.0f, 1.0f);             // Debug.Log(Wide01);             // 打球の角度             float Angle = Random.Range(0.5f, 0.8f);             // 打球がファールになるか、フェアになるかを決める             // このケースでは絶対値を取得してx<zのため必ずフェアになる             float Wide02 = Random.Range(Mathf.Abs(Wide01) + 1.0f, Mathf.Abs(Wide01) + 1.5f);             // Debug.Log(Wide02);              // Rigidbodyに力を加えて投球             // AddForceは最後にForceMode.Force(何も書かなければこちらがデフォルト)かForceMode.Impulseを選べる             Pitching.GetComponent<Rigidbody>().AddForce(Wide01, Angle, Wide02, ForceMode.Impulse);         }      }       void SingleHit()     {          if (Thirdrunner == 1)         {             Get1Score();             AddTeamScoreText();             ThirdRunnerDestroy();         }         if (SecondRunner == 1)         {             GenerateThirdRunner();             SecondRunnerDestroy();         }         if (FirstRunner == 1)         {             GenerateSecondRunner();             FirstRunnerDestroy();         }          GenerateFirstRunner();      }      // フォアボールでの押し出し、単打でのタイムリー、本塁打での打者自身の得点に用いる。     void Get1Score()     {          if (InningJudge == 0)         {             ScoreTopTeam++;         }         else         {             ScoreBottomTeam++;         }      }      // 走者一掃のときに用いる。三塁打ならば単独、本塁打ならGet1Scoreとの併用。     void ClearTheBases(){          if (InningJudge == 0)         {             ScoreTopTeam = ScoreTopTeam + FirstRunner + SecondRunner + Thirdrunner;         }         else         {             ScoreBottomTeam = ScoreBottomTeam + FirstRunner + SecondRunner + Thirdrunner;         }          FirstRunnerDestroy();         SecondRunnerDestroy();         ThirdRunnerDestroy();      }      void AddTeamScoreText()     {          if (InningJudge == 0)         {
            ScoreTopTeamTextBig.SetActive(true);
            ScoreBottomTeamTextBig.SetActive(true);
            this.ScoreTopTeamText.GetComponent<Text>().text = this.ScoreTopTeam.ToString();             this.ScoreTopTeamTextBig.GetComponent<Text>().color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 0f / 255.0f, 200.0f / 255.0f);
            this.ScoreTopTeamTextBig.GetComponent<Text>().text = this.ScoreTopTeam.ToString();              StatusScoreTopTeam++;         }         else         {             ScoreTopTeamTextBig.SetActive(true);             ScoreBottomTeamTextBig.SetActive(true);             this.ScoreBottomTeamText.GetComponent<Text>().text = this.ScoreBottomTeam.ToString();             this.ScoreBottomTeamTextBig.GetComponent<Text>().color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 0f / 255.0f, 200.0f / 255.0f);             this.ScoreBottomTeamTextBig.GetComponent<Text>().text = this.ScoreBottomTeam.ToString();              StatusScoreBottomTeam++;              CheckWalkOff();         }      }      void CheckStatusAdd(){          if (StatusScoreTopTeam == 1 || StatusScoreBottomTeam == 1)         {
            this.ScoreTopTeamTextBig.GetComponent<Text>().color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
            ScoreTopTeamTextBig.SetActive(false);
            this.ScoreBottomTeamTextBig.GetComponent<Text>().color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
            ScoreBottomTeamTextBig.SetActive(false);             StatusScoreTopTeam = 0;             StatusScoreBottomTeam = 0;         }     }      void GenerateFirstRunner(){         FirstRunner = 1;          // ランナーを生成         GameObject MakeFirstRunner = Instantiate(FirstRunnerPrefab) as GameObject;             MakeFirstRunner.transform.position = FirstRunnerPivot.position;     }      void GenerateSecondRunner()     {         SecondRunner = 1;          // ランナーを生成         GameObject MakeSecondRunner = Instantiate(SecondRunnerPrefab) as GameObject;         MakeSecondRunner.transform.position = SecondRunnerPivot.position;     }      void GenerateThirdRunner()     {         Thirdrunner = 1;          // ランナーを生成         GameObject MakeThirdRunner = Instantiate(ThirdRunnerPrefab) as GameObject;         MakeThirdRunner.transform.position = ThirdRunnerPivot.position;     }      void FirstRunnerDestroy(){         FirstRunner = 0;         GameObject[] FirstRunnerDestroyOccur = GameObject.FindGameObjectsWithTag("FirstRunnerPrefab");         foreach (GameObject obj in FirstRunnerDestroyOccur)         {             Destroy(obj);         }     }      void SecondRunnerDestroy()     {         SecondRunner = 0;         GameObject[] SecondRunnerDestroyOccur = GameObject.FindGameObjectsWithTag("SecondRunnerPrefab");         foreach (GameObject obj in SecondRunnerDestroyOccur)         {             Destroy(obj);         }     }      void ThirdRunnerDestroy()     {         Thirdrunner = 0;         GameObject[] ThirdRunnerDestroyOccur = GameObject.FindGameObjectsWithTag("ThirdRunnerPrefab");         foreach (GameObject obj in ThirdRunnerDestroyOccur)         {             Destroy(obj);         }     }       void OutCheck()     {          if (this.OutCount < 2)         {             OutCount++;             this.OutCountText.GetComponent<Text>().text = this.OutCount.ToString();         }         else         {             this.OutCount = 0;              FirstRunnerDestroy();             SecondRunnerDestroy();             ThirdRunnerDestroy();              this.OutCountText.GetComponent<Text>().text = this.OutCount.ToString();             Inningcheck();         }      }      void Inningcheck(){          Debug.Log(InningJudge);          // 表の攻撃が終了したとき         if (InningJudge == 0)         {
            // 9回の表終了時で後攻チームがリードしている場合
            if (InningCount == 9 && ScoreTopTeam < ScoreBottomTeam)
            {                 GameSet();             }             else             {
                InningJudge++;
                this.TopBottom.GetComponent<Text>().text = "裏";             }          }         // 裏の攻撃が終了したとき         else         {              if (InningCount > 8 && ScoreTopTeam > ScoreBottomTeam)             {                 GameSet();             }             else if (InningCount == 15)             {                 Draw();             }             else             {
                InningJudge = 0;
                InningCount++;
                this.Inning.GetComponent<Text>().text = this.InningCount.ToString();
                this.TopBottom.GetComponent<Text>().text = "表";             }          }      }      void CheckWalkOff(){          if (InningCount > 8)         {             this.Ampire.GetComponent<Text>().text = "サヨナラ!";             Debug.Log("試合が終了しました。");              ScoreTopTeamTextBig.SetActive(true);             ScoreBottomTeamTextBig.SetActive(true);             this.ScoreTopTeamText.GetComponent<Text>().text = this.ScoreTopTeam.ToString();             this.ScoreBottomTeamTextBig.GetComponent<Text>().color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 0f / 255.0f, 200.0f / 255.0f);             this.ScoreTopTeamTextBig.GetComponent<Text>().text = this.ScoreTopTeam.ToString();         }      }      void GameSet()     {         if (InningJudge == 0)         {             ScoreTopTeamTextBig.SetActive(true);             ScoreBottomTeamTextBig.SetActive(true);             this.ScoreTopTeamText.GetComponent<Text>().text = this.ScoreTopTeam.ToString();             this.ScoreBottomTeamTextBig.GetComponent<Text>().color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 0f / 255.0f, 200.0f / 255.0f);             this.ScoreTopTeamTextBig.GetComponent<Text>().text = this.ScoreTopTeam.ToString();         }         else         {             ScoreTopTeamTextBig.SetActive(true);             ScoreBottomTeamTextBig.SetActive(true);             this.ScoreBottomTeamText.GetComponent<Text>().text = this.ScoreBottomTeam.ToString();             this.ScoreTopTeamTextBig.GetComponent<Text>().color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 0f / 255.0f, 200.0f / 255.0f);             this.ScoreBottomTeamTextBig.GetComponent<Text>().text = this.ScoreBottomTeam.ToString();         }          this.Ampire.GetComponent<Text>().text = "試合終了!";         Debug.Log("試合が終了しました。");     }      void Draw()     {         ScoreTopTeamTextBig.SetActive(true);         ScoreBottomTeamTextBig.SetActive(true);         this.ScoreTopTeamTextBig.GetComponent<Text>().color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 0f / 255.0f, 200.0f / 255.0f);         this.ScoreBottomTeamTextBig.GetComponent<Text>().color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 0f / 255.0f, 200.0f / 255.0f);         this.Ampire.GetComponent<Text>().text = "延長15回引き分け!";     }      public GameObject BallPrefab;     public Transform Pitcher;     public float BaseSpeed = 1.0f;      // アクション用に打撃用の発射口を準備する(あとでActionBatting関数に含めてみる)     public GameObject HittingBallPrefab;     public Transform HittingPivot;      public GameObject FirstRunnerPrefab;     public GameObject SecondRunnerPrefab;     public GameObject ThirdRunnerPrefab;      public Transform FirstRunnerPivot;     public Transform SecondRunnerPivot;     public Transform ThirdRunnerPivot;      // 画面左下の審判     GameObject Ampire;     // 画面右下のストライク、ボール、アウトの各カウント     GameObject StrikeCountText;     GameObject BallCountText;     GameObject OutCountText;     // イニングの進行     GameObject Inning;     GameObject TopBottom;      GameObject ScoreTopTeamText;     GameObject ScoreBottomTeamText;      GameObject ScoreTopTeamTextBig;     GameObject ScoreBottomTeamTextBig;      int StrikeCount;     int BallCount;     int OutCount;      int InningCount = 1;     int InningJudge;      int FirstRunner;     int SecondRunner;     int Thirdrunner;      int ScoreTopTeam;     int ScoreBottomTeam;      // 得点後の掲示板を元に戻すための数値     int StatusScoreTopTeam;     int StatusScoreBottomTeam;      // Use this for initialization     void Start()     {         this.Ampire = GameObject.Find("Ampire");         this.StrikeCountText = GameObject.Find("StrikeCountText");         this.BallCountText = GameObject.Find("BallCountText");         this.OutCountText = GameObject.Find("OutCountText");         this.Inning = GameObject.Find("Inning");         this.TopBottom = GameObject.Find("TopBottom");         this.ScoreTopTeamText = GameObject.Find("ScoreTopTeamText");         this.ScoreBottomTeamText = GameObject.Find("ScoreBottomTeamText");         this.ScoreTopTeamTextBig = GameObject.Find("ScoreTopTeamTextBig");         this.ScoreBottomTeamTextBig = GameObject.Find("ScoreBottomTeamTextBig");          // 大きな得点表示は非表示         ScoreTopTeamTextBig.SetActive(false);         ScoreBottomTeamTextBig.SetActive(false);     }      // Update is called once per frame     void Update()     {          if (Input.GetKeyDown(KeyCode.B))         {             // 目に見える投球モーション部分              float InOut = Random.Range(-0.04f, 0.04f);             float HighLow = Random.Range(-0.08f, 0.07f);             float BallSpeed = Random.Range(-1.68f, -2.09f);              // プレハブの野球ボールを複製             GameObject PitchingStart = Instantiate(BallPrefab) as GameObject;             // どこからボールを投げるかを指定する             PitchingStart.transform.position = Pitcher.position;              PitchingStart.GetComponent<Rigidbody>().AddForce(InOut * BaseSpeed, HighLow * BaseSpeed, BallSpeed * BaseSpeed, ForceMode.Impulse);              // 前の打球を削除             GameObject[] FormerBallDestroy = GameObject.FindGameObjectsWithTag("HittingBallPrefab");             foreach (GameObject obj in FormerBallDestroy)             {                 Destroy(obj);             }

            // 大きな得点表示は色を白にして非表示
            CheckStatusAdd();              // シミュレーション部分              this.Ampire.GetComponent<Text>().text = "";             // Invoke("PitchingBatting", 0.5f);         }      } }  