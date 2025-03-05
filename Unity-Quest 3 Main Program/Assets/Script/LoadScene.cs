using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    /*
     * 只有在PlayData的這個scene會用到
     * 其他不需要
     * 
     */
    public int SceneNum;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(SceneNum, LoadSceneMode.Additive);
        Destroy(this.gameObject, 3f);
    }
}
