using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStopforUI : MonoBehaviour
{
    [SerializeField] private GameObject UItobeFrozen;
    void Update()
    {
        if(UItobeFrozen.activeSelf)
        {
            Time.timeScale=0;
        }
        if (UItobeFrozen.activeSelf == false)
        {
            Time.timeScale=1;
        }
    }
}
