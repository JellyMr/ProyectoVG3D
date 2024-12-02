using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform firePoint; // Punto desde donde disparará
    public float fireRate = 1f; // Tiempo entre disparos
    public float projectileSpeed = 10f; // Velocidad del proyectil
    public float detectionRange = 15f; // Rango de detección del jugador

    private Transform player; // Referencia al jugador
    private float nextFireTime = 0f;

    void Start()
    {
        // Encuentra al jugador por su etiqueta
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        if (player != null && Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            // Apunta al jugador
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            // Dispara si está en rango y es tiempo de disparar
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    private void Shoot()
    {
        if (firePoint != null && projectilePrefab != null)
        {
            // Crea el proyectil en el punto de disparo
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            // Aplica fuerza al proyectil
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = firePoint.forward * projectileSpeed;
            }
        }
    }
}
