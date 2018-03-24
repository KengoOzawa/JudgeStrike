using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldingFirst : MonoBehaviour {

    // 自身の子オブジェクトを取得する
    GameObject CallThrowPoint;

    public Transform m_target = null;
    public float m_speed = 5;
    public float m_attenuation = 0.5f;

    // BaseBall側に呼び出され、Trueになると追跡を開始する
    bool ChaseStartAction;

    bool isCalled;

    private Vector3 m_velocity;

    Transform target;
    Transform BallTarget;

    public void ChaseStartTrigger(Transform AssignedTransform, Transform AssignedBallposition)
    {
        target = AssignedTransform;
        BallTarget = AssignedBallposition;

        ChaseStartAction = true;
    }

    public void Chase(Transform m_target, Transform Ballposition)
    {

        m_velocity += (m_target.position - transform.position) * m_speed;
        m_velocity *= m_attenuation;
        transform.position += m_velocity *= Time.deltaTime;

        // 落下点までの距離
        float disfallpoint = Vector3.Distance(m_target.position, transform.position);
        Debug.Log("DistanceFall : " + disfallpoint);

        // 打球までの距離
        float DisBallPoint = Vector3.Distance(Ballposition.position, transform.position);
        Debug.Log("DistanceBall : " + DisBallPoint);

        if (disfallpoint < 0.5)
        {
            // ボールに追いついたため動作をやめる
            ChaseStartAction = false;
            Debug.Log("打球に追いつきました");
        }

        if(DisBallPoint < 1.2){

            // 打球を削除
            GameObject[] FormerBallDestroy = GameObject.FindGameObjectsWithTag("HittingBallPrefab");
            foreach (GameObject obj in FormerBallDestroy)
            {
                Destroy(obj);
            }

            if (isCalled == false)
            {
                isCalled = true;
                // ボールを送球する
                CallThrowPoint.GetComponent<ThrowBall>().Throw();

            }
        }


    }

    // 投球のたびに定位置に戻る
    public void ReturnHomePosition()
    {
        transform.position = new Vector3(19.0f, 1.0f, 24.0f);
    }

    // Use this for initialization
    void Start()
    {
        CallThrowPoint = GameObject.Find("SampleThrowPointFirst");
    }

    void Update()
    {

        if (ChaseStartAction == true)
        {
            Chase(target, BallTarget);
        }

    }
}
