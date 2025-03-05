using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    //private static bool created = false;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        /*
        // 這個條件確保只有第一個創建的GameObject會被保留
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else
        {
            // 如果已經有一個相同的GameObject存在，則銷毀這個重複創建的GameObject
            Destroy(this.gameObject);
        }*/
    }
}
