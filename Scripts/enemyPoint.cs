using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyPoint : MonoBehaviour
{
   // Indicator icon
    public Image img;
    // The target (location, enemy, etc..)
    public Transform target;
    // UI Text to display the distance

    public Vector3 offset;

    private void Update()
    {
        GameObject spaceship = GameObject.FindWithTag("ship");

        if (spaceship.GetComponent<FlyController>().FindClosestEnemy() == gameObject) {

        // Temporary variable to store the converted position from 3D world point to 2D screen point
            Vector2 pos = Camera.main.WorldToScreenPoint(target.position + offset);
        // Update the marker's position
            img.transform.position = pos;
        // Change the meter text to the distance with the meter unit 'm'
        }

    }
}

