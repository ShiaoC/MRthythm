using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AccuracyCheck : MonoBehaviour
{
    Vector3[] pointPosition;
    // Start is called before the first frame update
    void Start()
    {
        //取得要判斷8點的座標
        //現在座標-中心座標
        

    }

    public void MainCheck()
    {
        int touchCount = 0;
        /*
        touchCount += Touched(pointPosition[0], pointPosition[1], , );
        touchCount += Touched(pointPosition[2], pointPosition[3], , );
        touchCount += Touched(pointPosition[4], pointPosition[5], , );
        touchCount += Touched(pointPosition[6], pointPosition[7], , );
        */
        int grade = Grade(touchCount);
        //送出成績資料到主程式
    }
    
    int Touched(Vector3 ch1, Vector3 ch2, Vector3 mp1, Vector3 mp2)
    {
        int normal = Distance(ch1, mp1) + Distance(ch2, mp2);
        int change = Distance(ch1, mp2) + Distance(ch2, mp1);
        return Math.Max(normal, change);
    }

    int Distance(Vector3 first, Vector3 second)
    {
        //只需要計算XY的距離
        float count = (float)Math.Pow( Math.Pow(first.x-second.x, 2)+Math.Pow(first.y, second.y) , 0.5);
        if (count <= 2) return 1;
        return 0;
    }

    int Grade(int touched)
    {
        int count = 10 * touched;
        
        if(touched >= 6)
        {
            //Great
            count = (int)(count * 1.2f);
        }
        else if (touched >= 4)
        {
            //Nice
            count = (int)(count * 1.1f);
        }
        else
        {
            //Fail
            return 0;
        }

        //combo數增加的函式

        //取得目前combo數加乘
        float combo=0;
        count = (int)(count*(combo/5*0.1+1));

        return count;
    }
}
