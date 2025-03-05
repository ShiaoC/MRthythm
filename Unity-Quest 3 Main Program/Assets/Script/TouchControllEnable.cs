using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControllEnable : MonoBehaviour
{

    [SerializeField] GameObject[] showWhenTouched;
    [SerializeField] GameObject[] closeWhenTouched;
    [SerializeField] GameObject UI;



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "MainCamera")
        {
            if (UI != null)
            {
                UI.SetActive(true);
                UI.GetComponent<InterfaceAnimManager>().startAppear();
            }

            for (int i = 0; i < showWhenTouched.Length; i++)
            {
                showWhenTouched[i].SetActive(true);
            }
            for (int i = 0; i < closeWhenTouched.Length; i++)
            {
                closeWhenTouched[i].SetActive(false);
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            if (UI != null)
            {
                UI.GetComponent<InterfaceAnimManager>().startDisappear();
            }

            for (int i = 0; i < showWhenTouched.Length; i++)
            {
                showWhenTouched[i].SetActive(false);
            }
            for (int i = 0; i < closeWhenTouched.Length; i++)
            {
                closeWhenTouched[i].SetActive(true);
            }
        }
            
    }
}
