using UnityEngine;
using Cinemachine;

public class CameraAim : MonoBehaviour
{
    [Header("Cinemachine Settings")]
    public CinemachineFreeLook freeLookCamera; // La Cinemachine FreeLook Camera
    public Transform defaultFocus; // Foco predeterminado de la cámara
    public Transform aimFocus; // Foco de la cámara cuando se apunta

    [Header("Input Settings")]
    public KeyCode aimKey = KeyCode.Mouse1; // Tecla para entrar en modo de apuntado (clic derecho)

    public bool isAiming = false; // Bandera para saber si se está apuntando
    private Vector3 originalCameraRotation; // Almacena la rotación original de la cámara

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

        // Mantén la orientación actual de la cámara
        originalCameraRotation = freeLookCamera.transform.rotation.eulerAngles;

        // Cambia el Follow al foco de apuntado
        freeLookCamera.Follow = aimFocus;

        // Restablece la orientación original de la cámara
        freeLookCamera.transform.rotation = Quaternion.Euler(originalCameraRotation);
    }

    private void ExitAimMode()
    {
        isAiming = false;

        // Mantén la orientación actual de la cámara
        originalCameraRotation = freeLookCamera.transform.rotation.eulerAngles;

        // Cambia el Follow al foco predeterminado
        freeLookCamera.Follow = defaultFocus;

        // Restablece la orientación original de la cámara
        freeLookCamera.transform.rotation = Quaternion.Euler(originalCameraRotation);
    }
}
