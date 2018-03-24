using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldingCenter : MonoBehaviour {

    // 自身の子オブジェクトを取得する
    GameObject CallThrowPoint;

    public Transform m_target = null;
    public float m_speed = 5;
    public float m_attenuation = 0.5f;

    //ゴロの打球になると追跡を開始する
    bool ChaseGroundBallStart;
    //ゴロの打球になると追跡を開始する
    bool ChaseFlyBallStart;

    private Vector3 m_velocity;

    Transform target;
    Transform BallTarget;

    public void ChaseGroundBallStartTrigger(Transform AssignedTransform)
    {
        target = AssignedTransform;

        ChaseGroundBallStart = true;
    }

    public void ChaseGroundBall(Transform m_target)
    {

        m_velocity += (m_target.position - transform.position) * m_speed;
        m_velocity *= m_attenuation;
        transform.position += m_velocity *= Time.deltaTime;

        float DisBallPoint = Vector3.Distance(m_target.position, transform.position);
        // Debug.Log("Distance : " + dis);

        if (DisBallPoint < 1.0)
        {
            // ボールに追いついたため動作をやめる
            ChaseGroundBallStart = false;
            Debug.Log("打球に追いつきました");

            // 打球を削除
            GameObject[] FormerBallDestroy = GameObject.FindGameObjectsWithTag("HittingBallPrefab");
            foreach (GameObject obj in FormerBallDestroy)
            {
                Destroy(obj);
            }

            // ボールを送球する
            CallThrowPoint.GetComponent<ThrowBall>().Throw();
        }


    }

    public void ChaseFlyBallStartTrigger(Transform AssignedTransform, Transform AssignedBallposition)
    {
        target = AssignedTransform;
        BallTarget = AssignedBallposition;

        ChaseFlyBallStart = true;
    }

    public void ChaseFlyBall(Transform m_target, Transform Ballposition)
    {

        m_velocity += (m_target.position - transform.position) * m_speed;
        m_velocity *= m_attenuation;
        transform.position += m_velocity *= Time.deltaTime;

        // 落下点までの距離
        float disfallpoint = Vector3.Distance(m_target.position, transform.position);
        // Debug.Log("Distance : " + disfallpoint);

        // 打球までの距離
        float DisBallPoint = Vector3.Distance(Ballposition.position, transform.position);

        // ボールとの座標判定も行う
        if (disfallpoint < 0.5)
        {
            // ボールに追いついたため動作をやめる
            ChaseGroundBallStart = false;
            // Debug.Log("打球に追いつきました");

            // ボールは送球しない
        }

        if (DisBallPoint < 1.0)
        {
            // 打球を削除
            GameObject[] FormerBallDestroy = GameObject.FindGameObjectsWithTag("HittingBallPrefab");
            foreach (GameObject obj in FormerBallDestroy)
            {
                Destroy(obj);
            }

            // ボールは送球しない
        }

    }

    // 投球のたびに定位置に戻る
    public void ReturnHomePosition()
    {
        transform.position = new Vector3(0f, 1.0f, 80.0f);
    }

    // Use this for initialization
    void Start()
    {
        CallThrowPoint = GameObject.Find("SampleThrowPointCenter");
    }

    void Update()
    {

        if (ChaseGroundBallStart == true)
        {
            ChaseGroundBall(target);
        }

        if (ChaseFlyBallStart == true)
        {
            ChaseFlyBall(target,BallTarget);
        }

    }
}
