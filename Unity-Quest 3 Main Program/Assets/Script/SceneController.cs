using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    /*
     * 0 = �D�C��
     * 1 = ���
     * 99 = ��L
     */
    //public int scene;
    public float musicLatency = 8f;
    public float poseShowTime = 2.0f;
    public int DanceNum = 0;
    public int sceneNumForClose;

    /*
     * �C��scene���U�۪�controller�Ӧs����
     */
    [SerializeField] GameStartController GameStartController;

    [SerializeField] GameObject[] CloseWhenSceneChangeObject;
    [SerializeField] GameObject[] CloseWhenSceneChangeUI;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //PlayerPrefs.SetString("songStr", "2307311543");
        StartCoroutine( checkFirst() );
    }

    IEnumerator checkFirst()
    {
        yield return StartCoroutine(DanceData.instance.LoadAllDanceData());
        if (SetScene.instance.SceneNum == 0) yield return StartCoroutine(checkFirstMain());
        else if(SetScene.instance.SceneNum == 1) yield return StartCoroutine(checkFirstSelect());
    }

    IEnumerator checkFirstSelect()
    {
        //�Ҧ��{����B�檺�ɭԻݭn�����J��
        
        yield return StartCoroutine(DanceData.instance.LoadAllDanceData());
        
        // Ū�J�̨Φ��Z
        yield return StartCoroutine(DanceData.instance.LoadBestData());

        // ���ﶵ��l�� + �R�Ъ�l��
        yield return StartCoroutine(SongSelect.instance.DefaultSetting());

        // ����2��
        SelectStartController.instance.SetModelDanceMode(true);


        yield return true;
    }

    IEnumerator checkFirstMain()
    {
        //�Ҧ��{����B�檺�ɭԻݭn�����J��
        yield return StartCoroutine(DanceData.instance.LoadAllDanceData());

        //�q�o��ק�n���J���s��(DanceNum)
        AudioController.instance.dataID = PlayerPrefs.GetString("songStr");
        
        yield return StartCoroutine(DanceData.instance.LoadDanceData(PlayerPrefs.GetString("songStr")));


        GameStartController.showLock();
        
        yield return true;
    }

    IEnumerator UploadAllDanceFile()
    {
        //Ū�J����`��
        yield return StartCoroutine(DanceData.instance.LoadAllDanceData());
    }


    void CloseThings()
    {
        for(int i = 0; i<CloseWhenSceneChangeObject.Length; i++)
        {
            Debug.Log(i);
            CloseWhenSceneChangeObject[i].SetActive(false);
        }

    }

    IEnumerator CloseUI()
    {
        for (int i = 0; i < CloseWhenSceneChangeUI.Length; i++)
        {
            CloseWhenSceneChangeUI[i].GetComponent<InterfaceAnimManager>().startDisappear();
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(2f);
    }
    /*
    public void SwitchScene(int n)
    {
        //��o��scene���F������
        StartCoroutine(CloseUI());

        SceneManager.LoadScene(n, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(sceneNumForClose);
    }*/

    public IEnumerator SwitchSceneToMain()
    {
        //��o��scene���F������
        //�|�b����UI��۰�����L����
        yield return StartCoroutine(CloseUI());
        CloseThings();


        yield return new WaitForSeconds(1f);

        SetScene.instance.changeToMain();

        yield return true;
    }

}
