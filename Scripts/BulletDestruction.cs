using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestruction : MonoBehaviour
{
    void OnTriggerEnter(Collider target)
    {
            Destroy(gameObject);
    }
}
