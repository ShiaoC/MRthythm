using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 這個程式不是絕對位置
// 而是用向量和肢體長度進行推算相對位置
// 所以用的是localPosition

[System.Serializable]
public class VectorJoint
{
    GameObject  point = null;       // 我們要控制的IK點
    float       length = 0;         // 此IK點相對於父節點的位置
    float[][]    frameData;

    public VectorJoint(GameObject pointGameObject, float[][] data, float bodyPartLength)
    {
        this.point      = pointGameObject;
        this.frameData  = data;
        this.length     = bodyPartLength;
    }
    public void Update(int frame)
    {

        float x = frameData[frame][0] * length;
        float y = frameData[frame][1] * length;
        float z = frameData[frame][2] * length;

        point.transform.localPosition = new Vector3(x, y, z);
    }

    public void SetDanceData(float[][] data)
    {
        this.frameData = data;
    }
}
