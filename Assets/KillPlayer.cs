using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{
    public Transform player; // Referencia al jugador

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            HealthSystem playerHealth = player.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(100);
            }
        }
   
    }
}
