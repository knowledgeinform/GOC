using UnityEngine;

/* Base class that player and enemies can derive from to include stats. */

public class CharacterStats : MonoBehaviour {

	// Health
	public int maxHealth = 100;
	public int currentHealth { get; private set; }

	public Stat damage;
	public HealthBar healthBar;

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
        if (other.gameObject.CompareTag("Bullet")) 
		{
            TakeDamage(15);
        }
    }
	public void TakeDamage (int damage)
	{
		// Subtract the armor value
		damage = Mathf.Clamp(damage, 0, int.MaxValue);

		// Damage the character
		currentHealth -= damage;
		healthBar.SetHealth(currentHealth);

		// If health reaches zero
		if (currentHealth <= 0)
		{
			Die();
		}
	}

	public virtual void Die ()
	{
		// Die in some way
		// This method is meant to be overwritten
	}

}