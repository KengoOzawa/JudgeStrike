using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatRotate : MonoBehaviour {

    public float SwingSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        gameObject.transform.Rotate(0, 0, SwingSpeed);

        if (Input.GetKeyDown(KeyCode.F))
        {
            SwingSpeed += 1000.0f;
        }
		
	}
}
