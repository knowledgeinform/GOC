using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/* Controls the Enemy AI */

public class EnemyController : MonoBehaviour {

	public float lookRadius = 10f;	// Detection range for player

	Transform target;	// Reference to the player
	NavMeshAgent agent; // Reference to the NavMeshAgent
	float nextFire = 0;
	float fireRate = 0.5f;
	[SerializeField] private GameObject enemyBullet;
	Rigidbody tempRigidBodyBullet;
	[SerializeField] private GameObject eyes;
	public float enemyBulletSpeed = 500f;
	//CharacterCombat combat;

	// Use this for initialization
	void Start () {
		target = PlayerManager.instance.player.transform;
		agent = GetComponent<NavMeshAgent>();
		//combat = GetComponent<CharacterCombat>();
	}
	
	// Update is called once per frame
	void Update () {
		// Distance to the target
		float distance = Vector3.Distance(target.position, transform.position);

		// If inside the lookRadius
		if (distance <= lookRadius)
		{
			// Move towards the target
			agent.SetDestination(target.position);

			// If within attacking distance
			if (distance <= agent.stoppingDistance)
			{
				CharacterStats targetStats = target.GetComponent<CharacterStats>();
				if (targetStats != null)
				{
					//combat.Attack(targetStats);
				}

				FaceTarget();	// Make sure to face towards the target
				if(Time.time > nextFire)
				{
					nextFire = Time.time + fireRate;
					BlastinFellas();
				}
			}
		}
	}

	// Rotate to face the target
	void FaceTarget ()
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}

	// Show the lookRadius in editor
	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
	}
	void BlastinFellas()
	{
		GameObject tempBullet = Instantiate(enemyBullet, eyes.gameObject.transform.position, eyes.gameObject.transform.rotation) as GameObject; //shoots from enemies eyes
		tempBullet.layer = 8;
    	Rigidbody tempRigidBodyBullet = tempBullet.GetComponent<Rigidbody>();
		BulletDestruction destroyBullet = tempBullet.AddComponent<BulletDestruction>();
		tempRigidBodyBullet.useGravity = false;
		tempRigidBodyBullet.isKinematic = false;
					
    	tempRigidBodyBullet.AddForce(transform.forward * enemyBulletSpeed,ForceMode.Impulse);
   		Destroy(tempBullet, 15f);
	}
}