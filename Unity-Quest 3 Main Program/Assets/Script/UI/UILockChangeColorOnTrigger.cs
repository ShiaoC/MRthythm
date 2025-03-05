using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILockChangeColorOnTrigger : MonoBehaviour
{
    public bool touched = false;

    [SerializeField] string targetTag = "Wrist";
    [SerializeField] Color startColor = new Color(0.801f, 0.2313725f, 0.2467268f, 1.0f);
    [SerializeField] Color endColor = new Color(0.2302866f, 0.4717243f, 0.7075472f, 1.0f);
    [SerializeField] float transitionDuration = 2.0f;
    [SerializeField] bool leftSide;

    private bool isTransitioning = false;
    private float transitionStartTime;
    private Image[] Image;

    [SerializeField] GameObject[] SideLock;

    void Start()
    {
        // 初始化 Image 陣列，長度與 SideLock 陣列相同
        Image = new Image[SideLock.Length];

        touched = false;
        for(int i = 0; i<SideLock.Length; i++)
        {
            Image[i] = SideLock[i].GetComponent<Image>();
            Image[i].color = startColor;
        }
        
    }

    void FixedUpdate()
    {
        if (!touched)
        {
            if (isTransitioning)
            {
                float t = (Time.time - transitionStartTime) / transitionDuration;
                for (int i = 0; i < SideLock.Length; i++)
                    Image[i].color = Color.Lerp(startColor, endColor, t);

                if (t >= 0.9f)
                {
                    isTransitioning = false;
                    touched = true;
                    //Debug.Log("touched: "+ leftSide);
                }
            }

            //判定動作
            else if (leftSide) leftHandSideCheck();
            else rightHandSideCheck();
        }
        
    }


    void leftHandSideCheck()
    {
        if (Mathf.Abs(GamePlayController.instance.MediapipeDegrees[0]) > 150 && Mathf.Abs(GamePlayController.instance.MediapipeDegrees[2]) > 150)
        {
            if (!isTransitioning)
            {
                transitionStartTime = Time.time;
                isTransitioning = true;
            }
        }
        else
        {
            isTransitioning = false;
            for (int i = 0; i < SideLock.Length; i++)
                Image[i].color = startColor;
            touched = false;
        }
    }

    void rightHandSideCheck()
    {
        if (Mathf.Abs(GamePlayController.instance.MediapipeDegrees[1]) <30 && Mathf.Abs(GamePlayController.instance.MediapipeDegrees[3]) <30)
        {
            if (!isTransitioning)
            {
                transitionStartTime = Time.time;
                isTransitioning = true;
            }
        }
        else
        {
            isTransitioning = false;
            for (int i = 0; i < SideLock.Length; i++)
                Image[i].color = startColor;
            touched = false;
            //Debug.Log("Untouched: right");
        }
    }

}
