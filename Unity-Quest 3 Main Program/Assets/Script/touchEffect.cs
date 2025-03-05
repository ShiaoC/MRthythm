using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchEffect : MonoBehaviour
{
    [SerializeField] GameObject growUp; //改成由大到小，到0的時候
    [SerializeField] GameObject successEffect;
    

    //這兩個資料可改
    //從出現到測試的時間
    public float maxTime = 2f;
    
    //要觸碰位置的觸碰點名字(不用分左右)
    public string targetTag = "Elbow";
    public GameDataManager GDManager=null;
    public SceneController SceneController;

    //是遊玩者的
    public bool isPlayer;

    float nowTime;
    float size;
    bool checkEnd;
    bool checkTouchTarget;
    //好像還要設定一個最大範圍的框框，使用者才能看出什麼時候要碰它
    void Start()
    {
        maxTime = SceneController.poseShowTime;
        growUp.SetActive(true);
        successEffect.SetActive(false);

        //設定time
        nowTime = 0;
        checkEnd = false;
        checkTouchTarget = false;
        growUp.transform.localScale = new Vector3(0,0,0);

        if (isPlayer)
        {
            this.gameObject.GetComponent<SphereCollider>().enabled = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(nowTime < maxTime)
        {
            nowTime += Time.deltaTime;
            size = nowTime / maxTime;
            growUp.transform.localScale = new Vector3(size, size, size);
        }
        else if(!checkEnd)
        {
            checkEnd = true;
            //判斷是否成功碰到
            //不管成功失敗都會有的
            growUp.SetActive(false);

            if (isPlayer)
            {
                //成功
                //if (checkTouchTarget)
                if(true)
                {
                    successEffect.SetActive(true);
                    /*if (GDManager != null)
                        GDManager.AddCombo();*/
                    this.gameObject.GetComponent<AudioSource>().enabled = true;
                    Invoke("DestroyThis", 3);
                }
                //失敗
                else
                {
                    //直接消失不用有其他特效
                    //遊戲數據歸0
                    if (GDManager != null)
                        GDManager.ResetCombo();
                    DestroyThis();
                }
            }
            else
            {
                DestroyThis();
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == targetTag) checkTouchTarget = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == targetTag) checkTouchTarget = false;
    }

    void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}
