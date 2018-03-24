using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test5 : MonoBehaviour
{
    // 発射するボール
    public GameObject HittingBallPrefab;
    // ボールの発射位置
    public Transform HittingPivot;
    // 子オブジェクトの発見
    bool CallChangeigid;

    //ダミーの球
    [SerializeField]
    private GameObject dummySphere;

    //地面の座標
    [SerializeField]
    private float gp;


    private float stopTime = 0.0f;

    bool ChangeRigidbody;

    void Start()
    {
        dummySphere.transform.position = transform.position;
    }


    void Update()
    {

        // 打球の方向をランダムに決める
        float x = Random.Range(-10.0f, 10.0f);
        float y = Random.Range(10.0f, 25.0f);
        float z = Random.Range(10.0f, 50.0f);

        // 初速
        Vector3 v0 = new Vector3(x, y, z);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject Hitting = Instantiate(HittingBallPrefab) as GameObject;

            // 発射地点を決める
            Hitting.transform.position = HittingPivot.position;

            var t1 = (v0.y + Mathf.Sqrt(Mathf.Pow(-v0.y, 2.0f) + (-2 * -Physics.gravity.y * (-transform.position.y + gp)))) / (-Physics.gravity.y);
            var t2 = (v0.y - Mathf.Sqrt(Mathf.Pow(-v0.y, 2.0f) + (-2 * -Physics.gravity.y * (-transform.position.y + gp)))) / (-Physics.gravity.y);

            //秒数がNaNの時か秒数がマイナスの時は処理しない
            // NaN（Not a Number、非数、ナン）は、コンピュータにおいて、主に浮動小数点演算の結果として、不正なオペランドを与えられたために生じた結果を表す値またはシンボルである。
            if ((float.IsNaN(t1) && float.IsNaN(t2)) || (t1 < 0) && (t2 < 0))
                return;

            //地面に落ちるまでの時間
            /*
            条件演算（「～ ? ～ : ～」）
            この演算子は3つの値を持ち、それらを「?」記号と「:」記号で分離する。「式1?式2:式3」という順番に書き、式1の結果次第で、式2と式3のどちらを解釈するかを決める。
            */
            stopTime = (t1 > 0) ? t1 : t2;

            //地面に落ちる位置
            var pos = new Vector3(v0.x * stopTime, gp, v0.z * stopTime);
            pos.x += transform.position.x;
            pos.z += transform.position.z;

            //ダミーの球を落ちる位置に表示する
            dummySphere.transform.position = pos;

            Debug.Log(v0);

            if (CallChangeigid == true)
            {
                Hitting.GetComponent<Changerigid>().ChangeRigidbody();
            }

            Hitting.GetComponent<Rigidbody>().AddForce(v0, ForceMode.Impulse);

        }

        // ボールにアタッチしたスクリプトを呼び出してRigidbodyのMassとDragを変更
        if (Input.GetKeyDown(KeyCode.R))
        {
            CallChangeigid = true;
        }

    }
}
