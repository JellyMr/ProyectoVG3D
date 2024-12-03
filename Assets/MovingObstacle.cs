using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform pointA; // Punto inicial del movimiento
    public Transform pointB; // Punto final del movimiento
    public float speed = 3f; // Velocidad del obstáculo
    public float waitTime = 1f; // Tiempo de espera en cada punto

    [Header("Damage Settings")]
    public float damage = 10f; // Daño que inflige al jugador

    private Vector3 targetPosition; // Posición objetivo actual
    private bool isWaiting = false; // Bandera para esperar

    void Start()
    {
        // Inicia con el punto B como destino
        targetPosition = pointB.position;
    }

    void Update()
    {
        if (!isWaiting)
        {
            // Mueve el obstáculo hacia el destino actual
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Si alcanza el destino, activa la espera y cambia el objetivo
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                StartCoroutine(WaitAndSwitchTarget());
            }
        }
    }

    private System.Collections.IEnumerator WaitAndSwitchTarget()
    {
        isWaiting = true;

        // Espera un tiempo antes de cambiar de destino
        yield return new WaitForSeconds(waitTime);

        // Cambia el objetivo al otro punto
        if (targetPosition == pointA.position)
        {
            targetPosition = pointB.position;
        }
        else
        {
            targetPosition = pointA.position;
        }

        isWaiting = false;
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
