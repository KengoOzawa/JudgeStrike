﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour {

    private GameObject Camera01;
    private GameObject Camera02;
    private GameObject Camera03;
    private GameObject Camera04;
    private GameObject Camera05;
    private GameObject Camera06;
    private GameObject Camera07;

	// Use this for initialization
	void Start () {

        Camera01 = GameObject.Find("Camera01");
        Camera02 = GameObject.Find("Camera02");
        Camera03 = GameObject.Find("Camera03");
        Camera04 = GameObject.Find("Camera04");
        Camera05 = GameObject.Find("Camera05");
        Camera06 = GameObject.Find("Camera06");
        Camera07 = GameObject.Find("Camera07");

        Camera02.SetActive(false);
        Camera03.SetActive(false);
        Camera04.SetActive(false);
        Camera05.SetActive(false);
        Camera06.SetActive(false);
        Camera07.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("v"))
        {
            if (Camera01.activeSelf)
            {
                Camera01.SetActive(false);
                Camera02.SetActive(true);
                Camera03.SetActive(false);
                Camera04.SetActive(false);
                Camera05.SetActive(false);
                Camera06.SetActive(false);
                Camera07.SetActive(false);
            }else if(Camera02.activeSelf){
                Camera01.SetActive(false);
                Camera02.SetActive(false);
                Camera03.SetActive(true);
                Camera04.SetActive(false);
                Camera05.SetActive(false);
                Camera06.SetActive(false);
                Camera07.SetActive(false);
            }else if(Camera03.activeSelf){
                Camera01.SetActive(false);
                Camera02.SetActive(false);
                Camera03.SetActive(false);
                Camera04.SetActive(true);
                Camera05.SetActive(false);
                Camera06.SetActive(false);
                Camera07.SetActive(false);
            }else if (Camera04.activeSelf){
                Camera01.SetActive(false);
                Camera02.SetActive(false);
                Camera03.SetActive(false);
                Camera04.SetActive(false);
                Camera05.SetActive(true);
                Camera06.SetActive(false);
                Camera07.SetActive(false);
            }else if (Camera05.activeSelf){
                Camera01.SetActive(false);
                Camera02.SetActive(false);
                Camera03.SetActive(false);
                Camera04.SetActive(false);
                Camera05.SetActive(false);
                Camera06.SetActive(true);
                Camera07.SetActive(false);
            }else if (Camera06.activeSelf){
                Camera01.SetActive(false);
                Camera02.SetActive(false);
                Camera03.SetActive(false);
                Camera04.SetActive(false);
                Camera05.SetActive(false);
                Camera06.SetActive(false);
                Camera07.SetActive(true);
            }else{
                Camera01.SetActive(true);
                Camera02.SetActive(false);
                Camera03.SetActive(false);
                Camera04.SetActive(false);
                Camera05.SetActive(false);
                Camera06.SetActive(false);
                Camera07.SetActive(false);
            }
        }
		
	}
}

   