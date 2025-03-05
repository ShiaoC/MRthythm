using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongSelect : MonoBehaviour
{
    public static SongSelect instance;
    [SerializeField] GameObject menuPrefeb;
    //[SerializeField] DanceData DanceData.instance;

    //當歌曲被按下時會出現的按鈕
    [SerializeField] GameObject DetailButton;


    /*
     * 0-2  由左到右的3個按鈕(必然顯示)
     *      移動後根據方向可能0或2會被刪掉
     *      結束更換位置
     *      
     * 3    新出現的按鈕
     *      可能是最左或最右
     */
    public GameObject[] songButton;
    Vector3[] listTargetPosition = new Vector3[]
    {
        new Vector3(-0.12f, 0f, 0.03f),
        new Vector3(0f, 0f, 0f),
        new Vector3(0.12f, 0f, 0.03f),
    };
    Vector3[] listTargetScale = new Vector3[]
    {
        new Vector3(3f, 3f, 1f),
        new Vector3(5f, 5f, 1f),
        new Vector3(3f, 3f, 1f),
        new Vector3(0.001f, 0.001f, 0.001f),
    };

    int nowCenter;
    int nextNum;
    GameObject needDelete;
    bool check = true;

    private void Awake()
    {
        instance = this;
    }

    public IEnumerator CloseSong()
    {
        for(int i = 0; i<3; i++)
        {
            songButton[i].gameObject.GetComponent<SongDetail>().Close();
            yield return new WaitForSeconds(0.3f);
        }
    }

    public IEnumerator DefaultSetting()
    {
        nowCenter = 1;
        check = true;
        for (int i = 0; i < 3; i++)
        {
            songButton[i] = Instantiate(menuPrefeb);
            songButton[i].transform.SetParent(this.gameObject.transform, false);
            StartCoroutine(LoadAndSetData(i, songButton[i]));
            songButton[i].gameObject.transform.position = listTargetPosition[i] + transform.position;
            songButton[i].gameObject.transform.localScale = listTargetScale[i];
            songButton[i].gameObject.GetComponent<SongDetail>().SetData(transform.position, DetailButton, i, DanceData.instance);
            //songButton[i].transform.GetChild(0).GetComponent<MrtkButtonTrigger>().SongDetailUI = DetailButton;
        }
        songButton[0].gameObject.transform.position = new Vector3(-0.5f, 0f, 0.3f) + transform.position;
        songButton[1].gameObject.transform.position = new Vector3(0f, 0f, 0f) + transform.position;
        songButton[2].gameObject.transform.position = new Vector3(0.5f, 0f, 0.3f) + transform.position;

        yield return StartCoroutine(DanceData.instance.LoadDanceData(DanceData.instance.allDanceData.ID[nowCenter]));
        yield return true;
    }

    private IEnumerator LoadAndSetData(int index, GameObject button)
    {
        yield return StartCoroutine(DanceData.instance.LoadJDetailData(DanceData.instance.allDanceData.ID[index]));
    }

    public void ChangeSong(int num)
    {
        if (check)
        {
            check = false;
            nextNum = nowCenter + num*2 ;
            if (nextNum >= DanceData.instance.allDanceData.ID.Length)
                nextNum -= DanceData.instance.allDanceData.ID.Length;
            else if (nextNum < 0)
                nextNum += DanceData.instance.allDanceData.ID.Length;
            
            nowCenter += num;
            if (nowCenter >= DanceData.instance.allDanceData.ID.Length)
                nowCenter = 0;
            else if (nowCenter < 0)
                nowCenter = DanceData.instance.allDanceData.ID.Length - 1;
            //Debug.Log(nextNum);

            songButton[3] = Instantiate(menuPrefeb);
            songButton[3].transform.SetParent(this.gameObject.transform, false);
            StartCoroutine(LoadAndSetData(nextNum, songButton[3]));
            songButton[3].gameObject.GetComponent<SongDetail>().addPosition = transform.position;
            songButton[3].gameObject.transform.position = new Vector3(0f, 0f, 1f) + transform.position;
            songButton[3].gameObject.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
            songButton[3].gameObject.GetComponent<SongDetail>().SetData(transform.position, DetailButton, nextNum, DanceData.instance);
            //songButton[3].transform.GetChild(0).GetComponent<MrtkButtonTrigger>().SongDetailUI = DetailButton;
            /*
             * 往左移動
             * 要讀取的數字增加
             * 
             */
            if (num > 0)
            {
                //呼叫所有的移動函式
                //0號縮小
                songButton[0].gameObject.GetComponent<SongDetail>().MoveAndScaleToObject(0);
                songButton[1].gameObject.GetComponent<SongDetail>().MoveAndScaleToObject(1);
                songButton[2].gameObject.GetComponent<SongDetail>().MoveAndScaleToObject(2);
                songButton[3].gameObject.GetComponent<SongDetail>().MoveAndScaleToObject(3);



                //結束時清除0的資料     0    123
                //      覆蓋掉就可以了，程式本體那邊會進行刪除
                //並把所有資料往左移動一個
                needDelete = songButton[0].gameObject;
                songButton[0] = songButton[1].gameObject;
                songButton[1] = songButton[2].gameObject;
                songButton[2] = songButton[3].gameObject;
                songButton[3] = needDelete.gameObject;
                Invoke("afterButton", 1.5f);
            }
            if (num < 0)
            {
                //呼叫所有的移動函式
                //2號縮小
                songButton[0].gameObject.GetComponent<SongDetail>().MoveAndScaleToObject(2);
                songButton[1].gameObject.GetComponent<SongDetail>().MoveAndScaleToObject(3);
                songButton[2].gameObject.GetComponent<SongDetail>().MoveAndScaleToObject(4);
                songButton[3].gameObject.GetComponent<SongDetail>().MoveAndScaleToObject(1);


                //結束時清除2的資料    301   2
                //並把所有資料往右移動一個
                needDelete = songButton[2].gameObject;
                songButton[2] = songButton[1].gameObject;
                songButton[1] = songButton[0].gameObject;
                songButton[0] = songButton[3].gameObject;
                songButton[3] = needDelete.gameObject;
                Invoke("afterButton", 1.5f);
            }

            // 更換完歌曲後也更換動畫
            StartCoroutine(DanceData.instance.LoadDanceData(DanceData.instance.allDanceData.ID[nowCenter]));
            songButton[1].GetComponent<SongDetail>().SetSongDetail();
            //DanceData.instance.SetModelDanceData(nowCenter);
        }
    }

    void afterButton()
    {
        check = true;
        Destroy(songButton[3]);
    }
    

}
