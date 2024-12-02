using UnityEngine;
using Cinemachine;

public class CameraAim : MonoBehaviour
{
    [Header("Cinemachine Settings")]
    public CinemachineFreeLook freeLookCamera; // La Cinemachine FreeLook Camera
    public Transform defaultFocus; // Foco predeterminado de la c�mara
    public Transform aimFocus; // Foco de la c�mara cuando se apunta

    [Header("Input Settings")]
    public KeyCode aimKey = KeyCode.Mouse1; // Tecla para entrar en modo de apuntado (clic derecho)

    public bool isAiming = false; // Bandera para saber si se est� apuntando
    private Vector3 originalCameraRotation; // Almacena la rotaci�n original de la c�mara

    void Update()
    {
        if (Input.GetKeyDown(aimKey))
        {
            EnterAimMode();
        }
        else if (Input.GetKeyUp(aimKey))
        {
            ExitAimMode();
        }
    }

    private void EnterAimMode()
    {
        isAiming = true;

        // Mant�n la orientaci�n actual de la c�mara
        originalCameraRotation = freeLookCamera.transform.rotation.eulerAngles;

        // Cambia el Follow al foco de apuntado
        freeLookCamera.Follow = aimFocus;

        // Restablece la orientaci�n original de la c�mara
        freeLookCamera.transform.rotation = Quaternion.Euler(originalCameraRotation);
    }

    private void ExitAimMode()
    {
        isAiming = false;

        // Mant�n la orientaci�n actual de la c�mara
        originalCameraRotation = freeLookCamera.transform.rotation.eulerAngles;

        // Cambia el Follow al foco predeterminado
        freeLookCamera.Follow = defaultFocus;

        // Restablece la orientaci�n original de la c�mara
        freeLookCamera.transform.rotation = Quaternion.Euler(originalCameraRotation);
    }
}
