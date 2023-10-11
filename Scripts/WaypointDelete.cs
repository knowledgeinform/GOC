using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointDelete : MonoBehaviour
{
    [SerializeField] private GameObject Waypointicon; 
    //GameObject spaceship = GameObject.Find("parentspaceship");
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("ship")) {
            Debug.Log("entered WaypointDelete.OnTriggerEnter");
            Waypointicon.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("entered WaypointDelete.OntriggerExit");
        Waypointicon.SetActive(true);
    }
}
