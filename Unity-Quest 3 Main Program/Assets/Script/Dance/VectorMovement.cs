using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


//�o�ӳ̦n���Ჾ�ʨ�O���a��h
public enum BodyPart : int
{
    torso, t2n, t2r,
    Ln2s, Rn2s, Ls2e, Rs2e, Le2w, Re2w,
    Lr2h, Rr2h, Lh2k, Rh2k, Lk2a, Rk2a
}


// �o�ӵ{�����O�����m
// �ӬO�ΦV�q�M������׶i�����۹��m
// �ҥH�Ϊ��OlocalPosition
// �j�bIK�W
public class VectorMovement : MonoBehaviour
{
    public static VectorMovement instance;

    [SerializeField] float currentTime = 0;
    [SerializeField] GameObject Troso, RHip, RKnee, RAnkle, RShouder, RElbow, RWrist, LHip, LKnee, LAnkle, LShouder, LElbow, LWrist, HipCenter, ShouderCenter;
    [SerializeField] VectorJoint[] joint;



    bool ifRun = false;
    public bool gameEnd = false;
    int jtouchFileCount;
    float playerX = -2f;
    float playerHight = 1f;

    [Header("CheckPoint-Only need in Main Game")]
    [SerializeField] GameObject CheckPosePrefeb;
    public Transform pointBegin, pointEnd;


    int EffectLeadFrame = 0;
    int lastFrame = -2;



    public void changeState()
    {
        ifRun = !ifRun;
        Debug.Log(ifRun);
    }

    void Awake()
    {
        instance = this;
        if (joint.Length < 15)
        {
            joint = new VectorJoint[15];
        }
        
    }
    
    void Start()
    {
        jtouchFileCount = 0;
        currentTime = 0;
        gameEnd = false;
    }



    void FixedUpdate()
    {
        //�D�C���{��
        if(SetScene.instance.SceneNum == 0)
        {
            //Debug.Log("Main Game");
            if (GameDataManager.instance.isPlaying)
            {
                //Debug.Log("Is Playing");
                currentTime += Time.fixedDeltaTime;
                DanceMain();
            }
        }
        //���{��
        else if(SetScene.instance.SceneNum == 1)
        {
            if (SelectStartController.instance.modelDance)
            {
                currentTime += Time.fixedDeltaTime;
                DanceMenu();
            }
        }
    }



    /*-----------------------------------------------------------------
     * �]�w�R�и�ƪ�
     * ��l��/�ܧ�N�|�ϥ�
     * 
     * �ݭn�G
     * 1. �����l�Ƨ���--SetJoint()
     * -----------------------------------------------------------------
     */
    public IEnumerator changeDanceData(float[][][] data)
    {
        Debug.Log("Change Dance data");
        for (int i=0 ; i<joint.Length; i++)
        {
            float[][] array = new float[data[0].Length][];
            array = data[i];
            joint[i].SetDanceData(array);
        }
        yield return true;
    }


    /*-----------------------------------------------------------------
     * �]�w������
     * ���ثe�S��
     * -----------------------------------------------------------------
     */
    public void updateEffectLeadFrame()
    {
        /*
         * �����Ū�J�C���N���J
         * �ҥH�ݭn��start controller�өI�s�o��
         */
        EffectLeadFrame = (int)(SceneController.instance.poseShowTime / DanceData.instance._danceDetail.FrameInterval);
    }



    /*-----------------------------------------------------------------
     * �D�C���R��
     * -----------------------------------------------------------------
     */
    void DanceMain()
    {
        int num = (int)(currentTime / DanceData.instance._danceDetail.FrameInterval);
        //Debug.Log(num);
        
        //���ؤw�g����
        if(num >= DanceData.instance._danceDetail.FrameCount && !gameEnd)
        {
            //Debug.Log("Game end");
            gameEnd = true;
            GameStartController.instance.GameEnd();
        }

        //��s�ʵe
        else if (num!= lastFrame && !gameEnd)
        {
            //Debug.Log("num: "+num + "  last frame: " + lastFrame);
            lastFrame = num;
            for (int i = 0; i < joint.Length; i++)
            {
                joint[i].Update(num);
            }

            //��sĲ�I
            if (jtouchFileCount < DanceData.instance._checkpoint.Length)
            {
                if (num + EffectLeadFrame >= DanceData.instance._checkpoint[jtouchFileCount])
                {
                    jtouchFileCount++;
                    GameObject checkpoint = Instantiate(CheckPosePrefeb);
                    checkpoint.GetComponent<PoseCheckPoint>().SetPose(num + EffectLeadFrame, pointBegin, pointEnd);

                }
            }
        }

        

    }



