using UnityEngine;
using System.Collections;

public class DisappearingPlatform : MonoBehaviour
{
    public float disappearDelay = 2f; // Tiempo antes de que comience el desvanecimiento
    public float fadeDuration = 1f;  // Duración del cambio de color
    public Color targetColor = Color.red; // Color hacia el que cambiará antes de desaparecer

    private Renderer platformRenderer; // Renderer de la plataforma
    private Material platformMaterial; // Material único de la plataforma
    private Color originalColor; // Color original del material
    private bool isFading = false; // Bandera para evitar repeticiones

    private void Start()
    {
        // Obtiene el renderer y crea una instancia única del material
        platformRenderer = GetComponent<Renderer>();
        platformMaterial = Instantiate(platformRenderer.material); // Instancia única del material
        platformRenderer.material = platformMaterial;

        // Guarda el color original del material
        originalColor = platformMaterial.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isFading)
        {
            // Marca como "fading" para evitar repeticiones
            isFading = true;
            StartCoroutine(FadeAndDisappear());
        }
    }

    private IEnumerator FadeAndDisappear()
    {
        // Espera antes de comenzar a desvanecer
        yield return new WaitForSeconds(disappearDelay);

        float elapsedTime = 0f;

        // Gradualmente cambia el color del material
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            Color newColor = Color.Lerp(originalColor, targetColor, elapsedTime / fadeDuration);
            platformMaterial.color = newColor;
            yield return null;
        }

        // Desactiva la plataforma después del desvanecimiento
        gameObject.SetActive(false);

        // Restablecer el estado por si se reactiva en el futuro
        platformMaterial.color = originalColor;
        isFading = false;
    }
}
