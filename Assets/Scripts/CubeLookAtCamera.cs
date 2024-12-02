using UnityEngine;

public class CubeLookAtCamera : MonoBehaviour
{
    [Header("Settings")]
    public Transform cameraTransform; // La cámara que determinará la dirección
    public Transform playerTransform; // El jugador (para mantener al cubo sobre su cabeza)
    public Vector3 offset = new Vector3(0, 2f, 0); // Desplazamiento desde la posición del jugador

    void Update()
    {
        // Mantener el cubo sobre la cabeza del jugador
        transform.position = playerTransform.position + offset;

        // Apuntar en la dirección de la cámara
        Vector3 lookDirection = cameraTransform.forward;
        lookDirection.y = 0; // Ignorar la inclinación vertical
        if (lookDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }
}
