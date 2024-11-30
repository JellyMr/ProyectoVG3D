using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 50, 0); // Velocidad de rotaci�n
    private Vector3 previousPosition; // Posici�n de la plataforma en el �ltimo fotograma
    private Quaternion previousRotation; // Rotaci�n de la plataforma en el �ltimo fotograma

    void Start()
    {
        // Guardar la posici�n y rotaci�n inicial
        previousPosition = transform.position;
        previousRotation = transform.rotation;
    }

    void FixedUpdate()
    {
        // Actualiza la posici�n y rotaci�n previas para el siguiente c�lculo
        previousPosition = transform.position;
        previousRotation = transform.rotation;

        // Rota la plataforma
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionStay(Collision collision)
    {
        // Verifica si el objeto que est� colisionando es el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                // Calcula el desplazamiento de la plataforma desde el �ltimo fotograma
                Vector3 platformDeltaPosition = transform.position - previousPosition;

                // Calcula el cambio de rotaci�n de la plataforma
                Quaternion platformDeltaRotation = transform.rotation * Quaternion.Inverse(previousRotation);

                // Aplica el movimiento de la plataforma al jugador
                playerRb.MovePosition(playerRb.position + platformDeltaPosition);

                // Calcula la nueva posici�n del jugador basada en la rotaci�n de la plataforma
                Vector3 relativePlayerPosition = playerRb.position - transform.position;
                Vector3 rotatedPosition = platformDeltaRotation * relativePlayerPosition;
                playerRb.MovePosition(transform.position + rotatedPosition);

                // Opcional: ajusta tambi�n la rotaci�n del jugador
                playerRb.MoveRotation(platformDeltaRotation * playerRb.rotation);
            }
        }
    }
}
