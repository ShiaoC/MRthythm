using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �o�ӵ{�����O�����m
// �ӬO�ΦV�q�M������׶i�����۹��m
// �ҥH�Ϊ��OlocalPosition

[System.Serializable]
public class VectorJoint
{
    GameObject  point = null;       // �ڭ̭n���IK�I
    float       length = 0;         // ��IK�I�۹����`�I����m
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
