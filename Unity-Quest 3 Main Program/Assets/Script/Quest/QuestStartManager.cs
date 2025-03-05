using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class QuestStartManager : MonoBehaviour
{
    [SerializeField]
    Toggle m_PassthroughToggle;

    [SerializeField]
    ObjectSpawner m_ObjectSpawner;
    // Start is called before the first frame update
    void Start()
    {
        //³]©w¬°¬ï³z
        m_PassthroughToggle.isOn = true;

        if (m_ObjectSpawner == null)
        {
#if UNITY_2023_1_OR_NEWER
            m_ObjectSpawner = FindAnyObjectByType<ObjectSpawner>();
#else
            m_ObjectSpawner = FindObjectOfType<ObjectSpawner>();
#endif
        }
    }

}
