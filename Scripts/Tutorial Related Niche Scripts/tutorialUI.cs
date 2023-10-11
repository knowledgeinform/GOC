using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialUI : MonoBehaviour
{
    [SerializeField] private GameObject TutorialMove;
    [SerializeField] private GameObject TutorialWaypoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)^Input.GetKeyDown(KeyCode.A)^Input.GetKeyDown(KeyCode.S)^Input.GetKeyDown(KeyCode.D))
        {
            TutorialMove.SetActive(false);
        }
        if (TutorialMove.activeSelf==false)
        {
            TutorialWaypoint.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TutorialWaypoint.SetActive(false);
        }
    }
}
