using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabola : MonoBehaviour
{
    
    public GameObject HittingBallPrefab;
    public Transform HittingPivot;

    // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            float Wide03;

            // 打球の角度
            float Angle02 = 0.5f;

            // 打球がセンター方向へどのくらい飛ぶか決める
            float Wide04 = 0.5f;


    void DefaultPitchingBatting(float a,float b, float c, float d, float e, float f){

        // VisualStudio側からの引数を使用しないことによる警告を防ぐ
        float unused = e;

        Debug.Log("引数代入テストを行います!");

        GameObject Pitching = Instantiate(HittingBallPrefab) as GameObject;

        Pitching.transform.position = HittingPivot.position;

        // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
        float Wide01 = Random.Range(a, b);
        Debug.LogFormat("横方向への力 = {0}", Wide01);
        // 打球の角度
        float Angle = Random.Range(c, d);
        Debug.LogFormat("上方向への力 = {0}", Angle);
        // 打球がファールになるか、フェアになるかを決める
        // このケースでは絶対値を取得してx<zのため必ずフェアになる

        // 本来はRandom.Range(e, f);となるところだが、本ケースではeが使用されない
        float Wide02 = Random.Range(Mathf.Abs(Wide01), f);
        Debug.LogFormat("センター方向への力 = {0}", Wide02);

        // Rigidbodyに力を加えて投球
        Pitching.GetComponent<Rigidbody>().AddForce(Wide01, Angle, Wide02, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.M))
        {
            DefaultPitchingBatting(-1.0f, 1.0f, 0f, 1.0f, 0f, 1.0f);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {

            // 前の打球を削除
            GameObject[] FormerBallDestroy = GameObject.FindGameObjectsWithTag("HittingBallPrefab");
            foreach (GameObject obj in FormerBallDestroy)
            {
                Destroy(obj);
            }

            Debug.Log("自動打球テストを行います!");

            GameObject Pitching = Instantiate(HittingBallPrefab) as GameObject;
           
            Pitching.transform.position = HittingPivot.position;

            // 打球がレフトに飛ぶか、ライトに飛ぶかを決める
            float Wide01 = Random.Range(-1.0f, 1.0f);
            Debug.LogFormat("横方向への力 = {0}", Wide01);
            // 打球の角度
            float Angle = Random.Range(0f, 1.0f);
            // 打球がファールになるか、フェアになるかを決める
            // このケースでは絶対値を取得してx<zのため必ずフェアになる
            float Wide02 = Random.Range(Mathf.Abs(Wide01), 1.0f);
            Debug.LogFormat("センター方向への力 = {0}", Wide02);

            // Rigidbodyに力を加えて投球
            Pitching.GetComponent<Rigidbody>().AddForce(Wide01, Angle, Wide02, ForceMode.Impulse);
        }


        if (Input.GetKeyDown(KeyCode.W))
        {

            Debug.Log("手動打球テストを行います!");

            GameObject Pitching = Instantiate(HittingBallPrefab) as GameObject;

            Pitching.transform.position = HittingPivot.position;

            // Rigidbodyに力を加えて投球
            Pitching.GetComponent<Rigidbody>().AddForce(Wide03, Angle02, Wide04, ForceMode.Impulse);

            Debug.LogFormat("横方向への力 = {0}", Wide03);
            Debug.LogFormat("上方向への力 = {0}", Angle02);
            Debug.LogFormat("センター方向への力 = {0}", Wide04);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Wide03 += 0.1f;
            Debug.LogFormat("横方向への力 : {0}", Wide03);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Angle02 += 0.1f;
            Debug.LogFormat("上方向への力 : {0}", Angle02);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Wide04 += 0.1f;
            Debug.LogFormat("センター方向への力 : {0}", Wide04);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Wide03 -= 0.1f;
            Debug.LogFormat("横方向への力 : {0}", Wide03);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Angle02 -= 0.1f;
            Debug.LogFormat("上方向への力 : {0}", Angle02);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Wide04 -= 0.1f;
            Debug.LogFormat("センター方向への力 : {0}", Wide04);
        }

    }
}