using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    Vector3 ForceBall(Vector3 start, Vector3 force, float mass, Vector3 gravity, float gravityScale, float time){

        var speedX = force.x / mass * Time.fixedDeltaTime;
        var speedY = force.y / mass * Time.fixedDeltaTime;
        var speedZ = force.z / mass * Time.fixedDeltaTime;

        var halfGravityX = gravity.x * 0.5f * gravityScale;
        var halfGravityY = gravity.y * 0.5f * gravityScale;
        var halfGravityZ = gravity.z * 0.5f * gravityScale;

        var positionX = speedX * time + halfGravityX * Mathf.Pow(time, 2);
        var positionY = speedY * time + halfGravityY * Mathf.Pow(time, 2);
        var positionZ = speedZ * time + halfGravityZ * Mathf.Pow(time, 2);

        return start + new Vector3(positionX, positionY, positionZ);

    }

	// Use this for initialization
	void Start () {

        var force = new Vector3(300f, 300f, 0f);
        var time = 3;    // 3 seconds after.

        ForceBall(transform.position, force, 0.05f, Physics.gravity, 1, time);
	}
	
	// Update is called once per frame
	void Update () {


		
	}
}
