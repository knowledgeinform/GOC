using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDFLIGHT : MonoBehaviour
{
    [SerializeField] private GameObject WASD;
    [SerializeField] private GameObject Mouse;
    [SerializeField] private GameObject Waypoint;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)^Input.GetKeyDown(KeyCode.A)^Input.GetKeyDown(KeyCode.S)^Input.GetKeyDown(KeyCode.D))
        {
            Mouse.SetActive(true);
            Waypoint.SetActive(true);
            WASD.SetActive(false);
        }
    }
        

}
