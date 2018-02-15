using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Judge : MonoBehaviour {

    GameObject Ampire;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "StrikeZone")
        {
            this.Ampire.GetComponent<Text>().text = "ストライク!";
            Debug.Log("ストライク!");
        }
        else if (other.gameObject.tag == "BallZone")
        {
            this.Ampire.GetComponent<Text>().text = "ボール!";
            Debug.Log("ボール!");
        }
    }

	// Use this for initialization
	void Start () {
        this.Ampire = GameObject.Find("Ampire");
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}

