using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    public string dataID = "2305301306";

    [Header("Audio Stuff")]
    public AudioSource audioSource;
    public AudioClip audioClip;
    public string soundPath;

    private void Awake()
    {
        instance = this;
        //audioSource = gameObject.AddComponent<AudioSource>();
        soundPath = Application.streamingAssetsPath + "/_MusicFile/";
        //StartCoroutine(LoadAudio());
    }


    /* -----------------------------------------------------------------
     * Ū�Jaudio clip���
     * ���٨S������
     * -----------------------------------------------------------------
     */
    public IEnumerator LoadAudio(string ID)
    {
        yield return StartCoroutine(GetAudioFromFile(soundPath, "M_" + ID + ".ogg"));
        
        yield return true;
    }


    /* -----------------------------------------------------------------
     * ����Ū�J�����
     * -----------------------------------------------------------------
     */
    public void PlayAudioFile()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
        audioSource.loop = true;
    }



    /* -----------------------------------------------------------------
     * Ū�J�x�s��m������
     * �Q�I�s�ϥ�
     * -----------------------------------------------------------------
     */
    IEnumerator GetAudioFromFile(string path, string filename)
    {
        string audioToLoad = string.Format("{0}{1}", path, filename);
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(audioToLoad, AudioType.OGGVORBIS))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(audioToLoad);
                audioClip = DownloadHandlerAudioClip.GetContent(www);
            }
        }
    }

    public void StopMusic()
    {
        audioSource.volume = 0f;
    }
}