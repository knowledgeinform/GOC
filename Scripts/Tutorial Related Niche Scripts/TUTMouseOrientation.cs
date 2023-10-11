using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TUTMouseOrientation : MonoBehaviour
{
    [SerializeField] private GameObject WASD;
    [SerializeField] private GameObject Mouse;
    [SerializeField] private GameObject Waypoint;
    [SerializeField] private GameObject Roll;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("ship")) {
            Debug.Log("entered WaypointDelete.OnTriggerEnter");
            Waypoint.SetActive(false);
            WASD.SetActive(false);
            Mouse.SetActive(false);
            Roll.SetActive(true);
        }
    }
}
