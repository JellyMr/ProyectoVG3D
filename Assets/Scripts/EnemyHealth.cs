using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Salud m�xima del enemigo
    public float currentHealth;  // Salud actual del enemigo

    void Start()
    {
        currentHealth = maxHealth; // Inicializa la salud al m�ximo
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount; // Reduce la salud
        Debug.Log($"{gameObject.name} recibi� {amount} de da�o. Salud restante: {currentHealth}");

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
