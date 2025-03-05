using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorTrack : MonoBehaviour
{

    [SerializeField] GameObject[] mediapipePoint;
    [SerializeField] GameObject[] joint;
    [SerializeField] GameDataManager GameDataManager;
    

    //因為不用管時間對於動作的影響，一直刷新就行
    //所以就直接一般的update就行
    void Update()
    {
        /*Debug.Log(GameDataManager.isPlaying);
        if(GameDataManager.isPlaying)
            Dance();*/
    }


    //分別設置每一個物件的位置
    //以?????為中心點進行運算
    void Dance()
    {
        // 0 = HipCenter 不用設定
        Vector3 hipCenterPosition = GetCenterPosition(mediapipePoint[1].transform.position, mediapipePoint[2].transform.position);
        // 1 = RHip
        joint[1].transform.localPosition = CountPosition(hipCenterPosition, mediapipePoint[1].transform.position, SetBodyPartLength.GetLength(3));
        // 2 = LHip
        joint[2].transform.localPosition = CountPosition(hipCenterPosition, mediapipePoint[2].transform.position, SetBodyPartLength.GetLength(3));
        // 3 = RKnee
        joint[3].transform.localPosition = CountPosition(mediapipePoint[1].transform.position, mediapipePoint[3].transform.position, SetBodyPartLength.GetLength(4));
        // 4 = LKnee
        joint[4].transform.localPosition = CountPosition(mediapipePoint[2].transform.position, mediapipePoint[4].transform.position, SetBodyPartLength.GetLength(4));
        // 5 = RAnkle
        joint[5].transform.localPosition = CountPosition(mediapipePoint[3].transform.position, mediapipePoint[5].transform.position, SetBodyPartLength.GetLength(5));
        // 6 = LAnkle
        joint[6].transform.localPosition = CountPosition(mediapipePoint[4].transform.position, mediapipePoint[6].transform.position, SetBodyPartLength.GetLength(5));

        // 7 = ShouderCenter
        Vector3 shouderCenterPosition = GetCenterPosition(mediapipePoint[8].transform.position, mediapipePoint[9].transform.position);
        joint[7].transform.localPosition = CountPosition(hipCenterPosition, shouderCenterPosition, SetBodyPartLength.GetLength(6) + SetBodyPartLength.GetLength(7));
        // 8 = RShouder
        joint[8].transform.localPosition = CountPosition(shouderCenterPosition, mediapipePoint[8].transform.position, SetBodyPartLength.GetLength(0));
        // 9 = LShouder
        joint[9].transform.localPosition = CountPosition(shouderCenterPosition, mediapipePoint[9].transform.position, SetBodyPartLength.GetLength(0));
        // 10 = RElbow
        joint[10].transform.localPosition = CountPosition(mediapipePoint[8].transform.position, mediapipePoint[10].transform.position, SetBodyPartLength.GetLength(1));
        // 11 = LElbow
        joint[11].transform.localPosition = CountPosition(mediapipePoint[9].transform.position, mediapipePoint[11].transform.position, SetBodyPartLength.GetLength(1));
        // 12 = RWrist
        joint[12].transform.localPosition = CountPosition(mediapipePoint[10].transform.position, mediapipePoint[12].transform.position, SetBodyPartLength.GetLength(2));
        // 13 = LWrist
        joint[13].transform.localPosition = CountPosition(mediapipePoint[11].transform.position, mediapipePoint[13].transform.position, SetBodyPartLength.GetLength(2));

    }

    Vector3 CountPosition(Vector3 from, Vector3 to, float length) {
        float len = Mathf.Sqrt(Mathf.Pow(to.x - from.x, 2) + Mathf.Pow(to.y - from.y, 2) + Mathf.Pow(to.z - from.z, 2));
        float times = 0;
        if (len != 0)
        {
            times = length / len;
        }
        return new Vector3(times * (to.x - from.x) , times * (to.y - from.y), times * (to.z - from.z)); 
    }

    Vector3 GetCenterPosition(Vector3 first, Vector3 second)
    {
        return new Vector3(first.x / 2 + second.x / 2, first.y / 2 + second.y / 2, first.z / 2 + second.z / 2);
    }
}
