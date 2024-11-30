using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float bounceForce = 10f; // Fuerza del salto

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica si el objeto tiene un Rigidbody
        if (collision.gameObject.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            // Aplica una fuerza vertical
            rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
        }
    }
}
