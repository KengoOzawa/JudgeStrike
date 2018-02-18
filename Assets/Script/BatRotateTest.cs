using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatRotateTest : MonoBehaviour
{

    public float SwingSpeed = -10.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        gameObject.transform.Rotate(0, SwingSpeed, 0);

        if (Input.GetKeyDown(KeyCode.D))
        {
            SwingSpeed -= 10.0f;
        }

    }
}
