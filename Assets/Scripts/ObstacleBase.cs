using UnityEngine;

public class ObstacleBase : MonoBehaviour
{
    private Renderer obstacleRenderer; // Renderer del obst�culo
    private Material obstacleMaterial; // Material �nico del obst�culo

    [Header("Color Settings")]
    public bool useRandomColor = true; // Si se usa un color aleatorio
    public Color customColor = Color.white; // Color personalizado si no es aleatorio

    protected virtual void Start()
    {
        // Obtiene el renderer y crea una instancia �nica del material
        obstacleRenderer = GetComponent<Renderer>();
        if (obstacleRenderer != null)
        {
            obstacleMaterial = Instantiate(obstacleRenderer.material); // Instancia �nica del material
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
