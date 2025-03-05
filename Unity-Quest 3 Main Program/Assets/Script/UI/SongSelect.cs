using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongSelect : MonoBehaviour
{
    public static SongSelect instance;
    [SerializeField] GameObject menuPrefeb;
    //[SerializeField] DanceData DanceData.instance;

    //��q���Q���U�ɷ|�X�{�����s
    [SerializeField] GameObject DetailButton;


    /*
     * 0-2  �ѥ���k��3�ӫ��s(���M���)
     *      ���ʫ�ھڤ�V�i��0��2�|�Q�R��
     *      �����󴫦�m
     *      
     * 3    �s�X�{�����s
     *      �i��O�̥��γ̥k
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
             * ��������
             * �nŪ�����Ʀr�W�[
             * 
             */
            if (num > 0)
            {
                //�I�s�Ҧ������ʨ禡
                //0���Y�p
                songButton[0].gameObject.GetComponent<SongDetail>().MoveAndScaleToObject(0);
                songButton[1].gameObject.GetComponent<SongDetail>().MoveAndScaleToObject(1);
                songButton[2].gameObject.GetComponent<SongDetail>().MoveAndScaleToObject(2);
                songButton[3].gameObject.GetComponent<SongDetail>().MoveAndScaleToObject(3);



                //�����ɲM��0�����     0    123
                //      �л\���N�i�H�F�A�{�����騺��|�i��R��
                //�ç�Ҧ���Ʃ������ʤ@��
                needDelete = songButton[0].gameObject;
                songButton[0] = songButton[1].gameObject;
                songButton[1] = songButton[2].gameObject;
                songButton[2] = songButton[3].gameObject;
                songButton[3] = needDelete.gameObject;
                Invoke("afterButton", 1.5f);
            }
            if (num < 0)
            {
                //�I�s�Ҧ������ʨ禡
                //2���Y�p
                songButton[0].gameObject.GetComponent<SongDetail>().MoveAndScaleToObject(2);
                songButton[1].gameObject.GetComponent<SongDetail>().MoveAndScaleToObject(3);
                songButton[2].gameObject.GetComponent<SongDetail>().MoveAndScaleToObject(4);
                songButton[3].gameObject.GetComponent<SongDetail>().MoveAndScaleToObject(1);


                //�����ɲM��2�����    301   2
                //�ç�Ҧ���Ʃ��k���ʤ@��
                needDelete = songButton[2].gameObject;
                songButton[2] = songButton[1].gameObject;
                songButton[1] = songButton[0].gameObject;
                songButton[0] = songButton[3].gameObject;
                songButton[3] = needDelete.gameObject;
                Invoke("afterButton", 1.5f);
            }

            // �󴫧��q����]�󴫰ʵe
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
