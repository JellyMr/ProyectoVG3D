using System.Collections;
using UnityEngine;
using UnityEngine.AI; // Para usar NavMeshAgent

public class MeleeEnemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float maxHealth = 50f; // Salud máxima
    private float currentHealth;  // Salud actual
    public float attackRange = 1.5f; // Distancia para atacar al jugador
    public float attackCooldown = 2f; // Tiempo entre ataques
    public float damage = 10f; // Daño infligido al jugador

    [Header("References")]
    public NavMeshAgent agent; // Agente NavMesh para movimiento
    public Animator animator; // Controlador de animaciones
    public Transform player; // Referencia al jugador

    private float nextAttackTime = 0f; // Control del tiempo entre ataques
    private bool isDead = false; // Bandera para verificar si está muerto

    void Start()
    {
        // Inicializa la salud
        currentHealth = maxHealth;

        // Encuentra al jugador
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
    }

    void Update()
    {
        if (isDead) return;

        if (agent.isOnNavMesh) // Verifica si el agente está en el NavMesh
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= attackRange)
            {
                agent.isStopped = true;
                animator.SetBool("IsWalking", false);

                if (Time.time >= nextAttackTime)
                {
                    Attack();
                    nextAttackTime = Time.time + attackCooldown;
                }
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(player.position);
                animator.SetBool("IsWalking", true);
            }
        }
        else
        {
            Debug.LogWarning($"{gameObject.name} no está en un NavMesh.");
        }
    }

   


    private IEnumerator DeactivateAfterTime()
    {
        animator.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("IsAttacking", false);
    }

    private void Attack()
    {
        

        // Inflige daño al jugador (asegúrate de que el jugador tenga un script de salud)
        HealthSystem playerHealth = player.GetComponent<HealthSystem>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            StartCoroutine(DeactivateAfterTime());
        }
        

    }


}
