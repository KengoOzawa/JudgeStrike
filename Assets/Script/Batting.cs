using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batting : MonoBehaviour {

    // 生成したいバットのPrefab
    public GameObject BatPrefab;
    // クリックした位置座標
    private Vector3 ClickPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            
            // Vector3でマウスがクリックした位置座標を取得する
            ClickPosition = Input.mousePosition;
            // Z軸修正
            ClickPosition.z = 2.301328440403075103838f;
            // オブジェクト生成 : オブジェクト(GameObject), 位置(Vector3), 角度(Quaternion)
            // ScreenToWorldPoint(位置(Vector3))：スクリーン座標をワールド座標に変換する
            Instantiate(BatPrefab, Camera.main.ScreenToWorldPoint(ClickPosition), BatPrefab.transform.rotation);
        }
		
	}
}
