using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldingThird : MonoBehaviour {

    // 自身の子オブジェクトを取得する
    GameObject CallThrowPoint;

    public Transform m_target = null;
    public float m_speed = 5;
    public float m_attenuation = 0.5f;

    // BaseBall側に呼び出され、Trueになると追跡を開始する
    bool ChaseStartAction;

    private Vector3 m_velocity;

    Transform target;

    public void ChaseStartTrigger(Transform AssignedTransform)
    {
        target = AssignedTransform;

        ChaseStartAction = true;
    }

    public void Chase(Transform m_target)
    {
        
        m_velocity += (m_target.position - transform.position) * m_speed;
        m_velocity *= m_attenuation;
        transform.position += m_velocity *= Time.deltaTime;

        float dis = Vector3.Distance(m_target.position, transform.position);
        Debug.Log("Distance : " + dis);

        if (dis < 1.5)
        {
            // ボールに追いついたため動作をやめる
            ChaseStartAction = false;
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

    // 投球のたびに定位置に戻る
    public void ReturnHomePosition(){
        transform.position = new Vector3(-19.0f, 1.0f, 24.0f); 
    }

    // Use this for initialization
    void Start()
    {
        CallThrowPoint = GameObject.Find("SampleThrowPointThird");
    }

    void Update()
    {

        if (ChaseStartAction == true)
        {
            Chase(target);
        }

    }
}
