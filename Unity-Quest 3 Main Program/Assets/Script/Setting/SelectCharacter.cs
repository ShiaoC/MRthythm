using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    [SerializeField] GameObject[] CharacterGameObject;
    [SerializeField] int nowCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeModel(int n)
    {
        CharacterGameObject[nowCount].SetActive(false);

        nowCount += n;
        if (nowCount >= CharacterGameObject.Length) nowCount -= CharacterGameObject.Length;
        else if (nowCount < 0) nowCount += CharacterGameObject.Length;
        CharacterGameObject[nowCount].SetActive(true);
    }
}
