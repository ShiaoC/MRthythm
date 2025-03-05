using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System;

public class DanceData : MonoBehaviour
{
    public static DanceData instance;

    public JAllDance allDanceData;
    public JDanceDetail _danceDetail;
    public JBestData _bestData;
    public float[][][] _byteData = null;
    public int[] _checkpoint = null;

    string AllDanceFilePath;
    string JdanceDataFolderPath;
    string JtouchFolderPath;
    string JBestDataPath;
    string ByteDanceFolderPath;

    void Awake()
    {
        instance = this;

        //最佳分數之後要改成可修改的資料夾



        AllDanceFilePath = Path.Combine(Application.streamingAssetsPath, "_AllDance/AllDance.json");
        JtouchFolderPath = Path.Combine(Application.streamingAssetsPath, "_JsonFile");
        JBestDataPath    = Path.Combine(Application.streamingAssetsPath, "_BestData/BestData.json");
        ByteDanceFolderPath = Path.Combine(Application.streamingAssetsPath, "_ByteFile");

        //初始的運行交給Scene Controller
    }




    /*-----------------------------------------------------------------
     * 更換 / 初始加載
     * 1. Detail資料      (呼叫)
     * 2. 跳舞的byte資料  (呼叫)
     * -----------------------------------------------------------------
     */
    public IEnumerator LoadDanceData(string ID)
    {
        //string danceDetailFilePath = Path.Combine(Application.streamingAssetsPath, ID + ".json");

        yield return LoadJDetailData(ID);

        yield return new WaitForSeconds(0.1f);

        yield return LoadByteDance(ID);

        yield return StartCoroutine(AudioController.instance.LoadAudio(ID));

        if(SetScene.instance.SceneNum == 0)
        {
            StartCoroutine(LoadJTouchFile(ID));
        }
        else if (SetScene.instance.SceneNum == 1)
        {
            if (SelectStartController.instance.modelDance) AudioController.instance.PlayAudioFile();
        }

        yield return true;
    }

    /* -----------------------------------------------------------------
     * 讀入byte資料
     * 不可直接操控
     * -----------------------------------------------------------------
     */
    IEnumerator LoadByteDance(string ID)
    {
        string byteDanceFilePath = Path.Combine(ByteDanceFolderPath, ID + ".bytes");

        // 取用detail中的frame
        using (UnityWebRequest danceDataRequest = UnityWebRequest.Get(byteDanceFilePath))
        {
            yield return danceDataRequest.SendWebRequest();

            if (danceDataRequest.result == UnityWebRequest.Result.Success)
            {
                byte[] bytes = danceDataRequest.downloadHandler.data;
                int numRows = 15;
                int numCols = _danceDetail.FrameCount;  
                int depth = 3;

                //Debug.Log("numCols : " + numCols);

                _byteData = new float[numRows][][];
                for (int i = 0; i < numRows; i++)
                {
                    _byteData[i] = new float[numCols][];
                    for (int j = 0; j < numCols; j++)
                    {
                        _byteData[i][j] = new float[depth];
                    }
                }

                int floatSize = 4; 
                int byteIndex = 0;

                for (int i = 0; i < numRows; i++)
                {
                    for (int j = 0; j < numCols; j++)
                    {
                        for (int k = 0; k < depth; k++)
                        {
                            _byteData[i][j][k] = BitConverter.ToSingle(bytes, byteIndex);
                            byteIndex += floatSize;
                        }
                    }
                }
                //Debug.Log(_byteData[0][10][0]);
                //這邊把所有資料丟到肢體的陣列中

                // 设置VectorMovement的肢体数据
                VectorMovement.instance.SetJoint();
                VectorMovement.instance.changeDanceData(_byteData);
            }
            else
            {
                Debug.LogError("Failed to load dance data: " + byteDanceFilePath);
            }
        }
        yield return null;
    }




