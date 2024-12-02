using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Salud máxima del enemigo
    public float currentHealth;  // Salud actual del enemigo

    void Start()
    {
        currentHealth = maxHealth; // Inicializa la salud al máximo
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount; // Reduce la salud
        Debug.Log($"{gameObject.name} recibió {amount} de daño. Salud restante: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} ha muerto.");
        Destroy(gameObject,2f); // Destruye el enemigo
    }
}
