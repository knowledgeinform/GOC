using UnityEngine;

/* Base class that player and enemies can derive from to include stats. */

public class EnemyStat : MonoBehaviour {

	// Health
	public int maxHealth = 100;
	public int currentHealth { get; private set; }

	public Stat damage;
	public HealthBar healthBar;
	public GameObject parentEnemy;

	// Set current health to max health
	// when starting the game.
	void Awake ()
	{
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
	}

	// Damage the character
private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered OnTriggerEnter");
        TakeDamage(20);
    }
	public void TakeDamage (int damage)
	{
		// Subtract the armor value
		damage = Mathf.Clamp(damage, 0, int.MaxValue);

		// Damage the character
		currentHealth -= damage;
		Debug.Log(transform.name + " takes " + damage + " damage.");
		healthBar.SetHealth(currentHealth);

		// If health reaches zero
		if (currentHealth <= 0)
		{
			Die();
		}
	}

	public virtual void Die ()
	{
        parentEnemy.SetActive(false);
		Debug.Log(parentEnemy.transform.name + " died.");
	}

}