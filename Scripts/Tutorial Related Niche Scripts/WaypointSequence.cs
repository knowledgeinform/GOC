using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointSequence : MonoBehaviour
{
    [SerializeField] private GameObject Waypoint;
    [SerializeField] private GameObject ColliderActivate;
    [SerializeField] private GameObject UIActivate;
    [SerializeField] private Vector3 newWaypointPosition;
    [SerializeField] private GameObject WaypointIcon;
    [SerializeField] private GameObject ColliderDeactivate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("ship")) {
            Debug.Log("entered OnTriggerEnter");
            ColliderActivate.SetActive(true);
            UIActivate.SetActive(true);
            Waypoint.transform.position = newWaypointPosition;
            WaypointIcon.SetActive(false);
            ColliderDeactivate.SetActive(false);
        }
    }
}