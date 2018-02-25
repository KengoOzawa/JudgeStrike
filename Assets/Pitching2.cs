using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pitching2 : MonoBehaviour {

    public float BallSpeed = 1.0f;

	// Use this for initialization
	void Start () {
        Vector3 force;
        force = this.gameObject.transform.forward * BallSpeed;
        // Rigidbodyに力を加えて発射
        GetComponent<Rigidbody>().AddForce(0f * BallSpeed, 0f * BallSpeed, -1.8105f * BallSpeed, ForceMode.Impulse);

	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
