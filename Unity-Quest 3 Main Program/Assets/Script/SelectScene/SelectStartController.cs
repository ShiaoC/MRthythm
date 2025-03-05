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
     * ���Ƥw�gŪ��������~�|�B��ҫ����R��
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
