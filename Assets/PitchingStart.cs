using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchingStart : MonoBehaviour {

    GameObject CallBaseBall;

    // アニメーションイベントとして呼び出すためメソッド化する
    void Pitching(){
        CallBaseBall.GetComponent<BaseBall>().StartToPitch();
    }

	// Use this for initialization
	void Start () {
        this.CallBaseBall = GameObject.Find("BaseBall2");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
