using System.Collections;
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
        StartCoroutine(FlashRed());

        currentHealth -= amount; // Reduce la salud
        Debug.Log($"{gameObject.name} recibió {amount} de daño. Salud restante: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private IEnumerator  FlashRed()
    {
        Renderer renderer = GetComponent<Renderer>();
        Color originalColor = renderer.material.color;

        renderer.material.color = Color.red;
        yield return new WaitForSeconds(0.2f);

        renderer.material.color = originalColor;
    }


    private void Die()
    {
        Debug.Log($"{gameObject.name} ha muerto.");
        Destroy(gameObject,2f); // Destruye el enemigo
    }
}
