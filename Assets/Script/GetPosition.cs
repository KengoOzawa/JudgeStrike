using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPosition : MonoBehaviour {

    public float power;
    private Rigidbody rigid;

    Vector3 GetPos(float time, float power2, float mass)
    {
        float halfGravity = Physics.gravity.y * 0.5f;
        float x = time * -power2 / mass;
        float y = halfGravity * Mathf.Pow(time, 2);
        return new Vector3(x, y, 0);
    }

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
        rigid.isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            rigid.isKinematic = false;
            rigid.AddForce(Vector3.left * power, ForceMode.Impulse);

            Vector3 answer = GetPos(1.5f, 3.0f, 1.0f);
            Debug.Log(answer);
        }
		
	}
}
