using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager instance;


    public int combo;
    public int score;
    [SerializeField] TextMeshPro scoreText;
    [SerializeField] TextMeshPro comboText;

    //[SerializeField] SceneController SceneController;
    public int maxCombo;

    public bool isPlaying = false;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        combo = 0;
        score = 0;
        maxCombo = 0;
        isPlaying = false;
        updateUI();
    }

    public void AddCombo(int n)
    {
        score += (int)((combo/5*0.2f+1) * n);
        combo += 1;
        updateUI();

        maxCombo = Mathf.Max(maxCombo, combo);
    }

    public void ResetCombo()
    {
        combo = 0;
        updateUI();
    }

    void updateUI()
    {
        scoreText.text = score.ToString();
        comboText.text = "Combo\n" + combo.ToString();
        //小數點後兩位"F2"
    }

}
