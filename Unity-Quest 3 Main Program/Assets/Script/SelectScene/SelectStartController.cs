using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectStartController : MonoBehaviour
{
    public static SelectStartController instance;
    public bool modelDance = false;

    //[SerializeField] SongSelect SongSelect;
    //[SerializeField] AudioController AudioController;


    void Awake()
    {
        instance = this;
        modelDance = false;
    }

    void Update()
    {
        
    }

    /*
     * 
     * 當資料已經讀取完畢後才會運行模型的舞蹈
     * 
     */

    public void SetModelDanceMode(bool mode)
    {
        modelDance = mode;

        if (mode)
        {
            AudioController.instance.PlayAudioFile();
            Debug.Log("SetModelDanceMode Start");
        }
    }
}
