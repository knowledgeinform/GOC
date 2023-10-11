using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//the real mesh that moves, the parent of this will 
//have the nav mesh agent doing x and z movements while this script handles y movements
public class EnemySpaceship : MonoBehaviour
{
    public Transform GFX; //the child of the enemy object
    Transform target;
    public float lookRadius = 10f;
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Distance to the target
		float distance = Vector3.Distance(target.position, transform.position);

		// If inside the lookRadius
		if (distance <= lookRadius)
		{
            Vector3 newPos = new Vector3(GFX.transform.position.x, target.position.y, GFX.transform.position.z);
            GFX.transform.position = Vector3.Lerp(GFX.transform.position, newPos, Time.deltaTime);
        }
    }
}
