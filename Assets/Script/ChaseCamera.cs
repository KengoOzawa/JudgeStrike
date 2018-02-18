using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseCamera : MonoBehaviour
{
    // ターゲットへの参照
    public Transform target;
    // 相対座標
    private Vector3 offset;     

    void Start()
    {
        //自分自身とtargetとの相対距離を求める
        offset = GetComponent<Transform>().position - target.position;
    }

    void Update()
    {
        // 自分自身の座標に、targetの座標に相対座標を足した値を設定する
        GetComponent<Transform>().position = target.position + offset;
    }
}