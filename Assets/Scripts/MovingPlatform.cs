using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA; // Punto inicial
    public Transform pointB; // Punto final
    public float speed = 2f; // Velocidad de movimiento
    private Transform currentTarget; // Objetivo actual

    void Start()
    {
        // Configura el primer objetivo como pointB
        currentTarget = pointB;
    }

    void Update()
    {
        // Verifica si los puntos están asignados
        if (pointA == null || pointB == null)
        {
            Debug.LogError("Puntos A y B no asignados en el script MovingPlatform");
            return;
        }

        // Mueve la plataforma hacia el objetivo actual
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

        // Comprueba si llegó al objetivo
        if (Vector3.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            // Cambia al siguiente objetivo
            currentTarget = currentTarget == pointB ? pointA : pointB;

            // Mensaje de depuración
            Debug.Log($"Cambiando objetivo a: {currentTarget.name}");
        }
    }
}