    /*-----------------------------------------------------------------
     * 舞蹈詳細資料
     * 這個是把資料儲存在本地(DanceData)的
     * -----------------------------------------------------------------
     */
    public IEnumerator LoadJDetailData(string ID)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, ID + ".json");

        // 使用UnityWebRequest
        var www = new UnityEngine.Networking.UnityWebRequest(filePath);
        www.downloadHandler = new UnityEngine.Networking.DownloadHandlerBuffer();
        yield return www.SendWebRequest();

        if (www.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
        {
            string json = www.downloadHandler.text;
            _danceDetail = JsonUtility.FromJson<JDanceDetail>(json);
        }
        else
        {
            Debug.LogError("無法載入: " + filePath);
        }
    }

    /*-----------------------------------------------------------------
     * 舞蹈詳細資料
     * 這個是會把資料回傳的
     * 主要是用在選歌頁面
     * -----------------------------------------------------------------
     */
    public IEnumerator GetDanceDetail(int n, System.Action<JDanceDetail> onComplete)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, allDanceData.ID[n] + ".json");

        // 使用UnityWebRequest
        var www = new UnityEngine.Networking.UnityWebRequest(filePath);
        www.downloadHandler = new UnityEngine.Networking.DownloadHandlerBuffer();
        yield return www.SendWebRequest();

        if (www.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
        {
            string json = www.downloadHandler.text;
            JDanceDetail result = JsonUtility.FromJson<JDanceDetail>(json);
            onComplete(result);
        }
        else
        {
            Debug.LogError("無法載入: " + filePath);
            onComplete(null);
        }
    }



    /* -----------------------------------------------------------------
     * 讀入最佳分數資料
     * -----------------------------------------------------------------
     */
    public IEnumerator LoadBestData()
    {
        // 使用 UnityWebRequest 異步加載 JSON 文件
        UnityWebRequest www = UnityWebRequest.Get(JBestDataPath);
        yield return www.SendWebRequest();

        // 檢查是否成功加載 JSON 文件
        if (www.result == UnityWebRequest.Result.Success)
        {
            // 解析 JSON 數據並儲存
            _bestData = JsonUtility.FromJson<JBestData>(www.downloadHandler.text);
            // Debug.Log("bestData" + _bestData.score[4]);
        }
        else
        {
            Debug.Log(www.error);
            Debug.LogError("無法載入Best Data檔案 : " + JBestDataPath);
        }
    }




    /*-----------------------------------------------------------------
     * 加載碰觸文件
     * -----------------------------------------------------------------
     */
    public IEnumerator LoadJTouchFile(string ID)
    {
        string filePath = Path.Combine(JtouchFolderPath, "P_" + ID + ".json");
        // 使用 UnityWebRequest 異步加載 JSON 文件
        UnityWebRequest www = UnityWebRequest.Get(filePath);
        yield return www.SendWebRequest();

        // 檢查是否成功加載 JSON 文件
        if (www.result == UnityWebRequest.Result.Success)
        {
            // 解析 JSON 數據並儲存
            _checkpoint = JsonUtility.FromJson<JTouch>(www.downloadHandler.text).frame;

        }
        else
        {
            Debug.Log(www.error);
            Debug.LogError("無法載入All Dance檔案 : " + AllDanceFilePath);
        }
    }




    /* -----------------------------------------------------------------
     * 讀入所有舞蹈的資料
     * -----------------------------------------------------------------
     */
    public IEnumerator LoadAllDanceData()
    {
        // 使用 UnityWebRequest 異步加載 JSON 文件
        UnityWebRequest www = UnityWebRequest.Get(AllDanceFilePath);
        yield return www.SendWebRequest();

        // 檢查是否成功加載 JSON 文件
        if (www.result == UnityWebRequest.Result.Success)
        {
            // 解析 JSON 數據並儲存
            allDanceData = JsonUtility.FromJson<JAllDance>(www.downloadHandler.text);

        }
        else
        {
            Debug.Log(www.error);
            Debug.LogError("無法載入All Dance檔案 : " + AllDanceFilePath);
        }
    }




    // 只有在選擇關卡時會使用到
    // 切換選單就會切換舞蹈
    // 暫時沒用
    /*
    public IEnumerator SetModelDanceData(int n)
    {
        JDanceDetail jdetail = null;

        yield return StartCoroutine(LoadJDanceDetailData(allDanceData.ID[n], (data) =>
        {
            if (data != null)
            {
                jdetail = data;

                // 在加載完成後，調用加載 JDance 文件的方法
                StartCoroutine(LoadByteDance(jdetail.ID));
                if (_byteData != null)
                {
                    // 最終設定完成所有的數據後進行操作
                    VM.changeDanceData(_byteData);

                    // 加載 JTouch 文件
                    StartCoroutine(LoadJTouchFile(jdetail.ID, (touchData) =>
                    {
                        if (touchData != null)
                        {
                            // 在加載 JTouch 文件完成後，設置 _touch
                            _touch = touchData;

                            // 最終設定完成所有的數據後進行操作
                            //VM.changeDanceData(_byteData);
                        }
                        else
                        {
                            // 載入 JTouch 失敗
                            Debug.LogError("無法成功載入 JTouch 文件 : " + jdetail.ID);
                        }
                    }));
                }
                else
                {
                    // 載入 JDance 失敗
                    Debug.LogError("無法成功載入 JDance 文件 : " + jdetail.ID);
                }
            }
            else
            {
                // 載入 JDanceDetail 失敗
                Debug.LogError("無法成功載入 SongDetail 資料 : " + n);
            }
        }));
    }*/

}