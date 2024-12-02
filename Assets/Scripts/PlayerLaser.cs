using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    [Header("Laser Settings")]
    public float laserRange = 50f; // Rango del rayo láser
    public float laserDamage = 20f; // Daño que inflige el láser
    public LineRenderer laserLine; // LineRenderer para el efecto del láser
    public float laserDuration = 0.1f; // Duración del efecto visual del láser
    public Transform firePoint; // Punto desde donde sale el láser (opcional, para alinear visualmente)

    [Header("Camera Settings")]
    public Camera mainCamera; // La cámara activa (FreeLook Camera)

    [Header("Input Settings")]
    public KeyCode fireKey = KeyCode.Mouse0; // Tecla para disparar el láser (clic izquierdo)

    void Update()
    {
        if (Input.GetKeyDown(fireKey))
        {
            ShootLaser();
        }
    }

    private void ShootLaser()
    {
        // Calcula el origen del rayo (centro de la cámara)
        Ray cameraRay = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        Vector3 rayOrigin = cameraRay.origin;
        Vector3 rayDirection = cameraRay.direction;

        // Inicializa el LineRenderer si está configurado
        if (laserLine != null)
        {
            laserLine.enabled = true;
            laserLine.SetPosition(0, firePoint ? firePoint.position : rayOrigin); // Si hay un firePoint, úsalo
        }

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, laserRange))
        {
            if (laserLine != null)
            {
                laserLine.SetPosition(1, hit.point); // Punto donde impacta el láser
            }

            // Detecta si el objeto golpeado tiene el script EnemyHealth
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(laserDamage); // Aplica daño
            }
        }
        else
        {
            if (laserLine != null)
            {
                laserLine.SetPosition(1, rayOrigin + rayDirection * laserRange); // Rayo máximo si no golpea nada
            }
        }

        // Desactiva el láser después de un corto tiempo
        if (laserLine != null)
        {
            StartCoroutine(DisableLaser());
        }
    }

    private System.Collections.IEnumerator DisableLaser()
    {
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false; // Desactiva el láser visualmente
    }
}
