using UnityEngine;
using UnityEngine.UI; // Necesario para manipular imágenes y UI

public class HealthSystem : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f; // Salud máxima del jugador
    public float currentHealth; // Salud actual del jugador
    public Image healthBar; // Imagen de la barra de vida (UI)
    public GameOverScreen1 screen;
    public GameObject UI;
    public 


    void Start()
    {
        currentHealth = maxHealth; // Inicia con salud completa
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth); // Asegura que no sea menor a 0
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth); // Asegura que no exceda la salud máxima
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth; // Actualiza el porcentaje de la barra
        }
    }

    private void Die()
    {
        Debug.Log("El jugador ha muerto.");
        UI.SetActive(false);
        screen.Setup();
       
        Destroy(gameObject);
        Cursor.lockState = CursorLockMode.None;// Destruye el enemigo
        // Agrega lógica de muerte (reiniciar nivel, mostrar pantalla de derrota, etc.)
    }
}
