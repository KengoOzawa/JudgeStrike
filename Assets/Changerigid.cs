using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Changerigid : MonoBehaviour {

    private Rigidbody rigid;

    // ゲーム中使用する野球ボールの仕様に合わせる
    public void ChangeRigidbody(){
        
            Debug.Log("Rigidbodyを変更しました");

            rigid = GetComponent<Rigidbody>();

            rigid.mass = 0.144f;
            rigid.drag = 0.4f;

    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
