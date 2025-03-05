using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchEffect : MonoBehaviour
{
    [SerializeField] GameObject growUp; //�令�Ѥj��p�A��0���ɭ�
    [SerializeField] GameObject successEffect;
    

    //�o��Ӹ�ƥi��
    //�q�X�{����ժ��ɶ�
    public float maxTime = 2f;
    
    //�nĲ�I��m��Ĳ�I�I�W�r(���Τ����k)
    public string targetTag = "Elbow";
    public GameDataManager GDManager=null;
    public SceneController SceneController;

    //�O�C���̪�
    public bool isPlayer;

    float nowTime;
    float size;
    bool checkEnd;
    bool checkTouchTarget;
    //�n���٭n�]�w�@�ӳ̤j�d�򪺮خءA�ϥΪ̤~��ݥX����ɭԭn�I��
    void Start()
    {
        maxTime = SceneController.poseShowTime;
        growUp.SetActive(true);
        successEffect.SetActive(false);

        //�]�wtime
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
            //�P�_�O�_���\�I��
            //���ަ��\���ѳ��|����
            growUp.SetActive(false);

            if (isPlayer)
            {
                //���\
                //if (checkTouchTarget)
                if(true)
                {
                    successEffect.SetActive(true);
                    /*if (GDManager != null)
                        GDManager.AddCombo();*/
                    this.gameObject.GetComponent<AudioSource>().enabled = true;
                    Invoke("DestroyThis", 3);
                }
                //����
                else
                {
                    //�����������Φ���L�S��
                    //�C���ƾ��k0
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
