using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthComponent : MonoBehaviour
{
    private float currentHealth;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private bool isPlayer = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount) {
        currentHealth -= amount;
        if(currentHealth <= 0.0f) {
            Die();
        }
    }

    public void Heal(float amount) {
        currentHealth = Math.Clamp(currentHealth + amount, 0.0f, maxHealth);
    }

    public void Die() {
        if(!isPlayer) {
            Destroy(gameObject);
        }
        else {
            // PlayerMovement.instance.GetComponent<PlayerMovement>().TeleportPlayer(new Vector3(0f, 5f, 0f), Vector2.zero); // teleport player to position
            // Heal(maxHealth); // and heal player on death.
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // reset scene on player death
        }
    }
}
