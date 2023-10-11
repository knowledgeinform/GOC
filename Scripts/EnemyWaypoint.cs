using UnityEngine;

public class EnemyWaypoint : MonoBehaviour
{
    public float FixedSize;
    public Camera Camera;

    void Update ()
    {
        var distance = (Camera.transform.position - transform.position).magnitude;
        var size = distance * FixedSize * Camera.fieldOfView;
        transform.localScale = Vector3.one * size;
        transform.forward = transform.position - Camera.transform.position;
    }
}