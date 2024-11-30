using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 50, 0); // Velocidad de rotación
    private Vector3 previousPosition; // Posición de la plataforma en el último fotograma
    private Quaternion previousRotation; // Rotación de la plataforma en el último fotograma

    void Start()
    {
        // Guardar la posición y rotación inicial
        previousPosition = transform.position;
        previousRotation = transform.rotation;
    }

    void FixedUpdate()
    {
        // Actualiza la posición y rotación previas para el siguiente cálculo
        previousPosition = transform.position;
        previousRotation = transform.rotation;

        // Rota la plataforma
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionStay(Collision collision)
    {
        // Verifica si el objeto que está colisionando es el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                // Calcula el desplazamiento de la plataforma desde el último fotograma
                Vector3 platformDeltaPosition = transform.position - previousPosition;

                // Calcula el cambio de rotación de la plataforma
                Quaternion platformDeltaRotation = transform.rotation * Quaternion.Inverse(previousRotation);

                // Aplica el movimiento de la plataforma al jugador
                playerRb.MovePosition(playerRb.position + platformDeltaPosition);

                // Calcula la nueva posición del jugador basada en la rotación de la plataforma
                Vector3 relativePlayerPosition = playerRb.position - transform.position;
                Vector3 rotatedPosition = platformDeltaRotation * relativePlayerPosition;
                playerRb.MovePosition(transform.position + rotatedPosition);

                // Opcional: ajusta también la rotación del jugador
                playerRb.MoveRotation(platformDeltaRotation * playerRb.rotation);
            }
        }
    }
}
