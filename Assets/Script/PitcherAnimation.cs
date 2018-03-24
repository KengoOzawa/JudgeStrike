using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitcherAnimation : MonoBehaviour
{

    Animator AnimPitch;

    void SetPosition(){

        AnimPitch.SetBool("PitchStart", false);

    }

    // Use this for initialization
    void Start()
    {
        AnimPitch = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.B))
        {

            AnimPitch.SetBool("PitchStart", true);

            Invoke("SetPosition", 2.5f);

        }

    }
}
