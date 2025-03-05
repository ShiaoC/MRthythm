using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScene : MonoBehaviour
{
    public static SetScene instance;
    public GameObject SelectSongPrefab;
    public GameObject MainGamePrefab;

    public int SceneNum = 1;

    [SerializeField] GameObject nowScene;

    private void Awake()
    {
        instance = this;
    }

    public void changeToSelect()
    {
        SceneNum = 1;
        if (nowScene != null)
        {
            Destroy(nowScene);
        }
        // 生成SelectSong在0,0,0的位置
        nowScene = Instantiate(SelectSongPrefab, Vector3.zero, Quaternion.identity);
        
    }

    public void changeToMain()
    {
        SceneNum = 0;
        if (nowScene != null)
        {
            Destroy(nowScene);
        }
        // 生成MainGame在0,0,0的位置
        nowScene = Instantiate(MainGamePrefab, Vector3.zero, Quaternion.identity);
        
    }
}
