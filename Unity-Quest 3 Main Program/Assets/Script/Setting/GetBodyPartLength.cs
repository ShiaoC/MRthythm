using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBodyPartLength : MonoBehaviour
{
    [SerializeField] float x;
    [SerializeField] float y;
    [SerializeField] float z;
    [SerializeField] int bodyPart;
    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.localPosition = new Vector3(x * SetBodyPartLength.GetLength(bodyPart) 
                                                            , y * SetBodyPartLength.GetLength(bodyPart)
                                                            , z * SetBodyPartLength.GetLength(bodyPart));
    }
}
