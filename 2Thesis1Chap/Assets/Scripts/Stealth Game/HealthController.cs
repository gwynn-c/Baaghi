using UnityEngine;

public class HealthController : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;

    private bool isDead;
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0)
            Death();
        currentHealth -= damage;
    }

    void Death()
    {
        isDead = true;
        //Death triggered
    }

    public bool GetIsDead() => isDead;
}