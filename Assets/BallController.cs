using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    public GameObject block;
    Vector3 offset;
    Vector3 target;
    float deg;

    IEnumerator ThrowBall()
    {
        float b = Mathf.Tan(deg * Mathf.Deg2Rad);
        float a = (target.y - b * target.x) / (target.x * target.x);

        for (float x = 0; x <= this.target.x; x += 0.3f)
        {
            float y = a * x * x + b * x;
            transform.position = new Vector3(x, y, 0) + offset;
            yield return null;
        }
    }

    public void SetTarget(Vector3 target, float deg)
    {
        this.offset = transform.position;
        this.target = target - this.offset;
        this.deg = deg;

        StartCoroutine("ThrowBall");
    }

    void Start()
    {
        
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("発射");
            // ブロックに向かって60度の角度で射出
            SetTarget(block.transform.position, 60);
        }

    }


}