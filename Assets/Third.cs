using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// サードのうち、ボールの送球について扱うスクリプト
public class Third : MonoBehaviour
{
    
    // 子オブジェクトであるThrowPointを呼び出す
    GameObject CallThrowPoint;

    // 子オブジェクトにアタッチされたスクリプトを呼び出す
    void Throw(){
        CallThrowPoint.GetComponent<ThrowBall>().Throw();
    }

    // Use this for initialization
    void Start()
    {
        CallThrowPoint = GameObject.Find("ThrowPoint");
    }

    // Update is called once per frame
    public void Update()
    {
        
    }
}
