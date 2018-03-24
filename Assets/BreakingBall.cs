using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingBall : MonoBehaviour
{

    public int ThrowedBreakingBall;

    GameObject JudgePoint;

    bool FalkBall;

    // Use this for initialization
    void Start()
    {
        JudgePoint = GameObject.Find("JudgePoint");
    }

    // AssignedNumberで「Pitching」から受けた数値をThrowedBrakingBallに渡す
    public void SetBreakingBall(int AssignedNumber){
        ThrowedBreakingBall = AssignedNumber;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        switch (ThrowedBreakingBall)
        {
            // ストレートを投げる場合
            case 0:

                break;

            // スライダーを投げる場合
            case 1:

                if ((this.gameObject.transform.position.z - this.JudgePoint.transform.position.z < 18.0) && (0 < this.gameObject.transform.position.z - this.JudgePoint.transform.position.z))
                {
                    GetComponent<Rigidbody>().AddForce(0.0015f, 0f, 0f, ForceMode.Impulse);

                }

                break;

            // シュートを投げる場合
            case 2:

                if ((this.gameObject.transform.position.z - this.JudgePoint.transform.position.z < 18.0) && (0 < this.gameObject.transform.position.z - this.JudgePoint.transform.position.z))
                {
                    GetComponent<Rigidbody>().AddForce(-0.0015f, 0f, 0f, ForceMode.Impulse);

                }

                break;
            
            // フォークを投げる場合 
            case 3:

                if (this.gameObject.transform.position.z - this.JudgePoint.transform.position.z < 3.0)
                {

                    if (FalkBall == false)
                    {
                        FalkBall = true;
                        GetComponent<Rigidbody>().AddForce( 0f, -0.05f, 0f, ForceMode.Impulse);
                    }

                }

                break;

            // チェンジアップを投げる場合 
            case 4:

                if ((this.gameObject.transform.position.z - this.JudgePoint.transform.position.z < 7.0) && (0 < this.gameObject.transform.position.z - this.JudgePoint.transform.position.z))
                {
                    GetComponent<Rigidbody>().AddForce(0f, 0f, 0.0f, ForceMode.Impulse);
                }

                break;

        }

    }
}