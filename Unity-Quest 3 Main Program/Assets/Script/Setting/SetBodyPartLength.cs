using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SetBodyPartLength : MonoBehaviour
{
    static float[] bodyPartLength = new float[8];
    //static float[] modelPartLength = new float[8];



    // Start is called before the first frame update
    void Awake()
    {
        bodyPartLength[0] = 0.14101f;
        bodyPartLength[1] = 0.25892f;
        bodyPartLength[2] = 0.26345f;
        bodyPartLength[3] = 0.07633f;
        bodyPartLength[4] = 0.41265f;
        bodyPartLength[5] = 0.41410f;
        bodyPartLength[6] = 0.28342f;
        bodyPartLength[7] = 0.15830f;
    }
    public static float GetLength(int num)
    {
        return bodyPartLength[num];
    }

    /*public static float GetModelLength(int num)
    {
        return modelPartLength[num];
    }*/

    public void addPartLength(int part)
    {
        bodyPartLength[part] += 0.02f;
        Debug.Log("肢體 : " + part + "    長度 : " + bodyPartLength[part]);
    }
    public void subPartLength(int part)
    {
        bodyPartLength[part] -= 0.02f;
        Debug.Log("肢體 : " + part + "    長度 : " + bodyPartLength[part]);
    }

    public void SaveDataToPlayerPrefs(int n)
    {

    }
}
