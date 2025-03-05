using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartController : MonoBehaviour
{
    public static GameStartController instance;
    [Header("Controller")]
    //[SerializeField] GameDataManager GameDataManager;
    //[SerializeField] AudioController AudioController;
    //[SerializeField] VectorMovement VectorMovement;
    //[SerializeField] SceneController SceneController;
    //[SerializeField] DanceData DanceData;

    bool checkForPlaying = false;

    [Header("Things change When Start Game")]
    [SerializeField] GameObject[] ShowThings;
    [SerializeField] GameObject[] CloseThings;
    [SerializeField] GameObject UILock;
    [SerializeField] GameObject UIScore;

    [Header("Trigger for start game")]
    [SerializeField] UILockChangeColorOnTrigger leftLock;
    [SerializeField] UILockChangeColorOnTrigger rightLock;
    [SerializeField] GameObject LockDetermine;

    [Header("For End the Game")]
    [SerializeField] GameObject EndUI;
    [SerializeField] GameObject ExplosionEffect;
    [SerializeField] Transform ExplosionPosition;
    [SerializeField] GameObject[] EndCloseThings;
    [SerializeField] Text textGameName;
    [SerializeField] Text textScore;
    [SerializeField] Text textCombo;

    [Header("Put into GameObject")]
    [SerializeField] Transform pointBegin, pointEnd;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (leftLock.touched && rightLock.touched && !checkForPlaying)
        {
            StartCoroutine(StartGame());
        }
    }

    IEnumerator StartGame()
    {
        checkForPlaying = true;
        UILock.gameObject.GetComponent<InterfaceAnimManager>().startDisappear();
        LockDetermine.SetActive(false);
        UIScore.SetActive(true);
        VectorMovement.instance.updateEffectLeadFrame();
        yield return new WaitForSeconds(2.0f);


        for (int i = 0; i<ShowThings.Length; i++)
        {
            ShowThings[i].SetActive(true);
        }
        for (int i = 0; i < CloseThings.Length; i++)
        {
            CloseThings[i].SetActive(false);
        }
        GameDataManager.instance.isPlaying = true;
        yield return new WaitForSeconds(SceneController.instance.musicLatency);
        AudioController.instance.PlayAudioFile();


        VectorMovement.instance.pointBegin = pointBegin;
        VectorMovement.instance.pointEnd = pointEnd;

        yield break;
    }

    public void showLock()
    {
        UILock.SetActive(true);
        UILock.gameObject.GetComponent<InterfaceAnimManager>().startAppear();
    }

    public void GameEnd()
    {
        EndUI.SetActive(true);
        UIScore.SetActive(false);
        /*
         * ¥Ü½d¼Ò«¬Ãz¬µ
         */
        Instantiate(ExplosionEffect, ExplosionPosition.transform.position, transform.rotation);
        for(int i = 0; i< EndCloseThings.Length; i++)
        {
            EndCloseThings[i].SetActive(false);
        }
        textCombo.text = GameDataManager.instance.maxCombo.ToString();
        textScore.text = GameDataManager.instance.score.ToString();
        textGameName.text = DanceData.instance._danceDetail.Name;
    }

}
