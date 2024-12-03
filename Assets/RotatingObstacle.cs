using UnityEngine;

public class RotatingObstacle : MonoBehaviour
{
    [Header("Rotation Settings")]
    public Vector3 rotationAxis = Vector3.up; // Eje de rotaci�n (por defecto, arriba)
    public float rotationSpeed = 50f; // Velocidad de rotaci�n en grados por segundo

    [Header("Damage Settings")]
    public float damage = 10f; // Da�o que inflige al jugador

    private void Update()
    {
        // Rotar el obst�culo alrededor del eje especificado
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime, Space.World);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Inflige da�o al jugador al colisionar
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthSystem playerHealth = collision.gameObject.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
