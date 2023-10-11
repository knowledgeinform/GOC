using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingIntroduction : MonoBehaviour
{
    [SerializeField] private GameObject TrainingUI;

    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("ship")) 
        {
            Debug.Log("entered");
            TrainingUI.SetActive(true);
        }
    }
}
