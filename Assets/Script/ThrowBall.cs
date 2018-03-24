using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour {

    public GameObject ThrowBallPrefab;
    float BaseSpeed = 1.0f;

    GameObject FirstCatch;

    float BallSpeed = 0.0619195f;

    public void Throw()
    {

        // プレハブの野球ボールを複製
        GameObject PitchingStart = Instantiate(ThrowBallPrefab) as GameObject;

        Vector3 startPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        PitchingStart.transform.position = startPos;

        Vector3 forceVec = FirstCatch.transform.position - this.transform.position;

        // 投球の力を加える
        PitchingStart.GetComponent<Rigidbody>().AddForce(forceVec.x * BallSpeed , 0.2f, forceVec.z * BallSpeed, ForceMode.Impulse);

        Debug.Log(forceVec);

    }

	// Use this for initialization
	void Start () {
        FirstCatch = GameObject.Find("FirstCatch");
	}
	
	// Update is called once per frame
	void Update () {
    	
	}
}