    /*-----------------------------------------------------------------
     * �C����ܤ���
     * �º��s�ʵe
     * -----------------------------------------------------------------
     */
    void DanceMenu()
    {
        int num = (int)(currentTime / DanceData.instance._danceDetail.FrameInterval);
        //Debug.Log(num);

        //�W�L���Ƴ̤j�d��
        //�{�b�G���s�k�s
        //����G���ɮ�
        if (num >= DanceData.instance._danceDetail.FrameCount)
        {
            AudioController.instance.PlayAudioFile();
            currentTime = 0;
            num = 0;
        }

        //��s�ʵe
        if (num != lastFrame)
        {
            lastFrame = num;
            for (int i = 0; i < joint.Length; i++)
            {
                joint[i].Update(num);
            }
        }


    }


    public void changeDance(int n)
    {
        currentTime = (int)currentTime;
        currentTime++;
        if (currentTime >= DanceData.instance._danceDetail.FrameCount)
        {
            currentTime = 0;
            jtouchFileCount = 0;
        }
        if (currentTime > 0)
        {
            for (int i = 0; i < 15; i++)
            {
                joint[i].Update((int)currentTime);
            }
        }

    }


    /*------------------------------------------------------------------
     * �]�w�X�{num�Bframe���ʧ@
     * ------------------------------------------------------------------
     */
    void SetTouchEffect(int num)
    {
        
    }


    /*-----------------------------------------------------------------
     * ��l�]�w�y�и��
     * �b������׽T�{��~���
     * ����ӵ{���u����l�ݭn�I�s
     * -----------------------------------------------------------------
     */
    public void SetJoint()
    {
        
        joint[0] = new VectorJoint(Troso, DanceData.instance._byteData[(int)BodyPart.torso], 0.5f);
        joint[1] = new VectorJoint(RHip, DanceData.instance._byteData[(int)BodyPart.Rr2h], SetModelPartLength.instance.GetLength(3));
        joint[2] = new VectorJoint(RKnee, DanceData.instance._byteData[(int)BodyPart.Rh2k], SetModelPartLength.instance.GetLength(4));
        joint[3] = new VectorJoint(RAnkle, DanceData.instance._byteData[(int)BodyPart.Rk2a], SetModelPartLength.instance.GetLength(5));
        joint[4] = new VectorJoint(RShouder, DanceData.instance._byteData[(int)BodyPart.Rn2s], SetModelPartLength.instance.GetLength(0));
        joint[5] = new VectorJoint(RElbow, DanceData.instance._byteData[(int)BodyPart.Rs2e], SetModelPartLength.instance.GetLength(1));
        joint[6] = new VectorJoint(RWrist, DanceData.instance._byteData[(int)BodyPart.Re2w], SetModelPartLength.instance.GetLength(2));
        joint[7] = new VectorJoint(LHip, DanceData.instance._byteData[(int)BodyPart.Lr2h], SetModelPartLength.instance.GetLength(3));
        joint[8] = new VectorJoint(LKnee, DanceData.instance._byteData[(int)BodyPart.Lh2k], SetModelPartLength.instance.GetLength(4));
        joint[9] = new VectorJoint(LAnkle, DanceData.instance._byteData[(int)BodyPart.Lk2a], SetModelPartLength.instance.GetLength(5));
        joint[10] = new VectorJoint(LShouder, DanceData.instance._byteData[(int)BodyPart.Ln2s], SetModelPartLength.instance.GetLength(0));
        joint[11] = new VectorJoint(LElbow, DanceData.instance._byteData[(int)BodyPart.Ls2e], SetModelPartLength.instance.GetLength(1));
        joint[12] = new VectorJoint(LWrist, DanceData.instance._byteData[(int)BodyPart.Le2w], SetModelPartLength.instance.GetLength(2));
        joint[13] = new VectorJoint(HipCenter, DanceData.instance._byteData[(int)BodyPart.t2r], SetModelPartLength.instance.GetLength(7));
        joint[14] = new VectorJoint(ShouderCenter, DanceData.instance._byteData[(int)BodyPart.t2n], SetModelPartLength.instance.GetLength(6));
        
        currentTime = 0;
        gameEnd = false;
        jtouchFileCount = 0;
        DanceMenu();
    }

}
