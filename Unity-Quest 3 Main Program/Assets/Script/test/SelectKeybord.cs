using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectKeybord : MonoBehaviour
{
    /*void Update()
    {
        // 前後一首
        if (Input.GetKeyDown(KeyCode.F))
        {
            SongSelect.instance.ChangeSong(-1);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            SongSelect.instance.ChangeSong(1);
        }

        //Open song detail
        if (Input.GetKeyDown(KeyCode.A))
        {
            OpenCenterSongDetail();
        }

        //close detail panel
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ButtonBestScore.Instance.Close();
        }

        //Play Game
        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayGame();
        }

        //toSelect
        if (Input.GetKeyDown(KeyCode.T))
        {
            SetScene.instance.changeToSelect();
        }
    }*/

    public void OpenCenterSongDetail()
    {
        SongSelect.instance.songButton[1].GetComponent<SongDetail>().OpenSongDetail();
    }

    public void PlayGame()
    {
        SongSelect.instance.songButton[1].GetComponent<SongDetail>().PlayGame();
    }

}
