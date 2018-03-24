using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// サードのうち、ボールを追いかける流れについて扱うスクリプト
public class ChaseHittingBall : MonoBehaviour {

    // 自身にアタッチされたAnimatorを取得する
    Animator Animator;

    bool ChaseStartAction;

    // public Transform m_target = null;
    public float m_speed = 0.5f;
    public float m_attenuation = 0.5f;

    private Vector3 m_velocity;

    // 追いかける打球
    Transform target;

    // BaseBallにて打球判定になると呼び出される
    public void ChaseStartTrigger(Transform AssignedTransform)
    {
        Animator.SetBool("ChaseStart", true);
        // Update中のChaseを動かす
        ChaseStartAction = true;
        target = AssignedTransform;

    }

    public void Chase(Transform m_target){

        // ネットから引っ張ってきただけなので検証する
        m_velocity += (m_target.position - transform.position) * m_speed;
        m_velocity *= m_attenuation;
        transform.position += m_velocity *= Time.deltaTime;

        float PosX = m_target.position.x - transform.position.x;
        float PosZ = m_target.position.z - transform.position.z;

        Debug.Log("残りの距離X" + PosX);

        if (PosX < 1 && PosZ < 1)
        {
            // ボールに追いついたため動作をやめる
            ChaseStartAction = false;
            // ボールを捕球する動きのアニメーションを開始する
            Animator.SetBool("GroundBall", true);
        }

    }

	// Use this for initialization
	void Start () {
        Animator = this.gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
    public void Update () {

        if (ChaseStartAction == true)
        {
            Chase(target);
        }

	}
}
