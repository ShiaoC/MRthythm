using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    /*
     * �u���bPlayData���o��scene�|�Ψ�
     * ��L���ݭn
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
