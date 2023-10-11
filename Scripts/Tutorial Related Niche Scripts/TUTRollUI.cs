using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TUTRollUI : MonoBehaviour
{
    [SerializeField] private GameObject Roll;
    [SerializeField] private GameObject Hover;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)^Input.GetKeyDown(KeyCode.E))
        {
            Roll.SetActive(false);
            Hover.SetActive(true);
        }
    }
}
