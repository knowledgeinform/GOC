using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyController : MonoBehaviour
{
// FIELDS:
//-------------------------------------------------------------------------------
//SHIP FLYING SYSTEM
    [SerializeField] private float speed= 1;

    private float forwardSpeed = 10f, strafeSpeed = 7.5f, hoverSpeed = 5f, boostSpeed = 25f, normalSpeed = 10f;
    private float activeForwardSpeed, activeSpeed, activeStrafeSpeed, activeHoverSpeed;
    private float forwardAcceleration = 2.5f, strafeAcceleration =2f, hoverAcceleration=2f;
    private float rollInput;
    private float rollSpeed = 90f, rollAcceleration = 3.5f;

// SHIP AIMDOWNSIGHT SYSTEM
    GameObject target = null;

    public float lookRateSpeed = 90f;
    private Vector2 lookInput, screenCenter, mouseDistance;

// SHIP BULLET SYSTEM
    [SerializeField] private GameObject enemyBullet;
	Rigidbody tempRigidBodyBullet;
	[SerializeField] private GameObject eyes;
	public float enemyBulletSpeed = 500f;
    // Start is called before the first frame update

// ENEMY SHIP WAYPOINT SYSTEM
    public GameObject indicator;

//---------------------------------------------------------------------------------
    void Start()
    {
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height*.5f;

        Cursor.lockState = CursorLockMode.Confined;
        forwardSpeed*= speed;   
        strafeSpeed*=speed;
        hoverSpeed*=speed;
        boostSpeed*=speed;
        normalSpeed*=speed;

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitData;

        target = FindClosestEnemy();
        if(Input.GetKeyDown("left shift"))
        {
            forwardSpeed = boostSpeed;
        }
        if (Input.GetKeyUp("left shift"))
        {
            forwardSpeed = normalSpeed;
        }
        if (Input.GetMouseButton(1))
        {
            if (Physics.Raycast(ray, out hitData, Mathf.Infinity)){
                if (hitData.collider.tag == "enemy") {
                    if (Camera.main.fieldOfView > 65f) {
                        Camera.main.fieldOfView -= .6f;
                    }
                FaceEnemy();
                }
                
            }
        }
        if ((Input.GetMouseButtonDown(0)))
        {
            Blastin();
        }
        if (!(Input.GetMouseButton(1)))
        {
            if (Camera.main.fieldOfView < 90f)
            {
                Camera.main.fieldOfView += .6f;
            }
        }

        lookInput.x=Input.mousePosition.x;
        lookInput.y=Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCenter.x)/screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y)/screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration*Time.deltaTime);
        
        transform.Rotate(-mouseDistance.y*lookRateSpeed*Time.deltaTime, mouseDistance.x*lookRateSpeed*Time.deltaTime, rollInput*rollSpeed*Time.deltaTime, Space.Self);

        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical")*forwardSpeed, forwardAcceleration*Time.deltaTime);
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed,Input.GetAxisRaw("Horizontal")*strafeSpeed,strafeAcceleration * Time.deltaTime);
        activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover")*hoverSpeed, hoverAcceleration*Time.deltaTime);

        transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
        transform.position += (transform.right * activeStrafeSpeed *Time.deltaTime) + (transform.up*activeHoverSpeed*Time.deltaTime);

       
    }
    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("enemy");
        GameObject closestEnemy = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestEnemy = go;
                distance = curDistance;
            }
        }
        Ray indicatorTrigger = new Ray(transform.position, transform.forward);
        RaycastHit newHit;
        if (Physics.Raycast(indicatorTrigger, out newHit, Mathf.Infinity)) {
            if (newHit.collider.tag == "enemy") {
                if (newHit.collider.gameObject == target){
                    indicator.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
            else {
                indicator.GetComponent<SpriteRenderer>().enabled = false;
            }
        } 
        return closestEnemy;
    }
    void FaceEnemy()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    void Blastin()
	{
		GameObject tempBullet = Instantiate(enemyBullet, eyes.gameObject.transform.position, eyes.gameObject.transform.rotation) as GameObject; //shoots from enemies eyes
        tempBullet.layer = 3;
    	Rigidbody tempRigidBodyBullet = tempBullet.GetComponent<Rigidbody>();
        BulletDestruction destroyBullet = tempBullet.AddComponent<BulletDestruction>();
        tempRigidBodyBullet.useGravity = false;
    	tempRigidBodyBullet.AddForce(transform.forward * enemyBulletSpeed,ForceMode.Impulse);
   		Destroy(tempBullet, 15f);
	}

}

