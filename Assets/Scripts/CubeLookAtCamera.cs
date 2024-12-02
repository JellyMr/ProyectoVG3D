using UnityEngine;

public class CubeLookAtCamera : MonoBehaviour
{
    [Header("Settings")]
    public Transform cameraTransform; // La c�mara que determinar� la direcci�n
    public Transform playerTransform; // El jugador (para mantener al cubo sobre su cabeza)
    public Vector3 offset = new Vector3(0, 2f, 0); // Desplazamiento desde la posici�n del jugador

    void Update()
    {
        // Mantener el cubo sobre la cabeza del jugador
        transform.position = playerTransform.position + offset;

        // Apuntar en la direcci�n de la c�mara
        Vector3 lookDirection = cameraTransform.forward;
        lookDirection.y = 0; // Ignorar la inclinaci�n vertical
        if (lookDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }
}
