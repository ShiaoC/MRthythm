using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetModelPartLength : MonoBehaviour
{
    public static SetModelPartLength instance;
    float[] modelPartLength = new float[8] { 0.143346f, 0.244215f, 0.226693f, 0.087f, 0.440982f, 0.503121f, 0.3257f, 0.12f};

    private void Awake()
    {
        instance = this;
    }

    public float GetLength(int n)
    {
        return modelPartLength[n];
    }
    /*
    void SetModelLength()
    {
        Vector3 neckPosition = FindTwoPointCenter(GameObject.Find("mixamorig:LeftArm").transform.position, GameObject.Find("mixamorig:RightArm").transform.position);
        Vector3 hipPosition = FindTwoPointCenter(GameObject.Find("mixamorig:LeftUpLeg").transform.position, GameObject.Find("mixamorig:RightUpLeg").transform.position);
        modelPartLength[0] = (Vector2Length(neckPosition,
                                            GameObject.Find("mixamorig:LeftArm").transform.position)); // neck 2 shouder
        modelPartLength[1] = (Vector2Length(GameObject.Find("mixamorig:LeftArm").transform.position,
                                            GameObject.Find("mixamorig:LeftForeArm").transform.position)); // shouder 2 elbow
        modelPartLength[2] = (Vector2Length(GameObject.Find("mixamorig:LeftForeArm").transform.position,
                                            GameObject.Find("mixamorig:LeftHand").transform.position)); // elbow 2 wrist
        modelPartLength[3] = (Vector2Length(GameObject.Find("mixamorig:LeftUpLeg").transform.position,
                                            hipPosition)); // root 2 hip
        modelPartLength[4] = (Vector2Length(GameObject.Find("mixamorig:LeftUpLeg").transform.position,
                                            GameObject.Find("mixamorig:LeftLeg").transform.position)); // hip 2 knee
        modelPartLength[5] = (Vector2Length(GameObject.Find("mixamorig:LeftLeg").transform.position,
                                            GameObject.Find("mixamorig:LeftFoot").transform.position)); // knee 2 ankle
        modelPartLength[6] = (Vector2Length(GameObject.Find("mixamorig:Spine").transform.position,
                                            neckPosition)); // troso 2 neck
        modelPartLength[7] = (Vector2Length(GameObject.Find("mixamorig:Spine").transform.position,
                                            hipPosition)); // troso 2 hip

        for (int i = 0; i < 8; i++)
        {
            Debug.Log("modelPartLength "+ i + ": " + modelPartLength[i]);
        }
    }
    float Vector2Length(Vector3 from, Vector3 to)
    {
        float x = from.x - to.x;
        float y = from.y - to.y;
        float z = from.z - to.z;
        return (float)(Mathf.Pow((x * x) + (y * y) + (z * z), 0.5f));
    }

    Vector3 FindTwoPointCenter(Vector3 first, Vector3 second)
    {
        return new Vector3(first.x + (second.x - first.x) / 2,
                            first.y + (second.y - first.y) / 2,
                            first.z + (second.z - first.z) / 2);
    }*/
}
