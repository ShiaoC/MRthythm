using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonBestScore : MonoBehaviour
{
    public static ButtonBestScore Instance;
    [SerializeField] TextMeshPro textScore;
    [SerializeField] TextMeshPro textCombo;

    private void Awake()
    {
        Instance = this;
    }
    public void ChangeBestData(int score, int combo)
    {
        textScore.text = score.ToString();
        textCombo.text = combo.ToString();
    }

    public void Close()
    {
        this.gameObject.GetComponent<InterfaceAnimManager>().startDisappear();
    }
}
