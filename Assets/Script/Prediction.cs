using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prediction : MonoBehaviour
{
    //ダミーの球
    [SerializeField]
    private GameObject dummySphere;

    //初速
    [SerializeField]
    private Vector3 v0;

    //地面の座標
    [SerializeField]
    private float gp;

    private Rigidbody rigid;
    private bool isShot = false;
    // private float timeCnt = 0.0f;
    private float stopTime = 0.0f;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        if (!rigid)
        {
            rigid = gameObject.AddComponent<Rigidbody>();
        }
        rigid.isKinematic = true;
        dummySphere.transform.position = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var t1 = (v0.y + Mathf.Sqrt(Mathf.Pow(-v0.y, 2.0f) + (-2 * -Physics.gravity.y * (-transform.position.y + gp)))) / (-Physics.gravity.y);
            var t2 = (v0.y - Mathf.Sqrt(Mathf.Pow(-v0.y, 2.0f) + (-2 * -Physics.gravity.y * (-transform.position.y + gp)))) / (-Physics.gravity.y);
            Debug.Log("t1:" + t1.ToString("f3") + "     t2:" + t2.ToString("f3"));

            //秒数がNaNの時か秒数がマイナスの時は処理しない
            if ((float.IsNaN(t1) && float.IsNaN(t2)) || (t1 < 0) && (t2 < 0))
                return;

            //地面に落ちるまでの時間
            stopTime = (t1 > 0) ? t1 : t2;

            //地面に落ちる位置
            // (加筆部分) 地面から0.825f地点より発射した部分を引く
            var pos = new Vector3(v0.x * stopTime, gp - 0.825f, v0.z * stopTime);
            pos.x += transform.position.x;
            pos.z += transform.position.z;

            //ダミーの球を落ちる位置に表示する
            dummySphere.transform.position = pos;

            rigid.isKinematic = false;
            rigid.AddForce(v0, ForceMode.VelocityChange);
            isShot = true;
            // timeCnt = 0.0f;
        }

        if (!isShot)
            return;

        /*
        timeCnt += Time.deltaTime;
        if (timeCnt >= stopTime)
        {
            isShot = false;
            Debug.Break();
        } */
    }
}