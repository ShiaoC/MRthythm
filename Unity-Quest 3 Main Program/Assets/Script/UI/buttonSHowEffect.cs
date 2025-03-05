using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonSHowEffect : MonoBehaviour
{
    public GameObject specificObjectPrefab;
    public void Cclick()
    {
        Instantiate(specificObjectPrefab, this.transform.position + new Vector3( 0, 0.03f, 0), Quaternion.identity);
    }
            
    
}
