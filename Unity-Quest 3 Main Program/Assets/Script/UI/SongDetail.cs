using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SongDetail : MonoBehaviour
{
    string songID;
    [SerializeField] TextMeshPro s_name;
    [SerializeField] TextMeshPro s_time;
    [SerializeField] TextMeshPro s_songAuthor;
    [SerializeField] TextMeshPro s_danceAuthor;
    public int SongPosition;

    public Vector3 targetPosition;
    public Vector3 targetScale;
    public float duration = 1.5f;
    public Vector3 addPosition;
    public DanceData DanceData;
    JDanceDetail JDanceDetail;

    //所有曲子的detail button都是共通的
    public GameObject DetailButton;

    public void SetData(Vector3 _addPosition, GameObject _detailButton, int _songPosition, DanceData dd)
    {
        addPosition     = _addPosition;
        DetailButton    = _detailButton;
        SongPosition    = _songPosition;
        DanceData       = dd;


        StartCoroutine(DanceData.GetDanceDetail(SongPosition, (danceDetail) => {
            if (danceDetail != null)
            {
                JDanceDetail = danceDetail;
                setText();
            }
            else
            {
                // 處理錯誤
                Debug.LogError("無法讀取資料。");
            }
        }));
    }

    public void setText()
    {
        s_name.text = JDanceDetail.Name;
        s_time.text = (JDanceDetail.Length/60) + " : " + (JDanceDetail.Length % 60).ToString("00");
        s_songAuthor.text = JDanceDetail.SongAuthor;
        s_danceAuthor.text = JDanceDetail.DanceAuthor;

        songID = JDanceDetail.ID;
    }

    //設定開始進行移動
    public void MoveAndScaleToObject( int n)
    {
        //0 123 4
        //Debug.Log("songID : " + songID + "   localPosition   " + transform.position+ "   targetPosition   " + targetPosition);
        if(n == 0)
        {
            targetPosition = new Vector3(0f, 0f, 1f) + addPosition;
            targetScale = new Vector3(0.001f, 0.001f, 0.001f);
        }
        else if(n == 1)
        {
            targetScale = new Vector3(3f, 3f, 1f);
            targetPosition = new Vector3(-0.5f, 0f, 0.3f) + addPosition;
        }
        else if (n == 2)
        {
            targetScale = new Vector3(5f, 5f, 1f);
            targetPosition = new Vector3(0f, 0f, 0f) + addPosition;
        }
        else if (n == 3)
        {
            targetScale = new Vector3(3f, 3f, 1f);
            targetPosition = new Vector3(0.5f, 0f, 0.3f) + addPosition;
        }
        else if (n == 4)
        {
            targetPosition = new Vector3(0f, 0f, 1f) + addPosition;
            targetScale = new Vector3(0.001f, 0.001f, 0.001f) ;
        }
        StartCoroutine(MoveAndScaleCoroutine());
    }

    //移動到特定位置的程式
    private IEnumerator MoveAndScaleCoroutine()
    {
        Vector3 initialPosition = transform.position;
        Vector3 initialScale = transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calculate the current lerp percentage
            float t = elapsedTime / duration;

            // Smoothly move to the target position
            transform.position = Vector3.Lerp(initialPosition, targetPosition, t);

            // Smoothly change the object's scale
            transform.localScale = Vector3.Lerp(initialScale, targetScale, t);

            // Wait for the next frame
            yield return null;

            // Update the elapsed time
            elapsedTime += Time.deltaTime;
        }

        
    }

    /*---------------------------------------------------------------
     * 開始遊戲
     * --------------------------------------------------------------
     */
    public void PlayGame()
    {
        PlayerPrefs.SetString("songStr", songID);
        StartCoroutine(SceneController.instance.SwitchSceneToMain());
    }

    public void OpenSongDetail()
    {
        DetailButton.SetActive(true);
        DetailButton.GetComponent<ButtonBestScore>().ChangeBestData(DanceData._bestData.score[SongPosition], DanceData._bestData.combo[SongPosition]);
        DetailButton.GetComponent<InterfaceAnimManager>().startAppear();
        PlayerPrefs.SetString("songStr", songID);
    }

    public void SetSongDetail()
    {
        DetailButton.GetComponent<ButtonBestScore>().ChangeBestData(DanceData._bestData.score[SongPosition], DanceData._bestData.combo[SongPosition]);
        PlayerPrefs.SetString("songStr", songID);
    }

    public void Close()
    {
        this.gameObject.GetComponent<InterfaceAnimManager>().startDisappear();
    }
}
