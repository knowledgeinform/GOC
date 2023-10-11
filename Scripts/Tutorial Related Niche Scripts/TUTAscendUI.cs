using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TUTAscendUI : MonoBehaviour
{
    [SerializeField] private GameObject Hover;
    [SerializeField] private GameObject Button;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)^Input.GetKeyDown(KeyCode.LeftControl))
        {
            Hover.SetActive(false);
            Button.SetActive(true);
        }
    }
}
