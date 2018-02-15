using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLimitObj : MonoBehaviour
{

    public float life_time = 1.5f;
    float time;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > life_time)
        {
            Destroy(gameObject);
        }
    }
}