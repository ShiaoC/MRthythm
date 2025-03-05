using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    /*
     * 0 = 主遊戲
     * 1 = 選單
     * 99 = 其他
     */
    //public int scene;
    public float musicLatency = 8f;
    public float poseShowTime = 2.0f;
    public int DanceNum = 0;
    public int sceneNumForClose;

    /*
     * 每個scene有各自的controller來存放資料
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
        //所有程式初運行的時候需要先載入的
        
        yield return StartCoroutine(DanceData.instance.LoadAllDanceData());
        
        // 讀入最佳成績
        yield return StartCoroutine(DanceData.instance.LoadBestData());

        // 選單選項初始化 + 舞蹈初始化
        yield return StartCoroutine(SongSelect.instance.DefaultSetting());

        // 等待2秒
        SelectStartController.instance.SetModelDanceMode(true);


        yield return true;
    }

    IEnumerator checkFirstMain()
    {
        //所有程式初運行的時候需要先載入的
        yield return StartCoroutine(DanceData.instance.LoadAllDanceData());

        //從這邊修改要載入的編號(DanceNum)
        AudioController.instance.dataID = PlayerPrefs.GetString("songStr");
        
        yield return StartCoroutine(DanceData.instance.LoadDanceData(PlayerPrefs.GetString("songStr")));


        GameStartController.showLock();
        
        yield return true;
    }

    IEnumerator UploadAllDanceFile()
    {
        //讀入資料總集
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
        //把這個scene的東西關掉
        StartCoroutine(CloseUI());

        SceneManager.LoadScene(n, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(sceneNumForClose);
    }*/

    public IEnumerator SwitchSceneToMain()
    {
        //把這個scene的東西關掉
        //會在關完UI後自動關其他物件
        yield return StartCoroutine(CloseUI());
        CloseThings();


        yield return new WaitForSeconds(1f);

        SetScene.instance.changeToMain();

        yield return true;
    }

}
