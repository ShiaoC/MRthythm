using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseCheckPoint : MonoBehaviour
{
    /*--------------------------
     * 按照順序儲存
     * 1. IK的物件
     * 2. 動作座標點(可用於結束前判斷動作，但判定不會用到Z軸)
     * 3. 肢體長度
     */
    [SerializeField] GameObject[] IKPoint;
    [SerializeField] Vector3[] PointData;
    [SerializeField] float[] BodyPartLength = { 0.5f, 0.3047595f, 0.1702152f, 0.1516282f, 0.1516282f, 0.2784151f, 0.2784151f, 0.2832885f, 0.2832885f, 0.08207785f, 0.08207785f, 0.4437139f, 0.4437139f, 0.4452785f, 0.4452785f};
    [SerializeField] Transform pointBegin, pointEnd;

    [SerializeField] int checkframe;
    float transitionDuration, transitionTime = 0f;
    float[] bodyDegree = new float[8];
    bool moving = false;
    /*--------------------------
     * 設定動作
     * 出現的時候進行此動作
     * --------------------------
     */
    private void Awake()
    {
        PointData = new Vector3[15];
        transitionDuration = SceneController.instance.poseShowTime;
    }

    public void SetPose(int showFrame, Transform from, Transform to)
    {
        checkframe = showFrame;
        for(int i = 0; i<15; i++)
        {
            PointData[i] = new Vector3(DanceData.instance._byteData[i][showFrame][0], DanceData.instance._byteData[i][showFrame][1], DanceData.instance._byteData[i][showFrame][2]);
            IKPoint[i].transform.localPosition = PointData[i] * BodyPartLength[i];
        }
        pointBegin = from;
        pointEnd = to;


        moving = true;
        Invoke("SetDegreeData", 0.5f);
    }

    public void Update()
    {
        if (moving)
        {
            transitionTime += Time.deltaTime / transitionDuration;
            transitionTime = Mathf.Clamp01(transitionTime);

            transform.position = Vector3.Lerp(pointBegin.position, pointEnd.position, transitionTime);
            transform.localScale = Vector3.Lerp(pointBegin.localScale, pointEnd.localScale, transitionTime);

            if(transitionTime >= 1.0f)
            {
                moving = false;
                End();
            }
        }
    }
    
    void End()
    {
        //如果有判定就移動到判定結束後
        GamePlayController.instance.DoPoseCheck(bodyDegree);
        Destroy(this.gameObject);
    }

    //0.5秒後再開始算
    void SetDegreeData()
    {
        bodyDegree[0] = countDegree(Vector3.zero, PointData[5]);
        bodyDegree[1] = countDegree(Vector3.zero, PointData[6]);
        bodyDegree[2] = countDegree(Vector3.zero, PointData[7]);
        bodyDegree[3] = countDegree(Vector3.zero, PointData[8]);
        bodyDegree[4] = countDegree(Vector3.zero, PointData[11]);
        bodyDegree[5] = countDegree(Vector3.zero, PointData[12]);
        bodyDegree[6] = countDegree(Vector3.zero, PointData[13]);
        bodyDegree[7] = countDegree(Vector3.zero, PointData[14]);
    }

    float countDegree(Vector3 from, Vector3 to)
    {
        float x = to.x - from.x;
        float y = to.y - from.y;
        float angleInRadians = Mathf.Atan2(y, x);
        float angleInDegrees = Mathf.Rad2Deg * angleInRadians;
        return angleInDegrees;
    }

}
