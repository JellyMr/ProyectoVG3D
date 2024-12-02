using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    [Header("Laser Settings")]
    public float laserRange = 50f; // Rango del rayo l�ser
    public float laserDamage = 20f; // Da�o que inflige el l�ser
    public LineRenderer laserLine; // LineRenderer para el efecto del l�ser
    public float laserDuration = 0.1f; // Duraci�n del efecto visual del l�ser
    public Transform firePoint; // Punto desde donde sale el l�ser (opcional, para alinear visualmente)

    [Header("Camera Settings")]
    public Camera mainCamera; // La c�mara activa (FreeLook Camera)

    [Header("Input Settings")]
    public KeyCode fireKey = KeyCode.Mouse0; // Tecla para disparar el l�ser (clic izquierdo)

    void Update()
    {
        if (Input.GetKeyDown(fireKey))
        {
            ShootLaser();
        }
    }

    private void ShootLaser()
    {
        // Calcula el origen del rayo (centro de la c�mara)
        Ray cameraRay = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        Vector3 rayOrigin = cameraRay.origin;
        Vector3 rayDirection = cameraRay.direction;

        // Inicializa el LineRenderer si est� configurado
        if (laserLine != null)
        {
            laserLine.enabled = true;
            laserLine.SetPosition(0, firePoint ? firePoint.position : rayOrigin); // Si hay un firePoint, �salo
        }

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, laserRange))
        {
            if (laserLine != null)
            {
                laserLine.SetPosition(1, hit.point); // Punto donde impacta el l�ser
            }

            // Detecta si el objeto golpeado tiene el script EnemyHealth
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(laserDamage); // Aplica da�o
            }
        }
        else
        {
            if (laserLine != null)
            {
                laserLine.SetPosition(1, rayOrigin + rayDirection * laserRange); // Rayo m�ximo si no golpea nada
            }
        }

        // Desactiva el l�ser despu�s de un corto tiempo
        if (laserLine != null)
        {
            StartCoroutine(DisableLaser());
        }
    }

    private System.Collections.IEnumerator DisableLaser()
    {
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false; // Desactiva el l�ser visualmente
    }
}
