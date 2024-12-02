using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f; // Da�o que inflige al jugador
    public float lifetime = 5f; // Tiempo antes de destruirse autom�ticamente

    void Start()
    {
        Destroy(gameObject, lifetime); // Destruye el proyectil despu�s del tiempo
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Inflige da�o al jugador
            HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.TakeDamage(damage);
            }

            // Destruye el proyectil tras colisionar
            Destroy(gameObject);
        }
        else
        {
            // Destruye el proyectil al colisionar con cualquier otra cosa
            Destroy(gameObject);
        }
    }
}
