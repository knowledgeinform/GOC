using UnityEngine;

public class AlwaysFaceObject : MonoBehaviour
{
    private Transform transf;

    // Start is called before the first frame update
    void Start()
    {
        transf = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // rotate the camera every frame so it keeps looking at the target
        transform.LookAt(transform.position + transf.forward);
    }
}
