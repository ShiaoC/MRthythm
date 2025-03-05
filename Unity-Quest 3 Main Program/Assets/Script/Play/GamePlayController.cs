using extOSC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance;
    public OSCReceiver Receiver;
    public Animator playRoad;
    public AudioClip[] audioClips;
    AudioSource audioSource;
    const string _OSCAddress = "/Mediapipe/";
    string s;
    /*------------------------------
     * 取得Mediapipe的角度
     */
    public float[] MediapipeDegrees = new float[8];

    private void Awake()
    {
        instance = this;
        
    }

    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("OSC");
        if (objectsWithTag.Length > 0)
        {
            Receiver = objectsWithTag[0].GetComponent<OSCReceiver>();
        }


        Receiver.Bind(_OSCAddress + "0", ReceiveData0);
        Receiver.Bind(_OSCAddress + "1", ReceiveData1);
        Receiver.Bind(_OSCAddress + "2", ReceiveData2);
        Receiver.Bind(_OSCAddress + "3", ReceiveData3);
        Receiver.Bind(_OSCAddress + "4", ReceiveData4);
        Receiver.Bind(_OSCAddress + "5", ReceiveData5);
        Receiver.Bind(_OSCAddress + "6", ReceiveData6);
        Receiver.Bind(_OSCAddress + "7", ReceiveData7);
        for (int i =0; i < 8; i++)
        {
            MediapipeDegrees[i] = 90;
        }
    }

    public void DoPoseCheck(float[] danceData)
    {
        int successCount = 0;
        for(int i = 0; i<8; i++)
        {
            successCount += CheckSuccess(MediapipeDegrees[i], danceData[i]); 
        }


        if(successCount >= 6)
        {
            //Great
            GameDataManager.instance.AddCombo(12 * successCount);
            playRoad.SetTrigger("Great");
            audioSource.clip = audioClips[0];
            audioSource.Play();

        }
        else if(successCount >= 4)
        {
            //Nice
            GameDataManager.instance.AddCombo(11 * successCount);
            playRoad.SetTrigger("Nice");
            audioSource.clip = audioClips[1];
            audioSource.Play();
        }
        else
        {
            //Fail
            GameDataManager.instance.ResetCombo();
            playRoad.SetTrigger("Fail");
            audioSource.clip = audioClips[2];
            audioSource.Play();
        }
        //完成後更新UI
    }


    /*---------------------
     * 確認兩點角度是否成功
     */
    int CheckSuccess(float a, float b)
    {
        
        if(Mathf.Min( Mathf.Abs(a - b), Mathf.Abs(b - a)) <= 25)
        {
            return 1;
        }
        return 0;
    }

    void ReceiveData0(OSCMessage message)
    {
        if (message.ToFloat(out var value))
        {
            MediapipeDegrees[0] = value;
        }
    }
    void ReceiveData1(OSCMessage message)
    {
        if (message.ToFloat(out var value))
        {
            MediapipeDegrees[1] = value;
        }
    }
    void ReceiveData2(OSCMessage message)
    {
        if (message.ToFloat(out var value))
        {
            MediapipeDegrees[2] = value;
        }
    }
    void ReceiveData3(OSCMessage message)
    {
        if (message.ToFloat(out var value))
        {
            MediapipeDegrees[3] = value;
        }
    }
    void ReceiveData4(OSCMessage message)
    {
        if (message.ToFloat(out var value))
        {
            MediapipeDegrees[4] = value;
        }
    }
    void ReceiveData5(OSCMessage message)
    {
        if (message.ToFloat(out var value))
        {
            MediapipeDegrees[5] = value;
        }
    }
    void ReceiveData6(OSCMessage message)
    {
        if (message.ToFloat(out var value))
        {
            MediapipeDegrees[6] = value;
        }
    }
    void ReceiveData7(OSCMessage message)
    {
        if (message.ToFloat(out var value))
        {
            MediapipeDegrees[7] = value;
        }
    }
}
