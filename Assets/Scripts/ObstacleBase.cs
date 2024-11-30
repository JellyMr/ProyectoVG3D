using UnityEngine;

public class ObstacleBase : MonoBehaviour
{
    private Renderer obstacleRenderer; // Renderer del obstáculo
    private Material obstacleMaterial; // Material único del obstáculo

    [Header("Color Settings")]
    public bool useRandomColor = true; // Si se usa un color aleatorio
    public Color customColor = Color.white; // Color personalizado si no es aleatorio

    protected virtual void Start()
    {
        // Obtiene el renderer y crea una instancia única del material
        obstacleRenderer = GetComponent<Renderer>();
        if (obstacleRenderer != null)
        {
            obstacleMaterial = Instantiate(obstacleRenderer.material); // Instancia única del material
            obstacleRenderer.material = obstacleMaterial; // Asigna el nuevo material

            // Asigna el color al material
            if (useRandomColor)
            {
                obstacleMaterial.color = Random.ColorHSV(0f, 1f, 0.8f, 1f, 0.8f, 1f); // Color aleatorio
            }
            else
            {
                obstacleMaterial.color = customColor; // Color definido en el inspector
            }
        }
    }
}
