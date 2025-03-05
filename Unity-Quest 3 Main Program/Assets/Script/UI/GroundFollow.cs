using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFollow : MonoBehaviour
{
    public GameObject trans;
    void Update()
    {
        this.transform.position = new Vector3(trans.transform.position.x, this.transform.position.y, trans.transform.position.z);
    }
}
