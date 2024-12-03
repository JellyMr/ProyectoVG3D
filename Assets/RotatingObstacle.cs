using UnityEngine;

public class RotatingObstacle : MonoBehaviour
{
    [Header("Rotation Settings")]
    public Vector3 rotationAxis = Vector3.up; // Eje de rotación (por defecto, arriba)
    public float rotationSpeed = 50f; // Velocidad de rotación en grados por segundo

    [Header("Damage Settings")]
    public float damage = 10f; // Daño que inflige al jugador

    private void Update()
    {
        // Rotar el obstáculo alrededor del eje especificado
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime, Space.World);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Inflige daño al jugador al colisionar
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
