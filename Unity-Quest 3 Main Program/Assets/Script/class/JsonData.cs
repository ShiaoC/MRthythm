using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�Ҧ��R�Ъ�ID
// (DanceData)
[System.Serializable]
public class JAllDance
{
    public string[] ID;
}


//�歺�R�Ф����Ӹ`���
//(�S���a�^�媺���)
[System.Serializable]
public class JDanceDetail
{
    public string Name;
    public string ID;
    public int Length;
    public string SongAuthor;
    public string DanceAuthor;
    public int FrameCount;
    public double FrameInterval;
}


//�R�Ф��`��ݭn�P�_������
//(J_00000000)
[System.Serializable] 
public class JTouch
{
    public int[] frame;
}


[System.Serializable]
public class JBestData
{
    public int[] score;
    public int[] combo;
}