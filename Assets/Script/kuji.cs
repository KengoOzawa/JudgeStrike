using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kuji : MonoBehaviour {

	// Use this for initialization
	void Start () {

        // yが絶対よりも大きくならないプログラム
        // フェアゾーンにボールを飛ばすために有効
        int x = Random.Range(0, 10);
        Debug.Log(x);
        int y = Random.Range(0, x);
        Debug.Log(y);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
