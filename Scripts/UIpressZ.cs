using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIpressZ : MonoBehaviour
{
    public GameObject UItoSetActive;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            UItoSetActive.SetActive(true);
        }
    }
}
