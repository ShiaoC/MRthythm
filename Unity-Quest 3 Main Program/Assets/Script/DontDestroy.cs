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
        // �o�ӱ���T�O�u���Ĥ@�ӳЫت�GameObject�|�Q�O�d
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else
        {
            // �p�G�w�g���@�ӬۦP��GameObject�s�b�A�h�P���o�ӭ��ƳЫت�GameObject
            Destroy(this.gameObject);
        }*/
    }
}
