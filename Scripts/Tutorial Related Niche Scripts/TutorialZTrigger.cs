using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialZTrigger : MonoBehaviour
{
    [SerializeField] private GameObject zTutorial;
    private void OnTriggerEnter(Collider other)
    {
        zTutorial.SetActive(true);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            zTutorial.SetActive(false);
        }
    }
}
